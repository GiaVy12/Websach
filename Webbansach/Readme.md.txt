📚 StoreSach – Website Bán Sách
Website thương mại điện tử bán sách, xây dựng với ASP.NET (C#) và SQL Server, có đầy đủ luồng mua hàng cho khách và trang quản trị cho admin.
🚀 Tính năng
Người dùng
- Đăng ký / đăng nhập tài khoản
- Xem danh sách và chi tiết sách
- Thêm vào giỏ hàng, đặt hàng

Quản trị (Admin)
- Quản lý sách (CRUD: thêm / sửa / xóa / xem)
- Quản lý danh mục (Categories)
- Quản lý đơn hàng (Orders)
- Dashboard tổng quan

🛠 Công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Backend | ASP.NET (C#), Web Forms |
| Cơ sở dữ liệu | SQL Server |
| Giao diện | Bootstrap |

⚙️ Hướng dẫn cài đặt

1. Clone project
bash
git clone https://github.com/GiaVy12/Web-ban-sach.git


2. Tạo cơ sở dữ liệu
- Mở SQL Server Management Studio
- Tạo database tên `StoreSach`
- Chạy file script `database/StoreSach.sql`

3. Cấu hình kết nối
- Mở file `Web.config`
- Sửa `connection string` theo thông tin SQL Server trên máy bạn

4. Chạy project
- Mở solution bằng Visual Studio
- Nhấn `Run` (F5)

🔑 Tài khoản demo

> Tài khoản dưới đây chỉ dùng để demo trên môi trường local, không dùng cho môi trường thật.

| Vai trò | Tài khoản | Mật khẩu |
|---|---|---|
| Admin | admin | 123456 |

📌 Cấu trúc thư mục chính

StoreSach/
├── Admin/          # Trang quản trị: Books, Categories, Orders, Dashboard
├── database/        # File script SQL Server
└── Web.config        # Cấu hình kết nối cơ sở dữ liệu


📄 Ghi chú

Dự án được xây dựng với mục đích học tập, thực hành mô hình ASP.NET Web Forms kết hợp SQL Server trong một ứng dụng thương mại điện tử hoàn chỉnh.
