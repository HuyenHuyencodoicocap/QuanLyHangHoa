using AppQuanLyTro2.Data_Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThuHanh4.Model;


namespace ThuHanh4.Forms
{

   
    public partial class ListProduct : Form
    {
        public DataProcesser dt = new DataProcesser();
        public tblUser account1 = new tblUser();
        DataTable dtTable = new DataTable();
        OpenFileDialog chonAnh=new OpenFileDialog();
        public ListProduct()
        {
            InitializeComponent();
            refresh();
        }

        public ListProduct(tblUser account)
        {
            InitializeComponent();
            account1 = account;
            refresh();
        }

        private void ListProduct_Load(object sender, EventArgs e)
        {
            lbUserName.Text = "Xin chào " + account1.userName.ToLower();
        }

        public void refresh()
        {

            String query = "  select * from [dbo].[tblHang]";
            dtTable= dt.DocBang(query);  
            GridViewHangHoa.DataSource = dtTable;
            cmbChatLieu.Items.Clear();
            if (dtTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTable.Rows) {
                    cmbChatLieu.Items.Add(dr["ChatLieu"].ToString());
                }
            }
            txtMaHang.Text = "";
            txtTenHang.Text = "";
            txtSoLuong.Text = "";
            txtGiaNhap.Text = "";
            txtGiaban.Text = "";
            txtGhiChu.Text = "";
            cmbChatLieu.SelectedIndex = 0;
            picImage.Image = null;


        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
           
            chonAnh.Filter = "JPEG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif|BMP Files (*.bmp)|*.bmp|TIFF Files (*.tiff)|*.tiff|All Files (*.*)|*.*";
            if(chonAnh.ShowDialog() == DialogResult.OK)
            {
                String anh= chonAnh.FileName;   
                picImage.Image=Image.FromFile(anh);
                picImage.SizeMode= PictureBoxSizeMode.StretchImage;
            }
            else
            {
                MessageBox.Show("Mở tệp thất bại");
            }
        }

        
        private void GridViewHangHoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow selectedRow = GridViewHangHoa.Rows[e.RowIndex];
                txtMaHang.Text = selectedRow.Cells["MaHang"].Value.ToString();
                txtTenHang.Text = selectedRow.Cells["TenHang"].Value.ToString();
                txtSoLuong.Text = selectedRow.Cells["SoLuong"].Value.ToString();
                txtGiaNhap.Text = selectedRow.Cells["DonGiaNhap"].Value.ToString();
                txtGiaban.Text = selectedRow.Cells["DonGiaBan"].Value.ToString();
                cmbChatLieu.Text = selectedRow.Cells["ChatLieu"].Value.ToString();

                string imagePath = selectedRow.Cells["Anh"].Value.ToString();
                if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                {
                    try
                    {
                        picImage.Image = Image.FromFile(imagePath);
                        picImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể mở hình ảnh: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Đường dẫn hình ảnh không hợp lệ.");
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
         
            String mahang= txtMaHang.Text;
            string tenhang= txtTenHang.Text;
            if (string.IsNullOrEmpty(mahang) || string.IsNullOrEmpty(tenhang))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }
            String chatLieu=cmbChatLieu.Text;
            int soLuong= int.Parse(txtSoLuong.Text);
            if (string.IsNullOrEmpty(txtSoLuong.Text) || !int.TryParse(txtSoLuong.Text, out soLuong))
            {
                MessageBox.Show("Số lượng phải là nguyên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            float giaNhap= float.Parse(txtGiaNhap.Text);
            float giaBan = float.Parse(txtGiaban.Text);
            if (giaNhap >= giaBan)
            {
                MessageBox.Show("Giá nhập phải nhỏ hơn giá bán");
                return;

            }
            String anh= chonAnh.FileName;

            string insertSP = "insert into [dbo].[tblHang]" +
                              "values(N'" + mahang + "',N'" + tenhang + "',N'" + chatLieu + "'," + soLuong + "," + giaNhap + "," + giaBan + ",'" + anh + "')";

            dtTable = dt.DocBang("select MaHang from tblHang where Mahang= N'" + mahang + "'");
            if (dtTable.Rows.Count > 0)
            {
                MessageBox.Show("Mặt hàng này đã tồn tại");
                return;
            }
            else
            {
                dt.CapNhatDuLieu(insertSP);
                MessageBox.Show("Thêm Thành Công");
                refresh();
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result= MessageBox.Show("Bạn có chắc chắn muốn thoát","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                this.Close();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtMaHang.ReadOnly = true;
            String mahang = txtMaHang.Text;
            string tenhang = txtTenHang.Text;
            if (string.IsNullOrEmpty(mahang) || string.IsNullOrEmpty(tenhang))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }
            String chatLieu = cmbChatLieu.Text;
            int soLuong = int.Parse(txtSoLuong.Text);
            if (string.IsNullOrEmpty(txtSoLuong.Text) || !int.TryParse(txtSoLuong.Text, out soLuong))
            {
                MessageBox.Show("Số lượng phải là nguyên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            float giaNhap = float.Parse(txtGiaNhap.Text);
            float giaBan = float.Parse(txtGiaban.Text);
            if (giaNhap >= giaBan)
            {
                MessageBox.Show("Giá nhập phải nhỏ hơn giá bán");
                return;

            }
            String anh = chonAnh.FileName;

            String querySua = "UPDATE [dbo].[tblHang] " +
                "SET [TenHang] = N'" + tenhang + "', " +
                "[ChatLieu] = N'" + chatLieu + "', " +
                "[SoLuong] = " + soLuong + ", " +
                "[DonGiaNhap] = " + giaNhap + ", " +
                "[DonGiaBan] = " + giaBan + ", " +
                "[Anh] = N'" + anh + "' " + 
                "WHERE [MaHang] = '" + mahang + "'";

            dt.CapNhatDuLieu(querySua);
            MessageBox.Show("Sửa thành công");
            refresh();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtMaHang.Text))
            {
                txtMaHang.ReadOnly = true;
                String mahang = txtMaHang.Text;
                string tenhang = txtTenHang.Text;
                String queryXoa = "DELETE FROM [dbo].[tblHang] " +
                      "WHERE [MaHang] = N'" + mahang + "'";
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dt.CapNhatDuLieu(queryXoa);
                    MessageBox.Show("Xóa thành công sản phẩm ");
                    refresh();
                }
                
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa ");

            }



        }
    }
}
