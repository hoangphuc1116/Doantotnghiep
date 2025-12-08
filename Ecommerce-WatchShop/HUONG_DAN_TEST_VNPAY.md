# HƯỚNG DẪN TEST THANH TOÁN VNPAY

## 📋 MÔI TRƯỜNG TEST VNPAY

VNPay cung cấp môi trường Sandbox (test) miễn phí để developer test tích hợp thanh toán.

---

## 🔑 THÔNG TIN TÀI KHOẢN TEST

### Bước 1: Đăng ký tài khoản Sandbox

1. Truy cập: **https://sandbox.vnpayment.vn/devreg/**
2. Điền thông tin đăng ký:
   - Email
   - Số điện thoại
   - Tên công ty/cá nhân
3. Xác nhận email
4. Đăng nhập vào Dashboard

### Bước 2: Lấy thông tin cấu hình

Sau khi đăng nhập vào Dashboard Sandbox, bạn sẽ nhận được:

```
TMN Code (Terminal ID): Mã định danh merchant
Hash Secret: Mã bí mật để mã hóa
API URL: https://sandbox.vnpayment.vn/paymentv2/vpcpay.html
Return URL: URL callback sau khi thanh toán
```

**Ví dụ thông tin test:**
```
TMN Code: DEMO123456
Hash Secret: ABCDEFGHIJKLMNOPQRSTUVWXYZ123456
```

---

## 💳 THẺ TEST VNPAY

VNPay cung cấp các thẻ test để bạn thử nghiệm thanh toán:

### 1. Thẻ Nội địa (ATM)

#### Ngân hàng NCB (Ngân hàng Quốc Dân)
```
Số thẻ:        9704198526191432198
Tên chủ thẻ:   NGUYEN VAN A
Ngày phát hành: 07/15
Mật khẩu OTP:  123456
```

#### Ngân hàng Vietcombank
```
Số thẻ:        9704060000000001
Tên chủ thẻ:   NGUYEN VAN A
Ngày phát hành: 03/07
Mật khẩu OTP:  123456
```

#### Ngân hàng Techcombank
```
Số thẻ:        9704060000000002
Tên chủ thẻ:   NGUYEN VAN B
Ngày phát hành: 03/07
Mật khẩu OTP:  123456
```

#### Ngân hàng VietinBank
```
Số thẻ:        9704060000000003
Tên chủ thẻ:   NGUYEN VAN C
Ngày phát hành: 03/07
Mật khẩu OTP:  123456
```

### 2. Thẻ Quốc tế (Visa/MasterCard)

#### Visa Test Card
```
Số thẻ:        4111111111111111
Tên chủ thẻ:   NGUYEN VAN A
Ngày hết hạn:  12/25
CVV:           123
```

#### MasterCard Test Card
```
Số thẻ:        5555555555554444
Tên chủ thẻ:   NGUYEN VAN B
Ngày hết hạn:  12/25
CVV:           123
```

---

## 🔧 CẤU HÌNH TRONG PROJECT

### Cập nhật appsettings.json

```json
{
  "VnPay": {
    "TmnCode": "DEMO123456",
    "HashSecret": "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456",
    "BaseUrl": "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html",
    "ReturnUrl": "https://localhost:7000/Checkout/PaymentCallback",
    "Version": "2.1.0",
    "Command": "pay",
    "CurrCode": "VND",
    "Locale": "vn"
  }
}
```

**Lưu ý:** Thay `DEMO123456` và `ABCDEFGHIJKLMNOPQRSTUVWXYZ123456` bằng thông tin thực tế từ Dashboard Sandbox của bạn.

---

## 🧪 CÁCH TEST THANH TOÁN

### Bước 1: Tạo đơn hàng test
1. Vào trang web của bạn
2. Thêm sản phẩm vào giỏ hàng
3. Tiến hành thanh toán
4. Chọn phương thức "Thanh toán VNPay"

### Bước 2: Chọn ngân hàng
- Chọn một trong các ngân hàng test (NCB, Vietcombank, Techcombank...)

### Bước 3: Nhập thông tin thẻ
```
Số thẻ:        9704198526191432198
Tên chủ thẻ:   NGUYEN VAN A
Ngày phát hành: 07/15
```

### Bước 4: Xác thực OTP
```
Mật khẩu OTP: 123456
```

### Bước 5: Kiểm tra kết quả
- Thanh toán thành công: Redirect về ReturnUrl với mã giao dịch
- Thanh toán thất bại: Redirect về ReturnUrl với mã lỗi

---

## 📊 CÁC TRƯỜNG HỢP TEST

### 1. Test Thanh toán Thành công
```
Thẻ: 9704198526191432198
OTP: 123456
Kết quả: Giao dịch thành công (ResponseCode = 00)
```

### 2. Test Thanh toán Thất bại - Sai OTP
```
Thẻ: 9704198526191432198
OTP: 654321 (sai)
Kết quả: Giao dịch thất bại (ResponseCode = 07)
```

### 3. Test Thanh toán Thất bại - Hủy giao dịch
```
Thẻ: 9704198526191432198
Click "Hủy giao dịch"
Kết quả: Giao dịch bị hủy (ResponseCode = 24)
```

### 4. Test Thanh toán Thất bại - Hết thời gian
```
Thẻ: 9704198526191432198
Để quá 15 phút không xác thực
Kết quả: Timeout (ResponseCode = 99)
```

---

## 🔍 MÃ RESPONSE CODE VNPAY

