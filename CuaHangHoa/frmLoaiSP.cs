using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace CuaHangHoa
{
    public partial class frmLoaiSP : Form
    {
        public frmLoaiSP()
        {
            InitializeComponent();
            txtMaLSP.Enabled = false;
            txtTenLSP.Enabled = false;
            ShowLoaiSanPham(); 
        }
        private void Reset()
        {
            txtMaLSP.ResetText();
            txtTenLSP.ResetText();
        }
        private void frmLoaiSanPham_Load(object sender, EventArgs e)
        {

        }

        private void pnTimKiem_Paint(object sender, PaintEventArgs e)
        {

        }
        private void ShowLoaiSanPham()
        {
            DataTable dtLSP = new DataTable();
            LopHamXuLy.Connect();
            string sql = "SELECT * FROM LoaiSanPham";
            if (LopHamXuLy.TruyVan(sql, dtLSP))
            {
                dtgvLoaiSP.DataSource = dtLSP;
                dtgvLoaiSP.Columns[0].HeaderText = "Mã Loại Sản Phẩm";
                dtgvLoaiSP.Columns[1].HeaderText = "Tên Loại Sản Phẩm";
                dtgvLoaiSP.Refresh();

            }
        }
        private void txtThem_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            Reset();
            string sql = string.Format("SELECT CONCAT('LSP' ,RIGHT(CONCAT('0000000',ISNULL(right(max(MaLSP),7),0)+1),7)) from LoaiSanPham where MaLSP like 'LSP%'");
            txtTenLSP.Focus();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            LopHamXuLy.Connect();
            SqlCommand cmd = new SqlCommand(sql, LopHamXuLy.conn);
            var code = cmd.ExecuteScalar();
            string a = code.ToString();
            txtMaLSP.Text = a;
            txtTenLSP.Enabled = true;
            txtTenLSP.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            Reset();
            txtTenLSP.Enabled = true;
            txtTenLSP.Focus();
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaLSP.Text = "";
            txtTenLSP.Text = "";
            Reset();
            txtTenLSP.Focus();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (txtMaLSP.Text == "" || txtTenLSP.Text=="")
            {
                MessageBox.Show("Bạn chưa chọn Mã Loại Sản Phẩm !","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                LopHamXuLy.Connect();
                string sqlcheck = "select * from hoa , loaisanpham where hoa.malsp = '" + txtMaLSP.Text + "'";
                string sqlDelete = "DELETE FROM LoaiSanPham WHERE MaLSP = '" + txtMaLSP.Text + "'";
                if (MessageBox.Show("Bạn có chắc muốn xóa loại sản phẩm này không ?", "Xác nhận xóa !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                {
                    try
                    {
                        if(LopHamXuLy.CheckKey(sqlcheck))
                        {
                          MessageBox.Show("Có có sản phẩm tồn tại kiểu dáng này !", "Thông báo !", MessageBoxButtons.OK,MessageBoxIcon.Warning) ;
                            return;
                        }
                      else
                        {
                            LopHamXuLy.RunSQL(sqlDelete);
                            MessageBox.Show("Đã xóa thành công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowLoaiSanPham();
                            Reset();
                            txtTenLSP.Enabled = false;
                        }    
                    }
                    catch (Exception Loi)
                    {
                        MessageBox.Show(Loi.Message);
                    }
                }
            }
        }

        private void dtgvLoaiSP_Click(object sender, EventArgs e)
        {
            if (dtgvLoaiSP.CurrentRow.Cells["MaLSP"].Value.ToString() == "")
            {
                MessageBox.Show("Hãy chọn vào vùng có dữ liệu !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (btnSua.Enabled == false)
            {
                MessageBox.Show("Bạn đang ở chế độ thêm !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            txtMaLSP.Text = dtgvLoaiSP.CurrentRow.Cells["MaLSP"].Value.ToString();
            txtTenLSP.Text = dtgvLoaiSP.CurrentRow.Cells["TenLSP"].Value.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == true)
            {
                string sqlcheck = "select * from LoaiSanPham where TenLSP =N'" + txtTenLSP.Text + "'";
                if (LopHamXuLy.CheckKey(sqlcheck))
                {
                    MessageBox.Show("Tên của bạn đã trùng !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ThemLoaiSanPham();
                    btnLuu.Enabled = false;
                }
            }
            else
            {
                SuaLoaiSanPham();
                btnLuu.Enabled = false;

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtTenLSP.Enabled = true;
            txtTenLSP.SelectAll();
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
        }
        private void SuaLoaiSanPham()
        {
            if (txtMaLSP.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn Mã Loại Sản Phẩm !");
            }
            if (txtTenLSP.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn Tên Loại Sản Phẩm !");
            }
            else
            {
                string sqlUpdate = "UPDATE LoaiSanPham SET TenLSP = N'" + txtTenLSP.Text + "', MaLSP ='" + txtMaLSP.Text + "'WHERE MaLSP = '" + txtMaLSP.Text + "'";
                LopHamXuLy.Connect();
                if (MessageBox.Show("Bạn có muốn sửa lại chất liệu này ?", "Xác nhận sửa !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        LopHamXuLy.RunSQL(sqlUpdate);
                        MessageBox.Show("Đã Sửa Thành Công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowLoaiSanPham();
                        Reset();
                        btnThem.Enabled = true;
                        btnXoa.Enabled = true;
                        btnLuu.Enabled = true;
                    }
                    catch (Exception Loi)
                    {
                        MessageBox.Show(Loi.Message);
                    }
                }
            }
        }
        private void ThemLoaiSanPham()
        {
            if (txtMaLSP.Text == "")
            {
                MessageBox.Show("Mã Chất Liệu trống !");
                txtMaLSP.Focus();
            }
            if (txtTenLSP.Text == "")
            {
                MessageBox.Show("Tên Chất Liệu trống !");
                txtTenLSP.Focus();
            }
            else
            {
                LopHamXuLy.Connect();
                string sqlInsert = "INSERT INTO LoaiSanPham (MaLSP, TenLSP) VALUES ('" + txtMaLSP.Text + "' ,N'" + txtTenLSP.Text + "') ";
                if (MessageBox.Show("Bạn có muốn thêm Loại sản phẩm này ?", "Xác nhận thêm !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        LopHamXuLy.RunSQL(sqlInsert);
                        MessageBox.Show("Đã Thêm Thành Công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowLoaiSanPham();
                        Reset();
                        btnXoa.Enabled = true;
                        btnSua.Enabled = true;
                        btnLuu.Enabled = true;

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
            DataTable dtLSP = new DataTable();
            LopHamXuLy.Connect();
            string sql = "SELECT * FROM LoaiSanPham where TenLSP like N'%" + txtTim.Text+"%'";
            if (LopHamXuLy.TruyVan(sql, dtLSP))
            {
                dtgvLoaiSP.DataSource = dtLSP;
                dtgvLoaiSP.Columns[0].HeaderText = "Mã Loại Sản Phẩm";
                dtgvLoaiSP.Columns[1].HeaderText = "Tên Loại Sản Phẩm";
                dtgvLoaiSP.Refresh();

            }
        }

        private void ctnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn Có Muốn Thoát không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==DialogResult.Yes)
            {
                this.Close();
            } 
                
        }

        private void ctnZoom_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal) { 
                WindowState =FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void guna2PictureBox6_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;

        }

        private void txtTenLSP_TextChanged(object sender, EventArgs e)
        {
            
             
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
