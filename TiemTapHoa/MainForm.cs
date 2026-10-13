using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using FastReport;
using FastReport.Export.Pdf;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TiemTapHoa.Models;

namespace TiemTapHoa
{
    public partial class MainForm : Form
    {
        private int selectedMaHang = -1;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            LoadDataGridView();
        }

        private void LoadComboBoxData()
        {
            // Nạp dữ liệu cho ComboBox loại hàng
            cboLoaiHang.Items.Clear();
            cboLoaiHang.Items.AddRange(new object[] { "Đồ uống", "Thực phẩm khô", "Gia vị", "Đồ gia dụng" });

            // Nạp dữ liệu cho ComboBox đơn vị
            cboDonVi.Items.Clear();
            cboDonVi.Items.AddRange(new object[] { "Chai", "Gói", "Hộp", "Túi" });

            // Nạp dữ liệu cho ComboBox tìm kiếm/lọc loại
            cboLocLoai.Items.Clear();
            cboLocLoai.Items.Add("Tất cả các loại");
            cboLocLoai.Items.AddRange(new object[] { "Đồ uống", "Thực phẩm khô", "Gia vị", "Đồ gia dụng" });
            cboLocLoai.SelectedIndex = 0;
        }

        private void LoadDataGridView(string keyword = "", string loai = "Tất cả các loại", bool chiKinhDoanh = false)
        {
            using var db = new TiemTapHoaContext();
            var query = db.HangHoas.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(h => h.TenHang.Contains(keyword));
            }

            if (loai != "Tất cả các loại")
            {
                query = query.Where(h => h.LoaiHang == loai);
            }

            if (chiKinhDoanh)
            {
                query = query.Where(h => h.ConKinhDoanh == true);
            }

            var list = query.Select(h => new
            {
                h.MaHang,
                h.TenHang,
                h.LoaiHang,
                h.DonVi,
                h.GiaBan,
                SoLuongTon = h.SoLuongTon,
                h.NgayNhap,
                ConKinhDoanh = h.ConKinhDoanh ?? false
            }).ToList();

            dgvHangHoa.DataSource = list;

