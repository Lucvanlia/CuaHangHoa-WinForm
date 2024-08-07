using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.UI;
using COMExcel = Microsoft.Office.Interop.Excel;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms.VisualStyles;

namespace CuaHangHoa
{
    public partial class frmHoaDonBan2 : Form
    {
        public frmHoaDonBan2()
        {
            InitializeComponent();
        }
        public void ShowLuoiCTHD()
        {
            dtgvCTHD.Refresh();
            double tong = 0;
            DataTable dtLSP = new DataTable();
            try
            {
                LopHamXuLy.Connect();
                string sql = "select cthdb.MaHoa,h.TenHoa,cthdb.SoLuong,cthdb.KhuyenMai,h.giaban,cthdb.ThanhTien from CTHoaDonBan as cthdb , Hoa as h where cthdb.MaHoa  = h.MaHoa and cthdb.MAHDB = N'"+txtMaHD.Text+"'";
                if (LopHamXuLy.TruyVan(sql, dtLSP))
                {
                    dtgvCTHD.DataSource = dtLSP;
                    dtgvCTHD.Columns[0].HeaderText = "Mã Hoa";
                    dtgvCTHD.Columns[1].HeaderText = "Tên Hoa";
                    dtgvCTHD.Columns[2].HeaderText = "Số lượng";
                    dtgvCTHD.Columns[3].HeaderText = "Khuyến mãi";
                    dtgvCTHD.Columns[4].HeaderText = "Đơn giá bán";
                    dtgvCTHD.Columns[5].HeaderText = "Thành tiền";
                    dtgvCTHD.Columns[4].DefaultCellStyle.Format = "N0";
                    dtgvCTHD.Columns[4].DefaultCellStyle.Format = "N0";
                    dtgvCTHD.Columns[5].DefaultCellStyle.Format = "N0";
                     //================================================================
                    dtgvCTHD.Columns[0].DefaultCellStyle.Font = new Font("Nunito ", 10, FontStyle.Bold);
                    dtgvCTHD.Columns[1].DefaultCellStyle.Font = new Font("Nunito ", 10, FontStyle.Bold);
                    dtgvCTHD.Columns[2].DefaultCellStyle.Font = new Font("Nunito ", 10, FontStyle.Bold);
                    dtgvCTHD.Columns[3].DefaultCellStyle.Font = new Font("Nunito ", 10, FontStyle.Bold);
                    dtgvCTHD.Columns[4].DefaultCellStyle.Font = new Font("Nunito ", 10, FontStyle.Bold);
                    dtgvCTHD.Columns[5].DefaultCellStyle.Font = new Font("Nunito ", 10, FontStyle.Bold);
                    //================================================================
                    dtgvCTHD.ColumnHeadersDefaultCellStyle.Font = new Font(" Nunito ExtraBold", 12, FontStyle.Bold);
                    //================================================================
                    this.dtgvCTHD.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    this.dtgvCTHD.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    this.dtgvCTHD.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
              //      this.dtgvCTHD.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    //================================================================
                    if (dtgvCTHD.SelectedRows[0].Cells[0].Value.ToString() != "")
                    {
                        //MessageBox.Show(dtgvCTHD.Rows.Count.ToString());
                        for (int i = 0; i <= dtgvCTHD.Rows.Count - 2; i++)
                        {
                            if (dtgvCTHD[5, i].Value != DBNull.Value)
                            {
                                tong += double.Parse(dtgvCTHD.Rows[i].Cells[5].Value.ToString());
                                //  MessageBox.Show(tong.ToString());
                            }
                            dtgvCTHD.Rows[dtgvCTHD.Rows.Count - 1].DefaultCellStyle.Format = "N0";
                            dtgvCTHD[4, dtgvCTHD.Rows.Count - 1].Value = "Tổng tiền";
                            dtgvCTHD[5, dtgvCTHD.Rows.Count - 1].Value = tong;
                            dtgvCTHD.Columns[4].DefaultCellStyle.Format = "N0";
                            dtgvCTHD.Refresh();
                            this.dtgvCTHD.Rows[dtgvCTHD.Rows.Count - 1].DefaultCellStyle.Format = "N0";
                        }
                    }
                    dtgvCTHD.Refresh();
                }
                else
                {
                    dtgvCTHD.DataSource = null;
                }    
             
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmHoaDonBan2_Load(object sender, EventArgs e)
        {
            dtgvCTHD.Refresh();
            LopHamXuLy.Connect();
            LopHamXuLy.FillComBo("select MaHDB from HoaDonBan", cboHD, "MAHDB", "MAHDB");
            LopHamXuLy.FillComBo("select MAKH,Ten_KH from KhacHHang", cboMAKH, "MAKH", "MAKH");
            LopHamXuLy.FillComBo("select MAHOA from HOA", cboMAHOA, "MAHOA", "MAHOA");
            LopHamXuLy.FillComBo("select MANV from NhanVien", cboMANV, "MANV", "MANV");
            cboHD.SelectedIndex = -1;
            cboMAKH.SelectedIndex = -1;
            cboMAHOA.SelectedIndex = -1;
            cboMANV.SelectedIndex = -1;
            txtDonGia.ReadOnly = true;
            txtTenHoa.ReadOnly = true;
            txtTenKH.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtGiamGia.ReadOnly = false;
            txtSDT.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            if (txtMaHD.Text !="")
            {
                ShowThongTinChiTietHD();
            }    
            ShowLuoiCTHD();

        }
        public void ShowThongTinChiTietHD()
        {
            string sql;
            sql = "Select NgaylapHDB from hoadonban where MAHDB ='"+txtMaHD.Text+"'";
            piktime.Value = DateTime.Parse(LopHamXuLy.GetFieldValues(sql));
            piktime.Value.ToString("yyyy-MM-dd");
            sql = "Select MANV from hoadonban where MAHDB ='" + txtMaHD.Text + "'";
            cboMANV.Text = LopHamXuLy.GetFieldValues(sql);
            sql = "Select MAKH from hoadonban where MAHDB ='" + txtMaHD.Text + "'";
            cboMAKH.Text = LopHamXuLy.GetFieldValues(sql);
            sql = "Select Tongtien from hoadonban where MAHDB ='" + txtMaHD.Text + "'";
            txtTongTien.Text =  LopHamXuLy.GetFieldValues(sql);
            //sql = "Select MaHoa from CTHoaDonBan as ct , hoadonban as h where ct.MAHDB = h.MAHDB and ct.MAHDB ='" + txtMaHD.Text + "'";
            //cboMAHOA.Text = LopHamXuLy.GetFieldValues(sql);
            lblBangChu.Text = "Bằng chữ" ;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnLuu.Enabled = true;
            Reset();
            string sql = string.Format("SELECT CONCAT('HDB' ,RIGHT(CONCAT('000000',ISNULL(right(max(MaHDB),6),0)+1),6)) from HoadonBan where MAHDB like '%HDB%'");
            btnXoa.Enabled = false;
            btnXoaChiTiet.Enabled = false;
            btnSua.Enabled = false;
            LopHamXuLy.Connect();
            SqlCommand cmd = new SqlCommand(sql, LopHamXuLy.conn);
            var code = cmd.ExecuteScalar();
            string a = code.ToString();
            string tgian = LopHamXuLy.CreateKey("TG");
            txtMaHD.Text = tgian+ "-" + a   ;
            ShowLuoiCTHD();
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if(cboMAHOA.Text =="")
            {
                MessageBox.Show("Bạn vui lòng chọn sản phẩm  ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                tgsCbo.Enabled = true;
                btnXoa.Enabled = false;
                btnThem.Enabled = false;
                btnLuu.Enabled = false;
                btnLuuCT.Enabled = true;
                txtGiamGia.ReadOnly = false;
                cboMAHOA.Enabled = false;
  
            }    
        }

        public void Reset()
        {
            txtMaHD.ResetText();
            cboHD.SelectedIndex = -1;
            cboMAKH.SelectedIndex = -1;
            cboMAHOA.SelectedIndex = -1;
            cboMANV.SelectedIndex = -1;
            txtSoLuong.ResetText();
            txtGiamGia.ResetText();
            txtTongTien.ResetText();
            txtTenKH.ResetText();
            txtTongTien.ResetText();
            txtDiaChi.ResetText();
            txtSDT.ResetText();
            txtTenNV.ResetText();
            txtTenHoa.ResetText();
            piktime.Value =DateTime.Now;
            cboMAHOA.SelectedItem = null;
            dtgvCTHD.Columns.Clear();
            cboMANV.SelectedItem=null;
            cboMAHOA.SelectedItem=null;
        }

        private void cboMANV_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cboMAKH_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void cboMAKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMAKH.Text == "")
            {
                txtTenKH.Text = "";
                txtDiaChi.Text = "";
                txtSDT.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str = "Select Ten_KH from  Khachhang where MAKH = N'" + cboMAKH.SelectedValue + "'";
            txtTenKH.Text = LopHamXuLy.GetFieldValues(str);
            str = "Select DiaChi_KH from  Khachhang where MAKH = N'" + cboMAKH.SelectedValue + "'";
            txtDiaChi.Text = LopHamXuLy.GetFieldValues(str);
            str = "Select sdt_KH from  Khachhang where MAKH= N'" + cboMAKH.SelectedValue + "'";
            txtSDT.Text = LopHamXuLy.GetFieldValues(str);
        }

        private void cboMANV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql;
            if (cboMANV.Text == "")
                txtTenNV.Text = "";
            sql = "select TenNV from NhanVien where MaNV = N'" + cboMANV.SelectedValue + "' ";
            txtTenNV.Text = LopHamXuLy.GetFieldValues(sql);
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
           
                ThemHoaDon(); 
            
           
        }
        public Boolean CheckRongHD()
        {
            if(cboMANV.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập nhân viên","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                cboMANV.Focus();
                return false;
            }
            if (cboMAKH.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMAKH.Focus();
                return false;
            }
            if (cboMAHOA.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hoa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMAHOA.Focus();
                return false;
            }
            if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Focus();
                return false;
            }
            if ((txtGiamGia.Text.Trim().Length == 0) || (txtSoLuong.Text == ""))
            {
                MessageBox.Show("Bạn phải nhập giá giảm sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiamGia.Focus();
                return false;
            }
            return true;
        }
        public Boolean CheckRongCTHD()
        {
            if (cboMAHOA.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hoa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMAHOA.Focus();
                return false;
            }
            if((txtSoLuong.Text.Trim().Length == 0) ||(txtSoLuong.Text == "0") )
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Focus();
                return false;
            }
            if ((txtGiamGia.Text.Trim().Length == 0) || (txtSoLuong.Text == ""))
            {
                MessageBox.Show("Bạn phải nhập giá giảm sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiamGia.Focus();
                return false;
            }

            return true;
        }
        public Boolean CheckSoLuong(double sl)
        {
            if (Convert.ToSingle(txtSoLuong.Text) > sl)
            {
                MessageBox.Show(string.Format("Số lượng hoa trong kho {0} không đủ đáp ứng. Vui lòng nhập lại",sl), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Focus();
                return false;
            }
            return true;
        }

        private void cboHD_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (cboHD.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboHD.Focus();
                return;
            }
            txtMaHD.Text = cboHD.Text;
            ShowThongTinChiTietHD();
            ShowLuoiCTHD();
            btnXoa.Enabled = true;
            btnLuu.Enabled = true;
        }
        public void ThemHoaDon()
        {
            string sql, sqlInsert, sqlcheck, sqlUpDate;
            float sl, SLcon, tong, TongMoi;
            if (CheckRongHD())
            {
                sql = "Select * from Hoadonban where MAHDB = '" + txtMaHD.Text + "'";
                if (!LopHamXuLy.CheckKey(sql))
                {
                    LopHamXuLy.Connect();
                    sqlInsert = "INSERT INTO HOADONBAN (MAHDB,MANV,MAKH,NGAYLAPHDB,TONGTIEN,TrangThaiHDB) values (N'" + txtMaHD.Text.Trim() + "',N'" + cboMANV.SelectedValue.ToString() + "',N'" + cboMAKH.SelectedValue.ToString() + "',N'" + piktime.Value.ToString("yyyy-MM-dd hh:mm:ss") + "',N'" + txtTongTien.Text.Trim() + "',N'Đã bán' ) ";
                    try
                    {
                        LopHamXuLy.Connect();
                        LopHamXuLy.RunSQL(sqlInsert);
                        LopHamXuLy.FillComBo("select MaHDB from HoaDonBan", cboHD, "MAHDB", "MAHDB");
                    }
                    catch (Exception Loi)
                    {
                        MessageBox.Show(Loi.Message + "Loi ở dây hóa đơn");

                    }
                        sqlcheck = "SELECT MAHOA FROM CTHoaDonBan WHERE MAHOA =N'" + cboMAHOA.SelectedValue.ToString() + "' AND MAHDB = '" + txtMaHD.Text.Trim() + "' ";
                        if (!LopHamXuLy.CheckKey(sqlcheck))
                        {
                            // LÀM KT SỐ LƯỢNG ROI` MIK LÀM TIẾP
                            sl = Convert.ToSingle(LopHamXuLy.GetFieldValues("SELECT SOLUONG FROM HOA WHERE MAHOA = N'" + cboMAHOA.SelectedValue.ToString()+ "'"));
                            if (CheckSoLuong(sl))
                            {
                                 sqlInsert = "INSERT INTO CTHoaDonBan (MAHDB,MAHOA,SOLUONG,KHUYENMAI,THANHTIEN,DonGiaBan) VALUES ( N'" + txtMaHD.Text.Trim().ToString() + "',N'" + cboMAHOA.SelectedValue.ToString() + "',N'" + txtSoLuong.Text.Trim().ToString() + "',N'" + txtGiamGia.Text.Trim().ToString() + "',N'" + txtThanhTien.Text.Trim().ToString() + "' ,N'" + txtDonGia.Text.Trim().ToString() + "') ";
                            try
                            {
                                LopHamXuLy.Connect();
                                LopHamXuLy.RunSQL(sqlInsert);
                            }
                            catch (Exception loi)
                            {
                                MessageBox.Show(loi.Message);
                            }
                            SLcon = (float)(sl - Convert.ToDouble(txtSoLuong.Text));

                            sqlUpDate = "UPDATE HOA SET SOLUONG =  '" + SLcon + "'  WHERE MAHOA = N'" + cboMAHOA.SelectedValue.ToString() + "' ";
                                    try
                                    {
                                        LopHamXuLy.Connect();
                                        LopHamXuLy.RunSQL(sqlUpDate);
                                    
                                    }
                                    catch (Exception Loi)
                                    {
                                        MessageBox.Show("Loi ở dây số lượng", "THong báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        MessageBox.Show(Loi.Message);

                                    }
                                    tong = Convert.ToSingle(LopHamXuLy.GetFieldValues("SELECT TongTien FROM HoaDonBan WHERE MAHDB = N'" + txtMaHD.Text + "'"));
                                    TongMoi = tong + Convert.ToSingle(txtThanhTien.Text);
                                    sql = "UPDATE HoaDonBan SET TongTien =" + TongMoi + " WHERE MAHDB = N'" + txtMaHD.Text + "'";
                                    ResetChiTiet();
                                    ShowLuoiCTHD();
                                    txtMaHD.Text = txtMaHD.Text.Trim().ToString();
                                    txtTenHoa.ResetText();
                                    btnXoa.Enabled = true;
                                    btnThem.Enabled = true;
                                    btnSua.Enabled = true;
                                    btnLuu.Enabled = true;
                                    btnXoaChiTiet.Enabled = true;
                                    btnLuuCT.Enabled = false;
                                    txtTongTien.Text = TongMoi.ToString();
                        }//Check số lượng

                    } //kt trung san pham
                    else
                    {
                     MessageBox.Show("Sản phẩm đã tồn tại trong hóa đơn không thể thêm được lại chỉ cập nhật số lượng ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     return;

                    }

                }// check chi tiết hóa đơn
                else
                {
                    sqlcheck = "SELECT MAHOA FROM CTHoaDonBan WHERE MAHOA =N'" + cboMAHOA.SelectedValue.ToString() + "' AND MAHDB = '" + txtMaHD.Text.Trim() + "' ";
                    if (!LopHamXuLy.CheckKey(sqlcheck))
                    {
                        // LÀM KT SỐ LƯỢNG ROI` MIK LÀM TIẾP
                        sl = Convert.ToSingle(LopHamXuLy.GetFieldValues("SELECT SOLUONG FROM HOA WHERE MAHOA = N'" + cboMAHOA.SelectedValue.ToString() + "'"));
                        if (CheckSoLuong(sl))
                        {
                            sqlInsert = "INSERT INTO CTHoaDonBan (MAHDB,MAHOA,SOLUONG,KHUYENMAI,THANHTIEN,dongiaban) VALUES ( N'" + txtMaHD.Text.Trim().ToString() + "',N'" + cboMAHOA.SelectedValue.ToString() + "',N'" + txtSoLuong.Text.Trim().ToString() + "',N'" + txtGiamGia.Text.Trim().ToString() + "',N'" + txtThanhTien.Text.Trim().ToString() + "' ,N'" + txtDonGia.Text.Trim().ToString() + "' ) ";
                            try
                            {
                                LopHamXuLy.Connect();
                                LopHamXuLy.RunSQL(sqlInsert);
                            }
                            catch (Exception loi)
                            {
                                MessageBox.Show(loi.Message);
                            }
                            SLcon = (float)(sl - Convert.ToDouble(txtSoLuong.Text));

                            sqlUpDate = "UPDATE HOA SET SOLUONG =  '" + SLcon + "'  WHERE MAHOA = N'" + cboMAHOA.SelectedValue.ToString() + "' ";
                            try
                            {
                                LopHamXuLy.Connect();
                                LopHamXuLy.RunSQL(sqlUpDate);

                            }
                            catch (Exception Loi)
                            {
                                MessageBox.Show("Loi ở dây số lượng", "THong báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                MessageBox.Show(Loi.Message);

                            }
                            tong = Convert.ToSingle(LopHamXuLy.GetFieldValues("SELECT TongTien FROM hoadonban WHERE MAHDB = N'" + txtMaHD.Text + "'"));
                            TongMoi = tong + Convert.ToSingle(txtThanhTien.Text);
                            sql = "UPDATE hoadonban SET TongTien =" + TongMoi + " WHERE MAHDB = N'" + txtMaHD.Text + "'";
                            ResetChiTiet();
                            ShowLuoiCTHD();
                            txtMaHD.Text = txtMaHD.Text.Trim().ToString();
                            txtTenHoa.ResetText();
                            btnXoa.Enabled = true;
                            btnThem.Enabled = true;
                            btnSua.Enabled = true;
                            btnLuu.Enabled = true;
                            btnXoaChiTiet.Enabled = true;
                            txtTongTien.Text = TongMoi.ToString();
                            btnLuuCT.Enabled = true;

                        }


                    } //trung san pham 
                    else
                    {
                    
                            sl = Convert.ToSingle(LopHamXuLy.GetFieldValues("SELECT SOLUONG FROM HOA WHERE MAHOA = N'" + cboMAHOA.SelectedValue.ToString() + "'"));
                            float thanhtien = Convert.ToSingle(LopHamXuLy.GetFieldValues("SELECT thanhtien FROM CTHoaDonBan WHERE MAHOA = N'" + cboMAHOA.SelectedValue.ToString() + "' and MAHDB = N'" + txtMaHD.Text + "' "));
          
                            float slsp = Convert.ToSingle(LopHamXuLy.GetFieldValues("SELECT SOLUONG FROM CTHoaDonBan WHERE MAHOA = N'" + cboMAHOA.SelectedValue.ToString() + "' and MAHDB = N'" + txtMaHD.Text + "' "));

                            SLcon = (float)(sl - Convert.ToDouble(txtSoLuong.Text));
                            thanhtien = (float)(thanhtien + Convert.ToDouble(txtThanhTien.Text));

                            float SLcon2 = (float)(slsp + Convert.ToDouble(txtSoLuong.Text));
                            sqlUpDate = "UPDATE HOA SET SOLUONG =  '" + SLcon + "'  WHERE MAHOA = N'" + cboMAHOA.SelectedValue.ToString() + "' ";
                            string sqlUpDate2 = "UPDATE CTHoaDonBan SET SOLUONG =  '" + SLcon2 + "' , thanhtien =  N'" + thanhtien + "' WHERE MAHOA = N'" + cboMAHOA.SelectedValue.ToString() + "' and  MAHDB = N'" + txtMaHD.Text + "' ";

                            try
                            {
                                LopHamXuLy.Connect();
                                LopHamXuLy.RunSQL(sqlUpDate);
                                LopHamXuLy.RunSQL(sqlUpDate2);

                                ResetChiTiet();
                                ShowLuoiCTHD();
                            }
                            catch (Exception Loi)
                            {
                                MessageBox.Show("Loi ở dây số lượng", "THong báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                MessageBox.Show(Loi.Message);

                            }
                        string str;
                        str = "select SUM(thanhtien) as Tong from CTHoaDonBan ct  where ct.MAHDB ='" + txtMaHD.Text + "' group by ct.MAHDB";
                        if (txtMaHD.Text == "")
                        {
                            txtTongTien.Text = "";
                            lblBangChu.Text = "Bàng chữ";
                        }
                        else
                        {
                            txtTongTien.Text = LopHamXuLy.GetFieldValues(str).ToString();


                            lblBangChu.Text = "Bằng chữ: " + LopHamXuLy.ChuyenSoSangChuoi(double.Parse(txtTongTien.Text));
                            string sqlUpDate1 = "UPDATE HoadonBan SET TongTien =  '" + txtTongTien.Text + "'   where MAHDB ='" + txtMaHD.Text + "'";
                            try
                            {
                                LopHamXuLy.Connect();
                                LopHamXuLy.RunSQL(sqlUpDate1);

                            }
                            catch (Exception Loi)
                            {
                                MessageBox.Show("Loi ở tong tien", "THong báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                MessageBox.Show(Loi.Message);

                            }
                        }


                    }

                }

            }  // check rỗng

                }  //end hamd

        private void cboHD_SelectedValueChanged(object sender, EventArgs e)
        {
           // txtMaHD.Text = cboHD.Text;
            //ShowThongTinChiTietHD();
            //ShowLuoiCTHD();
            //btnXoa.Enabled = true;
            //btnLuu.Enabled = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
          
        }
        public void ResetChiTiet()
        {
            cboMAHOA.ResetText();
            cboMAHOA.Text = "";
            cboMAHOA.SelectedIndex = -1;
            txtTenHoa.ResetText();
            txtDonGia.ResetText();
            txtSoLuong.ResetText();
            txtGiamGia.ResetText();
            txtThanhTien.ResetText();
            cboMAHOA.SelectedItem = null;

        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

       
        private void txtGiamGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
           
        }

        private void cboMAHOA_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMAHOA.Text == "")
            {
                txtSoLuong.Text = "";
                txtDonGia.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            else
            {
                str = "Select GiaBan from  Hoa where MAHOA = N'" + cboMAHOA.SelectedValue + "'";
                txtDonGia.Text =  LopHamXuLy.GetFieldValues(str);
                str = "Select TenHoa from  Hoa where MAHOA = N'" + cboMAHOA.SelectedValue + "'";
                txtTenHoa.Text = LopHamXuLy.GetFieldValues(str);
         
            }
        }

        private void pnLeft_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            double tt, sl, dg, gg;
            int check;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - (sl * dg * gg / 100);
            if (gg > 100)
            {
                txtDonGia.Text = "100";
                gg = 100;
            }
            Int32.TryParse(txtGiamGia.Text, out check);
            if (check < 0 && txtGiamGia.Text != "")
            {
                txtGiamGia.ResetText();

            }
            else
                 if (check > 100 && txtGiamGia.Text != "")
            {
                txtGiamGia.ResetText();
                txtThanhTien.ResetText();
                MessageBox.Show("Giảm giá chỉ nhập từ 1-100 bạn đã nhập sai định dạng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            txtThanhTien.Text = tt.ToString();
        }
        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            double tt, sl, dg, gg;
            int check;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            Int32.TryParse(txtGiamGia.Text, out check);
            if (check < 0 && txtGiamGia.Text != "")
            {
                txtGiamGia.ResetText();

            }
            else
                if (check > 100 && txtGiamGia.Text != "")
            {
                txtGiamGia.ResetText();
                txtThanhTien.ResetText();
                MessageBox.Show("Giảm giá chỉ nhập từ 1-100 bạn đã nhập sai định dạng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            txtThanhTien.Text = tt.ToString();
        }
       
        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {
            string str;
            str = "select SUM(thanhtien) as Tong from CTHoaDonBan ct  where ct.MAHDB ='"+txtMaHD.Text+"' group by ct.MAHDB";
            if (txtMaHD.Text == "")
            {
                txtTongTien.Text = "";
                lblBangChu.Text = "Bàng chữ";
            }
            else
            {
                if (LopHamXuLy.GetFieldValues(str).ToString() == "") txtTongTien.Text = "0"; 
                else
                txtTongTien.Text = LopHamXuLy.GetFieldValues(str).ToString();
                lblBangChu.Text = "Bằng chữ: " + LopHamXuLy.ChuyenSoSangChuoi(double.Parse(txtTongTien.Text));
                string sqlUpDate = "UPDATE HoadonBan SET TongTien =  '" + txtTongTien.Text + "'   where MAHDB ='" + txtMaHD.Text + "'";
                try
                {
                    LopHamXuLy.Connect();
                    LopHamXuLy.RunSQL(sqlUpDate);

                }
                catch (Exception Loi)
                {
                    MessageBox.Show("Loi ở tong tien", "THong báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MessageBox.Show(Loi.Message);

                }
            }
        }

        private void lblBangChu_Click(object sender, EventArgs e)
        {
          
        }

        private void lblBangChu_TextChanged(object sender, EventArgs e)
        {
            string str;
            str = "select SUM(thanhtien) as Tong from CTHoaDonBan ct  where ct.MAHDB ='" + txtMaHD.Text + "' group by ct.MAHDB";
            if (txtMaHD.Text == "")
            {
                txtTongTien.Text = "";
                lblBangChu.Text = "Bàng chữ";
            }
            else
            {
                txtTongTien.Text = LopHamXuLy.GetFieldValues(str).ToString();
                string sqlUpDate = "UPDATE HoadonBan SET TongTien =  '" + txtTongTien.Text + "'   where MAHDB ='" + txtMaHD.Text + "'";
                try
                {
                    LopHamXuLy.Connect();
                    LopHamXuLy.RunSQL(sqlUpDate);

                }
                catch (Exception Loi)
                {
                    MessageBox.Show("Loi ở tong tien", "THong báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MessageBox.Show(Loi.Message);

                }
                lblBangChu.Text = "Bằng chữ: " + LopHamXuLy.ChuyenSoSangChuoi(double.Parse(txtTongTien.Text));
            }
            
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (txtMaHD.Text != "")
            {
                SqlDataAdapter da;
                DataTable dt = new DataTable();
                string strSQL = "INHDMAHD_MAHD";
                string thamso1 = "'" + cboMAKH.SelectedValue.ToString() + "'";
                string thamso2 = ",'" + txtMaHD.Text + "'";
                da = new SqlDataAdapter(strSQL + thamso1 + thamso2, LopHamXuLy.conn);
                da.Fill(dt);
                rptInHD r = new rptInHD();
                TextObject bangchu = (TextObject)r.Section4.ReportObjects["txtBangChu"];
                bangchu.Text = LopHamXuLy.ChuyenSoSangChuoi(double.Parse(txtTongTien.Text));
                r.SetDataSource(dt);
                frmShowRP_INHDB f = new frmShowRP_INHDB();
                f.crystalReportViewer1.ReportSource = r;
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để in ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            }
        string mahoa ;
        private void dtgvCTHD_Click(object sender, EventArgs e)
        {
            if (dtgvCTHD.Rows.Count == 0)
            {
                MessageBox.Show("Hãy chọn vào vùng có dữu liệu !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboHD.Focus();
                ShowLuoiCTHD();
                return;

            }
            else
            {

               
                if (dtgvCTHD.SelectedRows[0].Cells[0].Value.ToString() == "" )
                {
                    MessageBox.Show("Hãy chọn vào vùng có dữu liệu !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboHD.Focus();
                    return;
                }
                else
                {
                    double a;
                    //==================================================================

                    a = double.Parse(dtgvCTHD.SelectedRows[0].Cells[2].Value.ToString());
                    txtSoLuong.Text = a.ToString();
                    a = double.Parse(dtgvCTHD.SelectedRows[0].Cells[3].Value.ToString());
                    txtGiamGia.Text = a.ToString();
                    a = double.Parse(dtgvCTHD.SelectedRows[0].Cells[4].Value.ToString());
                    txtThanhTien.Text = a.ToString();

                    //==================================================================
                    cboMAHOA.Text = dtgvCTHD.SelectedRows[0].Cells[0].Value.ToString();
                    txtTenHoa.Text = dtgvCTHD.SelectedRows[0].Cells[1].Value.ToString();
                    mahoa = dtgvCTHD.SelectedRows[0].Cells[0].Value.ToString();
                }

            }

        }

        private void btnXoaChiTiet_Click(object sender, EventArgs e)
        {
            if (CheckRongCTHD())
            {
              double  sl = Convert.ToSingle(LopHamXuLy.GetFieldValues("SELECT SOLUONG FROM HOA WHERE MAHOA = N'" + cboMAHOA.SelectedValue.ToString() + "'"));
                double SLcon = (float)(sl + Convert.ToDouble(txtSoLuong.Text));
                string sqlDelete = "delete CTHoaDonBan where MAHDB = '" + txtMaHD.Text + "' and MaHoa = '" + cboMAHOA.SelectedValue.ToString() + "'";
               double tong = Convert.ToSingle(LopHamXuLy.GetFieldValues("SELECT TongTien FROM HoaDonBan WHERE MAHDB = N'" + txtMaHD.Text + "'"));
                double TongMoi = tong + Convert.ToSingle(txtThanhTien.Text);
                string sql = "UPDATE HoaDonBan SET TongTien =" + TongMoi + " WHERE MAHDB = N'" + txtMaHD.Text + "'";
               string  sqlUpDate_SL = "UPDATE HOA SET SOLUONG =  '" + SLcon + "'  WHERE MAHOA = N'" + cboMAHOA.SelectedValue.ToString() + "' ";

                if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm  này trong hóa đơn không ?", "Xác nhận xóa !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        LopHamXuLy.Connect();
                        LopHamXuLy.RunSQL(sqlUpDate_SL);
                        LopHamXuLy.RunSQL(sql);
                        LopHamXuLy.RunSQL(sqlDelete);
                        MessageBox.Show("Đã xóa thành công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetChiTiet();
                        dtgvCTHD.DataSource = null;
                        dtgvCTHD.Refresh();
                        sql = "Select Tongtien from hoadonnhap where MAHDN ='" + txtMaHD.Text + "'";
                        txtTongTien.Text = LopHamXuLy.GetFieldValues(sql);

                        ShowLuoiCTHD();

                    }//try
                    catch (Exception Loi)
                    {
                        MessageBox.Show("Lỗi xóa chi tiết" + Loi.Message);
                    }//catch
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaHD.Text != "")
            {
                string sqlDelete = "delete HoaDonBan where MAHDB = '" + txtMaHD.Text + "'";
                string sqlDelete_CT = "delete CTHoaDonBan where MAHDB = '" + txtMaHD.Text + "'";
                if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm  này trong hóa đơn không ?", "Xác nhận xóa !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        LopHamXuLy.Connect();
                        LopHamXuLy.RunSQL(sqlDelete_CT);
                        LopHamXuLy.RunSQL(sqlDelete);
                        MessageBox.Show("Đã xóa thành công !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetChiTiet();
                        Reset();
                        dtgvCTHD.DataSource = null;
                        LopHamXuLy.FillComBo("select MAHDB from hoadonban", cboHD, "MAHDB", "MAHDB");
                        cboHD.SelectedIndex = -1;

                    }//try
                    catch (Exception Loi)
                    {
                        MessageBox.Show(Loi.Message);
                    }//catch
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xóa !", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; 
            }
        }

        private void btnLuuCT_Click(object sender, EventArgs e)
        {
           if(btnThem.Enabled  == false)
            {
                SuaHD();
            }    
        }
        public void SuaHD()
        {
            string sqlUpdate;
            //float sl, SLcon, tong, TongMoi;

            // b1 check rong ~ thag` hd
            if (txtMaHD.Text != "" || cboMAKH.Text != "" ||txtSoLuong.Text !="" || txtGiamGia.Text !="")
            {
                // sua thag hoa don 
                // truy sửa

                sqlUpdate = "update HoaDonBan set MANV = N'" + cboMANV.SelectedValue.ToString() + "'  ,MAKH = N'" + cboMAKH.SelectedValue.ToString() + "',TongTien= N'" + txtTongTien.Text.Trim() + "',TrangThaiHDB=N'Đã Bán' ,NGAYLAPHDB = N'" + piktime.Value.ToString("yyyy-MM-dd hh:mm:ss") + "' where MAHDB = N'" + txtMaHD.Text.Trim() + "' ";
                // thực thi truy vấn 
                string sqlUpdatect = "Update CTHoaDonBan set  SoLuong =  N'"+txtSoLuong.Text+"'  , DonGiaBan = N'"+txtDonGia.Text+"' ,KhuyenMai = N'"+txtGiamGia.Text+"',ThanhTien = N'"+txtThanhTien.Text+"'   where MaHoa = N'"+cboMAHOA.SelectedValue.ToString()+"'  and MAHDB = N'"+txtMaHD.Text+"' "; 
                try
                {
                    LopHamXuLy.Connect();
                    double tong = Convert.ToSingle(LopHamXuLy.GetFieldValues("SELECT TongTien FROM HoaDonBan WHERE MAHDB = N'" + txtMaHD.Text + "'"));
                    double TongMoi = tong + Convert.ToSingle(txtThanhTien.Text);
                    string sql = "UPDATE HoaDonBan SET TongTien =" + TongMoi + " WHERE MAHDB = N'" + txtMaHD.Text + "'";
                    LopHamXuLy.RunSQL(sqlUpdate);
                    if(tgsCbo.Checked == true )
                    {
                        string sqlUpdatecthoamoi = "Update CTHoaDonBan set mahoa = '" + cboPhu.SelectedValue.ToString() + "',  SoLuong =  N'" + txtSoLuong.Text + "'  , DonGiaBan = N'" + txtDonGia.Text + "' ,KhuyenMai = N'" + txtGiamGia.Text + "',ThanhTien = N'" + txtThanhTien.Text + "'   where MaHoa = N'" + cboMAHOA.SelectedValue.ToString() + "'  and MAHDB = N'" + txtMaHD.Text + "' ";
                        LopHamXuLy.RunSQL(sqlUpdatecthoamoi);
                    }
                    else
                    {
                        LopHamXuLy.RunSQL(sqlUpdatect);
                    }
                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    cboPhu.Enabled = false;
                    btnXoa.Enabled = true;
                    btnXoaChiTiet.Enabled = true;
                    btnLuu.Enabled = false;
                    btnLuuCT.Enabled = false;
                    tgsCbo.Checked = false;
                    tgsCbo.Enabled = false;
                    cboPhu.Enabled = false;
                    ResetChiTiet();
                    sql = "Select Tongtien from hoadonnhap where MAHDN ='" + txtMaHD.Text + "'";
                    txtTongTien.Text = LopHamXuLy.GetFieldValues(sql);
                    ShowLuoiCTHD();

                }
                catch (Exception Loi)
                {
                    MessageBox.Show("Lỗi ở thêm hóa đơn" + Loi.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để cập nhật!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
         } 

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if(txtMaHD.Text !="")
            {
                COMExcel.Application exApp = new COMExcel.Application();
                COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
                COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
                COMExcel.Range exRange;
                string sql;
                int hang = 0, cot = 0;
                DataTable tblThongtinHD, tblThongtinHang;
                exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
                exSheet = exBook.Worksheets[1];
                // Định dạng chung
                exRange = exSheet.Cells[1, 1];
                exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
                exRange.Range["A1:B3"].Font.Size = 8;
                exRange.Range["A1:B3"].Font.Bold = true;
                exRange.Range["A1:B3"].Font.ColorIndex = 3; //Màu xanh da trời
                exRange.Range["A1:A1"].ColumnWidth = 7;
                exRange.Range["B1:B1"].ColumnWidth = 15;
                exRange.Range["A1:B1"].MergeCells = true;
                exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["A1:B1"].Value = "SHOP HOA THANH PHÁT";
                exRange.Range["A2:B2"].MergeCells = true;
                exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["A2:B2"].Value = "Địa chỉ: CTIM";
                exRange.Range["A3:B3"].MergeCells = true;
                exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["A3:B3"].Value = "Điện thoại:  0762.323.107";
                exRange.Range["C2:E2"].Font.Size = 16;
                exRange.Range["C2:E2"].Font.Bold = true;
                exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
                exRange.Range["C2:E2"].MergeCells = true;
                exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["C2:E2"].Value = "HÓA ĐƠN BÁN";
                // Biểu diễn thông tin chung của hóa đơn bán
                sql = "SELECT a.MaHDB, a.NgayLapHDB, a.TongTien, b.Ten_KH, b.DiaChi_KH, b.sdt_KH, c.Tennv FROM hoadonban AS a, Khachhang AS b, NhanVien AS c WHERE a.MaHDB = N'" + txtMaHD.Text + "' AND a.MaKH = b.MaKH AND a.MaNV = c.MaNV";
                tblThongtinHD = LopHamXuLy.GetDataToTable(sql);
                exRange.Range["B6:C9"].Font.Bold = true;
                exRange.Range["B6:C9"].Font.Size = 12;
                exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
                exRange.Range["C6:E6"].MergeCells = true;
                exRange.Range["C6:E6"].Value = tblThongtinHD.Rows[0][0].ToString();
                exRange.Range["B7:B7"].Value = "Khách hàng:";
                exRange.Range["C7:E7"].MergeCells = true;
                exRange.Range["C7:E7"].Value = tblThongtinHD.Rows[0][3].ToString();
                exRange.Range["B8:B8"].Value = "Địa chỉ:";
                exRange.Range["C8:F8"].MergeCells = true;
                exRange.Range["C8:E8"].Value = tblThongtinHD.Rows[0][4].ToString();
                exRange.Range["B9:B9"].Value = "Điện thoại:";
                exRange.Range["C9:E9"].MergeCells = true;
                exRange.Range["C9:E9"].Value = tblThongtinHD.Rows[0][5].ToString();
                //Lấy thông tin các mặt hàng
                sql = "SELECT b.TenHoa, a.SoLuong, b.GiaBan, a.KhuyenMai, a.ThanhTien " +
                      "FROM CTHoaDonBan AS a , Hoa AS b WHERE a.MaHDB = N'" +
                      txtMaHD.Text + "' AND a.MaHoa = b.MaHoa";
                tblThongtinHang = LopHamXuLy.GetDataToTable(sql);
                //Tạo dòng tiêu đề bảng
                exRange.Range["A11:F11"].Font.Bold = true;
                exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["C9:E9"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignLeft;
                exRange.Range["C11:F11"].ColumnWidth = 12;
                exRange.Range["A11:A11"].Value = "STT";
                exRange.Range["B11:B11"].Value = "Tên hàng";
                exRange.Range["C11:C11"].Value = "Số lượng";
                exRange.Range["D11:D11"].Value = "Đơn giá";
                exRange.Range["E11:E11"].Value = "Giảm giá";
                exRange.Range["F11:F11"].Value = "Thành tiền";
                for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++)
                {
                    //Điền số thứ tự vào cột 1 từ dòng 12
                    exSheet.Cells[1][hang + 12] = hang + 1;
                    for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                    //Điền thông tin hàng từ cột thứ 2, dòng 12
                    {
                        exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString();
                        if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString() + "%";
                    }
                }
                exRange = exSheet.Cells[cot][hang + 14];
                exRange.Font.Bold = true;
                exRange.Value2 = "Tổng tiền:";
                exRange = exSheet.Cells[cot + 1][hang + 14];
                exRange.Font.Bold = true;
                exRange.Value2 = tblThongtinHD.Rows[0][2].ToString();
                exRange = exSheet.Cells[1][hang + 15]; //Ô A1 
                exRange.Range["A1:F1"].MergeCells = true;
                exRange.Range["A1:F1"].Font.Bold = true;
                exRange.Range["A1:F1"].Font.Italic = true;
                exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
                exRange.Range["A1:F1"].Value = "Bằng chữ: " + LopHamXuLy.ChuyenSoSangChuoi(Convert.ToDouble((tblThongtinHD.Rows[0][2].ToString())));
                exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
                exRange.Range["A1:C1"].MergeCells = true;
                exRange.Range["A1:C1"].Font.Italic = true;
                exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                DateTime d = Convert.ToDateTime(tblThongtinHD.Rows[0][1]);
                exRange.Range["A1:C1"].Value = "Thành phố Hồ Chí Minh, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
                exRange.Range["A2:C2"].MergeCells = true;
                exRange.Range["A2:C2"].Font.Italic = true;
                exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["A2:C2"].Value = "Nhân viên bán hàng";
                exRange.Range["A6:C6"].MergeCells = true;
                exRange.Range["A6:C6"].Font.Italic = true;
                exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["A6:C6"].Value = tblThongtinHD.Rows[0][6];
                exSheet.Name = "Hóa đơn bán hàng";
                exApp.Visible = true;
            }    
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để in ","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }    
        }

        private void grbHD_Enter(object sender, EventArgs e)
        {

        }

        private void txtTenHoa_TextChanged(object sender, EventArgs e)
        {

           
           
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            cboPhu.Enabled = false;
            btnXoa.Enabled = true;
            btnXoaChiTiet.Enabled = true;
            btnLuu.Enabled = true;
            btnLuuCT.Enabled = false;
            tgsCbo.Checked = false;
            tgsCbo.Enabled = false;
            cboPhu.Enabled = false;
        }

        private void cboPhu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMAHOA.Text == "")
            {
                txtSoLuong.Text = "";
                txtDonGia.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            else
            {
                str = "Select GiaBan from  Hoa where MAHOA = N'" + cboPhu.SelectedValue + "'";
                txtDonGia.Text = LopHamXuLy.GetFieldValues(str);
                str = "Select TenHoa from  Hoa where MAHOA = N'" + cboPhu.SelectedValue + "'";
                txtTenHoa.Text = LopHamXuLy.GetFieldValues(str);

            }
        }

        private void guna2ToggleSwitch1_Click(object sender, EventArgs e)
        {
            if(tgsCbo.Checked == true)
            {
                cboPhu.Enabled = true;
                cboPhu.SelectedIndex = -1;
                string sql1 = "select MaHoa  from hoa  where hoa.MaHoa not in (select CTHoaDonBan.MaHoa from CTHoaDonBan where CTHoaDonBan.MAHDB = '" + txtMaHD.Text.Trim().ToString() + "')";
                LopHamXuLy.FillComBo(sql1, cboPhu, "MAHOA", "MAHOA");
            }    
            else
            {
                cboPhu.Enabled = false;
            }    
        }
    }
    }

