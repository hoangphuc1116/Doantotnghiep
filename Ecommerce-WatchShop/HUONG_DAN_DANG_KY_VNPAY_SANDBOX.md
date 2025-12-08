# HƯỚNG DẪN ĐIỀN THÔNG TIN ĐĂNG KÝ VNPAY SANDBOX

## ❓ VẤN ĐỀ: Chưa có Website/Domain thật

Khi đăng ký Sandbox VNPay, bạn cần điền:
- **Tên website**: Tên dự án của bạn
- **Địa chỉ URL**: URL website

Nhưng bạn đang phát triển trên localhost và chưa có domain thật!

---

## ✅ GIẢI PHÁP: Điền thông tin test

### 1. TÊN WEBSITE
Điền tên dự án của bạn (bất kỳ):

```
Ví dụ:
- Shop Đồng Hồ Online
- Ecommerce Watch Shop
- Cửa Hàng Đồng Hồ
- My Watch Store
```

**Lưu ý:** Đây chỉ là môi trường test, tên này không quan trọng lắm.

---

### 2. ĐỊA CHỈ URL

Có 3 cách điền:

#### Cách 1: Dùng localhost (Khuyến nghị cho test)
```
http://localhost:5000
hoặc
https://localhost:7000
```

#### Cách 2: Dùng ngrok (Nếu cần URL public)
```
https://abc123.ngrok.io
```

#### Cách 3: Dùng domain giả (Nếu VNPay yêu cầu domain thật)
```
http://myshop-test.com
hoặc
http://watchshop-dev.local
```

**Khuyến nghị:** Dùng `http://localhost:5000` hoặc `https://localhost:7000`

---

## 📝 CÁCH ĐIỀN FORM ĐĂNG KÝ

### Thông tin cần điền:

```
┌─────────────────────────────────────────────────────┐
│ Tên website:                                        │
│ [Shop Đồng Hồ Online                              ] │
│                                                     │
│ Địa chỉ URL:                                        │
│ [http://localhost:5000                            ] │
│                                                     │
│ Email:                                              │
│ [your-email@gmail.com                             ] │
│                                                     │
│ Mật khẩu:                                           │
│ [••••••••••                                       ] │
│                                                     │
│ Xác nhận mật khẩu:                                  │
│ [••••••••••                                       ] │
│                                                     │
│ Mã xác nhận:                                        │
│ [M87T5E                                           ] │
│                                                     │
│                    [Đăng ký]                        │
└─────────────────────────────────────────────────────┘
```

### Ví dụ cụ thể:

```
Tên website:     Shop Đồng Hồ Online
Địa chỉ URL:     http://localhost:5000
Email:           dienh5913@gmail.com
Mật khẩu:        YourPassword123!
Xác nhận MK:     YourPassword123!
Mã xác nhận:     M87T5E
```

---

## 🔧 SAU KHI ĐĂNG KÝ THÀNH CÔNG

### 1. Xác nhận Email
- Kiểm tra email (cả Spam/Junk)
- Click link xác nhận

### 2. Đăng nhập Dashboard
- Truy cập: https://sandbox.vnpayment.vn/
- Đăng nhập bằng email và mật khẩu

### 3. Lấy thông tin cấu hình
Trong Dashboard, bạn sẽ thấy:

```
┌─────────────────────────────────────────────────────┐
│ THÔNG TIN MERCHANT                                  │
├─────────────────────────────────────────────────────┤
│ TMN Code:      VNPAYTEST01                          │
│ Hash Secret:   ABCDEFGHIJKLMNOPQRSTUVWXYZ123456     │
│ API URL:       https://sandbox.vnpayment.vn/...     │
└─────────────────────────────────────────────────────┘
```

### 4. Cập nhật vào appsettings.json

```json
{
  "VnPay": {
    "TmnCode": "VNPAYTEST01",
    "HashSecret": "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456",
    "BaseUrl": "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html",
    "ReturnUrl": "http://localhost:5000/Checkout/PaymentCallback"
  }
}
```

**Quan trọng:** `ReturnUrl` phải khớp với URL bạn đang chạy project!

---

## 🌐 NẾU CẦN URL PUBLIC (Dùng ngrok)

Nếu VNPay yêu cầu URL public hoặc bạn muốn test từ thiết bị khác:

### Bước 1: Cài đặt ngrok
```bash
# Download từ: https://ngrok.com/download
# Hoặc dùng chocolatey (Windows)
choco install ngrok
```

### Bước 2: Chạy ngrok
```bash
# Mở terminal mới
ngrok http 5000
```

### Bước 3: Lấy URL public
```
Forwarding: https://abc123.ngrok.io -> http://localhost:5000
```

### Bước 4: Cập nhật ReturnUrl
```json
{
  "VnPay": {
    "ReturnUrl": "https://abc123.ngrok.io/Checkout/PaymentCallback"
  }
}
```

