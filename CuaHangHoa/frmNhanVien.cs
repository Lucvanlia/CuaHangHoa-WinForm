using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CuaHangHoa
{
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void pnShow_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Bat()
        {
            txtTenNV.Enabled = true;
            txtNoiSinh.Enabled = true;
            txtEmail.Enabled = true;
            txtTaiKhoan.Enabled = true;
            txtPass.Enabled = true;
            txtSDT.Enabled = true;
            txtNhom.Enabled = true;
            txtDiaChi.Enabled = true;
        }
        private void Tat()
        {
            txtMaNV.Enabled = false;
            txtTenNV.Enabled = false;
            txtNoiSinh.Enabled = false;
            txtEmail.Enabled = false;
            txtTaiKhoan.Enabled = false;
            txtPass.Enabled = false;
            txtSDT.Enabled = false;
            txtNhom.Enabled = false;
            txtDiaChi.Enabled = false;
        }
        private void Reset()
        {
            txtLinkAnh.ResetText();
            txtMaNV.Enabled = true;
            txtTaiKhoan.Enabled = true;
            txtMaNV.ReadOnly = true;
            txtTaiKhoan.ReadOnly = true;
            txtMaNV.ResetText();
            txtTenNV.ResetText();
            txtNoiSinh.ResetText();
            txtEmail.ResetText();
            txtTaiKhoan.ResetText();
            txtPass.ResetText();
            txtSDT.ResetText();
            txtNhom.ResetText();
            txtDiaChi.ResetText();
            pbAnhNV.ResetText();
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
            pbAnhNV.Image = Image.FromFile(paths + "\\Images\\Images_nv\\anh_avarta.jpg");
        }
        private Boolean CheckRong()
        {
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Mã nhân viên không được dể trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNV.Focus();
                return false;
            }
            if (txtTenNV.Text == "")
            {
                MessageBox.Show("Tên nhân viên không được dể trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNV.Focus();
                return false;
            }
            if (txtNoiSinh.Text == "")
            {
                MessageBox.Show("Nơi sinh nhân viên không được dể trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNoiSinh.Focus();
                return false;
            }
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Email nhân viên không được dể trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return false;
            }
            if (txtNhom.Text == "")
            {
                MessageBox.Show("Nhóm nhân viên không được dể trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNhom.Focus();
                return false;
            }
           
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Địa chỉ nhân viên không được dể trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return false;
            }
            if (txtSDT.Text == "")
            {
                MessageBox.Show("Số điện thoại nhân viên không được dể trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSDT.Focus();
                return false;
            }
            if (txtTaiKhoan.Text == "")
            {
                MessageBox.Show("Tài khoản nhân viên không được dể trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTaiKhoan.Focus();
                return false;
            }
            if(txtLinkAnh.Text =="")
            {
                MessageBox.Show("Ảnh nhân viên không được dể trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTaiKhoan.Focus();
                return false;
            }    
            return true;
        }

        private void ShowNhanVien()
        {
            DataTable dtLSP = new DataTable();
            LopHamXuLy.Connect();
            string sql = "SELECT MANV,TenNV,NoiSinh_NV,DiaChi_nv,sdt_nv,email_nv,ngaysinh_nv,taikhoan_nv,nhom_nv,gioitinh_nv,Anh_Nv FROM NhanVien";
            if (LopHamXuLy.TruyVan(sql, dtLSP))
            {
                dtgvNV.DataSource = dtLSP;
                //============================================================================
                dtgvNV.Columns[0].HeaderText = "Mã nhân viên";
                this.dtgvNV.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dtgvNV.Columns[1].HeaderText = "Tên nhân viên";
                this.dtgvNV.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dtgvNV.Columns[2].HeaderText = "Nơi Sinh";
                this.dtgvNV.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dtgvNV.Columns[3].HeaderText = "Địa chỉ";
                dtgvNV.Columns[4].HeaderText = "Số điện thoại";
                this.dtgvNV.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dtgvNV.Columns[5].HeaderText = "Email";
                this.dtgvNV.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dtgvNV.Columns[6].HeaderText = "Ngày sinh";
                this.dtgvNV.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dtgvNV.Columns[7].HeaderText = "Tài khoản";
                this.dtgvNV.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dtgvNV.Columns[8].HeaderText = "Nhóm";
                this.dtgvNV.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dtgvNV.Columns[9].HeaderText = "Giới tính";
                this.dtgvNV.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dtgvNV.Columns[10].HeaderText = "Hình";
                 this.dtgvNV.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dtgvNV.AllowUserToAddRows =false;
                //============================================================================
                dtgvNV.Columns[0].DefaultCellStyle.Font = new Font("nunito", 8, FontStyle.Bold);
                dtgvNV.Columns[1].DefaultCellStyle.Font = new Font("nunito", 8, FontStyle.Bold);
                dtgvNV.Columns[2].DefaultCellStyle.Font = new Font("nunito", 8, FontStyle.Bold);
                dtgvNV.Columns[3].DefaultCellStyle.Font = new Font("nunito", 8, FontStyle.Bold);
                dtgvNV.Columns[4].DefaultCellStyle.Font = new Font("nunito", 8, FontStyle.Bold);
                dtgvNV.Columns[5].DefaultCellStyle.Font = new Font("nunito", 8, FontStyle.Bold);
                dtgvNV.Columns[6].DefaultCellStyle.Font = new Font("nunito", 8, FontStyle.Bold);
                dtgvNV.Columns[7].DefaultCellStyle.Font = new Font("nunito", 8, FontStyle.Bold);
                dtgvNV.Columns[8].DefaultCellStyle.Font = new Font("nunito", 8, FontStyle.Bold);
                dtgvNV.Columns[9].DefaultCellStyle.Font = new Font("nunito", 8, FontStyle.Bold);
                dtgvNV.Columns[10].DefaultCellStyle.Font = new Font("nunito", 8, FontStyle.Bold);
                dtgvNV.ColumnHeadersDefaultCellStyle.Font = new Font("nunito", 9, FontStyle.Bold);
                //============================================================================
                dtgvNV.Refresh();
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            Tat();
            ShowNhanVien();
            this.WindowState = FormWindowState.Maximized;
            btnLuu.Enabled = false;
            btnChonAnh.Enabled = false;
            LopHamXuLy.Connect();
            string username = frmLogin.username;
            string sql = "select tennv from nhanvien where manv ='" + username.Trim().ToString() + "' and nhom_nv='admin' ";
            if (LopHamXuLy.CheckKey(sql))
            {
                chkTaiKhoan.Enabled = true;

            }//kt if check nhóm tài khoản 
            else
            {
                chkTaiKhoan.Enabled = false;

            }
        }
        
        private void dtgvNV_Click(object sender, EventArgs e)
        {
            if (dtgvNV.CurrentRow.Cells["Manv"].Value.ToString() == "")
            {
             
                MessageBox.Show("Hãy chọn vào vùng có dữ liệu !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (btnSua.Enabled == false)
            {
         
                MessageBox.Show("Bạn đang ở chế độ thêm !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
              
            }
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
            txtMaNV.Text = dtgvNV.CurrentRow.Cells["MaNV"].Value.ToString();
            txtTenNV.Text = dtgvNV.CurrentRow.Cells["TenNV"].Value.ToString();
            txtNoiSinh.Text = dtgvNV.CurrentRow.Cells["NoiSinh_NV"].Value.ToString();
            txtEmail.Text = dtgvNV.CurrentRow.Cells["Email_nv"].Value.ToString();
            txtTaiKhoan.Text = dtgvNV.CurrentRow.Cells["TaiKhoan_nv"].Value.ToString();
            txtSDT.Text = dtgvNV.CurrentRow.Cells["sdt_nv"].Value.ToString();
            txtNhom.Text = dtgvNV.CurrentRow.Cells["nhom_nv"].Value.ToString();
            txtDiaChi.Text = dtgvNV.CurrentRow.Cells["DiaChi_nv"].Value.ToString();
            piktime.Text = dtgvNV.CurrentRow.Cells["NgaySinh_Nv"].Value.ToString();
            if (dtgvNV.CurrentRow.Cells["GioiTinh_NV"].Value.ToString() == "Nam") chkcheck.Checked = true;
            else chkcheck.Checked = false;
            if (dtgvNV.CurrentRow.Cells["Anh_NV"].Value.ToString() == "") pbAnhNV.Image = Image.FromFile(paths + "\\Images\\Images_nv\\anh_avarta.jpg");
            else
                pbAnhNV.Image = Image.FromFile(paths + dtgvNV.CurrentRow.Cells["Anh_NV"].Value.ToString());
   

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            Random res = new Random();

            // String of alphabets  
            String str = "abcdefghijklmnopqrstuvwxyz1234567890ASDFGHNCVZMXLOPQERWT";
            int size = 150;

            // Initializing the empty string 
            String ran = "";

            for (int i = 0; i < size; i++)
            {

                // Selecting a index randomly 
                int x = res.Next(40);

                // Appending the character at the  
                // index to the random string. 
                ran = ran + str[x];
            }
            string anh = "";
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.InitialDirectory = "C:\\";
                open.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png|   All Files (*.*)|*.*";
                if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    anh = open.FileName;
                    pbAnhNV.ImageLocation = anh;
                    string anhgoc = System.IO.Path.GetFileName(open.FileName);
                    //=======================================
                    string tennv = txtTenNV.Text.Replace(" ", "_");
                    string tenbodau = LopHamXuLy.BoDau(tennv)+ran;
                    string tenfile = System.IO.Path.GetFileNameWithoutExtension(open.FileName);
                    int sophanbiet = LopHamXuLy.RandomNumber(10, 999999);

                    //=======================================
                    string sqlcheck = "select Anh_nv from nhanvien where Anh_nv = '" + txtLinkAnh.Text + "'";
                    if (LopHamXuLy.CheckKey(sqlcheck))
                    {
                        string maphanbiet = LopHamXuLy.RandomString(10, true);
                        string phoihop = maphanbiet + sophanbiet.ToString();
                        string rename = string.Format("{0}{1}{2}", tenbodau, phoihop, System.IO.Path.GetExtension(anhgoc));
                        txtLinkAnh.Text = rename;
                    }
                    else
                    {
                        string rename = string.Format("{0}{1}{2}", tenbodau, sophanbiet, System.IO.Path.GetExtension(anhgoc));
                        txtLinkAnh.Text = rename;
                    }
                }
                if(CheckRong())
                {
                    
                        string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                        System.IO.File.Copy(open.FileName, paths + "\\Images\\Images_nv\\" + txtLinkAnh.Text);
                    
                }    
            }
            catch (Exception loi)
            {
                MessageBox.Show(loi.Message);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == true)
            {
                ThemNhanVien();
            }
            else
            {
                SuaNhanVien();

            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
            Bat();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            Reset();
            LopHamXuLy.Connect();
            string sql = string.Format("SELECT CONCAT('MNV' ,RIGHT(CONCAT('0000000',ISNULL(right(max(MaNV),7),0)+1),7)) from NhanVien where MaNV like 'MNV%'");
            SqlCommand cmd = new SqlCommand(sql, LopHamXuLy.conn);
            var code = cmd.ExecuteScalar();
            string a = code.ToString();
            txtMaNV.Text = a;
            txtTaiKhoan.Text = a;
            txtTaiKhoan.Enabled = false;
            txtPass.Enabled = false;
            txtTenNV.Focus();
            btnLuu.Enabled = true;
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
          
            Bat();
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
            txtTenNV.Focus();
            btnLuu.Enabled = true;

        }
        public void ThemNhanVien()
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.CheckFileExists)
            {
                string gt = "";
                Random random = new Random();
                string code = (random.Next(99999999)).ToString();
                Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (chkcheck.Checked == true)
                        gt = "Nam";
                else
                        gt ="Nữ";
                if(CheckRong())
                {
                    if (reg.IsMatch(txtEmail.Text))
                    {
                        string sqlcheck_sdt = "select * from nhanvien where sdt_nv = '" + txtSDT.Text + "' ";
                        string sqlcheck_email = "select * from nhanvien where email_nv = '"+txtEmail.Text+"' ";
                        string sqlInsert = "INSERT INTO NhanVien (MaNV,TenNV,NoiSinh_nv,sdt_nv,email_nv,TaiKhoan_nv,MatKhau_nv,Anh_nv,Nhom_nv,DiaChi_nv,Ngaysinh_nv,gioitinh_nv) VALUES (N'" + txtMaNV.Text + "',N'" + txtTenNV.Text + "',N'" + txtNoiSinh.Text + "',N'" + txtSDT.Text + "',N'" + txtEmail.Text + "',N'" + txtMaNV.Text + "',N'" + code + "',N' \\Images\\Images_nv\\" + txtLinkAnh.Text + "',N'" + txtNhom.Text + "',N'" + txtDiaChi.Text + "',N'" + piktime.Value.ToString("yyyy-MM-dd") + "',N'" + gt + "' ) ";
                        if (MessageBox.Show("Bạn có muốn thêm nhân viên này ?", "Xác nhận thêm !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                LopHamXuLy.Connect();
                                if(LopHamXuLy.CheckKey(sqlcheck_email))
                                {
                                    MessageBox.Show("Email đã tồn tại không thể thêm !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;

                                }
                                if (LopHamXuLy.CheckKey(sqlcheck_sdt))
                                {
                                    MessageBox.Show("Số điện thoại đã tồn tại không thể thêm !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;

                                }
                                LopHamXuLy.RunSQL(sqlInsert);
                                MessageBox.Show("Đã Thêm Thành Công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ShowNhanVien();
                                Reset();
                                btnXoa.Enabled = true;
                                btnSua.Enabled = true;
                                btnLuu.Enabled = false;

                            }
                            catch (Exception Loi)
                            {
                                MessageBox.Show(Loi.Message);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Email cửa bạn không đúng định dạng", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }    
            }
          
        }

        public void SuaNhanVien()
        {
            string gt;
            Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            txtPass.Enabled = true;
            txtTaiKhoan.ReadOnly =true;
            if (txtPass.Text == "")
            {
                if (chkcheck.Checked == true)
                    gt = "Nam";
                else
                    gt = "Nữ";
                if (reg.IsMatch(txtEmail.Text))
                {
                    //  string sqlcheck_sdt = "select * from nhanvien where sdt_nv = '" + txtSDT.Text + "' ";
                    //string sqlcheck_email = "select * from nhanvien where email_nv = '" + txtEmail.Text + "' ";
                    string sqlUpdate = "UPDATE NhanVien SET TenNV =  N'" + txtTenNV.Text + "' , NoiSinh_nv = N'" + txtNoiSinh.Text + "', sdt_nv = N'" + txtSDT.Text + "',email_nv = N'" + txtEmail.Text + "',Anh_nv= N' \\Images\\Images_nv\\" + txtLinkAnh.Text + "',Nhom_nv = N'" + txtNhom.Text + "',DiaChi_nv = N'" + txtDiaChi.Text + "',Ngaysinh_nv = N'" + piktime.Value.ToString("yyyy-MM-dd") + "',gioitinh_nv = N'" + gt + "'  ,taikhoan_nv = '"+txtTaiKhoan.Text.Trim()+"' WHERE Manv = '" + txtMaNV.Text + "'";
                    string sqlUpdate1 = "UPDATE NhanVien SET TenNV =  N'" + txtTenNV.Text + "' , NoiSinh_nv = N'" + txtNoiSinh.Text + "', sdt_nv = N'" + txtSDT.Text + "',email_nv = N'" + txtEmail.Text + "',Nhom_nv = N'" + txtNhom.Text + "',DiaChi_nv = N'" + txtDiaChi.Text + "',Ngaysinh_nv = N'" + piktime.Value.ToString("yyyy-MM-dd") + "',gioitinh_nv = N'" + gt + "'   ,taikhoan_nv = '"+txtTaiKhoan.Text.Trim()+"'WHERE Manv = '" + txtMaNV.Text + "'";
                    LopHamXuLy.Connect();
                    if (MessageBox.Show("Bạn có muốn sửa thông tin nhân viên này ?", "Xác nhận sửa !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            /*     if (LopHamXuLy.CheckKey(sqlcheck_email))
                                 {
                                     MessageBox.Show("Email đã tồn tại không thể thêm !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                     return;

                                 }
                                 if (LopHamXuLy.CheckKey(sqlcheck_sdt))
                                 {
                                     MessageBox.Show("Số điện thoại đã tồn tại không thể thêm !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                     return;

                                 }
                            */
                            if (txtLinkAnh.Text =="")
                            {
                                LopHamXuLy.RunSQL(sqlUpdate1);
                            }
                            else
                            {
                                LopHamXuLy.RunSQL(sqlUpdate);
                            }

                            MessageBox.Show("Đã Sửa Thành Công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowNhanVien();
                            Reset();
                            btnThem.Enabled = true;
                            btnXoa.Enabled = true;
                            btnLuu.Enabled = false;
                            btnLuu.Enabled = false;

                        }
                        catch (Exception Loi)
                        {
                            MessageBox.Show(Loi.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Email cửa bạn không đúng định dạng", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                if (chkcheck.Checked == true)
                    gt = "Nam";
                else
                    gt = "Nữ";
                if (reg.IsMatch(txtEmail.Text))
                {
                    string sqlUpdate1 = "UPDATE NhanVien SET TenNV =  N'" + txtTenNV.Text + "' , NoiSinh_nv = N'" + txtNoiSinh.Text + "', sdt_nv = N'" + txtSDT.Text + "',email_nv = N'" + txtEmail.Text + "',Nhom_nv = N'" + txtNhom.Text + "',DiaChi_nv = N'" + txtDiaChi.Text + "',Ngaysinh_nv = N'" + piktime.Value.ToString("yyyy-MM-dd") + "',gioitinh_nv = N'" + gt + "'  ,taikhoan_nv = '" + txtTaiKhoan.Text + "', matkhau_nv = N'"+txtPass.Text.Trim().ToString()+"'WHERE Manv = '" + txtMaNV.Text + "'";

                    string sqlUpdate = "UPDATE NhanVien SET TenNV =  N'" + txtTenNV.Text + "' , NoiSinh_nv = N'" + txtNoiSinh.Text + "', sdt_nv = N'" + txtSDT.Text + "',email_nv = N'" + txtEmail.Text + "',Anh_nv= N' \\Images\\Images_nv\\" + txtLinkAnh.Text + "',Nhom_nv = N'" + txtNhom.Text + "',DiaChi_nv = N'" + txtDiaChi.Text + "',Ngaysinh_nv = N'" + piktime.Value.ToString("yyyy-MM-dd") + "',gioitinh_nv = N'" + gt + "',matkhau_nv = N'"+txtPass.Text.Trim().ToString()+"'  ,taikhoan_nv = '"+txtTaiKhoan.Text.Trim()+"'  WHERE Manv = '" + txtMaNV.Text + "'";
                    LopHamXuLy.Connect();
                    if (MessageBox.Show("Bạn có muốn sửa lại chất liệu này ?", "Xác nhận sửa !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            if (txtLinkAnh.Text == "")
                            {
                                LopHamXuLy.RunSQL(sqlUpdate1);
                            }
                            else
                            {
                                LopHamXuLy.RunSQL(sqlUpdate);
                            }
                            MessageBox.Show("Đã Sửa Thành Công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowNhanVien();
                            Reset();
                            btnThem.Enabled = true;
                            btnXoa.Enabled = true;
                            btnLuu.Enabled = false;

                        }
                        catch (Exception Loi)
                        {
                            MessageBox.Show(Loi.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Email cửa bạn không đúng định dạng", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void piktime_ValueChanged(object sender, EventArgs e)
        {
            piktime.CustomFormat = "dd/mm/yyyy";
        }

        private void piktime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                piktime.CustomFormat = " ";
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if(txtMaNV.Text=="")
            {
                MessageBox.Show("Bạn chưa chọn nhân viên","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string sqlDelete = "DELETE FROM NhanVien WHERE MaNV = '" + txtMaNV.Text + "'";
                string sqlcheck = "Select * from hoadonban , nhanvien where hoadonban.manv  = '" + txtMaNV.Text + "'";
                if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên?", "Xác nhận xóa !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        LopHamXuLy.Connect();
                        if(LopHamXuLy.CheckKey(sqlcheck))
                        {
                            MessageBox.Show("nhân viên đã tồn tại trong hóa đơn không thể xóa !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        LopHamXuLy.RunSQL(sqlDelete);
                        MessageBox.Show("Đã xóa thành công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowNhanVien();
                        Reset();
                    }
                    catch (Exception Loi)
                    {
                        MessageBox.Show(Loi.Message);
                    }
                }
            }

        }

        private void txtTenNV_TextChanged(object sender, EventArgs e)
        {
            btnChonAnh.Enabled = true;
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Reset();
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;

        }

        private void guna2TextBox8_TextChanged(object sender, EventArgs e)
        {
            DataTable dtLSP = new DataTable();
            LopHamXuLy.Connect();
            string sql = "SELECT MANV,TenNV,NoiSinh_NV,DiaChi_nv,sdt_nv,email_nv,ngaysinh_nv,taikhoan_nv,nhom_nv,gioitinh_nv,Anh_Nv FROM NhanVien where tennv like '%"+txtTim.Text+"%' " ;
            if (LopHamXuLy.TruyVan(sql, dtLSP))
            {
                dtgvNV.DataSource = dtLSP;
                dtgvNV.Columns[0].HeaderText = "Mã nhân viên";
                dtgvNV.Columns[0].Width = 150;
                dtgvNV.Columns[1].HeaderText = "Tên nhân viên";
                dtgvNV.Columns[1].Width = 150;
                dtgvNV.Columns[2].HeaderText = "Nơi Sinh";
                dtgvNV.Columns[3].HeaderText = "Địa chỉ";
                dtgvNV.Columns[4].HeaderText = "Số điện thoại";
                dtgvNV.Columns[4].Width = 150;
                dtgvNV.Columns[5].HeaderText = "Email";
                dtgvNV.Columns[6].HeaderText = "Ngày sinh";
                dtgvNV.Columns[7].HeaderText = "Tài khoản";
                dtgvNV.Columns[8].HeaderText = "Nhóm";
                dtgvNV.Columns[9].HeaderText = "Giới tính";
                dtgvNV.Columns[10].HeaderText = "Hình";
                dtgvNV.AllowUserToAddRows = false;
                dtgvNV.Refresh();
            }
        }

        private void chkNu_CheckedChanged(object sender, EventArgs e)
        {
            if(chkNu.Checked == true )
            {
                chkNam.Checked = false;
                chkBanHang.Checked = false;
                DataTable dtLSP = new DataTable();
                LopHamXuLy.Connect();
                string sql = "SELECT MANV,TenNV,NoiSinh_NV,DiaChi_nv,sdt_nv,email_nv,ngaysinh_nv,taikhoan_nv,nhom_nv,gioitinh_nv,Anh_Nv FROM NhanVien where gioitinh_nv = N'Nữ' ";
                if (LopHamXuLy.TruyVan(sql, dtLSP))
                {
                    dtgvNV.DataSource = dtLSP;
                    dtgvNV.Columns[0].HeaderText = "Mã nhân viên";
                    dtgvNV.Columns[0].Width = 150;
                    dtgvNV.Columns[1].HeaderText = "Tên nhân viên";
                    dtgvNV.Columns[1].Width = 150;
                    dtgvNV.Columns[2].HeaderText = "Nơi Sinh";
                    dtgvNV.Columns[3].HeaderText = "Địa chỉ";
                    dtgvNV.Columns[4].HeaderText = "Số điện thoại";
                    dtgvNV.Columns[4].Width = 150;
                    dtgvNV.Columns[5].HeaderText = "Email";
                    dtgvNV.Columns[6].HeaderText = "Ngày sinh";
                    dtgvNV.Columns[7].HeaderText = "Tài khoản";
                    dtgvNV.Columns[8].HeaderText = "Nhóm";
                    dtgvNV.Columns[9].HeaderText = "Giới tính";
                    dtgvNV.Columns[10].HeaderText = "Hình";
                    dtgvNV.AllowUserToAddRows = false;
                    dtgvNV.Refresh();
                }
            }
            else
            {
                ShowNhanVien();
            }

        }

        private void chkNam_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNam.Checked == true)
            {
                chkNu.Checked = false;
                chkBanHang.Checked = false;
                DataTable dtLSP = new DataTable();
                LopHamXuLy.Connect();
                string sql = "SELECT MANV,TenNV,NoiSinh_NV,DiaChi_nv,sdt_nv,email_nv,ngaysinh_nv,taikhoan_nv,nhom_nv,gioitinh_nv,Anh_Nv FROM NhanVien where gioitinh_nv = N'NAM' ";
                if (LopHamXuLy.TruyVan(sql, dtLSP))
                {
                    dtgvNV.DataSource = dtLSP;
                    dtgvNV.Columns[0].HeaderText = "Mã nhân viên";
                    dtgvNV.Columns[0].Width = 150;
                    dtgvNV.Columns[1].HeaderText = "Tên nhân viên";
                    dtgvNV.Columns[1].Width = 150;
                    dtgvNV.Columns[2].HeaderText = "Nơi Sinh";
                    dtgvNV.Columns[3].HeaderText = "Địa chỉ";
                    dtgvNV.Columns[4].HeaderText = "Số điện thoại";
                    dtgvNV.Columns[4].Width = 150;
                    dtgvNV.Columns[5].HeaderText = "Email";
                    dtgvNV.Columns[6].HeaderText = "Ngày sinh";
                    dtgvNV.Columns[7].HeaderText = "Tài khoản";
                    dtgvNV.Columns[8].HeaderText = "Nhóm";
                    dtgvNV.Columns[9].HeaderText = "Giới tính";
                    dtgvNV.Columns[10].HeaderText = "Hình";
                    dtgvNV.AllowUserToAddRows = false;
                    dtgvNV.Refresh();
                }
            }
            else
            {
                ShowNhanVien();
            } 
                
        }

        private void chkBanHang_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBanHang.Checked == true)
            {
                chkNu.Checked = false;
                chkNam.Checked = false;
                DataTable dtLSP = new DataTable();
                LopHamXuLy.Connect();
                string sql = "SELECT nhanvien.MaNV , nhanvien.TenNV,NoiSinh_NV,DiaChi_nv,sdt_nv,email_nv,ngaysinh_nv,taikhoan_nv,nhom_nv,gioitinh_nv,Anh_Nv FROM hoadonban ,NhanVien where NhanVien.MaNV = HoaDonBan.MANV group by nhanvien.MaNV ,nhanvien.TenNV,NoiSinh_NV,DiaChi_nv,sdt_nv,email_nv,ngaysinh_nv,taikhoan_nv,nhom_nv,gioitinh_nv,Anh_Nv";
                if (LopHamXuLy.TruyVan(sql, dtLSP))
                {
                    dtgvNV.DataSource = dtLSP;
                    dtgvNV.Columns[0].HeaderText = "Mã nhân viên";
                    dtgvNV.Columns[0].Width = 150;
                    dtgvNV.Columns[1].HeaderText = "Tên nhân viên";
                    dtgvNV.Columns[1].Width = 150;
                    dtgvNV.Columns[2].HeaderText = "Nơi Sinh";
                    dtgvNV.Columns[3].HeaderText = "Địa chỉ";
                    dtgvNV.Columns[4].HeaderText = "Số điện thoại";
                    dtgvNV.Columns[4].Width = 150;
                    dtgvNV.Columns[5].HeaderText = "Email";
                    dtgvNV.Columns[6].HeaderText = "Ngày sinh";
                    dtgvNV.Columns[7].HeaderText = "Tài khoản";
                    dtgvNV.Columns[8].HeaderText = "Nhóm";
                    dtgvNV.Columns[9].HeaderText = "Giới tính";
                    dtgvNV.Columns[10].HeaderText = "Hình";
                    dtgvNV.AllowUserToAddRows = false;
                    dtgvNV.Refresh();
                }
            }
            else
            {
                ShowNhanVien();
            }
        }

        private void chkTaiKhoan_CheckedChanged(object sender, EventArgs e)
        {
            
     
            if (chkTaiKhoan.Checked == true)
            {
                txtTaiKhoan.Enabled = true;
                txtTaiKhoan.ReadOnly = false;
            }
            else
            {
                txtTaiKhoan.Enabled = false;
                txtTaiKhoan.ReadOnly = true;
            }

        }

        private void chkSendEmail_CheckedChanged(object sender, EventArgs e)
        {
         

        }

        private void chkSendEmail_Click(object sender, EventArgs e)
        {
         
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Vui lòng chọn nhân viên !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }//check rỗng 
            string code = "", to;
            string b = "&nbsp;</p></td></tr> <tr>\r\n<td align=\"center\" style=\"padding:0;Margin:0;padding-top:30px\"> <!--[if mso]><a href=\"https://viewstripo.email/\" target=\"_blank\" hidden> <v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" esdevVmlButton href=\"https://viewstripo.email/\" style=\"height:52px; v-text-anchor:middle; width:327px\" arcsize=\"50%\" strokecolor=\"#ffdda9\" strokeweight=\"2px\" fillcolor=\"#ffdda9\"> <w:anchorlock></w:anchorlock> <center style='color:#000000; font-family:\"Exo 2\", sans-serif; font-size:20px; font-weight:400; line-height:20px; mso-text-raise:1px'>Không được chia sẽ mã này</center> </v:roundrect></a>\r\n<![endif]--> <!--[if !mso]><!-- --><span class=\"msohide es-button-border\" style=\"border-style:solid;border-color:#FFDDA9;background:#FFDDA9;border-width:0px 0px 2px 0px;display:inline-block;border-radius:30px;width:auto;mso-hide:all\"><a href=\"https://viewstripo.email/\" class=\"es-button\" target=\"_blank\" style=\"mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;color:#000000;font-size:20px;padding:15px 30px 15px 30px;display:inline-block;background:#FFDDA9;border-radius:30px;font-family:'Exo 2', sans-serif;font-weight:normal;font-style:normal;line-height:24px;width:auto;text-align:center;mso-padding-alt:0;mso-border-alt:10px solid #FFDDA9\">Không được chia sẽ mã này</a></span> <!--<![endif]--></td></tr></table></td></tr></table></td></tr> <tr>\r\n<td class=\"es-m-p30t es-m-p30b es-m-p20r es-m-p20l\" align=\"left\" bgcolor=\"#f9f9f9\" style=\"padding:40px;Margin:0;background-color:#f9f9f9;border-radius:0px 0px 20px 20px\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"><tr><td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"><tr><td align=\"center\" style=\"padding:0;Margin:0\"><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:'Exo 2', sans-serif;line-height:27px;color:#666666;font-size:18px\">57A đường số 2, P. Trường Thọ, TP. Thủ Đức, TP HCM</p>\r\n <p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:'Exo 2', sans-serif;line-height:27px;color:#666666;font-size:18px\"><strong>0316690342 - Hotline&nbsp;: 0909191639</strong></p></td></tr><tr><td align=\"center\" class=\"es-m-txt-c\" style=\"padding:0;Margin:0;padding-top:15px;font-size:0\"><table cellpadding=\"0\" cellspacing=\"0\" class=\"es-table-not-adapt es-social\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"><tr><td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;padding-right:10px\"><img title=\"Facebook\" src=\"https://enbifrn.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png\" alt=\"Fb\" height=\"32\" width=\"32\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\"></td>\r\n <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;padding-right:10px\"><img title=\"Twitter\" src=\"https://enbifrn.stripocdn.email/content/assets/img/social-icons/logo-black/twitter-logo-black.png\" alt=\"Tw\" height=\"32\" width=\"32\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\"></td><td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;padding-right:10px\"><img title=\"Instagram\" src=\"https://enbifrn.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png\" alt=\"Inst\" height=\"32\" width=\"32\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\"></td>\r\n <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;padding-right:10px\"><img title=\"Youtube\" src=\"https://enbifrn.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png\" alt=\"Yt\" height=\"32\" width=\"32\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\"></td><td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;padding-right:10px\"><img title=\"Telegram\" src=\"https://enbifrn.stripocdn.email/content/assets/img/messenger-icons/logo-black/telegram-logo-black.png\" alt=\"Telegram\" height=\"32\" width=\"32\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\"></td>\r\n <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;padding-right:10px\"><img title=\"TikTok\" src=\"https://enbifrn.stripocdn.email/content/assets/img/social-icons/logo-black/tiktok-logo-black.png\" alt=\"Tt\" height=\"32\" width=\"32\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\"></td><td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0\"><img title=\"Pinterest\" src=\"https://enbifrn.stripocdn.email/content/assets/img/social-icons/logo-black/pinterest-logo-black.png\" alt=\"P\" height=\"32\" width=\"32\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\"></td></tr></table></td></tr></table></td></tr></table></td></tr> <tr>\r\n<td align=\"left\" style=\"padding:0;Margin:0;padding-top:30px;padding-left:40px;padding-right:40px\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"><tr><td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"><tr><td align=\"center\" height=\"15\" style=\"padding:0;Margin:0\"></td></tr></table></td></tr></table></td></tr> <tr><td class=\"es-m-p30t es-m-p30b\" align=\"left\" bgcolor=\"#ffffff\" style=\"padding:0;Margin:0;padding-top:40px;padding-bottom:40px;background-color:#ffffff;border-radius:20px\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"><tr>\r\n<td class=\"es-m-p20l es-m-p20r\" align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;padding-left:40px;padding-right:40px;width:640px\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"><tr><td align=\"center\" class=\"es-m-txt-c\" style=\"padding:0;Margin:0\"><h1 style=\"Margin:0;line-height:43px;mso-line-height-rule:exactly;font-family:'Exo 2', sans-serif;font-size:36px;font-style:normal;font-weight:bold;color:#000000\">&nbsp;Shop Hoa Đất Việt</h1></td></tr> <tr>\r\n<td align=\"center\" class=\"es-m-txt-c\" style=\"padding:0;Margin:0;padding-top:15px\"><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:'Exo 2', sans-serif;line-height:27px;color:#666666;font-size:18px\">HOA TƯƠI ĐẤT VIỆT (DAT VIET FLOWERS) chuyên cung cấp dịch vụ điện hoa, hoa chúc mừng khai trương, hoa sinh nhật, hoa chúc mừng các dịp lễ - tết, hoa cưới, hoa chia buồn, hoa sự kiện, trang trí cổng cưới long phụng, kết tráp rồng phượng và đào tạo học viên cắm hoa, ...<br>Với đội ngũ nhân viên trẻ đầy nhiệt huyết và sự sáng tạo, tận tâm với nghề, chúng tôi cam kết luôn mang lại cho quý khách sản phẩm có tính thẩm mỹ cao với chi phí thấp nhất, dịch vụ chăm sóc khách hàng chu đáo nhất.</p></td></tr></table></td></tr></table></td></tr> <tr>\r\n<td align=\"left\" style=\"padding:0;Margin:0;padding-top:30px;padding-left:40px;padding-right:40px\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"><tr><td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"><tr><td align=\"center\" height=\"15\" style=\"padding:0;Margin:0\"></td></tr></table></td></tr></table></td></tr></table></td></tr></table> <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-footer\" align=\"center\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top\"><tr>\r\n<td class=\"es-m-p15r es-m-p15l\" align=\"center\" style=\"padding:0;Margin:0\"><table class=\"es-footer-body\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:640px\" role=\"none\"><tr><td class=\"es-m-p30t es-m-p30b es-m-p20r es-m-p20l\" align=\"left\" bgcolor=\"#ffffff\" style=\"padding:40px;Margin:0;background-color:#ffffff;border-radius:20px\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"><tr><td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"><tr>\r\n<td align=\"center\" class=\"es-m-txt-l\" style=\"padding:0;Margin:0;padding-top:15px\"><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:'Exo 2', sans-serif;line-height:24px;color:#666666;font-size:16px\"><strong>https://hoatuoidatviet.vn/</strong><br/> <h3>Được tạo bởi Thanh Phát</h3></p> </td></tr></table></td></tr></table></td></tr></table></td></tr></table> <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\"><tr><td align=\"center\" style=\"padding:0;Margin:0\"><table bgcolor=\"transparent\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:640px\" role=\"none\"><tr>\r\n<td align=\"left\" style=\"Margin:0;padding-left:20px;padding-right:20px;padding-top:40px;padding-bottom:40px\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"><tr><td valign=\"top\" align=\"center\" style=\"padding:0;Margin:0;width:560px\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"><tr>\r\n<td class=\"es-infoblock made_with\" align=\"center\" style=\"padding:0;Margin:0;line-height:14px;font-size:0;color:#CCCCCC\"><a target=\"_blank\" href=\"https://viewstripo.email/?utm_source=templates&utm_medium=email&utm_campaign=gadgets_6&utm_content=share_your_wishlists_for_a_bonus\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#CCCCCC;font-size:12px\"><img src=\"https://enbifrn.stripocdn.email/content/guids/CABINET_9df86e5b6c53dd0319931e2447ed854b/images/64951510234941531.png\" alt width=\"125\" height=\"56\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\"></a> </td></tr></table></td></tr></table></td></tr></table></td></tr></table></td></tr></table></div></body></html>";
            string a = "\r\n<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" lang=\"vi\" style=\"font-family:'Exo 2', sans-serif\"><head><meta charset=\"UTF-8\"><meta content=\"width=device-width, initial-scale=1\" name=\"viewport\"><meta name=\"x-apple-disable-message-reformatting\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><meta content=\"telephone=no\" name=\"format-detection\"><title>New Template</title> <!--[if (mso 16)]><style type=\"text/css\">     a {text-decoration: none;}     </style><![endif]--> <!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]--> <!--[if gte mso 9]><xml> <o:OfficeDocumentSettings> <o:AllowPNG></o:AllowPNG> <o:PixelsPerInch>96</o:PixelsPerInch> </o:OfficeDocumentSettings> </xml>\r\n<![endif]--> <!--[if !mso]><!-- --><link href=\"https://fonts.googleapis.com/css2?family=Exo+2:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap\" rel=\"stylesheet\"> <!--<![endif]--><style type=\"text/css\">.rollover span { font-size:0;}.rollover:hover .rollover-first { max-height:0px!important; display:none!important;}.rollover:hover .rollover-second { max-height:none!important; display:block!important;}.es-menu.es-table-not-adapt td a:hover,a.es-button:hover { text-decoration:underline!important;}#outlook a { padding:0;}.es-button { mso-style-priority:100!important; text-decoration:none!important;} a[x-apple-data-detectors] { color:inherit!important; text-decoration:none!important; font-size:inherit!important; font-family:inherit!important; font-weight:inherit!important; line-height:inherit!important;}\r\n.es-desk-hidden { display:none; float:left; overflow:hidden; width:0; max-height:0; line-height:0; mso-hide:all;}@media only screen and (max-width:600px) {p, ul li, ol li, a { line-height:150%!important } h1, h2, h3, h1 a, h2 a, h3 a { line-height:120%!important } h1 { font-size:28px!important; text-align:left } h2 { font-size:24px!important; text-align:left } h3 { font-size:20px!important; text-align:left } .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a { font-size:28px!important; text-align:left } .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a { font-size:24px!important; text-align:left } .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a { font-size:20px!important; text-align:left } .es-menu td a { font-size:16px!important } .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a { font-size:16px!important }\r\n .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a { font-size:16px!important } .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a { font-size:16px!important } .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a { font-size:12px!important } *[class=\"gmail-fix\"] { display:none!important } .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 { text-align:center!important } .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 { text-align:right!important } .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 { text-align:left!important } .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img { display:inline!important } .es-button-border { display:inline-block!important } a.es-button, button.es-button { font-size:20px!important; display:inline-block!important } .es-adaptive table, .es-left, .es-right { width:100%!important }\r\n .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header { width:100%!important; max-width:600px!important } .es-adapt-td { display:block!important; width:100%!important } .adapt-img { width:100%!important; height:auto!important } .es-m-p0 { padding:0!important } .es-m-p0r { padding-right:0!important } .es-m-p0l { padding-left:0!important } .es-m-p0t { padding-top:0!important } .es-m-p0b { padding-bottom:0!important } .es-m-p20b { padding-bottom:20px!important } .es-mobile-hidden, .es-hidden { display:none!important } tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden { width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important } tr.es-desk-hidden { display:table-row!important } table.es-desk-hidden { display:table!important } td.es-desk-menu-hidden { display:table-cell!important } .es-menu td { width:1%!important }\r\n table.es-table-not-adapt, .esd-block-html table { width:auto!important } table.es-social { display:inline-block!important } table.es-social td { display:inline-block!important } .es-m-p5 { padding:5px!important } .es-m-p5t { padding-top:5px!important } .es-m-p5b { padding-bottom:5px!important } .es-m-p5r { padding-right:5px!important } .es-m-p5l { padding-left:5px!important } .es-m-p10 { padding:10px!important } .es-m-p10t { padding-top:10px!important } .es-m-p10b { padding-bottom:10px!important } .es-m-p10r { padding-right:10px!important } .es-m-p10l { padding-left:10px!important } .es-m-p15 { padding:15px!important } .es-m-p15t { padding-top:15px!important } .es-m-p15b { padding-bottom:15px!important } .es-m-p15r { padding-right:15px!important } .es-m-p15l { padding-left:15px!important } .es-m-p20 { padding:20px!important } .es-m-p20t { padding-top:20px!important } .es-m-p20r { padding-right:20px!important }\r\n .es-m-p20l { padding-left:20px!important } .es-m-p25 { padding:25px!important } .es-m-p25t { padding-top:25px!important } .es-m-p25b { padding-bottom:25px!important } .es-m-p25r { padding-right:25px!important } .es-m-p25l { padding-left:25px!important } .es-m-p30 { padding:30px!important } .es-m-p30t { padding-top:30px!important } .es-m-p30b { padding-bottom:30px!important } .es-m-p30r { padding-right:30px!important } .es-m-p30l { padding-left:30px!important } .es-m-p35 { padding:35px!important } .es-m-p35t { padding-top:35px!important } .es-m-p35b { padding-bottom:35px!important } .es-m-p35r { padding-right:35px!important } .es-m-p35l { padding-left:35px!important } .es-m-p40 { padding:40px!important } .es-m-p40t { padding-top:40px!important } .es-m-p40b { padding-bottom:40px!important } .es-m-p40r { padding-right:40px!important } .es-m-p40l { padding-left:40px!important } .m-c-p16r { padding-right:8vw }\r\n .es-desk-hidden { display:table-row!important; width:auto!important; overflow:visible!important; max-height:inherit!important } }@media screen and (max-width:384px) {.mail-message-content { width:414px!important } }</style>\r\n </head>\r\n <body style=\"width:100%;font-family:'Exo 2', sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0\"><div dir=\"ltr\" class=\"es-wrapper-color\" lang=\"vi\" style=\"background-color:#12022F\"> <!--[if gte mso 9]><v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\"> <v:fill type=\"tile\" src=\"https://enbifrn.stripocdn.email/content/guids/CABINET_7550686899481ac1ae35908cede0c283/images/group_10_DPF.png\" color=\"#12022f\" origin=\"0.5, 0\" position=\"0.5, 0\"></v:fill> </v:background><![endif]--><table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" background=\"https://enbifrn.stripocdn.email/content/guids/CABINET_7550686899481ac1ae35908cede0c283/images/group_10_DPF.png\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-image:url(https://enbifrn.stripocdn.email/content/guids/CABINET_7550686899481ac1ae35908cede0c283/images/group_10_DPF.png);background-repeat:no-repeat;background-position:center top;background-color:#12022F\" role=\"none\"><tr>\r\n<td valign=\"top\" style=\"padding:0;Margin:0\"><table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\"><tr><td class=\"es-m-p15r es-m-p15l\" align=\"center\" style=\"padding:0;Margin:0\"><table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:640px\"><tr><td class=\"es-m-p30t es-m-p30b es-m-p20r es-m-p20l\" align=\"left\" bgcolor=\"#ffffff\" style=\"padding:40px;Margin:0;background-color:#ffffff;border-radius:20px 20px 0px 0px\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"><tr>\r\n<td align=\"left\" style=\"padding:0;Margin:0;width:560px\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\"><tr><td align=\"center\" style=\"padding:0;Margin:0\"><h1 style=\"Margin:0;line-height:43px;mso-line-height-rule:exactly;font-family:'Exo 2', sans-serif;font-size:36px;font-style:normal;font-weight:bold;color:#000000\">Xác nhận thông tin đăng nhập&nbsp;</h1> </td></tr><tr><td align=\"center\" style=\"padding:0;Margin:0;padding-top:20px;font-size:0px\"><img class=\"adapt-img\" src=\"https://enbifrn.stripocdn.email/content/guids/CABINET_158d1ee0d3579aa1456484b5bebda300/images/following_2.gif\" alt style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\" width=\"560\" height=\"420\"></td></tr><tr>\r\n<td align=\"center\" style=\"padding:0;Margin:0;padding-top:30px\"><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:'Exo 2', sans-serif;line-height:27px;color:#666666;font-size:18px\">Xin chào người dùng: " + txtTenNV.Text + "</p></td></tr><tr><td><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:'Exo 2', sans-serif;line-height:27px;text-align: center;color:#666666;font-size:18px\">Tên tài khoản truy cập:" + txtTaiKhoan.Text + "</p></td></tr> <tr><td align=\"center\" style=\"padding:0;Margin:0;padding-top:5px\"><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:'Exo 2', sans-serif;line-height:27px;color:#666666;font-size:18px\">Mật khẩu truy cập vào hệ thống:";
            string from, pass, Messageboxbody;
            Random random = new Random();
            MailMessage mess = new MailMessage();
            to = txtEmail.Text.ToString();
            string sql = "select matkhau_nv from nhanvien where manv = '" + txtMaNV.Text + "' ";
            code = LopHamXuLy.GetFieldValues(sql).Trim().ToString();
            from = "";
            pass = "";
            Messageboxbody = a + code + b;
            mess.To.Add(to);
            mess.From = new MailAddress(from);
            mess.IsBodyHtml = true;
            mess.Body = Messageboxbody;
            mess.Subject = "Thông Báo: Xác nhận thông tin đăng nhập";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);
            try
            {
                smtp.Send(mess);
                MessageBox.Show("Bạn đã gửi yêu cầu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtgvNV_Click_1(object sender, EventArgs e)
        {
            if (dtgvNV.CurrentRow.Cells["Manv"].Value.ToString() == "")
            {

                MessageBox.Show("Hãy chọn vào vùng có dữ liệu !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (btnSua.Enabled == false)
            {

                MessageBox.Show("Bạn đang ở chế độ thêm !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
            txtMaNV.Text = dtgvNV.CurrentRow.Cells["MaNV"].Value.ToString();
            txtTenNV.Text = dtgvNV.CurrentRow.Cells["TenNV"].Value.ToString();
            txtNoiSinh.Text = dtgvNV.CurrentRow.Cells["NoiSinh_NV"].Value.ToString();
            txtEmail.Text = dtgvNV.CurrentRow.Cells["Email_nv"].Value.ToString();
            txtTaiKhoan.Text = dtgvNV.CurrentRow.Cells["TaiKhoan_nv"].Value.ToString();
            txtSDT.Text = dtgvNV.CurrentRow.Cells["sdt_nv"].Value.ToString();
            txtNhom.Text = dtgvNV.CurrentRow.Cells["nhom_nv"].Value.ToString();
            txtDiaChi.Text = dtgvNV.CurrentRow.Cells["DiaChi_nv"].Value.ToString();
            piktime.Text = dtgvNV.CurrentRow.Cells["NgaySinh_Nv"].Value.ToString();
            if (dtgvNV.CurrentRow.Cells["GioiTinh_NV"].Value.ToString() == "Nam") chkcheck.Checked = true;
            else chkcheck.Checked = false;
            if (dtgvNV.CurrentRow.Cells["Anh_NV"].Value.ToString() == "") pbAnhNV.Image = Image.FromFile(paths + "\\Images\\Images_nv\\anh_avarta.jpg");
            else
                pbAnhNV.Image = Image.FromFile(paths + dtgvNV.CurrentRow.Cells["Anh_NV"].Value.ToString());


        }
    }
}
