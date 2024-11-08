using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLBinh
{
    public partial class MenuItems : Form
    {
        Boolean submit = false;



        public MenuItems()
        {
            InitializeComponent();
            // Đặt tất cả các textbox về 0
            ResetAllTextBoxesInAllGroupBoxes();
        }

        //-----------------------------------------------------------------------------------
        


        public Boolean Submit
        {
            get { return submit; }
            set { submit = value; }
        }

        private void ResetAllTextBoxesInAllGroupBoxes()
        {
            // Duyệt qua tất cả các GroupBox trong form
            foreach (Control control in this.Controls)
            {
                if (control is GroupBox)
                {
                    // Gọi hàm Reset cho mỗi GroupBox
                    ResetAllTextBoxesInGroupBox((GroupBox)control);
                }
            }
        }

        private void ResetAllTextBoxesInGroupBox(GroupBox groupBox)
        {
            // Duyệt qua tất cả các điều khiển trong GroupBox
            foreach (Control control in groupBox.Controls)
            {
                if (control is TextBox)
                {
                    // Đặt giá trị của TextBox thành "0"
                    ((TextBox)control).Text = "0";
                }
            }
        }

        // Hàm dùng để cập nhật giá trị trong TextBox của GroupBox tương ứng
        private void UpdateTextBoxInGroupBox(GroupBox groupBox, int changeValue)
        {
            // Duyệt qua tất cả các điều khiển trong GroupBox
            foreach (Control control in groupBox.Controls)
            {
                // Kiểm tra nếu điều khiển là TextBox
                if (control is TextBox)
                {
                    // Kiểm tra xem TextBox có phải là số hợp lệ không
                    if (int.TryParse(((TextBox)control).Text, out int currentValue))
                    {
                        // Tính toán giá trị mới
                        int newValue = currentValue + changeValue;

                        // Nếu giá trị mới lớn hơn hoặc bằng 0, cập nhật giá trị
                        if (newValue >= 0)
                        {
                            ((TextBox)control).Text = newValue.ToString();
                        }
                        else
                        {
                            // Nếu giá trị mới nhỏ hơn 0, không thay đổi giá trị
                            ((TextBox)control).Text = "0"; // Bạn có thể để lại giá trị 0 nếu muốn
                        }
                    }
                    else
                    {
                        // Nếu không phải số hợp lệ, có thể đặt lại giá trị về 0
                        ((TextBox)control).Text = "0";
                    }
                }
            }
        }

        public int GetNumCapheDen()
        {
            int result;
            // Thử chuyển giá trị của TextBox sang int
            int.TryParse(txtCaPheDen.Text, out result);
            return result;
        }

        public int GetNumCapheSua()
        {
            int result;
            // Thử chuyển giá trị của TextBox sang int
            int.TryParse(txtCaPheSua.Text, out result);
            return result;
        }

        public int GetNumTraSuaTranChau()
        {
            int result;
            // Thử chuyển giá trị của TextBox sang int
            int.TryParse(txtTraSuaTranChau.Text, out result);
            return result;

        }

        public int GetNumSinhToDau()
        {
            int result;
            // Thử chuyển giá trị của TextBox sang int
            int.TryParse(txtSinhToDau.Text, out result);
            return result;
        }

        public int GetNumNuocCamEp()
        {
            int result;
            // Thử chuyển giá trị của TextBox sang int
            int.TryParse(txtNuocCamEp.Text, out result);
            return result;
        }

        public int GetNumSodaChanh()
        {
            int result;
            // Thử chuyển giá trị của TextBox sang int
            int.TryParse(txtSodaChanh.Text, out result);
            return result;
        }

        public int GetNumBanhNgotDau()
        {
            int result;
            // Thử chuyển giá trị của TextBox sang int
            int.TryParse(txtBanhNgotDau.Text, out result);
            return result;
        }

        public int GetNumBanhMiThit()
        {
            int result;
            // Thử chuyển giá trị của TextBox sang int
            int.TryParse(txtBanhMiThit.Text, out result);
            return result;
        }

        public int GetNumPizza()
        {
            int result;
            // Thử chuyển giá trị của TextBox sang int
            int.TryParse(txtPizza.Text, out result);
            return result;
        }

        public int GetNumSalad()
        {
            int result;
            // Thử chuyển giá trị của TextBox sang int
            int.TryParse(txtSalad.Text, out result);
            return result;
        }

        public int GetNumHamburger()
        {
            int result;
            // Thử chuyển giá trị của TextBox sang int
            int.TryParse(txtHamburger.Text, out result);
            return result;
        }

        public int GetNumKemDua()
        {
            int result;
            // Thử chuyển giá trị của TextBox sang int
            int.TryParse(txtKemDua.Text, out result);
            return result;
        }

        public int GetNumNuocEpTao()
        {
            int result;
            // Thử chuyển giá trị của TextBox sang int
            int.TryParse(txtNuocEpTao.Text, out result);
            return result;
        }

        public int GetNumCaPheDaXay()
        {
            int result;
            // Thử chuyển giá trị của TextBox sang int
            int.TryParse(txtCaPheDaXay.Text, out result);
            return result;
        }

        public int GetNumTraSuaMatcha()
        {
            int result;
            // Thử chuyển giá trị của TextBox sang int
            int.TryParse(txtTraSuaMatcha.Text, out result);
            return result;
        }

        public string getCaPheDen()
        {
            return gbCaPheDen.Text;
        }

        public string getCaPheSua()
        {
            return gbCaPheSua.Text;
        }

        public string getTraSuaTranChau()
        {
            return gbtraSuaTranChau.Text;
        }

        public string getSinhToDau()
        {
            return gbtraSuaTranChau.Text;
        }

        public string getNuocCamEp()
        {
            return gbNuocCamEp.Text;
        }

        public string getSodaChanh()
        {
            return gbSodaChanh.Text;
        }

        public string getBanhNgotDau()
        {
            return gbBanhNgotDau.Text;
        }

        public string getBanhMiThit()
        {
            return gbBanhMiThit.Text;
        }

        public string getPizza()
        { return gbPizza.Text; }

        public string getSalad()
        { return gbSalad.Text; }

        public string getHamburger()
        { return gbHamburger.Text; }

        public string getKemDua()
        { return gbKemDua.Text; }

        public string getNuocEpTao()
        {  return gbNuocEpTao.Text;}

        public string getCaPheDaXay()
        { return gbCaPheDaXay.Text; }

        public string getTraSuaMatcha()
        { return gbTraSuaMatcha.Text; }

        //----------------------------------------------------------------------------------------------

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbCaPheDen, 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbCaPheDen, -1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbCaPheSua, 1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbCaPheSua, -1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbtraSuaTranChau, 1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbtraSuaTranChau, -1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbSinhToDau, 1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbSinhToDau, -1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbNuocCamEp, 1);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbNuocCamEp, -1);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbSodaChanh, 1);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbSodaChanh, -1);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbBanhNgotDau, 1);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbBanhNgotDau, -1);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbBanhMiThit, 1);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbBanhMiThit, -1);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbPizza, 1);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbPizza, -1);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbSalad, 1);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbSalad, -1);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbHamburger, 1);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbHamburger, -1);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbKemDua, 1);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbKemDua, -1);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbNuocEpTao, 1);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbNuocEpTao, -1);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbCaPheDaXay, 1);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbCaPheDaXay, -1);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbTraSuaMatcha, 1);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            UpdateTextBoxInGroupBox(gbTraSuaMatcha, -1);
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            submit = true;
            this.Hide();
        }

        







        //-----------------------------------------------------------------------------------------



    }
}
