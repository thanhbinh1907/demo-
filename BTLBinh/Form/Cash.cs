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
    public partial class Cash : Form
    {
        string tongTien;
        List<int> numbers = new List<int>(new int[9]);

        public event EventHandler ButtonClicked;

        //int number1;
        //int number2;
        //int number3;
        //int number4;
        //int number5;
        //int number6;
        //int number7;
        //int number8;
        //int number9;


        public Cash(string tongTien)
        {
            InitializeComponent();
            this.tongTien = tongTien;
            lbTongTien.Text = tongTien;

            //number1 = 0;
            //numbers.Add(number1);
            //number2 = 0;
            //numbers.Add(number2);
            //number3 = 0;
            //numbers.Add(number3);
            //number4 = 0;
            //numbers.Add(number4);
            //number5 = 0;
            //numbers.Add(number5);
            //number6 = 0;
            //numbers.Add(number6);
            //number7 = 0;
            //numbers.Add(number7);
            //number8 = 0;
            //numbers.Add(number8);
            //number9 = 0;
            //numbers.Add(number9);

            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] = 0;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------
        public void change()
        {
            decimal tienThua = 0;
            tienThua = 1000 * numbers[0] + 2000 * numbers[1] + 5000 * numbers[2] + 10000 * numbers[3] + 20000 * numbers[4] + 50000 * numbers[5] + 100000 * numbers[6] + 200000 * numbers[7] + 500000 * numbers[8] - decimal.Parse(tongTien);

            // Định dạng số tiền với dấu ngăn cách hàng nghìn
            lbChange.Text = tienThua.ToString("N0");
        }


        //----------------------------------------------------------------------------------------------------------------------

        private void btReset_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Text = "0";
                }
            }

            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] = 0;
            }

            change();
        }

        private void pb1k_Click(object sender, EventArgs e)
        {
            numbers[0]++;
            txt1k.Text = numbers[0].ToString();
            change();
        }

        private void pb2k_Click(object sender, EventArgs e)
        {
            numbers[1]++;
            txt2k.Text = numbers[1].ToString();
            change();
        }

        private void pb5k_Click(object sender, EventArgs e)
        {
            numbers[2]++;
            txt5k.Text = numbers[2].ToString();
            change();
        }

        private void pb10k_Click(object sender, EventArgs e)
        {
            numbers[3]++;
            txt10k.Text = numbers[3].ToString();
            change();
        }

        private void pb20k_Click(object sender, EventArgs e)
        {
            numbers[4]++;
            txt20k.Text = numbers[4].ToString();
            change();
        }

        private void pb50k_Click(object sender, EventArgs e)
        {
            numbers[5]++;
            txt50k.Text = numbers[5].ToString();
            change();
        }

        private void pb100k_Click(object sender, EventArgs e)
        {
            numbers[6]++;
            txt100k.Text = numbers[6].ToString();
            change();
        }

        private void pb200k_Click(object sender, EventArgs e)
        {
            numbers[7]++;
            txt200k.Text = numbers[7].ToString();
            change();
        }

        private void pb500k_Click(object sender, EventArgs e)
        {
            numbers[8]++;
            txt500k.Text = numbers[8].ToString();
            change();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu sự kiện đã được đăng ký
            ButtonClicked?.Invoke(this, EventArgs.Empty);
            this.Close();
        }
    }
}
