using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;


namespace Minesweeper
{
    public partial class Minesweeper : Form
    {
        int sizex=10,sizey=10,mine=10;
        Button[] buttons = new Button[720]; //Button төрлийн Array үүсгэж байна.
        Random random = new Random();
        int[] bomb = new int[668];
        int[] arr = new int[720];
        string[] checksum = new string[668];

        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        private TimeSpan time = new TimeSpan();

        public Minesweeper()
        {
            InitializeComponent();
            timer1.Interval = 100;

            timer1.Tick += new EventHandler(timer1_Tick);
        }
        
        private void Form1_Load(object sender, EventArgs e)
{
    reset();        
}
        public void reset()
        {
            timer1.Start();
            time = TimeSpan.FromMilliseconds(0);

            label1.Text = "0:0";
            for (int j = 0; j < sizey; j++)
                for (int i = 0; i < sizex; i++)
                {
                    arr[j * sizey + i] = 0;
                }
            
            for (int i = 0; i < mine; i++)
                bomb[i] = 0;

            for (int i = 0; i < mine; i++)
            {
                bomb[i] = random.Next(0, sizex*sizey);
                for (int j = 0; j < i; j++)
                {
                    if (bomb[j] != bomb[i])
                        arr[bomb[i]] = -1;
                    else
                        i--;
                }
            }
            for (int j = 0; j < sizey; j++)
                for (int i = 0; i < sizex; i++) // Нийт sizey ширхэг button.
                {
                    buttons[j * sizey + i] = new Button(); // Button бүрийг үүсгэж байна.
                    buttons[j * sizey + i].Location = new Point(i * 30 + 30, 30 * (j + 1)); // Байршилыг нь тодорхойлж байна. 
                    //Формын зүүн дээд өнцгийн координат (0,0) байна.
                    buttons[j * sizey + i].Name = (j * sizey + i).ToString(); //Нэр
                    buttons[j * sizey + i].Size = new Size(30, 30); //Хэмжээ
                    buttons[j * sizey + i].Font = new Font(buttons[j * sizey + i].Font, FontStyle.Bold);

                    buttons[j * sizey + i].MouseDown += new MouseEventHandler(ButtonsClicked); // Хулганы аль нэг товч дарагдахад ButtonsClicked method дуудагдана.
                    //Дараах мөр бичлэгээр дээр үүсгэсэн button-оо формтойгоо холбож өгнө.
                    this.Controls.Add(buttons[j * sizey + i]);
                    if (arr[j * sizey + i] == -1)
                    {
                        //right
                        if (i + 1 < sizey && arr[j * sizey + i + 1] != -1)
                            arr[j * sizey + i + 1]++;
                        //left
                        if (i - 1 >= 0 && arr[j * sizey + i - 1] != -1)
                            arr[j * sizey + i - 1]++;
                        //up
                        if (j - 1 >= 0 && arr[(j - 1) * sizey + i] != -1)
                            arr[(j - 1) * sizey + i]++;
                        //down
                        if (j + 1 < sizey && arr[(j + 1) * sizey + i] != -1)
                            arr[(j + 1) * sizey + i]++;
                        //right above
                        if (j - 1 >= 0 && i + 1 < sizey && arr[(j - 1) * sizey + i + 1] != -1)
                            arr[(j - 1) * sizey + i + 1]++;
                        //right below
                        if (j + 1 < sizey && i + 1 < sizey && arr[(j + 1) * sizey + i + 1] != -1)
                            arr[(j + 1) * sizey + i + 1]++;
                        //left above
                        if (j - 1 >= 0 && i - 1 >= 0 && arr[(j - 1) * sizey + i - 1] != -1)
                            arr[(j - 1) * sizey + i - 1]++;
                        //Left below
                        if (j + 1 < sizey && i - 1 >= 0 && arr[(j + 1) * sizey + i - 1] != -1)
                            arr[(j + 1) * sizey + i - 1]++;

                    }
                }
}
        int attend = 0;
        private void ButtonsClicked(object sender, MouseEventArgs e)

{
Button currentButton = (Button)sender; //sender объектыг Button төрөл рүү хувиргаж байна
int checkmate = -1;
for (int c = 0; c <= attend&&c<mine; c++)
    if (checksum[c] == currentButton.Name)
    checkmate = c; 

    if (e.Button == MouseButtons.Left&&checkmate==-1)
    {
        for (int j = 0; j < sizey; j++)
            for (int i = 0; i < sizex; i++)
            {
                string check = (j * sizey + i).ToString();
                if (currentButton.Name == check)
                {
                    if (arr[j * sizey + i] == -1)
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            currentButton.BackColor = Color.Red;
                            currentButton.Font = new Font(currentButton.Font.FontFamily, 20);
                            currentButton.Text = "*";
                        }
                        for (int x = 0; x < mine; x++)
                        {                          
                            buttons[bomb[x]].BackColor = Color.Red;
                            buttons[bomb[x]].Font = new Font(buttons[bomb[x]].Font.FontFamily, 20);
                            buttons[bomb[x]].Text = "*";
                        }
                        MessageBox.Show("Game over");
                        Application.Exit();

                    }

                    if (arr[j * sizey + i] == 0)
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            
                            currentButton.BackColor = Color.Azure;
                        }
                        int a = i, b = j;
                        for (int x; j < sizey; j++)
                        {
                            x = j * sizey + i;
                            if (arr[x] == 0)
                            {
                                
                                buttons[x].BackColor = Color.Azure;

                                //right
                                for (int m = j * sizey + i + 1; m < sizey*sizex; m++)
                                {
                                    if (arr[m] == 0)
                                    {
                                        
                                        buttons[m].BackColor = Color.Azure;
                                    }
                                    else
                                    {
                                        if (arr[m] != -1)
                                        {
                                            
                                            buttons[m].BackColor = Color.Azure;
                                            buttons[m].Text = arr[m].ToString();
                                            switch (arr[m])
                                            {
                                                case 1: { buttons[m].ForeColor = Color.Blue; break; }
                                                case 2: { buttons[m].ForeColor = Color.Green; break; }
                                                default: { buttons[m].ForeColor = Color.Red; break; }
                                            }
                                            break;
                                        } break;

                                    }
                                }
                                //left
                                for (int m = j * sizey + i - 1; m >= 0; m--)
                                {
                                    if (arr[m] == 0)
                                    {
                                        
                                        buttons[m].BackColor = Color.Azure;
                                    }
                                    else
                                    {
                                        if (arr[m] != -1)
                                        {
                                            
                                            buttons[m].BackColor = Color.Azure;
                                            buttons[m].Text = arr[m].ToString();
                                            switch (arr[m])
                                            {
                                                case 1: { buttons[m].ForeColor = Color.Blue; break; }
                                                case 2: { buttons[m].ForeColor = Color.Green; break; }
                                                default: { buttons[m].ForeColor = Color.Red; break; }
                                            }
                                            break;
                                        } break;
                                    }
                                }
                            }
                            else break;
                        }
                        i = a; j = b;
                        for (int x; j > 0; j--)
                        {
                            x = j * sizey + i;
                            if (arr[x] == 0)
                            {
                                
                                buttons[x].BackColor = Color.Azure;

                                //right
                                for (int m = j * sizey + i + 1; m < sizey*sizex; m++)
                                {
                                    if (arr[m] == 0)
                                    {
                                        
                                        buttons[m].BackColor = Color.Azure;
                                    }
                                    else
                                    {
                                        if (arr[m] != -1)
                                        {
                                            
                                            buttons[m].BackColor = Color.Azure;
                                            buttons[m].Text = arr[m].ToString();
                                            switch (arr[m])
                                            {
                                                case 1: { buttons[m].ForeColor = Color.Blue; break; }
                                                case 2: { buttons[m].ForeColor = Color.Green; break; }
                                                default: { buttons[m].ForeColor = Color.Red; break; }
                                            }
                                            break;
                                        } break;

                                    }
                                }
                                //left
                                for (int m = j * sizey + i - 1; m >= 0; m--)
                                {
                                    if (arr[m] == 0)
                                    {
                                        
                                        buttons[m].BackColor = Color.Azure;
                                    }
                                    else
                                    {
                                        if (arr[m] != -1)
                                        {
            
                                            buttons[m].BackColor = Color.Azure;
                                            buttons[m].Text = arr[m].ToString();
                                            switch (arr[m])
                                            {
                                                case 1: { buttons[m].ForeColor = Color.Blue; break; }
                                                case 2: { buttons[m].ForeColor = Color.Green; break; }
                                                default: { buttons[m].ForeColor = Color.Red; break; }
                                            }
                                            break;
                                        } break;
                                    }
                                }
                            }
                            else break;
                        }
                        i = a; j = b;
                    }
                    if (arr[j * sizey + i] > 0)
                    {
                        if (e.Button == MouseButtons.Left)
                            currentButton.BackColor = Color.Azure;
                        
                        currentButton.Text = arr[j * sizey + i].ToString();
                        switch (arr[j * sizey + i])
                        {
                            case 1: { currentButton.ForeColor = Color.Blue; break; }
                            case 2: { currentButton.ForeColor = Color.Green; break; }
                            default: { currentButton.ForeColor = Color.Red; break; }
                        }
                        break;
                    }

                }

            }
        for (int z = 0,count1=0; z < sizey * sizey;z++ )
            if (buttons[z].BackColor == Color.Azure)
            {
                count1++;
                if (count1 == (sizey * sizex - mine))
                {
                    MessageBox.Show("You win");
                    Application.Exit();
                }
            }
    }


