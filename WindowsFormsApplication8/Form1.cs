using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Drawing.Imaging;

namespace WindowsFormsApplication8
{

    public partial class Form1 : Form
    {

        Point p;
        Color currentcolor = Color.Black;
        ArrayList lines;
        float penwidth;

        public Form1()
        {
            InitializeComponent();
            lines = new ArrayList();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                colorDialog1.Color = currentcolor;
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                    currentcolor = colorDialog1.Color;

            }

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Capture = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            p = e.Location;
            Capture = true;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {



            if (e.Button == MouseButtons.Left && Capture)
            {
                Graphics s = CreateGraphics();
                // s.DrawLine(new Pen(Color.Red, 5), p, e.Location);
                s.DrawLine(new Pen(currentcolor, penwidth), p, e.Location);
                s.DrawLine(new Pen(currentcolor), p, e.Location);
                lines.Add(new MyLine(p, e.Location, currentcolor, penwidth));
                p = e.Location;


            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void button1_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = currentcolor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                currentcolor = colorDialog1.Color;

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (MyLine l in lines)
                e.Graphics.DrawLine(new Pen(l.color, l.width), l.start, l.end);

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            penwidth = (float)numericUpDown1.Value;
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            lines.Clear();
            Invalidate();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {

            {
                saveFileDialog1.Filter = "BIS Drawing Files (*.dra)|*.dra";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileStream stream = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                    BinaryWriter writer = new BinaryWriter(stream);
                    foreach (MyLine l in lines)
                    {
                        writer.Write(l.color.ToArgb());
                        double d = l.width;
                        writer.Write(d);
                        writer.Write(l.start.X);
                        writer.Write(l.start.Y);
                        writer.Write(l.end.X);
                        writer.Write(l.end.Y);
                    }
                    writer.Close();
                    stream.Close();
                }
            }

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "BIS Drawing Files (*.dra)|*.dra";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                lines.Clear();
                FileStream stream = new FileStream(openFileDialog1.FileName, FileMode.Open);
                BinaryReader reader = new BinaryReader(stream);
                try
                {
                    //while (true)
                    do
                    {
                        int color, sx, sy, ex, ey;
                        float w;
                        color = reader.ReadInt32();
                        w = (float)reader.ReadDouble();
                        sx = reader.ReadInt32();
                        sy = reader.ReadInt32();
                        ex = reader.ReadInt32();
                        ey = reader.ReadInt32();
                        lines.Add(new MyLine(new Point(sx, sy), new Point(ex, ey), Color.FromArgb(color), w));
                    }
                    while (true);
                }
                catch (Exception) { }
                Invalidate();
                reader.Close();
                stream.Close();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPEG Files (*.jpg)|*.jpg";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap bm = new Bitmap(1024, 768);
                Graphics g = Graphics.FromImage(bm);
                g.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, 1024, 768));
                foreach (MyLine l in lines)
                    g.DrawLine(new Pen(l.color, l.width), l.start, l.end);
                bm.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                g.Dispose();

            }
        }
    }
}
    

        
    