            if (dgvHangHoa.Columns["GiaBan"] != null)
                dgvHangHoa.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
            if (dgvHangHoa.Columns["NgayNhap"] != null)
                dgvHangHoa.Columns["NgayNhap"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void dgvHangHoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvHangHoa.Rows[e.RowIndex];
                selectedMaHang = Convert.ToInt32(row.Cells[0].Value);
                txtTenHang.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
                cboLoaiHang.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
                cboDonVi.Text = row.Cells[3].Value?.ToString() ?? string.Empty;
                numGiaBan.Value = Convert.ToDecimal(row.Cells[4].Value);
                numSoLuongTon.Value = Convert.ToInt32(row.Cells[5].Value);
                dtpNgayNhap.Value = Convert.ToDateTime(row.Cells[6].Value);
                chkConKinhDoanh.Checked = Convert.ToBoolean(row.Cells[7].Value);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenHang.Text))
            {
                MessageBox.Show("Tên hàng không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenHang.Focus();
                return false;
            }
            if (numGiaBan.Value <= 0)
            {
                MessageBox.Show("Giá bán phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numGiaBan.Focus();
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            using (var db = new TiemTapHoaContext())
            {
                var hang = new HangHoa
                {
                    TenHang = txtTenHang.Text.Trim(),
                    LoaiHang = cboLoaiHang.Text,
                    DonVi = cboDonVi.Text,
                    GiaBan = numGiaBan.Value,
                    SoLuongTon = (int)numSoLuongTon.Value,
                    NgayNhap = dtpNgayNhap.Value,
                    ConKinhDoanh = chkConKinhDoanh.Checked
                };
                db.HangHoas.Add(hang);
                db.SaveChanges();
            }
            LoadDataGridView();
            btnLamMoi_Click(sender, e);
            MessageBox.Show("Thêm hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaHang == -1)
            {
                MessageBox.Show("Vui lòng chọn hàng hóa cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInput()) return;

            using (var db = new TiemTapHoaContext())
            {
                var hang = db.HangHoas.Find(selectedMaHang);
                if (hang != null)
                {
                    hang.TenHang = txtTenHang.Text.Trim();
                    hang.LoaiHang = cboLoaiHang.Text;
                    hang.DonVi = cboDonVi.Text;
                    hang.GiaBan = numGiaBan.Value;
                    hang.SoLuongTon = (int)numSoLuongTon.Value;
                    hang.NgayNhap = dtpNgayNhap.Value;
                    hang.ConKinhDoanh = chkConKinhDoanh.Checked;
                    db.SaveChanges();
                }
            }
            LoadDataGridView();
            MessageBox.Show("Cập nhật hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaHang == -1)
            {
                MessageBox.Show("Vui lòng chọn hàng hóa cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa mặt hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                using (var db = new TiemTapHoaContext())
                {
                    var hang = db.HangHoas.Find(selectedMaHang);
                    if (hang != null)
                    {
                        db.HangHoas.Remove(hang);
                        db.SaveChanges();
                    }
                }
                LoadDataGridView();
                btnLamMoi_Click(sender, e);
                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            selectedMaHang = -1;
            txtTenHang.Clear();
            cboLoaiHang.SelectedIndex = -1;
            cboDonVi.SelectedIndex = -1;
            numGiaBan.Value = 0;
            numSoLuongTon.Value = 0;
            dtpNgayNhap.Value = DateTime.Now;
            chkConKinhDoanh.Checked = true;
            txtTenHang.Focus();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            timerTimKiem.Stop();
            timerTimKiem.Start();
        }

        private void timerTimKiem_Tick(object sender, EventArgs e)
        {
            timerTimKiem.Stop();
            FilterData();
        }

        private void cboLocLoai_SelectedIndexChanged(object sender, EventArgs e) => FilterData();
        private void chkLocKinhDoanh_CheckedChanged(object sender, EventArgs e) => FilterData();

        private void FilterData()
        {
            string keyword = txtTimKiem.Text.Trim();
            string loai = cboLocLoai.SelectedItem?.ToString() ?? "Tất cả các loại";
            bool chiKinhDoanh = chkLocKinhDoanh.Checked;
            LoadDataGridView(keyword, loai, chiKinhDoanh);
        }

        private static DataTable GetDataTableBangGia()
        {
            using var db = new TiemTapHoaContext();
            var list = db.HangHoas
                         .Where(h => h.ConKinhDoanh == true)
                         .OrderBy(h => h.MaHang)
                         .Select(h => new
                         {
                             h.TenHang,
                             h.DonVi,
                             h.GiaBan
                         }).ToList();

            DataTable dt = new("HangHoa");
            dt.Columns.Add("TenHang", typeof(string));
            dt.Columns.Add("DonVi", typeof(string));
            dt.Columns.Add("GiaBan", typeof(decimal));

            foreach (var item in list)
            {
                dt.Rows.Add(item.TenHang, item.DonVi, item.GiaBan);
            }
            return dt;
        }

        private static Report InitReport()
        {
            Report report = new();
            string reportPath = "BangGia.frx";

            if (!File.Exists(reportPath))
            {
                string templateContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Report ScriptLanguage=""CSharp"" ReportInfo.Name=""BangGia"">
  <Dictionary>
    <TableDataSource Name=""HangHoa"" ReferenceName=""HangHoa"" DataType=""System.Int32"" Enabled=""true"">
      <Column Name=""TenHang"" DataType=""System.String""/>
      <Column Name=""DonVi"" DataType=""System.String""/>
      <Column Name=""GiaBan"" DataType=""System.Decimal""/>
    </TableDataSource>
  </Dictionary>
  <ReportPage Name=""Page1"">
    <ReportTitleBand Name=""ReportTitle1"" Width=""718.2"" Height=""50"">
      <TextObject Name=""Text1"" Left=""189"" Top=""10"" Width=""340"" Height=""30"" Text=""BẢNG GIÁ HÀNG HÓA TẠP HÓA"" Font=""Arial, 16pt, style=Bold"" HorzAlign=""Center""/>
    </ReportTitleBand>
    <PageHeaderBand Name=""PageHeader1"" Width=""718.2"" Height=""30"">
      <TextObject Name=""Text2"" Left=""0"" Top=""0"" Width=""300"" Height=""25"" Text=""Tên hàng"" Font=""Arial, 10pt, style=Bold"" Border.Lines=""All""/>
      <TextObject Name=""Text3"" Left=""300"" Top=""0"" Width=""150"" Height=""25"" Text=""Đơn vị"" Font=""Arial, 10pt, style=Bold"" Border.Lines=""All""/>
      <TextObject Name=""Text4"" Left=""450"" Top=""0"" Width=""200"" Height=""25"" Text=""Giá bán"" Font=""Arial, 10pt, style=Bold"" Border.Lines=""All"" HorzAlign=""Right""/>
    </PageHeaderBand>
    <DataBand Name=""Data1"" Width=""718.2"" Height=""25"" DataSource=""HangHoa"">
      <TextObject Name=""Text5"" Left=""0"" Top=""0"" Width=""300"" Height=""25"" Text=""[HangHoa.TenHang]"" Font=""Arial, 10pt"" Border.Lines=""All""/>
      <TextObject Name=""Text6"" Left=""300"" Top=""0"" Width=""150"" Height=""25"" Text=""[HangHoa.DonVi]"" Font=""Arial, 10pt"" Border.Lines=""All""/>
      <TextObject Name=""Text7"" Left=""450"" Top=""0"" Width=""200"" Height=""25"" Text=""[HangHoa.GiaBan]"" Format=""Currency"" Format.UseLocale=""true"" Font=""Arial, 10pt"" Border.Lines=""All"" HorzAlign=""Right""/>
    </DataBand>
    <PageFooterBand Name=""PageFooter1"" Width=""718.2"" Height=""30"">
      <TextObject Name=""Text8"" Left=""550"" Top=""5"" Width=""150"" Height=""20"" Text=""Trang [Page#]"" HorzAlign=""Right""/>
    </PageFooterBand>
  </ReportPage>
</Report>";
                File.WriteAllText(reportPath, templateContent);
            }

            report.Load(reportPath);

            DataTable dt = GetDataTableBangGia();
            report.RegisterData(dt, "HangHoa");
            var dataSource = report.GetDataSource("HangHoa");
            if (dataSource != null)
            {
                dataSource.Enabled = true;
            }

            report.SetParameterValue("NgayIn", DateTime.Now.ToString("dd/MM/yyyy"));
            return report;
        }

        private void btnXuatPDF_Click_1(object sender, EventArgs e)
        {
            try
            {
                using Report report = InitReport();
                report.Prepare();

                using (var pdfExport = new FastReport.Export.Pdf.PDFExport())
                {
                    string path = "BangGia.pdf";
                    report.Export(pdfExport, path);
                    MessageBox.Show($"Đã xuất file PDF thành công tại:\n{Path.GetFullPath(path)}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất PDF: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using Report report = InitReport();
            report.Design();
        }

        private void btnXemBangGia_Click_1(object sender, EventArgs e)
        {
            try
            {
                using Report report = InitReport();
                report.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem bảng giá: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatExcelLuoi_Click_1(object sender, EventArgs e)
        {
            try
            {
                using var db = new TiemTapHoaContext();
                var list = db.HangHoas.Select(h => new
                {
                    h.MaHang,
                    h.TenHang,
                    h.LoaiHang,
                    h.DonVi,
                    h.GiaBan,
                    h.SoLuongTon,
                    h.NgayNhap,
                    ConKinhDoanh = (h.ConKinhDoanh ?? false) ? "Đang kinh doanh" : "Ngừng kinh doanh"
                }).ToList();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("DanhSachHangHoa");

                    worksheet.Cell("A1").Value = "DANH SÁCH HÀNG HÓA TẠP HÓA";
                    worksheet.Cell("A1").Style.Font.Bold = true;
                    worksheet.Cell("A1").Style.Font.FontSize = 16;
                    worksheet.Range("A1:H1").Merge();
                    worksheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    int row = 3;
                    worksheet.Cell(row, 1).Value = "Mã hàng";
                    worksheet.Cell(row, 2).Value = "Tên hàng";
                    worksheet.Cell(row, 3).Value = "Loại hàng";
                    worksheet.Cell(row, 4).Value = "Đơn vị";
                    worksheet.Cell(row, 5).Value = "Giá bán";
                    worksheet.Cell(row, 6).Value = "Số lượng tồn";
                    worksheet.Cell(row, 7).Value = "Ngày nhập";
                    worksheet.Cell(row, 8).Value = "Trạng thái";

                    var headerRange = worksheet.Range($"A{row}:H{row}");
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    foreach (var item in list)
                    {
                        row++;
                        worksheet.Cell(row, 1).Value = item.MaHang;
                        worksheet.Cell(row, 2).Value = item.TenHang;
                        worksheet.Cell(row, 3).Value = item.LoaiHang;
                        worksheet.Cell(row, 4).Value = item.DonVi;
                        worksheet.Cell(row, 5).Value = item.GiaBan;
                        worksheet.Cell(row, 6).Value = item.SoLuongTon;
                        worksheet.Cell(row, 7).Value = item.NgayNhap?.ToString("dd/MM/yyyy");
                        worksheet.Cell(row, 8).Value = item.ConKinhDoanh;
                    }

                    var tableRange = worksheet.Range($"A3:H{row}");
                    tableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    tableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Columns().AdjustToContents();

                    string filePath = "DanhSachHangHoa.xlsx";
                    workbook.SaveAs(filePath);
                    MessageBox.Show($"Xuất file Excel thành công tại:\n{Path.GetFullPath(filePath)}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatExcelTonKho_Click(object sender, EventArgs e)
        {
            using SaveFileDialog sfd = new() { Filter = "Excel Files|*.xlsx", FileName = "TonKho_TapHoaUTC2.xlsx" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using var workbook = new XLWorkbook();
                    var ws = workbook.Worksheets.Add("TonKho");

                    ws.Cell("A1").Value = "STT";
                    ws.Cell("B1").Value = "Tên hàng";
                    ws.Cell("C1").Value = "Đơn vị";
                    ws.Cell("D1").Value = "Giá bán";
                    ws.Cell("E1").Value = "Số lượng tồn";
                    ws.Cell("F1").Value = "Giá trị tồn";

                    var headerRow = ws.Row(1);
                    headerRow.Style.Font.Bold = true;
                    headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;

                    using (var db = new TiemTapHoaContext())
                    {
                        var data = db.HangHoas
                                     .Where(h => h.ConKinhDoanh == true)
                                     .ToList();

                        int rowIdx = 2;
                        foreach (var item in data)
                        {
                            ws.Cell(rowIdx, 1).Value = rowIdx - 1;
                            ws.Cell(rowIdx, 2).Value = item.TenHang;
                            ws.Cell(rowIdx, 3).Value = item.DonVi;
                            ws.Cell(rowIdx, 4).Value = item.GiaBan;
                            ws.Cell(rowIdx, 5).Value = item.SoLuongTon;
                            ws.Cell(rowIdx, 6).FormulaA1 = $"=D{rowIdx}*E{rowIdx}";
                            rowIdx++;
                        }

                        ws.Cell(rowIdx, 5).Value = "Tổng:";
                        ws.Cell(rowIdx, 5).Style.Font.Bold = true;
                        ws.Cell(rowIdx, 6).FormulaA1 = $"=SUM(F2:F{rowIdx - 1})";
                        ws.Cell(rowIdx, 6).Style.Font.Bold = true;
                    }

                    ws.SheetView.FreezeRows(1);
                    ws.RangeUsed().SetAutoFilter();
                    ws.Columns().AdjustToContents();

                    workbook.SaveAs(sfd.FileName);
                    MessageBox.Show("Xuất Excel tồn kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (IOException)
                {
                    MessageBox.Show("File Excel đang được mở bởi chương trình khác. Vui lòng đóng file rồi thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Bổ sung đầy đủ các phương thức sự kiện còn thiếu để tránh lỗi biên dịch
        private void cboLoaiHang_SelectedIndexChanged(object sender, EventArgs e) { }
        private void chkConKinhDoanh_CheckedChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }

        private void btnXuatExcelTonKho_Click_1(object sender, EventArgs e)
        {
            using SaveFileDialog sfd = new()
            {
                Filter = "Excel Files|*.xlsx",
                FileName = "TonKho_TapHoaUTC2.xlsx",
                Title = "Chọn nơi lưu file báo cáo tồn kho"
            };

            // Hiển thị hộp thoại chọn file và ép nổi lên trên cùng để không bị ẩn
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    using var workbook = new XLWorkbook();
                    var ws = workbook.Worksheets.Add("TonKho");

                    ws.Cell("A1").Value = "STT";
                    ws.Cell("B1").Value = "Tên hàng";
                    ws.Cell("C1").Value = "Đơn vị";
                    ws.Cell("D1").Value = "Giá bán";
                    ws.Cell("E1").Value = "Số lượng tồn";
                    ws.Cell("F1").Value = "Giá trị tồn";

                    var headerRow = ws.Row(1);
                    headerRow.Style.Font.Bold = true;
                    headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;

                    using (var db = new TiemTapHoaContext())
                    {
                        var data = db.HangHoas
                                     .Where(h => h.ConKinhDoanh == true)
                                     .ToList();

                        int rowIdx = 2;
                        foreach (var item in data)
                        {
                            ws.Cell(rowIdx, 1).Value = rowIdx - 1;
                            ws.Cell(rowIdx, 2).Value = item.TenHang;
                            ws.Cell(rowIdx, 3).Value = item.DonVi;
                            ws.Cell(rowIdx, 4).Value = item.GiaBan;
                            ws.Cell(rowIdx, 5).Value = item.SoLuongTon;
                            ws.Cell(rowIdx, 6).FormulaA1 = $"=D{rowIdx}*E{rowIdx}";
                            rowIdx++;
                        }

                        ws.Cell(rowIdx, 5).Value = "Tổng:";
                        ws.Cell(rowIdx, 5).Style.Font.Bold = true;
                        ws.Cell(rowIdx, 6).FormulaA1 = $"=SUM(F2:F{rowIdx - 1})";
                        ws.Cell(rowIdx, 6).Style.Font.Bold = true;
                    }

                    ws.SheetView.FreezeRows(1);
                    ws.RangeUsed().SetAutoFilter();
                    ws.Columns().AdjustToContents();

                    workbook.SaveAs(sfd.FileName);
                    MessageBox.Show("Xuất Excel tồn kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (IOException)
                {
                    MessageBox.Show("File Excel đang được mở bởi chương trình khác. Vui lòng đóng file rồi thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi phát sinh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}