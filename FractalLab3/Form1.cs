using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FractalLab3
{
    public partial class Form1 : Form
    {
        int Iterations = 6;

        readonly Pen pen = new Pen(Color.Black, 2);
        readonly List<Mark> Marks = new List<Mark>();

        public Form1()
        {
            InitializeComponent();
            Text = "Кладбище";
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void DrawFractal(Graphics g)
        {
            Marks.Clear();

            var root = new Mark(250, 50, 300);

            DrawMark(g, root, 1);

            root.Draw(g);

            foreach(Mark mark in Marks)
            {
                mark.Draw(g);
            }
        }

        private void DrawMark(Graphics g, Mark node, int depth)
        {
            if (depth > Iterations)
            {
                return;
            }
            
            var p_left_top = new Mark(node.X, node.Y,node.Size/3);
            var p_right_top = new Mark(node.X+(node.Size/3)*2, node.Y, node.Size / 3);
            var p_left_bot = new Mark(node.X, node.Y+(node.Size/3)*2, node.Size / 3);
            var p_right_bot = new Mark(node.X + (node.Size / 3) * 2, node.Y + (node.Size / 3) * 2, node.Size / 3);
            
            Marks.Add(node);
            Marks.Add(p_left_top);
            Marks.Add(p_right_top);
            Marks.Add(p_right_bot);
            Marks.Add(p_left_bot);

            DrawMark(g, p_left_top, depth + 1);
            DrawMark(g, p_right_top, depth + 1);
            DrawMark(g, p_left_bot, depth + 1);
            DrawMark(g, p_right_bot, depth + 1);
        }

        private class Mark
        {
            public Mark(int x, int y, int size)
            {
                X = x;
                Y = y;
                Size = size;
            }

            public int X { get; }
            public int Y { get; }
            public int Size { get; }
            public void Draw(Graphics graph)
            {
                SolidBrush brush = new SolidBrush(Color.Black);
                graph.FillRectangle(new SolidBrush(Color.White), new Rectangle(X, Y, Size, Size));
                graph.FillRectangle(brush,X+Size/3,Y,(Size/3)+1,Size+1);
                graph.FillRectangle(brush, X, (Y+Size/3), Size+1, (Size/3)+1);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Iterations = (int)numericUpDown1.Value-1;
            Graphics g = this.CreateGraphics();
            g.Clear(Color.White);
            DrawFractal(g);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