| Mã | Ý nghĩa |
|----|---------|
| 00 | Giao dịch thành công |
| 07 | Trừ tiền thành công. Giao dịch bị nghi ngờ (liên quan tới lừa đảo, giao dịch bất thường) |
| 09 | Giao dịch không thành công do: Thẻ/Tài khoản của khách hàng chưa đăng ký dịch vụ InternetBanking tại ngân hàng |
| 10 | Giao dịch không thành công do: Khách hàng xác thực thông tin thẻ/tài khoản không đúng quá 3 lần |
| 11 | Giao dịch không thành công do: Đã hết hạn chờ thanh toán. Xin quý khách vui lòng thực hiện lại giao dịch |
| 12 | Giao dịch không thành công do: Thẻ/Tài khoản của khách hàng bị khóa |
| 13 | Giao dịch không thành công do Quý khách nhập sai mật khẩu xác thực giao dịch (OTP) |
| 24 | Giao dịch không thành công do: Khách hàng hủy giao dịch |
| 51 | Giao dịch không thành công do: Tài khoản của quý khách không đủ số dư để thực hiện giao dịch |
| 65 | Giao dịch không thành công do: Tài khoản của Quý khách đã vượt quá hạn mức giao dịch trong ngày |
| 75 | Ngân hàng thanh toán đang bảo trì |
| 79 | Giao dịch không thành công do: KH nhập sai mật khẩu thanh toán quá số lần quy định |
| 99 | Các lỗi khác |

---

## 🛠️ KIỂM TRA TÍCH HỢP

### 1. Kiểm tra URL được tạo
```csharp
// Log URL thanh toán để kiểm tra
Console.WriteLine($"Payment URL: {paymentUrl}");
```

URL hợp lệ sẽ có dạng:
```
https://sandbox.vnpayment.vn/paymentv2/vpcpay.html?
vnp_Amount=10000000&
vnp_Command=pay&
vnp_CreateDate=20231208150000&
vnp_CurrCode=VND&
vnp_IpAddr=127.0.0.1&
vnp_Locale=vn&
vnp_OrderInfo=Thanh+toan+don+hang+123&
vnp_OrderType=other&
vnp_ReturnUrl=https://localhost:7000/Checkout/PaymentCallback&
vnp_TmnCode=DEMO123456&
vnp_TxnRef=123456789&
vnp_Version=2.1.0&
vnp_SecureHash=ABC123...
```

### 2. Kiểm tra Callback
```csharp
// Log các tham số callback
foreach (var param in Request.Query)
{
    Console.WriteLine($"{param.Key}: {param.Value}");
}
```

### 3. Kiểm tra SecureHash
```csharp
// Verify SecureHash
bool isValidSignature = vnPayHelper.ValidateSignature(
    Request.Query, 
    hashSecret, 
    Request.Query["vnp_SecureHash"]
);
Console.WriteLine($"Signature Valid: {isValidSignature}");
```

---

## 📝 CHECKLIST TEST

- [ ] Đăng ký tài khoản Sandbox VNPay
- [ ] Lấy TMN Code và Hash Secret
- [ ] Cập nhật appsettings.json
- [ ] Test thanh toán thành công với thẻ NCB
- [ ] Test thanh toán thất bại - Sai OTP
- [ ] Test thanh toán thất bại - Hủy giao dịch
- [ ] Test thanh toán với các ngân hàng khác
- [ ] Kiểm tra callback URL
- [ ] Kiểm tra SecureHash validation
- [ ] Kiểm tra cập nhật trạng thái đơn hàng
- [ ] Test trên mobile
- [ ] Test với số tiền khác nhau

---

## 🚨 LƯU Ý QUAN TRỌNG

1. **Môi trường Sandbox:**
   - Chỉ dùng để test, không dùng cho production
   - Không có tiền thật được giao dịch
   - Dữ liệu có thể bị xóa định kỳ

2. **Bảo mật:**
   - KHÔNG commit Hash Secret vào Git
   - Dùng User Secrets hoặc Environment Variables
   - HTTPS bắt buộc cho ReturnUrl

3. **Chuyển sang Production:**
   - Đăng ký tài khoản VNPay chính thức
   - Ký hợp đồng và nộp hồ sơ
   - Cập nhật TMN Code và Hash Secret mới
   - Đổi BaseUrl sang production: `https://vnpayment.vn/paymentv2/vpcpay.html`

---

## 🔗 TÀI LIỆU THAM KHẢO

- **Sandbox Dashboard:** https://sandbox.vnpayment.vn/
- **Tài liệu API:** https://sandbox.vnpayment.vn/apis/docs/
- **Hỗ trợ kỹ thuật:** support@vnpay.vn
- **Hotline:** 1900 55 55 77

---

## 💡 MẸO TEST NHANH

### Tạo script test tự động
```bash
# Test thanh toán thành công
curl -X POST http://localhost:5000/Checkout/CreatePayment \
  -H "Content-Type: application/json" \
  -d '{"orderId": "TEST001", "amount": 100000}'

# Kiểm tra callback
curl "http://localhost:5000/Checkout/PaymentCallback?vnp_ResponseCode=00&vnp_TxnRef=TEST001"
```

### Dùng Postman
1. Import collection VNPay API
2. Tạo environment với TMN Code và Hash Secret
3. Test các endpoint

---

## ✅ KẾT LUẬN

Với hướng dẫn này, bạn có thể:
- Đăng ký và lấy thông tin test VNPay
- Sử dụng thẻ test để thử nghiệm
- Kiểm tra các trường hợp thanh toán
- Debug và fix lỗi tích hợp

**Chúc bạn test thành công! 🎉**
