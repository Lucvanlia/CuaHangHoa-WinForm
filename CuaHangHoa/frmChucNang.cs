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
    public partial class frmChucNang : Form
    {
        public frmChucNang()
        {
            InitializeComponent();
            txtMaLSP.Enabled = false;
            txtTenLSP.Enabled = false;
            ShowChucNang();
        }
        private void Reset()
        {
            txtMaLSP.Enabled = true;
            txtTenLSP.Enabled = true;
            txtMaLSP.ResetText();
            txtTenLSP.ResetText();
            txtMaLSP.Enabled = false;
            txtTenLSP.Enabled = false;
        }
        private void ShowChucNang()
        {
            DataTable dtLSP = new DataTable();
            LopHamXuLy.Connect();
            string sql = "SELECT * FROM ChucNang";
            if (LopHamXuLy.TruyVan(sql, dtLSP))
            {
                dtgvLoaiSP.DataSource = dtLSP;
                dtgvLoaiSP.Columns[0].HeaderText = "Mã chức năng";
                dtgvLoaiSP.Columns[0].Width = 105;
                dtgvLoaiSP.Columns[1].HeaderText = "Tên chức năng";
                dtgvLoaiSP.Refresh();
             //   this.dtgvLoaiSP.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //  this.dtgvLoaiSP.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //this.dtgvLoaiSP.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dtgvLoaiSP.Columns[0].DefaultCellStyle.Font = new Font("nunito", 12, FontStyle.Bold);
                dtgvLoaiSP.Columns[1].DefaultCellStyle.Font = new Font("nunito", 12, FontStyle.Bold);

            }
        }

        private void frmChucNang_Load(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Reset();
            txtTenLSP.Focus();
            txtMaLSP.ResetText();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            txtTenLSP.Enabled = true;
            txtTenLSP.Focus();
            btnLuu.Enabled = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {

            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            Reset();
            txtTenLSP.Enabled = true;
            txtTenLSP.Focus();
            btnLuu.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaLSP.Text == "" || txtTenLSP.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn Mã Loại chức năng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                LopHamXuLy.Connect();
                string sqlcheck = "select * from  phanquyen p  where p.idcn = '"+txtMaLSP.Text+"'";
                string sqlDelete = "DELETE FROM Chucnang WHERE idcn = '" + txtMaLSP.Text + "'";
                if (MessageBox.Show("Bạn có chắc muốn xóa loại chức năng này không ?", "Xác nhận xóa !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (LopHamXuLy.CheckKey(sqlcheck))
                        {
                            MessageBox.Show("Có có chức năng tồn tại chức năng này !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            LopHamXuLy.RunSQL(sqlDelete);
                            MessageBox.Show("Đã xóa thành công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowChucNang();
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
            if (dtgvLoaiSP.CurrentRow.Cells["idcn"].Value.ToString() == "")
            {
                MessageBox.Show("Chọn vào vùng có dữ liệu!", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                txtMaLSP.Text = dtgvLoaiSP.CurrentRow.Cells["idcn"].Value.ToString();
                txtTenLSP.Text = dtgvLoaiSP.CurrentRow.Cells["TenCN"].Value.ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == true)
            {

    
                    ThemChucNang();
                
            }
            else
            {
                SuaChucNang();
            }
            txtMaLSP.Enabled = false;
            txtTenLSP.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtTenLSP.SelectAll();
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            txtTenLSP.Enabled = true;
            btnLuu.Enabled = true;

        }
        private void SuaChucNang()
        {
            if (txtMaLSP.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn Mã Loại chức năng !");
            }
            if (txtTenLSP.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn Tên Loại chức năng !");
            }
            else
            {
                string sqlUpdate = "UPDATE ChucNang SET TenCN =N'" + txtTenLSP.Text + "' WHERE idcn = '" + txtMaLSP.Text + "'";
                LopHamXuLy.Connect();
                if (MessageBox.Show("Bạn có muốn sửa lại chất liệu này ?", "Xác nhận sửa !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        LopHamXuLy.RunSQL(sqlUpdate);
                        MessageBox.Show("Đã Sửa Thành Công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowChucNang();
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
        }
        private void ThemChucNang()
        {
     
            if (txtTenLSP.Text == "")
            {
                MessageBox.Show("Tên chức năng  không để trống!");
                txtTenLSP.Focus();
                return; 
            }
            else
            {
                LopHamXuLy.Connect();
                string sqlInsert = "INSERT INTO ChucNang ( TenCN) VALUES (N'" + txtTenLSP.Text + "') ";
                if (MessageBox.Show("Bạn có muốn thêm chức năng ", "Xác nhận thêm !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string sqlcheck = "select * from chucnang where TenCN =N'" + txtTenLSP.Text + "'";
                        if (LopHamXuLy.CheckKey(sqlcheck))
                        {
                            MessageBox.Show("Tên của bạn đã trùng !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        LopHamXuLy.RunSQL(sqlInsert);
                        MessageBox.Show("Đã Thêm Thành Công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowChucNang();
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
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            DataTable dtLSP = new DataTable();
            LopHamXuLy.Connect();
            string sql = "SELECT * FROM chucnang where tencn like N'%" + txtTim.Text + "%'";
            if (LopHamXuLy.TruyVan(sql, dtLSP))
            {
                dtgvLoaiSP.DataSource = dtLSP;
                dtgvLoaiSP.Columns[0].HeaderText = "Mã chức năng";
                dtgvLoaiSP.Columns[1].HeaderText = "Tên  chức năng";
                dtgvLoaiSP.Refresh();

            }
        }
    }

}
