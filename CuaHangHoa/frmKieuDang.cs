using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuaHangHoa
{
    public partial class frmKieuDang : Form
    {
        public frmKieuDang()
        {
            InitializeComponent();
            txtMaKD.Enabled = false;
            txtTenKD.Enabled = false;
            txtMoTa.Enabled = false;
            ShowLoaiSanPham();
        }
        private void Reset()
        {
            txtMaKD.ResetText();
            txtTenKD.ResetText();
            txtMoTa.ResetText();
        }
        private void ShowLoaiSanPham()
        {
            DataTable dtLSP = new DataTable();
            LopHamXuLy.Connect();
            string sql = "SELECT * FROM KieuDang";
            if (LopHamXuLy.TruyVan(sql, dtLSP))
            {
                dtgvLoaiSP.DataSource = dtLSP;
                dtgvLoaiSP.Columns[0].HeaderText = "Mã kiểu dáng";
                dtgvLoaiSP.Columns[1].HeaderText = "Tên kiểu dáng";
                dtgvLoaiSP.Columns[2].HeaderText = "Mô tả kiểu dáng";
                dtgvLoaiSP.Refresh();
            }
        }
        private void lblMa_Click(object sender, EventArgs e)
        {

        }

        private void frmKieuDang_Load(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true; 
            Reset();
            string sql = string.Format("SELECT CONCAT('KDH' ,RIGHT(CONCAT('0000000',ISNULL(right(max(MaKD),7),0)+1),7)) from KieuDang where MaKD like 'KDH%'");
            txtTenKD.Focus();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            LopHamXuLy.Connect();
            SqlCommand cmd = new SqlCommand(sql, LopHamXuLy.conn);
            var code = cmd.ExecuteScalar();
            string a = code.ToString();
            txtMaKD.Text = a;
            txtTenKD.Enabled = true;
            txtMoTa.Enabled = true;
            txtTenKD.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaKD.Text == "" || txtTenKD.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn Mã Loại Sản Phẩm !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                LopHamXuLy.Connect();
                string sqlcheck = "select * from hoa , kieudang where hoa.makd = '" + txtMaKD.Text + "'";
                string sqlDelete = "DELETE FROM KieuDang WHERE MaKD = '" + txtMaKD.Text + "'";
                if (MessageBox.Show("Bạn có chắc muốn xóa loại sản phẩm này không ?", "Xác nhận xóa !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (LopHamXuLy.CheckKey(sqlcheck))
                        {
                            MessageBox.Show("Có có sản phẩm tồn tại kiểu dáng này !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            LopHamXuLy.RunSQL(sqlDelete);
                            MessageBox.Show("Đã xóa thành công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowLoaiSanPham();
                            Reset();
                            txtTenKD.Enabled = false;
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
           if(dtgvLoaiSP.CurrentRow.Cells["MaKD"].Value.ToString() =="")
            {
                MessageBox.Show("Chọn vào vùng có dữ liệu!", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           if(btnSua.Enabled == false )
            {
                MessageBox.Show("Bạn đang ở chế độ thêm", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    
            else
            {
                txtMaKD.Text = dtgvLoaiSP.CurrentRow.Cells["MaKD"].Value.ToString();
                txtTenKD.Text = dtgvLoaiSP.CurrentRow.Cells["TenKieuDang"].Value.ToString();
                txtMoTa.Text = dtgvLoaiSP.CurrentRow.Cells["MoTaKD"].Value.ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == true)
            {
                string sqlcheck = "select * from KieuDang where TenKieuDang =N'" + txtTenKD.Text + "'";
                if (LopHamXuLy.CheckKey(sqlcheck))
                {
                    MessageBox.Show("Tên của bạn đã trùng !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
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
            btnLuu.Enabled = true;
            txtTenKD.SelectAll();
            txtTenKD.Enabled = true;
            txtMoTa.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
        }
        private void SuaLoaiSanPham()
        {
            if (txtMaKD.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn Mã Loại Sản Phẩm !");
            }
            if (txtTenKD.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn Tên Loại Sản Phẩm !");
            }
            else
            {
                string sqlUpdate = "UPDATE KieuDang SET TenKieuDang = N'" + txtTenKD.Text + "', MaKD ='" + txtMaKD.Text + "' , MoTaKD = N'" + txtMoTa.Text + "' WHERE MaKD = '" + txtMaKD.Text + "'";
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
            if (txtMaKD.Text == "")
            {
                MessageBox.Show("Mã Chất Liệu trống !");
                txtMaKD.Focus();
            }
            if (txtTenKD.Text == "")
            {
                MessageBox.Show("Tên Chất Liệu trống !");
                txtTenKD.Focus();
            }
            else
            {
                LopHamXuLy.Connect();
                string sqlInsert = "INSERT INTO KieuDang (MaKD, TenKieuDang,MoTaKD) VALUES ('" + txtMaKD.Text + "' ,N'" + txtTenKD.Text + "',N'" + txtMoTa.Text + "') ";
                if (MessageBox.Show("Bạn có muốn thêm kiểu dáng  sản phẩm này ?", "Xác nhận thêm !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        LopHamXuLy.RunSQL(sqlInsert);
                        MessageBox.Show("Đã Thêm Thành Công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowLoaiSanPham();
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

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            DataTable dtLSP = new DataTable();
            LopHamXuLy.Connect();
            string sql = "SELECT * FROM KieuDang where TenKieuDang like N'%" + txtTim.Text + "%'";
            if (LopHamXuLy.TruyVan(sql, dtLSP))
            {
                dtgvLoaiSP.DataSource = dtLSP;
                dtgvLoaiSP.Columns[0].HeaderText = "Mã kiểu dáng";
                dtgvLoaiSP.Columns[1].HeaderText = "Tên  kiểu dáng";
                dtgvLoaiSP.Refresh();

            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            Reset();
        }
    }
}
