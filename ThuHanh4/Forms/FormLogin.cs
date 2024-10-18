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
    public partial class FormLogin : Form
    {
        public DataProcesser dtbase = new DataProcesser();
        public tblUser account = new tblUser();
        public FormLogin()
        {
            InitializeComponent();
            
        }
        public FormLogin(tblUser account)
        {
            InitializeComponent();
            this.account = account;
        }

        private void FormLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtUserName.Text) || String.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Tên đăng nhập và mật khẩu không được để trống");
                return;
            }
            else
            {
                DataTable datatable = dtbase.DocBang("select * from tblUser where userName = '" + txtUserName.Text + "'");

                if (datatable.Rows.Count == 0)
                {
                    MessageBox.Show("Tên đăng nhập không đúng");
                    return;
                }

                if (datatable.Rows[0]["passWord"].ToString() == txtPassword.Text)
                {
                    account.userName = txtUserName.Text;
                    account.passWord = txtPassword.Text;

                    new ListProduct(account).Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Mật khẩu không đúng");
                }

            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
        }
    }
}
