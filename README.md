# Lập trình Windows - Đồ án hệ thống mua bán hàng cho cửa hàng nhỏ.

## Nhóm

- 22120026 - Phan Minh Gia Bảo
- 22120035 - Dương Thiện Chí

## Chi tiết milestone 01:

### Các chức năng đã hoàn thành:

- Tài khoản:
  - admin - 1234
  - manager - 1234
  - shopper - 1234
- Trang xem sản phẩm của người mua hàng (shopper)
  - Hiển thị danh sách sản phẩm
  - Tìm kiếm sản phẩm theo tên (dùng AutoSuggestionBox để hiển thị danh sách đề xuất)
  - Lọc sản phẩm theo loại hàng
- Trang hiển thị giỏ hàng (shopper)
  - Thêm xóa sản phẩm trong giỏ hàng
  - Tự động cập nhật khoản tiền thanh toán
- Trang dashboard của cửa hàng và admin (2 trang)
  - Thêm xóa sửa sản phẩm và cập nhật lên database.
  - Hiển thị các sản phẩm dựa theo category.
  - Thêm category và cập nhật lên database.
- Trang quản lý các tài khoản (admin)
  - Thêm xóa sửa các tài khoản và cập nhật lên database.
  - Trang đăng nhập/quên mật khẩu/ đăng ký (3 trang)
- Đăng nhập: dùng Bcrypt để mã hóa mật khẩu và xác thực mật khẩu, có sử dụng checkbox remember me để lưu trữ tài khoản trước đó
- Đăng ký: tạo tài khoản mới và lưu vào database, mật khẩu mã hóa bằng Bcrypt

### Database:

#### Thiết kế:

![Ảnh database](assets/db.png)

- Ảnh trên là database cơ bản phục vụ cho các chức năng ở milestone 01 này, sau này sẽ mở rộng thêm.

### UI/UX Design:

#### Về giao diện:

- Nhóm chúng em sử dụng template figma: https://www.figma.com/design/g4XuE4b5jkY7ayEogwApLc/POS-System-Web-UI-(Community)
- Chúng em lấy ý tưởng dựa trên thiết kế trên và có chỉnh sửa lại cho phù hợp với chủ đề của đồ án

#### Về xử lí lỗi và responsive:

- Giao diện hiện tại có bắt các lỗi nhập thiếu dữ liệu.
- Giao diện hỗ trợ tốt cho fullscreen laptop [1920x1080]

### Design Patterns - Architecture

#### Factory Design Pattern:

- Chúng em dùng Factory Design Pattern để khởi tạo đối tượng các lớp DAO và BUS hỗ trợ cho mô hình Three Layers. Điều này giúp việc thay đổi luồng dữ liệu đầu vào một cách nhanh chóng, giúp quá trình test cũng như mở rộng hệ thống dễ dàng hơn.

#### Architecture:

- Sử dụng Three Layers Architecture để tách biệt phần xử lí GUI, Logic và Data; giúp hệ thống dễ bảo trì, mở rộng, tách biệt code giúp quá trình làm việc tường minh hơn.
- Nhóm chúng em dùng MVVM cho phần GUI giúp tách biệt phần xử lí dữ liệu và hiển thị dữ liệu ra riêng biệt, hỗ trợ việc kiểm tra unit test vì phần xử lí đã tách riêng ra phần hiển thị dữ liệu (View), và dễ dàng chỉnh sửa lại giao diện nếu có thay đổi

#### Tổng kết:

Việc sử dụng kết hợp giữa Factory Design Pattern, Three Layers và MVVM giúp mã nguồn của dự án tường minh hơn, một mã nguồn tách biệt từng phần sẽ dễ dàng test và sửa lỗi cũng như dễ mở rộng. Tuy nhiên, nhóm vẫn chưa hoàn thiện được mô hình MVVM ở milestone 01 này, kiến trúc hệ thống vẫn chưa hoàn thiện và ứng dụng được hết ý nghĩa của các kĩ thuật/kiến trúc trên.

### Advanceed Topics:

- Sử dụng AutoSuggestionBox để hỗ trợ tìm kiếm sản phẩm, khi người dùng nhập chữ sẽ tự động đề xuất và hiển thị danh sách.
- Sử dụng lớp ICommand để có thể binding command từ View sang ViewModel.
- Dùng Extension Configuration và Configuration.Json để đọc file appsettings.json.
- Dùng UserControl để tạo dialog trên Page
- Dùng Bcrypt để mã hóa mật khẩu
- Dùng Invoke trong UserControll để tới dữ liệu qua Page

### Teamwork - Git flow:

#### Meeting:

- Trong quá trình làm việc nhóm sẽ trao đổi thông tin, báo cáo tiến độ qua nhóm chat.
- Chi tiết biên bản họp nhóm ở link sau:

* Lần 1: https://docs.google.com/document/d/1jukkav0Mdove9GiL-h8xHUe2Gqvqrya3gDNOAjmQETE/edit?usp=sharing
* Lần 2: https://docs.google.com/document/d/15kw1f-y4ToJVGdCeA8kOGc6K1QZZOYwOT-CVQdTNzbE/edit?usp=sharing

#### Git flow:

- Tổ chức và làm việc với repo bằng mô hình Git Flow đơn giản: nhánh master là nhánh chính, develop là nhánh trong quá trình phát triển và các nhánh feature tạo từ develop để code các chức năng khác nhau.
- Khi một tính năng được hoàn thành và kiểm tra lỗi sẽ đươc gộp nhánh develop.
- Với milestone 01 này sẽ là version 01 của dự án khi này nhánh develop sẽ gộp vào main để ra mắt phiên bản đầu tiên.
- Nếu ứng dụng sau khi ra mắt có lỗi sẽ tạo 1 nhánh hotfix từ master để sửa lỗi.
- Vì đây là dự án nhỏ và với team size là 2 người nên chúng em tinh gọn lại bỏ đi nhánh release.

### Quality Assurance

- Các chức năng sẽ được thành viên thực hiện test thủ công rồi báo cáo cho nhóm để đưa vào nhánh chính.
- Sau khi đưa vào nhánh chính nếu có xung đột với chức năng khác thì sẽ được chỉnh sửa rồi gộp lại
- Các điều trên được thể hiện qua git graph:

![Git Graph 1](assets/graph1.png)

![Git Graph 1](assets/graph2.png)

## Set up:

### Database:

- Nhóm dùng PostgreSQL 13.16 \
   `docker run -e POSTGRES_USER=USER -e POSTGRES_PASSWORD=PASSWORD -e POSTGRES_DB=BCMarket -p PORT:5432 --name NAME -d postgres:13.16`
- Chạy migration để tạo database: \
  `npm install`
- Cần tạo file .env trong thư mục migration gồm:

```dotenv
NODE_ENV=development
PG_HOST=localhost
PG_PORT=Port
PG_USER=Username
PG_PASSWORD=Password
PG_DATABASE=DatabaseName
```

- Tạo bảng và nhập dữ liệu: \
  `knex migrate:latest`\
  `knex seed:run`
- Tạo file appsettings.json trong thư mục BC_Market:

```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=port;Database=DatabaseName;Username=username;Password=Password"
  }
 }
```

#### Demo link:
