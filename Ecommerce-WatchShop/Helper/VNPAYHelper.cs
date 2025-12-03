using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Ecommerce_WatchShop.Models;
using Microsoft.Extensions.Configuration;

namespace Ecommerce_WatchShop.Helper
{
    public class VNPAYHelper
    {
        private readonly IConfiguration _configuration;
        private readonly string _vnpayTmnCode;
        private readonly string _vnpayHashSecret;
        private readonly string _vnpayUrl;
        private readonly string _vnpayReturnUrl;

        public VNPAYHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            var vnpayConfig = _configuration.GetSection("VNPAY");
            _vnpayTmnCode = vnpayConfig["TmnCode"];
            _vnpayHashSecret = vnpayConfig["HashSecret"];
            _vnpayUrl = vnpayConfig["PaymentUrl"];
            _vnpayReturnUrl = vnpayConfig["ReturnUrl"];

            // Kiểm tra cấu hình
            if (string.IsNullOrEmpty(_vnpayTmnCode) || string.IsNullOrEmpty(_vnpayHashSecret) ||
                string.IsNullOrEmpty(_vnpayUrl) || string.IsNullOrEmpty(_vnpayReturnUrl))
            {
                throw new ArgumentNullException("VNPay configuration is missing or invalid.");
            }
        }

        public string CreatePaymentUrl(HoaDon hoaDon, string ipAddress)
        {
            if (hoaDon == null || hoaDon.TongTien == null)
            {
                throw new ArgumentNullException(nameof(hoaDon), "Order or TotalMoney cannot be null.");
            }

            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = "127.0.0.1"; // Dự phòng cho localhost
            }

            System.Diagnostics.Debug.WriteLine($"[GenerateVNPayUrl] TmnCode: {_vnpayTmnCode}, HashSecret: {_vnpayHashSecret}, Url: {_vnpayUrl}, ReturnUrl: {_vnpayReturnUrl}");

            var orderCode = hoaDon.MaHoaDon > 0 ? hoaDon.MaHoaDon.ToString("D6") : $"ORDER{DateTime.Now.Ticks}";
            var orderInfo = $"Thanh toan don hang {orderCode}";
            var expireDate = DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss");
            var createDate = DateTime.Now.ToString("yyyyMMddHHmmss");

            var vnpayParams = new SortedDictionary<string, string>
            {
                { "vnp_Version", "2.1.0" },
                { "vnp_Command", "pay" },
                { "vnp_TmnCode", _vnpayTmnCode },
                { "vnp_Amount", ((long)Math.Round(hoaDon.TongTien * 100)).ToString() }, // Làm tròn để tránh lỗi thập phân
                { "vnp_CreateDate", createDate },
                { "vnp_CurrCode", "VND" },
                { "vnp_IpAddr", ipAddress },
                { "vnp_Locale", "vn" },
                { "vnp_OrderInfo", Uri.EscapeDataString(orderInfo) }, // Mã hóa đầy đủ
                { "vnp_OrderType", "250000" }, // Loại hàng hóa (e-commerce)
                { "vnp_ReturnUrl", _vnpayReturnUrl },
                { "vnp_ExpireDate", expireDate },
                { "vnp_TxnRef", orderCode }
            };

            // Tạo signData với mã hóa đúng
            var signData = string.Join("&", vnpayParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            System.Diagnostics.Debug.WriteLine($"[GenerateVNPayUrl] SignData: {signData}");

            var secureHash = HmacSHA512(signData, _vnpayHashSecret);
            System.Diagnostics.Debug.WriteLine($"[GenerateVNPayUrl] SecureHash: {secureHash}");

            // Thêm SecureHash vào tham số
            vnpayParams["vnp_SecureHash"] = secureHash;

            // Tạo URL thanh toán với mã hóa đầy đủ
            var paymentUrl = _vnpayUrl + "?" + string.Join("&", vnpayParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            System.Diagnostics.Debug.WriteLine($"[GenerateVNPayUrl] PaymentUrl: {paymentUrl}");

            return paymentUrl;
        }

        public bool ValidateSignature(Dictionary<string, string> vnp_Params, string hashSecret)
        {
            if (!vnp_Params.ContainsKey("vnp_SecureHash"))
            {
                System.Diagnostics.Debug.WriteLine("[ValidateSignature] Missing vnp_SecureHash");
                return false;
            }

            string vnp_SecureHash = vnp_Params["vnp_SecureHash"];
            vnp_Params.Remove("vnp_SecureHash");

            var sortedParams = new SortedDictionary<string, string>(vnp_Params);
            string signData = string.Join("&", sortedParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            System.Diagnostics.Debug.WriteLine($"[ValidateSignature] SignData: {signData}");

            string computedHash = HmacSHA512(signData, hashSecret);
            System.Diagnostics.Debug.WriteLine($"[ValidateSignature] ComputedHash: {computedHash}, ReceivedHash: {vnp_SecureHash}");

            return vnp_SecureHash.Equals(computedHash, StringComparison.OrdinalIgnoreCase);
        }

        private string HmacSHA512(string input, string key)
        {
            var hash = new HMACSHA512(Encoding.UTF8.GetBytes(key));
            var hashBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}