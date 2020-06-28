using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_sphere_and_points
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Point3D a, b, c;
        Graphics g;
        Bitmap bmp;
        void draw2D(int num_of_points, double mult)
        {
            //bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //pictureBox1.Refresh();
            //g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);
            int m = min(pictureBox1.Width, pictureBox1.Height);//диаметр
            g.DrawEllipse(Pens.Black, 0, 0, m, m);
            //int R = pictureBox1.Width;
            int x1, x2, y1, y2;
            for (int i = 0; i <= num_of_points; i++)
            {
                x1 = (m / 2) + Convert.ToInt32((m / 2) * Math.Cos(i * 2 * Math.PI / num_of_points));
                y1 = (m / 2) + Convert.ToInt32((m / 2) * Math.Sin(i * 2 * Math.PI / num_of_points));
                x2 = (m / 2) + Convert.ToInt32((m / 2) * Math.Cos(i * mult * 2 * Math.PI / num_of_points));
                y2 = (m / 2) + Convert.ToInt32((m / 2) * Math.Sin(i * mult * 2 * Math.PI / num_of_points));
                g.DrawLine(Pens.White, x1, y1, x2, y2);
            }
            pictureBox1.Image = bmp;
        }
        int min(int a, int b)
        {
            if (a < b)
                return a;
            else
                return b;
        }
        void draw3D()
        {
            g.Clear(Color.Black);
            int m = min(pictureBox1.Width, pictureBox1.Height);
            int a1 = trackBar2.Value*2/3, a2 = trackBar2.Value/3;
            double k = Convert.ToDouble(textBox1.Text);
            double l = Convert.ToDouble(textBox1.Text);//множетили
            //a1 = 180;
            d3 p1, p2;
            for(int i=0;i<a1;i+=10)
            {
                for(int j=0;j<a2;j+=10)
                {
                    p1 = new d3((m / 2) * Math.Sin(i * 2 * Math.PI / a1) * Math.Sin(j * 2 * Math.PI / a2),
                        (m / 2) * Math.Cos(i * 2 * Math.PI / a1) * Math.Sin(j * 2 * Math.PI / a2),
                        (m / 2) * Math.Cos(j * 2 * Math.PI / a2));
                    p2 = new d3((m / 2) * Math.Sin(i * 2 * k * Math.PI / a1) * Math.Sin(j * 2 * l * Math.PI / a2),
                        (m / 2) * Math.Cos(i * 2 * k * Math.PI / a1) * Math.Sin(j * 2 * l * Math.PI / a2),
                        (m / 2) * Math.Cos(j * 2 * l * Math.PI / a2));
                    d3.draw_line(p1,p2,pictureBox1,g);
                }
            }
            pictureBox1.Image = bmp;
        }
        private void PictureBox1_SizeChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "2D")
                draw2D(Convert.ToInt32(textBox2.Text), Convert.ToDouble(textBox1.Text));
            else if (comboBox1.Text == "3D")
            {
                draw3D();
            }
        }
        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = (trackBar1.Value * (0.001)).ToString();
        }
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            //pictureBox1.Refresh();
            if (comboBox1.Text == "2D")
                draw2D(Convert.ToInt32(textBox2.Text), Convert.ToDouble(textBox1.Text));
            else if (comboBox1.Text == "3D")
            {
                //draw3D();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            comboBox1.Items.Add("2D");
            comboBox1.Items.Add("3D");
            comboBox1.Text = "2D";
            //
            d3.reset();
            //
        }
        
        private void TrackBar2_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = trackBar2.Value.ToString();
        }
        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "2D")
                draw2D(Convert.ToInt32(textBox2.Text), Convert.ToDouble(textBox1.Text));
            else if (comboBox1.Text == "3D") { }
                //draw3D();
        }

        Point start,end;
        bool is_mouse_down = false,f=false;
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            is_mouse_down = true;
            //timer1.Enabled = true;
            start = new Point(e.X, e.Y);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                g.Clear(Color.Black);
                d3.reset();
                d3.rotate(trackBar3.Value, trackBar4.Value, trackBar5.Value);
                //pictureBox1.Image = bmp;
                draw3D();
            }
        }

        private void TrackBar5_Scroll(object sender, EventArgs e)
        {
            //Point3D.a_OX = trackBar5.Value *( Math.PI / 180);
            //draw3D();
        }

        private void TrackBar4_Scroll(object sender, EventArgs e)
        {
            //Point3D.a_OY = trackBar4.Value * (Math.PI / 180);
            //draw3D();
        }

        private void TrackBar3_Scroll(object sender, EventArgs e)
        {
            //Point3D.a_OZ = trackBar3.Value * (Math.PI / 180);
            //draw3D();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex==0)
            {
                trackBar3.Visible = false;
                trackBar4.Visible = false;
                trackBar5.Visible = false;
                timer1.Enabled = false;
            }
            else
            {
                trackBar3.Visible = true;
                trackBar4.Visible = true;
                trackBar5.Visible = true;
                timer1.Enabled = true;
            }
        }

        //int ex, ey;
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(is_mouse_down)
            {
                //if (f)
                //    end = new Point(ex, ey);
                //else
                //    start = new Point(ex, ey);
                //f = !f;


                //end = new Point(e.X, e.Y);
                //if (start.X - end.X > 0)
                //    if (start.Y - end.Y > 0)
                //        p[0].Rotate(0.01, 0.01);
                //    else
                //        p[0].Rotate(0.01, -0.01);
                //else
                //    if (start.Y - end.Y > 0)
                //    p[0].Rotate(-0.01, 0.01);
                //else
                //    p[0].Rotate(-0.01, -0.01);

                //label4.Text = (Point3D.a_XY).ToString() + " - XY\n" + (Point3D.a_XZ).ToString() + " - XZ\n" + (Point3D.a_YZ).ToString() + " - YZ";
            }
            if (comboBox1.Text == "2D")
                draw2D(Convert.ToInt32(textBox2.Text), Convert.ToDouble(textBox1.Text));
            else if (comboBox1.Text == "3D")
            { }// draw3D();
        }
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            is_mouse_down = false;
        }
    }
}
