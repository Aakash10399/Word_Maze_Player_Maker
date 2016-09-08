using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Word_Maze
{
    public partial class Form1 : Form
    {
        public static string[] words;
        public static int maxlength;
        public static bool click = false;
        public static int[] cord = new int[4];
        public static int c;
        public static string[] wordsc;
        public static List<int> ya = new List<int>();
        
        
        public Form1()
        {
            InitializeComponent();
            AutoScroll = true;
            System.IO.StreamReader file = new System.IO.StreamReader("C:\\Users\\Public\\words.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                c++;
            }
            file.Close();
            words = new string[c];
            
            c = 0;
            System.IO.StreamReader file1 = new System.IO.StreamReader("C:\\Users\\Public\\words.txt");
            while ((line = file1.ReadLine()) != null)
            {
                words[c] = line;
                c++;
            }
            wordsc = words;
            textBox1.Text = "0";
            MW();
            Maze_Construct();
            Word_Placement();
            
            
            
        }
        
        public void MW()
        {
            maxlength = 0;
            for(int i=0;i<words.Length;i++)
            {
                if(words[i].Length>maxlength)
                {
                    maxlength = words[i].Length;
                }
            }
            maxlength = maxlength + 5;
            
        }
        public void Maze_Construct() {
            Random r = new Random();
            for(int i =1;i<=maxlength;i++)
            {
                for(int j=1;j<=maxlength;j++)
                {
                    var txt = new Button();
                    txt.Name = "WM" + i.ToString() + "," + j.ToString();
                    txt.Text = ((char)('a' + r.Next(0, 26))).ToString(); ;
                    txt.Click += new EventHandler(bc1);
                    txt.Location = new Point(300 + (j * 30), 100+(i*30));
                    txt.Visible = true;
                    txt.Height = 35;
                    txt.Width = 25;
                    txt.Font = new Font(txt.Font.FontFamily, 20);
                    this.Controls.Add(txt);
                }
            }
        }
        public void Word_Placement() {
           
            Random r = new Random();
            int r1 = r.Next(1, 3);
            int r2 = r.Next(0, wordsc.Length);
              r1 = r.Next(1, maxlength - wordsc[r2].Length);
                for (int i = r1; i < wordsc[r2].Length + r1; i++)
                {
                    Control ctn = this.Controls["WM" + (i).ToString() + "," + "1"];
                    ctn.Text = wordsc[r2].Substring(i - r1, 1);
                }
                wordsc = wordsc.Where(w => w != wordsc[r2]).ToArray();
            
                r2 = r.Next(0,wordsc.Length);
            
                r1 = r.Next(1, maxlength - wordsc[r2].Length);
                for (int i = r1; i < wordsc[r2].Length + r1; i++)
                {
                    Control ctn = this.Controls["WM" + (i).ToString() + "," + maxlength.ToString()];
                    ctn.Text = wordsc[r2].Substring(i - r1, 1);
                }
                wordsc = wordsc.Where(w => w != wordsc[r2]).ToArray();
            
            
            
            int r3=maxlength;
            int r4;
            for (int i = 1; i <= r3;i++)
            {
                ya.Add(i);
            }
                while (wordsc.Length > 0)
                {
                    r1 = r.Next(0, wordsc.Length );
                    r2 = ya[r.Next(1, r3)];
                    r4 = r.Next(2,maxlength-wordsc[r1].Length);

                    for (int i = 0; i < wordsc[r1].Length; i++)
                    {
                        Control ctn = this.Controls["WM" + r2.ToString() + "," + r4.ToString()];
                        r4++;
                        ctn.Text = wordsc[r1].Substring(i, 1);



                    }
                    ya.Remove(r2); r3--;
                    wordsc = wordsc.Where(w => w != wordsc[r1]).ToArray(); 
                }
        }
            


        
        public void bc1(object sender, EventArgs e) {
            Button btn = sender as Button;
            btn.BackColor = Color.PowderBlue;
            if (click == false)
            {
                click = true;

                cord[0] = Convert.ToInt32(btn.Name.Substring(2, btn.Name.IndexOf(",") - 2));
                cord[1] = Convert.ToInt32(btn.Name.Substring(btn.Name.IndexOf(",")+1));
                
                
                
                

            }
            else if(click==true)
            {
                click = false;

                cord[2] = Convert.ToInt32(btn.Name.Substring(2, btn.Name.IndexOf(",") - 2));
                cord[3] = Convert.ToInt32(btn.Name.Substring(btn.Name.IndexOf(",")+1));
                
                
                
                check();
            }
        
        }
        
        public void check() {
            string temp = " ";
            
            if(cord[0]==cord[2])
            {
                for (int i = Math.Min(cord[1], cord[3]); i <= Math.Max(cord[1], cord[3]); i++)
                {
                    Control ctn = this.Controls["WM" + cord[0].ToString() + "," + i.ToString()];
                    temp = temp + ctn.Text;
                }
                temp = temp.Trim();
                if (words.Contains(temp))
                { textBox1.Text = ((Convert.ToInt32(textBox1.Text)) + 1).ToString(); temp = " ";
                for (int i = Math.Min(cord[1], cord[3]); i <= Math.Max(cord[1], cord[3]); i++)
                {
                    Control ctn = this.Controls["WM" + cord[0].ToString() + "," + i.ToString()];
                    ctn.BackColor = Color.GreenYellow;
                }
                
                }
                else {
                    Control ctn = this.Controls["WM" + cord[0].ToString() + "," + cord[1].ToString()];
                    ctn.BackColor = Color.Transparent;
                    ctn = this.Controls["WM" + cord[2].ToString() + "," + cord[3].ToString()];
                    ctn.BackColor = Color.Transparent;
                }
            }
            if(cord[1]==cord[3])
            {
                for (int i = Math.Min(cord[0], cord[2]); i <= Math.Max(cord[0], cord[2]); i++)
                {
                    Control ctn = this.Controls["WM" + i.ToString() + "," + cord[1].ToString()];
                    temp = temp + ctn.Text;
                }
                temp = temp.Trim();
                if (words.Contains(temp))
                { textBox1.Text = ((Convert.ToInt32(textBox1.Text)) + 1).ToString(); temp = " ";
                for (int i = Math.Min(cord[0], cord[2]); i <= Math.Max(cord[0], cord[2]); i++)
                {
                    Control ctn = this.Controls["WM" + i.ToString() + "," + cord[1].ToString()];
                    ctn.BackColor = Color.GreenYellow;
                }
                
                }
                else {
                    Control ctn = this.Controls["WM" + cord[0].ToString() + "," + cord[1].ToString()];
                    ctn.BackColor = Color.Transparent;
                    ctn = this.Controls["WM" + cord[2].ToString() + "," + cord[3].ToString()];
                    ctn.BackColor = Color.Transparent;
                }
            }
        
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