**Lưu ý:** URL ngrok thay đổi mỗi lần chạy (bản free). Nếu muốn URL cố định, cần đăng ký ngrok Pro.

---

## 🎯 CHECKLIST ĐĂNG KÝ

- [ ] Điền tên website: `Shop Đồng Hồ Online`
- [ ] Điền URL: `http://localhost:5000`
- [ ] Điền email hợp lệ
- [ ] Tạo mật khẩu mạnh
- [ ] Nhập đúng mã xác nhận
- [ ] Click "Đăng ký"
- [ ] Xác nhận email
- [ ] Đăng nhập Dashboard
- [ ] Copy TMN Code và Hash Secret
- [ ] Cập nhật vào appsettings.json
- [ ] Test thanh toán

---

## ❓ CÂU HỎI THƯỜNG GẶP

### Q1: Tôi điền localhost có được không?
**A:** Có! Sandbox VNPay chấp nhận localhost cho môi trường test.

### Q2: URL có cần HTTPS không?
**A:** Không bắt buộc cho Sandbox. Nhưng nên dùng HTTPS cho production.

### Q3: Tôi có thể đổi URL sau không?
**A:** Có! Bạn có thể cập nhật trong Dashboard sau khi đăng ký.

### Q4: Nếu tôi quên mật khẩu?
**A:** Click "Quên mật khẩu" trên trang đăng nhập Sandbox.

### Q5: TMN Code và Hash Secret ở đâu?
**A:** Sau khi đăng nhập Dashboard, vào mục "Thông tin Merchant" hoặc "Cấu hình".

### Q6: Tôi có thể tạo nhiều tài khoản test không?
**A:** Có, nhưng mỗi email chỉ đăng ký được 1 tài khoản.

### Q7: Sandbox có giới hạn giao dịch không?
**A:** Không giới hạn số lượng giao dịch test.

### Q8: Khi nào cần đăng ký Production?
**A:** Khi bạn đã test xong và sẵn sàng deploy website thật lên domain thật.

---

## 🚀 SAU KHI CÓ DOMAIN THẬT

Khi bạn có domain thật (VD: `https://watchshop.vn`):

### 1. Đăng ký VNPay Production
- Truy cập: https://vnpay.vn/
- Đăng ký tài khoản doanh nghiệp
- Nộp hồ sơ pháp lý (GPKD, CMND...)
- Ký hợp đồng

### 2. Cập nhật thông tin
```json
{
  "VnPay": {
    "TmnCode": "REAL_TMN_CODE",
    "HashSecret": "REAL_HASH_SECRET",
    "BaseUrl": "https://vnpayment.vn/paymentv2/vpcpay.html",
    "ReturnUrl": "https://watchshop.vn/Checkout/PaymentCallback"
  }
}
```

### 3. Test kỹ trước khi go-live
- Test với thẻ thật (số tiền nhỏ)
- Kiểm tra callback
- Kiểm tra cập nhật đơn hàng
- Test trên mobile

---

## 💡 MẸO HAY

### 1. Dùng nhiều môi trường
```json
// appsettings.Development.json
{
  "VnPay": {
    "ReturnUrl": "http://localhost:5000/Checkout/PaymentCallback"
  }
}

// appsettings.Production.json
{
  "VnPay": {
    "ReturnUrl": "https://watchshop.vn/Checkout/PaymentCallback"
  }
}
```

### 2. Dùng User Secrets (Development)
```bash
dotnet user-secrets set "VnPay:TmnCode" "YOUR_TMN_CODE"
dotnet user-secrets set "VnPay:HashSecret" "YOUR_HASH_SECRET"
```

### 3. Dùng Environment Variables (Production)
```bash
# Linux/Mac
export VnPay__TmnCode="YOUR_TMN_CODE"
export VnPay__HashSecret="YOUR_HASH_SECRET"

# Windows
set VnPay__TmnCode=YOUR_TMN_CODE
set VnPay__HashSecret=YOUR_HASH_SECRET
```

---

## 📞 HỖ TRỢ

Nếu gặp vấn đề khi đăng ký:

- **Email:** support@vnpay.vn
- **Hotline:** 1900 55 55 77
- **Facebook:** https://www.facebook.com/vnpayofficial
- **Tài liệu:** https://sandbox.vnpayment.vn/apis/docs/

---

## ✅ TÓM TẮT

**Điền thông tin đăng ký như sau:**

```
Tên website:  Shop Đồng Hồ Online (hoặc tên bất kỳ)
Địa chỉ URL:  http://localhost:5000 (hoặc port bạn đang dùng)
Email:        Email thật của bạn
Mật khẩu:     Mật khẩu mạnh
```

**Sau đó:**
1. Xác nhận email
2. Đăng nhập Dashboard
3. Lấy TMN Code và Hash Secret
4. Cập nhật vào appsettings.json
5. Bắt đầu test!

**Chúc bạn đăng ký thành công! 🎉**
