# Lập trình Windows - Đồ án hệ thống mua bán hàng cho cửa hàng nhỏ.
## Nhóm 
- 22120026 - Phan Minh Gia Bảo
- 22120035 - Dương Thiện Chí
##  Chi tiết milestone 01:
### UI/UX Design:
#### Về giao diện:
- Nhóm chúng em sử dụng template figma: https://www.figma.com/design/g4XuE4b5jkY7ayEogwApLc/POS-System-Web-UI-(Community)
- Chúng em lấy ý tưởng dựa trên thiết kế trên và có chỉnh sửa lại cho phù hợp với chủ đề của đồ án
#### Về xử lí lỗi và responsive:
### Design Patterns - Architecture
#### Factory Design Pattern:
- Chúng em dùng Factory Design Pattern để khởi tạo đối tượng các lớp DAO và BUS hỗ trợ cho mô hình Three Layers. Điều này giúp việc thay đổi luồng dữ liệu đầu vào một cách nhanh chóng, giúp quá trình test cũng như mở rộng hệ thống dễ dàng hơn.
#### Architecture:
- Sử dụng Three Layers Architecture để tách biệt phần xử lí GUI, Logic và Data; giúp hệ thống dễ bảo trì, mở rộng, tách biệt code giúp quá trình làm việc tường minh hơn.
- Nhóm chúng em dùng MVVM cho phần GUI giúp tách biệt phần xử lí dữ liệu và hiển thị dữ liệu ra riêng biệt, hỗ trợ việc kiểm tra unit test vì phần xử lí đã tách riêng ra phần hiển thị dữ liệu (View), và dễ dàng chỉnh sửa lại giao diện nếu có thay đổi
#### Tổng kết:
Việc sử dụng kết hợp giữa Factory Design Pattern, Three Layers và MVVM giúp mã nguồn của dự án tường minh hơn, một mã nguồn tách biệt từng phần sẽ dễ dàng test và sửa lỗi cũng như dễ mở rộng. Tuy nhiên, nhóm vẫn chưa hoàn thiện được mô hình MVVM ở milestone 01 này, kiến trúc hệ thống vẫn chưa hoàn thiện và ứng dụng được hết ý nghĩa của các kĩ thuật/kiến trúc trên.
### Advanceed Topics:
### Teamwork - Git flow:
#### Meeting:
- Trong quá trình làm việc nhóm sẽ trao đổi thông tin, báo cáo tiến độ qua nhóm chat.
- Chi tiết biên bản họp nhóm ở link sau:
+ Lần 1: https://docs.google.com/document/d/1jukkav0Mdove9GiL-h8xHUe2Gqvqrya3gDNOAjmQETE/edit?usp=sharing
+ Lần 2: https://docs.google.com/document/d/15kw1f-y4ToJVGdCeA8kOGc6K1QZZOYwOT-CVQdTNzbE/edit?usp=sharing
#### Git flow:
- Tổ chức và làm việc với repo bằng mô hình Git Flow đơn giản: nhánh master là nhánh chính, develop là nhánh trong quá trình phát triển và các nhánh feature tạo từ develop để code các chức năng khác nhau.
- Khi một tính năng được hoàn thành và kiểm tra lỗi sẽ đươc gộp nhánh develop.
- Với milestone 01 này sẽ là version 01 của dự án khi này nhánh develop sẽ gộp vào main để ra mắt phiên bản đầu tiên.
- Nếu ứng dụng sau khi ra mắt có lỗi sẽ tạo 1 nhánh hotfix từ master để sửa lỗi.
- Vì đây là dự án nhỏ và với team size là 2 người nên chúng em tinh gọn lại bỏ đi nhánh release.
### Quality Assurance
