namespace TiemTapHoa
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtTenHang = new MaskedTextBox();
            cboLoaiHang = new ComboBox();
            cboDonVi = new ComboBox();
            numGiaBan = new NumericUpDown();
            numSoLuongTon = new NumericUpDown();
            dtpNgayNhap = new DateTimePicker();
            chkConKinhDoanh = new CheckBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            cboLocLoai = new ComboBox();
            chkLocKinhDoanh = new CheckBox();
            txtTimKiem = new TextBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            dgvHangHoa = new DataGridView();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnXuatPDF = new Button();
            btnXemBangGia = new Button();
            btnXuatExcelLuoi = new Button();
            btnXuatExcelTonKho = new Button();
            lblTrangThai = new StatusStrip();
            timerTimKiem = new System.Windows.Forms.Timer(components);
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)numGiaBan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSoLuongTon).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHangHoa).BeginInit();
            SuspendLayout();
            // 
            // txtTenHang
            // 
            txtTenHang.Location = new Point(108, 37);
            txtTenHang.Name = "txtTenHang";
            txtTenHang.Size = new Size(596, 27);
            txtTenHang.TabIndex = 0;
            // 
            // cboLoaiHang
            // 
            cboLoaiHang.FormattingEnabled = true;
            cboLoaiHang.Location = new Point(108, 87);
            cboLoaiHang.Name = "cboLoaiHang";
            cboLoaiHang.Size = new Size(150, 28);
            cboLoaiHang.TabIndex = 1;
            cboLoaiHang.SelectedIndexChanged += cboLoaiHang_SelectedIndexChanged;
            // 
            // cboDonVi
            // 
            cboDonVi.FormattingEnabled = true;
            cboDonVi.Location = new Point(108, 153);
            cboDonVi.Name = "cboDonVi";
            cboDonVi.Size = new Size(150, 28);
            cboDonVi.TabIndex = 2;
            // 
            // numGiaBan
            // 
            numGiaBan.Location = new Point(118, 222);
            numGiaBan.Name = "numGiaBan";
            numGiaBan.Size = new Size(150, 27);
            numGiaBan.TabIndex = 3;
            // 
            // numSoLuongTon
            // 
            numSoLuongTon.Location = new Point(542, 87);
            numSoLuongTon.Name = "numSoLuongTon";
            numSoLuongTon.Size = new Size(162, 27);
            numSoLuongTon.TabIndex = 4;
            // 
            // dtpNgayNhap
            // 
            dtpNgayNhap.Location = new Point(432, 149);
            dtpNgayNhap.Name = "dtpNgayNhap";
            dtpNgayNhap.Size = new Size(272, 27);
            dtpNgayNhap.TabIndex = 5;
            // 
            // chkConKinhDoanh
            // 
            chkConKinhDoanh.AutoSize = true;
            chkConKinhDoanh.Location = new Point(471, 223);
            chkConKinhDoanh.Name = "chkConKinhDoanh";
            chkConKinhDoanh.Size = new Size(134, 24);
            chkConKinhDoanh.TabIndex = 6;
            chkConKinhDoanh.Text = "Còn kinh doanh";
            chkConKinhDoanh.UseVisualStyleBackColor = true;
            chkConKinhDoanh.CheckedChanged += chkConKinhDoanh_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 95);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.No;
            label2.Size = new Size(77, 20);
            label2.TabIndex = 8;
            label2.Text = "Loại hàng:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 229);
            label3.Name = "label3";
            label3.Size = new Size(60, 20);
            label3.TabIndex = 9;
            label3.Text = "Giá bán";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 44);
            label4.Name = "label4";
            label4.Size = new Size(69, 20);
            label4.TabIndex = 10;
            label4.Text = "Tên hàng";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 161);
            label5.Name = "label5";
            label5.Size = new Size(55, 20);
            label5.TabIndex = 11;
            label5.Text = "Đơn vị:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(321, 95);
            label6.Name = "label6";
            label6.Size = new Size(98, 20);
            label6.TabIndex = 12;
            label6.Text = "Số lượng tồn:";
            label6.Click += label6_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(321, 156);
            label7.Name = "label7";
            label7.Size = new Size(84, 20);
            label7.TabIndex = 13;
            label7.Text = "Ngày nhập:";
            // 
            // cboLocLoai
            // 
            cboLocLoai.FormattingEnabled = true;
            cboLocLoai.Location = new Point(21, 92);
            cboLocLoai.Name = "cboLocLoai";
            cboLocLoai.Size = new Size(260, 28);
            cboLocLoai.TabIndex = 16;
            cboLocLoai.Text = "Tất cả các loại";
            // 
            // chkLocKinhDoanh
            // 
            chkLocKinhDoanh.AutoSize = true;
            chkLocKinhDoanh.Location = new Point(21, 149);
            chkLocKinhDoanh.Name = "chkLocKinhDoanh";
            chkLocKinhDoanh.Size = new Size(226, 24);
            chkLocKinhDoanh.TabIndex = 18;
            chkLocKinhDoanh.Text = "Chỉ hiển hàng còn kinh doanh";
            chkLocKinhDoanh.UseVisualStyleBackColor = true;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(21, 40);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(260, 27);
            txtTimKiem.TabIndex = 19;
            txtTimKiem.Text = "Nước";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtTenHang);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(cboLoaiHang);
            groupBox1.Controls.Add(chkConKinhDoanh);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(dtpNgayNhap);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(cboDonVi);
            groupBox1.Controls.Add(numSoLuongTon);
            groupBox1.Controls.Add(numGiaBan);
            groupBox1.Location = new Point(12, 40);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(746, 275);
            groupBox1.TabIndex = 20;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin hàng hóa";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtTimKiem);
            groupBox2.Controls.Add(cboLocLoai);
            groupBox2.Controls.Add(chkLocKinhDoanh);
            groupBox2.Location = new Point(817, 40);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(308, 275);
            groupBox2.TabIndex = 21;
            groupBox2.TabStop = false;
            groupBox2.Text = "Tìm kiếm";
            // 
            // dgvHangHoa
            // 
            dgvHangHoa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHangHoa.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5, Column6, Column7, Column8 });
            dgvHangHoa.Location = new Point(18, 372);
            dgvHangHoa.Name = "dgvHangHoa";
            dgvHangHoa.RowHeadersWidth = 51;
            dgvHangHoa.Size = new Size(1122, 272);
            dgvHangHoa.TabIndex = 22;
            dgvHangHoa.CellContentClick += dgvHangHoa_CellContentClick;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(35, 675);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(78, 29);
            btnThem.TabIndex = 23;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(140, 675);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(72, 29);
            btnSua.TabIndex = 24;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(245, 675);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(76, 29);
            btnXoa.TabIndex = 25;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(355, 675);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(91, 29);
            btnLamMoi.TabIndex = 26;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // btnXuatPDF
            // 
            btnXuatPDF.Location = new Point(695, 675);
            btnXuatPDF.Name = "btnXuatPDF";
            btnXuatPDF.Size = new Size(85, 29);
            btnXuatPDF.TabIndex = 27;
            btnXuatPDF.Text = "Xuất PDF";
            btnXuatPDF.UseVisualStyleBackColor = true;
            // 
            // btnXemBangGia
            // 
            btnXemBangGia.Location = new Point(554, 675);
            btnXemBangGia.Name = "btnXemBangGia";
            btnXemBangGia.Size = new Size(116, 29);
            btnXemBangGia.TabIndex = 28;
            btnXemBangGia.Text = "Xem bảng giá";
            btnXemBangGia.UseVisualStyleBackColor = true;
            // 
            // btnXuatExcelLuoi
            // 
            btnXuatExcelLuoi.Location = new Point(800, 675);
            btnXuatExcelLuoi.Name = "btnXuatExcelLuoi";
            btnXuatExcelLuoi.Size = new Size(94, 29);
            btnXuatExcelLuoi.TabIndex = 29;
            btnXuatExcelLuoi.Text = "Xuất excel";
            btnXuatExcelLuoi.UseVisualStyleBackColor = true;
            // 
            // btnXuatExcelTonKho
            // 
            btnXuatExcelTonKho.Location = new Point(920, 675);
            btnXuatExcelTonKho.Name = "btnXuatExcelTonKho";
            btnXuatExcelTonKho.Size = new Size(143, 29);
            btnXuatExcelTonKho.TabIndex = 30;
            btnXuatExcelTonKho.Text = "Xuất excel tồn kho";
            btnXuatExcelTonKho.UseVisualStyleBackColor = true;
            // 
            // lblTrangThai
            // 
            lblTrangThai.ImageScalingSize = new Size(20, 20);
            lblTrangThai.Location = new Point(0, 755);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(1152, 22);
            lblTrangThai.TabIndex = 31;
            lblTrangThai.Text = "Tìm thấy n mặt hàng";
            // 
            // timerTimKiem
            // 
            timerTimKiem.Interval = 350;
            // 
            // Column1
            // 
            Column1.DataPropertyName = "MaHang";
            Column1.HeaderText = "Mã";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.Width = 80;
            // 
            // Column2
            // 
            Column2.DataPropertyName = "TenHang";
            Column2.HeaderText = "Tên hàng";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.Width = 230;
            // 
            // Column3
            // 
            Column3.DataPropertyName = "LoaiHang";
            Column3.HeaderText = "Loại hàng";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.Width = 180;
            // 
            // Column4
            // 
            Column4.DataPropertyName = "DonVi";
            Column4.HeaderText = "Đơn vị";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.Width = 85;
            // 
            // Column5
            // 
            Column5.DataPropertyName = "GiaBan";
            Column5.HeaderText = "Giá bán";
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";
            Column5.Width = 125;
            // 
            // Column6
            // 
            Column6.DataPropertyName = "SoLuongTon";
            Column6.HeaderText = "Tồn";
            Column6.MinimumWidth = 6;
            Column6.Name = "Column6";
            Column6.Width = 125;
            // 
            // Column7
            // 
            Column7.DataPropertyName = "NgayNhap";
            Column7.HeaderText = "Ngày nhập";
            Column7.MinimumWidth = 6;
            Column7.Name = "Column7";
            Column7.Width = 160;
            // 
            // Column8
            // 
            Column8.DataPropertyName = "ConKinhDoanh";
            Column8.HeaderText = "Kinh doanh";
            Column8.MinimumWidth = 6;
            Column8.Name = "Column8";
            Column8.Width = 80;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1152, 777);
            Controls.Add(lblTrangThai);
            Controls.Add(btnXuatExcelTonKho);
            Controls.Add(btnXuatExcelLuoi);
            Controls.Add(btnXemBangGia);
            Controls.Add(btnXuatPDF);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(dgvHangHoa);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "MainForm";
            Text = "Quản lý cửa hàng - tạp hóa UTC2";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)numGiaBan).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSoLuongTon).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHangHoa).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaskedTextBox txtTenHang;
        private ComboBox cboLoaiHang;
        private ComboBox cboDonVi;
        private NumericUpDown numGiaBan;
        private NumericUpDown numSoLuongTon;
        private DateTimePicker dtpNgayNhap;
        private CheckBox chkConKinhDoanh;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private ComboBox cboLocLoai;
        private CheckBox chkLocKinhDoanh;
        private TextBox txtTimKiem;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private DataGridView dgvHangHoa;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnXuatPDF;
        private Button btnXemBangGia;
        private Button btnXuatExcelLuoi;
        private Button btnXuatExcelTonKho;
        private StatusStrip lblTrangThai;
        private System.Windows.Forms.Timer timerTimKiem;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewCheckBoxColumn Column8;
    }
}
