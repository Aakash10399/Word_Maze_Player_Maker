using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Word_Maze_Maker
{
    public partial class Form1 : Form
    {
        public static List<string> lines = new List<string>();
        
        public Form1()
        {
            InitializeComponent();



        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lines.Add(textBox1.Text);
            textBox1.Text = "";
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lines = lines.Distinct().ToList();
            System.IO.File.WriteAllLines(@"C:\Users\Public\words.txt", lines);
            

            
            Environment.Exit(0);
        }
    }
}
