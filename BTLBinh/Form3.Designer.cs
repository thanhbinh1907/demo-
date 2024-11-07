namespace BTLBinh
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.danhMụcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hóaĐơnNhậpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nhânViênToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nhàCungCấpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiệnÍchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trợGiúpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvDanhSach = new System.Windows.Forms.DataGridView();
            this.txtMaHDB = new System.Windows.Forms.TextBox();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.dtpNgayBan = new System.Windows.Forms.DateTimePicker();
            this.btnNhanVien = new System.Windows.Forms.Button();
            this.btnChiTiet = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSach)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.danhMụcToolStripMenuItem,
            this.hóaĐơnNhậpToolStripMenuItem,
            this.acnToolStripMenuItem,
            this.nhânViênToolStripMenuItem,
            this.nhàCungCấpToolStripMenuItem,
            this.tiệnÍchToolStripMenuItem,
            this.trợGiúpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 25, 0, 25);
            this.menuStrip1.Size = new System.Drawing.Size(256, 769);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // danhMụcToolStripMenuItem
            // 
            this.danhMụcToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.danhMụcToolStripMenuItem.Name = "danhMụcToolStripMenuItem";
            this.danhMụcToolStripMenuItem.Padding = new System.Windows.Forms.Padding(50, 25, 50, 25);
            this.danhMụcToolStripMenuItem.Size = new System.Drawing.Size(247, 79);
            this.danhMụcToolStripMenuItem.Text = "DANH MỤC";
            this.danhMụcToolStripMenuItem.Click += new System.EventHandler(this.danhMụcToolStripMenuItem_Click);
            // 
            // hóaĐơnNhậpToolStripMenuItem
            // 
            this.hóaĐơnNhậpToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hóaĐơnNhậpToolStripMenuItem.Name = "hóaĐơnNhậpToolStripMenuItem";
            this.hóaĐơnNhậpToolStripMenuItem.Padding = new System.Windows.Forms.Padding(50, 25, 50, 25);
            this.hóaĐơnNhậpToolStripMenuItem.Size = new System.Drawing.Size(247, 79);
            this.hóaĐơnNhậpToolStripMenuItem.Text = "NHẬP HÀNG";
            this.hóaĐơnNhậpToolStripMenuItem.Click += new System.EventHandler(this.hóaĐơnNhậpToolStripMenuItem_Click);
            // 
            // acnToolStripMenuItem
            // 
            this.acnToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.acnToolStripMenuItem.Name = "acnToolStripMenuItem";
            this.acnToolStripMenuItem.Padding = new System.Windows.Forms.Padding(50, 25, 50, 25);
            this.acnToolStripMenuItem.Size = new System.Drawing.Size(247, 79);
            this.acnToolStripMenuItem.Text = "BÁN HÀNG";
            // 
            // nhânViênToolStripMenuItem
            // 
            this.nhânViênToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nhânViênToolStripMenuItem.Name = "nhânViênToolStripMenuItem";
            this.nhânViênToolStripMenuItem.Padding = new System.Windows.Forms.Padding(50, 25, 50, 25);
            this.nhânViênToolStripMenuItem.Size = new System.Drawing.Size(247, 79);
            this.nhânViênToolStripMenuItem.Text = "NHÂN VIÊN ";
            this.nhânViênToolStripMenuItem.Click += new System.EventHandler(this.nhânViênToolStripMenuItem_Click);
            // 
            // nhàCungCấpToolStripMenuItem
            // 
            this.nhàCungCấpToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nhàCungCấpToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.nhàCungCấpToolStripMenuItem.Name = "nhàCungCấpToolStripMenuItem";
            this.nhàCungCấpToolStripMenuItem.Padding = new System.Windows.Forms.Padding(50, 25, 50, 25);
            this.nhàCungCấpToolStripMenuItem.Size = new System.Drawing.Size(247, 79);
            this.nhàCungCấpToolStripMenuItem.Text = "NHÀ CUNG CẤP";
            this.nhàCungCấpToolStripMenuItem.Click += new System.EventHandler(this.btnNhaCC_Click);
            // 
            // tiệnÍchToolStripMenuItem
            // 
            this.tiệnÍchToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tiệnÍchToolStripMenuItem.Name = "tiệnÍchToolStripMenuItem";
            this.tiệnÍchToolStripMenuItem.Padding = new System.Windows.Forms.Padding(50, 25, 50, 25);
            this.tiệnÍchToolStripMenuItem.Size = new System.Drawing.Size(247, 79);
            this.tiệnÍchToolStripMenuItem.Text = "TIỆN ÍCH";
            // 
            // trợGiúpToolStripMenuItem
            // 
            this.trợGiúpToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trợGiúpToolStripMenuItem.Name = "trợGiúpToolStripMenuItem";
            this.trợGiúpToolStripMenuItem.Padding = new System.Windows.Forms.Padding(50, 25, 50, 25);
            this.trợGiúpToolStripMenuItem.Size = new System.Drawing.Size(247, 79);
            this.trợGiúpToolStripMenuItem.Text = "TRỢ GIÚP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(792, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 29);
            this.label1.TabIndex = 9;
            this.label1.Text = "HÓA ĐƠN BÁN HÀNG";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnXoa);
            this.groupBox1.Controls.Add(this.btnSua);
            this.groupBox1.Controls.Add(this.btnThem);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(384, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(142, 256);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chức năng";
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(29, 191);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 31);
            this.btnXoa.TabIndex = 9;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Location = new System.Drawing.Point(29, 125);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 31);
            this.btnSua.TabIndex = 8;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(29, 62);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 31);
            this.btnThem.TabIndex = 7;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(665, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Mã Hóa Đơn Bán";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(665, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Mã Nhân Viên";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(665, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Tổng Tiền";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1062, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "Mã Khách Hàng";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1062, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "Ngày Bán";
            // 
            // dgvDanhSach
            // 
            this.dgvDanhSach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSach.Location = new System.Drawing.Point(290, 343);
            this.dgvDanhSach.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvDanhSach.Name = "dgvDanhSach";
            this.dgvDanhSach.RowHeadersWidth = 51;
            this.dgvDanhSach.RowTemplate.Height = 24;
            this.dgvDanhSach.Size = new System.Drawing.Size(1120, 403);
            this.dgvDanhSach.TabIndex = 11;
            this.dgvDanhSach.SelectionChanged += new System.EventHandler(this.DgvDanhSach_SelectionChanged);
            // 
            // txtMaHDB
            // 
            this.txtMaHDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaHDB.Location = new System.Drawing.Point(822, 117);
            this.txtMaHDB.Name = "txtMaHDB";
            this.txtMaHDB.Size = new System.Drawing.Size(164, 26);
            this.txtMaHDB.TabIndex = 18;
            // 
            // txtMaNV
            // 
            this.txtMaNV.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaNV.Location = new System.Drawing.Point(822, 177);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(135, 26);
            this.txtMaNV.TabIndex = 19;
            // 
            // txtTongTien
            // 
            this.txtTongTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongTien.Location = new System.Drawing.Point(822, 230);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.Size = new System.Drawing.Size(164, 26);
            this.txtTongTien.TabIndex = 20;
            // 
            // txtMaKH
            // 
            this.txtMaKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaKH.Location = new System.Drawing.Point(1196, 175);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(164, 26);
            this.txtMaKH.TabIndex = 22;
            // 
            // dtpNgayBan
            // 
            this.dtpNgayBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayBan.Location = new System.Drawing.Point(1196, 121);
            this.dtpNgayBan.Name = "dtpNgayBan";
            this.dtpNgayBan.Size = new System.Drawing.Size(227, 26);
            this.dtpNgayBan.TabIndex = 26;
            // 
            // btnNhanVien
            // 
            this.btnNhanVien.Location = new System.Drawing.Point(1007, 173);
            this.btnNhanVien.Name = "btnNhanVien";
            this.btnNhanVien.Size = new System.Drawing.Size(23, 23);
            this.btnNhanVien.TabIndex = 30;
            this.btnNhanVien.Text = "...";
            this.btnNhanVien.UseVisualStyleBackColor = true;
            this.btnNhanVien.Click += new System.EventHandler(this.nhânViênToolStripMenuItem_Click);
            // 
            // btnChiTiet
            // 
            this.btnChiTiet.AutoSize = true;
            this.btnChiTiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChiTiet.Location = new System.Drawing.Point(1007, 228);
            this.btnChiTiet.Name = "btnChiTiet";
            this.btnChiTiet.Size = new System.Drawing.Size(75, 30);
            this.btnChiTiet.TabIndex = 31;
            this.btnChiTiet.Text = "Chi Tiết";
            this.btnChiTiet.UseVisualStyleBackColor = true;
            this.btnChiTiet.Click += new System.EventHandler(this.btnChiTiet_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1435, 769);
            this.Controls.Add(this.btnChiTiet);
            this.Controls.Add(this.btnNhanVien);
            this.Controls.Add(this.dtpNgayBan);
            this.Controls.Add(this.txtMaKH);
            this.Controls.Add(this.txtTongTien);
            this.Controls.Add(this.txtMaNV);
            this.Controls.Add(this.txtMaHDB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvDanhSach);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem danhMụcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hóaĐơnNhậpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tiệnÍchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trợGiúpToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvDanhSach;
        private System.Windows.Forms.TextBox txtMaHDB;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.TextBox txtMaKH;
        private System.Windows.Forms.DateTimePicker dtpNgayBan;
        private System.Windows.Forms.Button btnNhanVien;
        private System.Windows.Forms.ToolStripMenuItem nhânViênToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nhàCungCấpToolStripMenuItem;
        private System.Windows.Forms.Button btnChiTiet;
    }
}