using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.PeerToPeer;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace CuaHangHoa
{
    public partial class frmNCC : Form
    {
        public frmNCC()
        {
            InitializeComponent();
      

        }
        public void Reset()
        {
            txtMaNCC.ResetText();
            txtTenNCC.ResetText();
            txtDiaChi.ResetText();
            txtSDT.ResetText();
        }
        public Boolean CheckRong()
        {
            if (txtTenNCC.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNCC.Focus();
                return false;
            }
            if (txtMaNCC.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCC.Focus();
                return false;
            }
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return false;
            }
            if (txtSDT.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSDT.Focus();
                return false;
            }
            return true;
        }
        private void ShowNCC()
        {
            DataTable dtLSP = new DataTable();
            string sql = "SELECT * FROM NhaCungCap";
            try
            {
                LopHamXuLy.Connect();
                if (LopHamXuLy.TruyVan(sql, dtLSP))
                {
                    dtgvNCC.DataSource = dtLSP;
                    dtgvNCC.Columns[0].HeaderText = "Mã nhà cung cấp";
                    dtgvNCC.Columns[1].HeaderText = "Tên nhà cung cấp";
                    dtgvNCC.Columns[2].HeaderText = "Địa chỉ";
                    dtgvNCC.Columns[3].HeaderText = "Số điện thoại";

                    dtgvNCC.Refresh();

                }
                else
                {
                    dtgvNCC.DataSource = null; 
                }
                dtgvNCC.Refresh();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void lblMa_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            Reset();
            string sql = string.Format("SELECT CONCAT('NCC' ,RIGHT(CONCAT('0000000',ISNULL(right(max(MaNCC),7),0)+1),7)) from NhaCungCap where mancc  like 'NCC%'");
            txtTenNCC.Focus();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            LopHamXuLy.Connect();
            SqlCommand cmd = new SqlCommand(sql, LopHamXuLy.conn);
            var code = cmd.ExecuteScalar();
            string a = code.ToString();
            txtMaNCC.Text = a;
            txtTenNCC.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSDT.Enabled = true;
            txtTenNCC.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (CheckRong())
            {
                string sqlcheck = "select * from hoa , nhacungcap where hoa.mancc = '"+txtMaNCC.Text+"'";
                string sqlDelete = "DELETE FROM NhaCungCap WHERE MaNCC = '" + txtMaNCC.Text + "'";
                if (MessageBox.Show("Bạn có chắc muốn xóa nhà cung cấp này không ?", "Xác nhận xóa !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        LopHamXuLy.Connect();
                        if(LopHamXuLy.CheckKey(sqlcheck))
                        {
                            MessageBox.Show("Nhà cung cấp này hiện đã có sản phẩm không thể xóa","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            LopHamXuLy.RunSQL(sqlDelete);
                            MessageBox.Show("Đã xóa thành công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowNCC();
                            Reset();
                            txtMaNCC.Enabled = false;
                            txtTenNCC.Enabled = false;
                            txtDiaChi.Enabled = false;
                            txtSDT.Enabled = false;
                        }
                    }//try
                    catch (Exception Loi)
                    {
                        MessageBox.Show(Loi.Message);
                    }//catch
                }//ifdk
            }//ifcheckrong
        }

        private void dtgvNCC_Click(object sender, EventArgs e)
        {

                    txtMaNCC.Text = dtgvNCC.CurrentRow.Cells["MANCC"].Value.ToString();
                    txtTenNCC.Text = dtgvNCC.CurrentRow.Cells["TenNCC"].Value.ToString();
                    txtDiaChi.Text = dtgvNCC.CurrentRow.Cells["DiaChiNCC"].Value.ToString();
                    txtSDT.Text = dtgvNCC.CurrentRow.Cells["SDTNCC"].Value.ToString();       
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            txtTenNCC.Focus();
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
            txtTenNCC.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSDT.Enabled = true;
            txtTenNCC.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == true)
            {
                string sqlcheck = "select * from Nhacungcap where TenNCC =N'" + txtTenNCC.Text + "'";
                if (LopHamXuLy.CheckKey(sqlcheck))
                {
                    MessageBox.Show("Tên của bạn đã trùng !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ThemNCC();
                    btnLuu.Enabled = true;
                }
            }  //ktif nut them
            else
            {
                SuaNCC();
                btnLuu.Enabled = true;

            }
        }
        public void ThemNCC()
        {
            if (CheckRong())
            {
                LopHamXuLy.Connect();
                string sqlInsert = "INSERT INTO Nhacungcap (Mancc, TenNCC,DiachiNCC,SdTNCC) VALUES ('" + txtMaNCC.Text + "' ,N'" + txtTenNCC.Text + "',N'" + txtDiaChi.Text + "',N'" + txtSDT.Text + "') ";
                if (MessageBox.Show("Bạn có muốn thêm nhà cung cấp  này ?", "Xác nhận thêm !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        LopHamXuLy.RunSQL(sqlInsert);
                        MessageBox.Show("Đã Thêm Thành Công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowNCC();
                        Reset();
                        btnXoa.Enabled = true;
                        btnSua.Enabled = true;
                    }
                    catch (Exception Loi)
                    {
                        MessageBox.Show(Loi.Message);
                    }
                }
            }
        }
        public void SuaNCC()
        {
            if (CheckRong())
            {

                string sqlUpdate = "UPDATE NhaCungCap SET TenNCC =N'" + txtTenNCC.Text + "', MaNCC ='" + txtMaNCC.Text + "',DiaChiNCC =N'" + txtDiaChi.Text + "',sdtNCC = '" + txtSDT.Text + "' WHERE MaNCC = '" + txtMaNCC.Text + "'";
                LopHamXuLy.Connect();
                if (MessageBox.Show("Bạn có muốn sửa nhà cung cấp này ?", "Xác nhận sửa !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        LopHamXuLy.RunSQL(sqlUpdate);
                        MessageBox.Show("Đã Sửa Thành Công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowNCC();
                        Reset();
                        btnThem.Enabled = true;
                        btnXoa.Enabled = true;
                    }
                    catch (Exception Loi)
                    {
                        MessageBox.Show(Loi.Message);
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowNCC();
        }

        private void frmNCC_Load(object sender, EventArgs e)
        {
            txtMaNCC.Enabled = false;
            txtTenNCC.Enabled = false;
            txtDiaChi.Enabled = false;
            txtSDT.Enabled = false;
            ShowNCC();
            btnLuu.Enabled = false;
        }
       

        private void btnExcel_Click(object sender, EventArgs e)
        {
        
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void dtgvNCC_Click_1(object sender, EventArgs e)
        {
            if (dtgvNCC.CurrentRow.Cells["MaNCC"].Value.ToString() == "")
            {
                MessageBox.Show("Chọn vào vùng có dữ liệu!", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (btnSua.Enabled == false)
            {
                MessageBox.Show("Bạn đang ở chế độ thêm", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                txtMaNCC.Text = dtgvNCC.CurrentRow.Cells["MANCC"].Value.ToString();
                txtTenNCC.Text = dtgvNCC.CurrentRow.Cells["TenNCC"].Value.ToString();
                txtDiaChi.Text = dtgvNCC.CurrentRow.Cells["DiaChiNCC"].Value.ToString();
                txtSDT.Text = dtgvNCC.CurrentRow.Cells["SDTNCC"].Value.ToString();
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaNCC.Enabled = true;
            txtMaNCC.ResetText();
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            Reset();
            txtMaNCC.Enabled = false;
        }
    }
}
