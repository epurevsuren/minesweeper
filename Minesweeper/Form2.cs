using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        
        public string bomb,x,y;
        public int check=0;
        private void button1_Click(object sender, EventArgs e)
        {

                int mine=0, sizex=0, sizey=0;
                bomb = textBox3.Text;
                x = textBox2.Text;
                y = textBox1.Text;
                if (bomb != "" && x != "" && y != "")
                {
                    mine = Int32.Parse(bomb);
                    sizex = Int32.Parse(x);
                    sizey = Int32.Parse(y);
                }
                if (sizey > 9 && sizey < 24 && sizey > 9 && sizey < 30 && mine > 9 && mine < 668)
                this.Close();
                else
                    MessageBox.Show("Buruu ogogdol oruulsan");
            
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            check = 1;
            this.Close();
        }
    }
}