if (e.Button == MouseButtons.Right&&attend<mine&&checkmate==-1)
{
    checksum[attend] = currentButton.Name;
    currentButton.ForeColor = Color.Red;    
    currentButton.Text = "!";
    attend++;
}
if (checkmate > -1 && e.Button == MouseButtons.Right)
{

        attend--;
    if(attend>=0&&attend<mine)
    checksum[checkmate] = checksum[attend];
    currentButton.ForeColor = Color.Black;
    currentButton.Text = "";
}
           
}

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Application.Exit();
        }

        private void beginnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            for (int j = 0; j < sizey; j++)
                for (int i = 0; i < sizex; i++)
                    buttons[j * sizey + i].Dispose();
            sizex = 10; sizey = 10; mine = 10;
            this.Size = new System.Drawing.Size(381, 388);
            reset();
        }

        private void intermediateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            for (int j = 0; j < sizey; j++)
                for (int i = 0; i < sizex; i++)
                    buttons[j * sizey + i].Dispose();
                    sizex = 16; sizey = 16; mine = 40;
            this.Size = new System.Drawing.Size(560, 560);
            reset();

        }

        private void expertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            for (int j = 0; j < sizey; j++)
                for (int i = 0; i < sizex; i++)
                    buttons[j * sizey + i].Dispose();
            sizex = 30; sizey = 16; mine = 99;
            this.Size = new System.Drawing.Size(980,560);
            reset();
        }
        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            for (int j = 0; j < sizey; j++)
                for (int i = 0; i < sizex; i++)
                    buttons[j * sizey + i].Dispose();
            Form2 cc= new Form2();
            string a = bomb.ToString();
            
            cc.ShowDialog();
            if (cc.check != 1)
            {
                mine = Int32.Parse(cc.bomb);
                sizex = Int32.Parse(cc.x);
                sizey = Int32.Parse(cc.y);
            }
            this.Size = new System.Drawing.Size(sizex*38, sizey*38);
            reset();  
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time = time.Add(TimeSpan.FromMilliseconds(50));

            label1.Text = string.Format("{0}:{1}", time.Minutes, time.Seconds);

        }

 
    }

}
