using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Management;
using System.Windows.Forms;

namespace CuaHangHoa
{
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }
        private void Bat()
        {
            txtTenKH.Enabled = true;
            txtNoiSinh.Enabled = true;
            txtEmail.Enabled = true;
            txtSDT.Enabled = true;
            txtDiaChi.Enabled = true;
        }
        private void Tat()
        {
            txtMaKH.Enabled = false;
            txtTenKH.Enabled = false;
            txtNoiSinh.Enabled = false;
            txtEmail.Enabled = false;
            txtSDT.Enabled = false;
            txtDiaChi.Enabled = false;
        }
        private void     Reset()
        {
            txtMaKH.ResetText();
            txtTenKH.ResetText();
            txtNoiSinh.ResetText();
            txtEmail.ResetText();
            txtSDT.ResetText();
            txtDiaChi.ResetText();
        }
        private Boolean CheckRong()
        {
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Mã nhân viên không được dể trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKH.Focus();
                return false;
            }
            if (txtTenKH.Text == "")
            {
                MessageBox.Show("Tên nhân viên không được dể trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKH.Focus();
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
            return true;
        }
        private void ShowKhachHang()
        {
            DataTable dtKH = new DataTable();
            LopHamXuLy.Connect();
            string sql = "SELECT MAKH,Ten_KH,gioitinh_KH,DiaChi_KH,sdt_KH,email_KH,ngaysinh_KH,NoiSinh_KH FROM KhachHang";
            if (LopHamXuLy.TruyVan(sql, dtKH))
            {
                dtgvKH.DataSource = dtKH;
                dtgvKH.Columns[0].HeaderText = "Mã khách hàng";
                dtgvKH.Columns[0].Width = 170;
                dtgvKH.Columns[1].HeaderText = "Tên khách hàng";
                dtgvKH.Columns[1].Width = 180;
                dtgvKH.Columns[2].HeaderText = "Giới tính";
                dtgvKH.Columns[3].HeaderText = "Địa chỉ";
                dtgvKH.Columns[4].HeaderText = "Số điện thoại";
                dtgvKH.Columns[4].Width = 150;
                dtgvKH.Columns[5].HeaderText = "Email";
                dtgvKH.Columns[6].HeaderText = "Ngày sinh";
                dtgvKH.Columns[7].HeaderText = "Nơi Sinh";
                dtgvKH.AllowUserToAddRows = false;
                this.dtgvKH.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dtgvKH.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dtgvKH.Refresh();
            }
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            Tat();
            ShowKhachHang();
            this.WindowState = FormWindowState.Maximized;
            btnLuu.Enabled = false;
        }

        private void dtgvKH_Click(object sender, EventArgs e)
        {
            if (dtgvKH.CurrentRow.Cells["MaKH"].Value.ToString() == "")
            {
                MessageBox.Show("Vui lòng chọn vào vùng có dữ liệu !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (btnSua.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm không thể chọn  !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }    
            txtMaKH.Text = dtgvKH.CurrentRow.Cells["MaKH"].Value.ToString();
            txtTenKH.Text = dtgvKH.CurrentRow.Cells["Ten_KH"].Value.ToString();
            txtNoiSinh.Text = dtgvKH.CurrentRow.Cells["NoiSinh_KH"].Value.ToString();
            txtEmail.Text = dtgvKH.CurrentRow.Cells["Email_KH"].Value.ToString();
            txtSDT.Text = dtgvKH.CurrentRow.Cells["sdt_KH"].Value.ToString();
            txtDiaChi.Text = dtgvKH.CurrentRow.Cells["DiaChi_KH"].Value.ToString();
            piktime.Text = dtgvKH.CurrentRow.Cells["NgaySinh_KH"].Value.ToString();
            if (dtgvKH.CurrentRow.Cells["GioiTinh_KH"].Value.ToString() == "Nam") chkcheck.Checked = true;
            else chkcheck.Checked = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == true)
            {
                ThemKhachHang();
            }
            else
            {
                SuaKhachHang();
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Bat();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            Reset();
            LopHamXuLy.Connect();
            string sql = string.Format("SELECT CONCAT('MKH' ,RIGHT(CONCAT('0000000',ISNULL(right(max(MaKH),7),0)+1),7)) from KhachHang where MaKH like 'MKH%'");
            SqlCommand cmd = new SqlCommand(sql, LopHamXuLy.conn);
            var code = cmd.ExecuteScalar();
            string a = code.ToString();
            txtMaKH.Text = a;
            txtMaKH.Enabled = false;
            txtTenKH.Focus();
            btnLuu.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Bat();
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
            txtMaKH.Enabled = false;
            txtTenKH.Focus();
            btnLuu.Enabled = true;
        }
        public void ThemKhachHang()
        {
            string gt;
            Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (chkcheck.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";
            if (CheckRong())
            {
                if (reg.IsMatch(txtEmail.Text))
                {
                    string check_sdt = "select sdt_kh from khachhang where sdt_kh = '"+txtSDT.Text+"' ";
                    string check_diachi = "select diachi_kh from khachhang where diachi_kh = '" + txtDiaChi.Text + "' ";
                    string check_email = "select email_kh from khachhang where email_kh = '" + txtEmail.Text + "' ";

                    string sqlInsert = "INSERT INTO KhachHang (MaKH,Ten_KH,NoiSinh_KH,sdt_KH,email_KH,DiaChi_KH,Ngaysinh_KH,gioitinh_KH) values(N'" + txtMaKH.Text + "',N'" + txtTenKH.Text + "',N'" + txtNoiSinh.Text + "',N'" + txtSDT.Text + "',N'" + txtEmail.Text + "',N'" + txtDiaChi.Text + "',N'" + piktime.Value.ToString("yyyy-MM-dd") + "',N'" + gt + "') ";
                    if (MessageBox.Show("Bạn có muốn thêm khách hàng này ?", "Xác nhận thêm !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            LopHamXuLy.Connect();
                            if(LopHamXuLy.CheckKey(check_sdt))
                            {
                                MessageBox.Show("Số điện thoại này đã tồn tại người đăng ký !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (LopHamXuLy.CheckKey(check_diachi))
                            {
                                MessageBox.Show("Địa chỉi này đã tồn tại người đăng ký !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (LopHamXuLy.CheckKey(check_email))
                            {
                                MessageBox.Show("Email này đã tồn tại người đăng ký !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            LopHamXuLy.RunSQL(sqlInsert);
                            MessageBox.Show("Đã Thêm Thành Công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowKhachHang();
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
        public void SuaKhachHang()
        {
            string gt;
            Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (chkcheck.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";
            if (CheckRong())
            {
                if (reg.IsMatch(txtEmail.Text))
                {
                    string sqlUpdate = "update KhachHang set MaKH = N'" + txtMaKH.Text + "',Ten_KH = N'" + txtTenKH.Text + "',NoiSinh_KH = N'" + txtNoiSinh.Text + "',sdt_KH = N'" + txtSDT.Text + "',email_KH = N'" + txtEmail.Text + "' ,DiaChi_KH = N'" + txtDiaChi.Text + "',Ngaysinh_KH = N'" + piktime.Value.ToString("yyyy-MM-dd") + "', gioitinh_KH = N'" + gt + "'  where makh = N'" + txtMaKH.Text + "'  " ;
                    if (MessageBox.Show("Bạn có muốn sửa khách hàng này ?", "Xác nhận thêm !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            LopHamXuLy.Connect();
                            LopHamXuLy.RunSQL(sqlUpdate);
                            MessageBox.Show("Đã Sửa Thành Công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowKhachHang();
                            Reset();
                            btnXoa.Enabled = true;
                            btnSua.Enabled = true;
                            btnThem.Enabled = true;
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

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string sqlDelete = "DELETE FROM KhachHang WHERE MaKH = '" + txtMaKH.Text + "'";
                string sqlcheck = "Select * from hoadonban , nhanvien where hoadonban.makh = '" + txtMaKH.Text + "'";
                if (MessageBox.Show("Bạn có chắc muốn xóa khách hàng này không ?", "Xác nhận xóa !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        LopHamXuLy.Connect();
                        if (LopHamXuLy.CheckKey(sqlcheck))
                        {
                            MessageBox.Show("Khách hàng đã tồn tại trong hóa đơn không thể xóa !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        LopHamXuLy.RunSQL(sqlDelete);
                        MessageBox.Show("Đã xóa thành công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowKhachHang();
                        Reset();
                    }
                    catch (Exception Loi)
                    {
                        MessageBox.Show(Loi.Message);
                    }
                }
            }
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            DataTable dtKH = new DataTable();
            LopHamXuLy.Connect();
            string sql = "SELECT MAKH,Ten_KH,gioitinh_KH,DiaChi_KH,sdt_KH,email_KH,ngaysinh_KH,NoiSinh_KH FROM KhachHang where ten_kh like N'%"+txtTim.Text+"%'";
            if (LopHamXuLy.TruyVan(sql, dtKH))
            {
                dtgvKH.DataSource = dtKH;
                dtgvKH.Columns[0].HeaderText = "Mã khách hàng";
                dtgvKH.Columns[0].Width = 170;
                dtgvKH.Columns[1].HeaderText = "Tên khách hàng";
                dtgvKH.Columns[1].Width = 180;
                dtgvKH.Columns[2].HeaderText = "Giới tính";
                dtgvKH.Columns[3].HeaderText = "Địa chỉ";
                dtgvKH.Columns[4].HeaderText = "Số điện thoại";
                dtgvKH.Columns[4].Width = 150;
                dtgvKH.Columns[5].HeaderText = "Email";
                dtgvKH.Columns[6].HeaderText = "Ngày sinh";
                dtgvKH.Columns[7].HeaderText = "Nơi Sinh";
                dtgvKH.AllowUserToAddRows = false;
                this.dtgvKH.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dtgvKH.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dtgvKH.Refresh();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Reset();
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chknam.Checked == false || chknu.Checked == false || chkmua.Checked == false || chkchuamua.Checked == false)
                ShowKhachHang();
            if(chknam.Checked == true )
            {
                chknu.Checked = false;
                chkmua.Checked = false;
                chkchuamua.Checked = false;
                DataTable dtKH = new DataTable();
                LopHamXuLy.Connect();
                string sql = "SELECT MAKH,Ten_KH,gioitinh_KH,DiaChi_KH,sdt_KH,email_KH,ngaysinh_KH,NoiSinh_KH FROM KhachHang where gioitinh_KH = N'nam'";
                if (LopHamXuLy.TruyVan(sql, dtKH))
                {
                    dtgvKH.DataSource = dtKH;
                    dtgvKH.Columns[0].HeaderText = "Mã khách hàng";
                    dtgvKH.Columns[0].Width = 170;
                    dtgvKH.Columns[1].HeaderText = "Tên khách hàng";
                    dtgvKH.Columns[1].Width = 180;
                    dtgvKH.Columns[2].HeaderText = "Giới tính";
                    dtgvKH.Columns[3].HeaderText = "Địa chỉ";
                    dtgvKH.Columns[4].HeaderText = "Số điện thoại";
                    dtgvKH.Columns[4].Width = 150;
                    dtgvKH.Columns[5].HeaderText = "Email";
                    dtgvKH.Columns[6].HeaderText = "Ngày sinh";
                    dtgvKH.Columns[7].HeaderText = "Nơi Sinh";
                    dtgvKH.AllowUserToAddRows = false;
                    this.dtgvKH.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    this.dtgvKH.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    dtgvKH.Refresh();
                }
            }    
        }

        private void chknu_CheckedChanged(object sender, EventArgs e)
        {
            if (chknam.Checked == false || chknu.Checked == false || chkmua.Checked == false || chkchuamua.Checked == false)
                ShowKhachHang();
            if (chknu.Checked == true)
            {
                chknam.Checked = false;
                chkmua.Checked = false;
                chkchuamua.Checked = false;
                DataTable dtKH = new DataTable();
                LopHamXuLy.Connect();
                string sql = "SELECT MAKH,Ten_KH,gioitinh_KH,DiaChi_KH,sdt_KH,email_KH,ngaysinh_KH,NoiSinh_KH FROM KhachHang where gioitinh_KH = N'Nữ'";
                if (LopHamXuLy.TruyVan(sql, dtKH))
                {
                    dtgvKH.DataSource = dtKH;
                    dtgvKH.Columns[0].HeaderText = "Mã khách hàng";
                    dtgvKH.Columns[0].Width = 170;
                    dtgvKH.Columns[1].HeaderText = "Tên khách hàng";
                    dtgvKH.Columns[1].Width = 180;
                    dtgvKH.Columns[2].HeaderText = "Giới tính";
                    dtgvKH.Columns[3].HeaderText = "Địa chỉ";
                    dtgvKH.Columns[4].HeaderText = "Số điện thoại";
                    dtgvKH.Columns[4].Width = 150;
                    dtgvKH.Columns[5].HeaderText = "Email";
                    dtgvKH.Columns[6].HeaderText = "Ngày sinh";
                    dtgvKH.Columns[7].HeaderText = "Nơi Sinh";
                    dtgvKH.AllowUserToAddRows = false;
                    this.dtgvKH.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    this.dtgvKH.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    dtgvKH.Refresh();
                }
            }
            
        }

        private void chkmua_CheckedChanged(object sender, EventArgs e)
        {
            if (chknam.Checked == false || chknu.Checked == false || chkmua.Checked == false || chkchuamua.Checked == false)
                ShowKhachHang();
            if (chkmua.Checked == true)
            {
                chkchuamua.Checked = false;
                chknam.Checked = false;
                chknu.Checked = false;
                DataTable dtKH = new DataTable();
                LopHamXuLy.Connect();
                string sql = "select MAKH,Ten_KH,gioitinh_KH,DiaChi_KH,sdt_KH,email_KH,ngaysinh_KH,NoiSinh_KH\r\nfrom KhacHHang kh\r\nwhere kh.MaKH  in (\r\n\tselect hd.MaKH \r\n\tfrom HoaDonBan hd\r\n)";
                if (LopHamXuLy.TruyVan(sql, dtKH))
                {
                    dtgvKH.DataSource = dtKH;
                    dtgvKH.Columns[0].HeaderText = "Mã khách hàng";
                    dtgvKH.Columns[0].Width = 170;
                    dtgvKH.Columns[1].HeaderText = "Tên khách hàng";
                    dtgvKH.Columns[1].Width = 180;
                    dtgvKH.Columns[2].HeaderText = "Giới tính";
                    dtgvKH.Columns[3].HeaderText = "Địa chỉ";
                    dtgvKH.Columns[4].HeaderText = "Số điện thoại";
                    dtgvKH.Columns[4].Width = 150;
                    dtgvKH.Columns[5].HeaderText = "Email";
                    dtgvKH.Columns[6].HeaderText = "Ngày sinh";
                    dtgvKH.Columns[7].HeaderText = "Nơi Sinh";
                    dtgvKH.AllowUserToAddRows = false;
                    this.dtgvKH.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    this.dtgvKH.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    dtgvKH.Refresh();
                }
            }
        }

        private void chkchuamua_CheckedChanged(object sender, EventArgs e)
        {
            if (chknam.Checked == false || chknu.Checked == false || chkmua.Checked == false || chkchuamua.Checked == false)
                ShowKhachHang();
            if (chkchuamua.Checked == true)
            {
                chkmua.Checked = false;
                chknam.Checked = false;
                chknu.Checked = false;
                DataTable dtKH = new DataTable();
                LopHamXuLy.Connect();
                string sql = "select MAKH,Ten_KH,gioitinh_KH,DiaChi_KH,sdt_KH,email_KH,ngaysinh_KH,NoiSinh_KH\r\nfrom KhacHHang kh\r\nwhere kh.MaKH not   in (\r\n\tselect hd.MaKH \r\n\tfrom HoaDonBan hd\r\n)";
                if (LopHamXuLy.TruyVan(sql, dtKH))
                {
                    dtgvKH.DataSource = dtKH;
                    dtgvKH.Columns[0].HeaderText = "Mã khách hàng";
                    dtgvKH.Columns[0].Width = 170;
                    dtgvKH.Columns[1].HeaderText = "Tên khách hàng";
                    dtgvKH.Columns[1].Width = 180;
                    dtgvKH.Columns[2].HeaderText = "Giới tính";
                    dtgvKH.Columns[3].HeaderText = "Địa chỉ";
                    dtgvKH.Columns[4].HeaderText = "Số điện thoại";
                    dtgvKH.Columns[4].Width = 150;
                    dtgvKH.Columns[5].HeaderText = "Email";
                    dtgvKH.Columns[6].HeaderText = "Ngày sinh";
                    dtgvKH.Columns[7].HeaderText = "Nơi Sinh";
                    dtgvKH.AllowUserToAddRows = false;
                    this.dtgvKH.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    this.dtgvKH.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    dtgvKH.Refresh();
                }
            }
        }
    }
    }


