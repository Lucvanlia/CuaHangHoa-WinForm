using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace CuaHangHoa
{
    public partial class frmPhanQuyen : Form
    {
        public frmPhanQuyen()
        {
            InitializeComponent();
            txtMaPQ.Enabled = false;
            ShowPhanQuyen();
            LopHamXuLy.Connect();
            string sql = "select MANV,TENNV from NhanVien  nv where MaNV not in (select MANV from NhanVien  nv where nv.Nhom_nv=N'admin' )";
            LopHamXuLy.FillComBo(sql, cbonv, "MaNV", "TenNV");
            sql = "select * from ChucNang";
            LopHamXuLy.FillComBo(sql, cbocn, "idcn", "TenCN");
            cbonv.SelectedIndex = -1;
            cbocn.SelectedIndex = -1;
        }
        private void ShowPhanQuyen()
        {
            DataTable dtPQ = new DataTable();
            try
            {
                LopHamXuLy.Connect();
                string sql = "SELECT p.idpq,nv.tennv as tennv , cn.tencn   FROM phanquyen p inner join chucnang cn on p.idcn = cn.idcn inner join nhanvien nv on nv.manv = p.manv";
                if (LopHamXuLy.TruyVan(sql, dtPQ))
                {
                    dtgvLoaiSP.DataSource = dtPQ;
                    dtgvLoaiSP.Columns[0].HeaderText = "Mã chức năng";
                    dtgvLoaiSP.Columns[0].Width = 100;
                    dtgvLoaiSP.Columns[1].HeaderText = "Tên nhân viên";
                    dtgvLoaiSP.Columns[2].HeaderText = "Tên chức năng";
                    dtgvLoaiSP.Refresh();
                    //this.dtgvLoaiSP.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    this.dtgvLoaiSP.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    //this.dtgvLoaiSP.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    dtgvLoaiSP.Columns[0].DefaultCellStyle.Font = new Font("nunito", 12, FontStyle.Bold);
                    dtgvLoaiSP.Columns[1].DefaultCellStyle.Font = new Font("nunito", 12, FontStyle.Bold);
                    dtgvLoaiSP.Columns[2].DefaultCellStyle.Font = new Font("nunito", 12, FontStyle.Bold);


                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmPhanQuyen_Load(object sender, EventArgs e)
        {
            if(btnThem.Enabled == false)
            {
                string sql = "select * from ChucNang";
                LopHamXuLy.FillComBo(sql, cbocn, "idcn", "TenCN");
            }    
            cbocn.Enabled = false;
            cbonv.Enabled = false;
            cbophu.Enabled = false;
          
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
          
          
        }

        private void lblMa_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            cbonv.SelectedIndex = -1;
            cbocn.SelectedIndex = -1;
            txtMaPQ.ResetText();
            txtMaPQ.Text = "";
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            cbocn.Enabled = true;
            cbonv.Enabled = true;
            cbophu.Enabled = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaPQ.Text = "";
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            txtMaPQ.ResetText();
            cbophu.Enabled = false;
            cbonv.SelectedIndex = -1;
            cbocn.SelectedIndex = -1;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (cbocn.Text == "" || cbonv.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn đủ thông tin !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; 
            }
            else
            {
                LopHamXuLy.Connect();
                string sqlDelete = "DELETE FROM phanquyen WHERE idpq = '" + txtMaPQ.Text + "' ";
                if (MessageBox.Show("Bạn có chắc muốn xóa phân quyền nhân viên này không ?", "Xác nhận xóa !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                            LopHamXuLy.RunSQL(sqlDelete);
                            MessageBox.Show("Đã xóa thành công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtMaPQ.ResetText();
                                cbonv.Text = "";
                                cbocn.Text = "";
                                cbophu.Text = "";
                        cbonv.SelectedIndex = -1;
                            cbocn.SelectedIndex = -1;
                             cbophu.SelectedIndex = -1;
                                ShowPhanQuyen();

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
            if (dtgvLoaiSP.CurrentRow.Cells["idpq"].Value.ToString() == "")
            {
                MessageBox.Show("Chọn vào vùng có dữ liệu!", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(btnSua.Enabled == false)
            {
                MessageBox.Show("Bạn đang ở chế độ thêm!", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    
            if(btnThem.Enabled  == false)
            {
                MessageBox.Show("Bạn đang ở chế độ Sửa!", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    
            else
            {
                txtMaPQ.Text = dtgvLoaiSP.CurrentRow.Cells["idpq"].Value.ToString(); ;
                string sql = "SELECT nv.tennv as tennv   FROM phanquyen p inner join chucnang cn on p.idcn = cn.idcn inner join nhanvien nv on nv.manv = p.manv  where idpq = '"+txtMaPQ.Text+"'";
                cbonv.Text = LopHamXuLy.GetFieldValues(sql);
                sql = "SELECT cn.tencn as tencn   FROM phanquyen p inner join chucnang cn on p.idcn = cn.idcn inner join nhanvien nv on nv.manv = p.manv  where idpq = '" + txtMaPQ.Text + "'";
                cbocn.Text = LopHamXuLy.GetFieldValues(sql);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == true)
            {
                string sqlcheck = "select * from phanquyen where idcn =N'" + cbocn.SelectedValue.ToString() + "' and  manv = '" + cbonv.SelectedValue.ToString() + "'";
                if (LopHamXuLy.CheckKey(sqlcheck))
                {
                    MessageBox.Show("Nhân viên này đã có chức năng !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ThemLoaiSanPham();
                }
            }
            else
            {
                SuaLoaiSanPham();
            }
            cbonv.SelectedIndex = -1;
            cbocn.SelectedIndex = -1;
        }
        private void SuaLoaiSanPham()
        {
            if (cbocn.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn  chức năng !");
                return; 
            }
            if (cbonv.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn nhân viên !");
                return;

            }
            else
            {
                string sqlUpdate = "UPDATE PhanQuyen SET  idcn =N'" + cbophu.SelectedValue.ToString() + "' , manv = '" + cbonv.SelectedValue.ToString() + "'  WHERE idpq = '" + txtMaPQ.Text + "'";
                LopHamXuLy.Connect();
                if (MessageBox.Show("Bạn có muốn nhân chức năng nhân viên này?", "Xác nhận sửa !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        LopHamXuLy.RunSQL(sqlUpdate);
                        MessageBox.Show("Đã Sửa Thành Công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbonv.SelectedIndex = -1;
                        cbocn.SelectedIndex = -1;
                        btnThem.Enabled = true;
                        btnXoa.Enabled = true;
                        txtMaPQ.ResetText();
                        ShowPhanQuyen();
                        cbocn.Enabled = false;
                        cbonv.Enabled = false;
                        cbophu.Enabled = false;

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

            if (cbocn.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn  chức năng !");
                return;
            }
            if (cbonv.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn nhân viên !");
                return;

            }
            else
            {
                LopHamXuLy.Connect();
                string sqlInsert = "INSERT INTO PhanQuyen (idcn,manv) VALUES ( N'" + cbocn.SelectedValue.ToString() + "' ,  N'" + cbonv.SelectedValue.ToString() + "') ";
                if (MessageBox.Show("Bạn có muốn quyền cho nhân viên ", "Xác nhận thêm !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        LopHamXuLy.RunSQL(sqlInsert);
                        MessageBox.Show("Đã Thêm Thành Công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowPhanQuyen();
                        txtMaPQ.ResetText();
                        cbonv.SelectedIndex = -1;
                        cbocn.SelectedIndex = -1;
                        btnXoa.Enabled = true;
                        btnSua.Enabled = true;
                        cbocn.Enabled = false;
                        cbonv.Enabled = false;
                        cbophu.Enabled = false;
                    }
                    catch (Exception Loi)
                    {
                        MessageBox.Show(Loi.Message);
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(cbonv.Text=="")
            {
                MessageBox.Show("Vui lòng chọn nhân viên để phân quyền !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                cbocn.Enabled = false;
                cbonv.Enabled = true;
                cbophu.Enabled = true;
                string sql = "select *  from ChucNang  where ChucNang.idcn not in (	select phanquyen.idcn from PhanQuyen  where PhanQuyen.MANV = N'" + cbonv.SelectedValue.ToString() + "' )";
                LopHamXuLy.FillComBo(sql, cbophu, "idcn", "TenCN");
            }
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            DataTable dtPQ = new DataTable();
            LopHamXuLy.Connect();
            string sql = "SELECT p.idpq,nv.tennv as tennv , cn.tencn   FROM phanquyen p inner join chucnang cn on p.idcn = cn.idcn inner join nhanvien nv on nv.manv = p.manv where nv.tennv like '%"+txtTim.Text+"%' ";
            if (LopHamXuLy.TruyVan(sql, dtPQ))
            {
                dtgvLoaiSP.DataSource = dtPQ;
                dtgvLoaiSP.Columns[0].HeaderText = "Mã chức năng";
                dtgvLoaiSP.Columns[1].HeaderText = "Tên chức năng";
                dtgvLoaiSP.Refresh();

            }
        }

        private void cbonv_TextChanged(object sender, EventArgs e)
        {
            if (cbocn.Text == "" && cbonv.Text == "")
            {
                chkLoc.Enabled = false;
            }
            else
            {
                chkLoc.Enabled = true;

            }
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
         
            if (chkLoc.Checked == true && cbonv.Text != "")
            {
                DataTable dtPQ = new DataTable();
                LopHamXuLy.Connect();
                string sql = "SELECT p.idpq,nv.tennv as tennv , cn.tencn   FROM phanquyen p inner join chucnang cn on p.idcn = cn.idcn inner join nhanvien nv on nv.manv = p.manv where p.manv = '" + cbonv.SelectedValue.ToString() + "'";
                if (LopHamXuLy.TruyVan(sql, dtPQ))
                {
                    dtgvLoaiSP.DataSource = dtPQ;
                    dtgvLoaiSP.Columns[0].HeaderText = "Mã chức năng";
                    dtgvLoaiSP.Columns[1].HeaderText = "Tên chức năng";
                    dtgvLoaiSP.Refresh();

                }
            }
          if(chkLoc.Checked == false)
            {
                LopHamXuLy.Connect();
                DataTable dtPQ = new DataTable();
                string sql = "SELECT p.idpq,nv.tennv as tennv , cn.tencn   FROM phanquyen p inner join chucnang cn on p.idcn = cn.idcn inner join nhanvien nv on nv.manv = p.manv";
                if (LopHamXuLy.TruyVan(sql, dtPQ))
                {
                    dtgvLoaiSP.DataSource = dtPQ;
                    dtgvLoaiSP.Columns[0].HeaderText = "Mã chức năng";
                    dtgvLoaiSP.Columns[1].HeaderText = "Tên chức năng";
                    dtgvLoaiSP.Refresh();

                }
            }
        }
    }
}
