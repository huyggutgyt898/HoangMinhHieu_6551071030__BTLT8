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
            report.Load("BangGia.frx");

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

        private void btnXemBangGia_Click(object sender, EventArgs e)
        {
            using Report report = InitReport();
            report.Show();
        }

        private void btnXuatPDF_Click(object sender, EventArgs e)
        {
            using SaveFileDialog sfd = new() { Filter = "PDF Files|*.pdf", FileName = "BangGia_TapHoaUTC2.pdf" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using Report report = InitReport();
                report.Prepare();
                report.Export(new FastReport.Export.Pdf.PDFExport(), sfd.FileName);

                if (MessageBox.Show("Xuất PDF thành công! Bạn có muốn mở file không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                }
            }
        }

        private void btnXuatExcelLuoi_Click(object sender, EventArgs e)
        {
            using SaveFileDialog sfd = new() { Filter = "Excel Files|*.xlsx", FileName = "HangHoa_Luoi.xlsx" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using var workbook = new XLWorkbook();
                    var worksheet = workbook.Worksheets.Add("HangHoa");

                    for (int i = 0; i < dgvHangHoa.Columns.Count; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = dgvHangHoa.Columns[i].HeaderText;
                    }
                    worksheet.Row(1).Style.Font.Bold = true;

                    for (int i = 0; i < dgvHangHoa.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvHangHoa.Columns.Count; j++)
                        {
                            var val = dgvHangHoa.Rows[i].Cells[j].Value;
                            if (val != null)
                            {
                                if (decimal.TryParse(val.ToString(), out decimal numVal))
                                    worksheet.Cell(i + 2, j + 1).Value = numVal;
                                else
                                    worksheet.Cell(i + 2, j + 1).Value = val.ToString();
                            }
                        }
                    }
                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(sfd.FileName);

                    MessageBox.Show("Xuất Excel từ lưới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (IOException)
                {
                    MessageBox.Show("File Excel đang được mở bởi chương trình khác. Vui lòng đóng file rồi thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void label3_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }

        private void cboLoaiHang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkConKinhDoanh_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}