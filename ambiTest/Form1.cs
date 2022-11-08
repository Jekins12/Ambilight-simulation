using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ambiTest
{
    public partial class Form1 : Form
    {

        Bitmap bitmap = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
        static int NUM_LED = 30;
        

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

           

        }


        public int[] getFrame(int frame_num)
        {
           // var frame_time = System.Diagnostics.Stopwatch.StartNew();
            var screenshot_time = System.Diagnostics.Stopwatch.StartNew();
            int[] frame = new int[3];
            //textBox1.AppendText(" in getFrame 1 ");
            if (frame_num == 1)
            {
                Graphics graphics = Graphics.FromImage(bitmap as Image); // Create a new graphics objects that can capture the screen
                graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size); // Screenshot moment → screen content to graphics object
            }
            
            //textBox1.AppendText(" after screenshot 3 ");
            //textBox1.Text = bitmap.Size.ToString();

            

            int sumR = 0;
            int sumG = 0;
            int sumB = 0;

            var frame_time = System.Diagnostics.Stopwatch.StartNew();

            if (frame_num <= NUM_LED/2)
            {
                for (int x = frame_num*100-100; x < frame_num*100; x += 5)
                {

                    for (int y = 0; y < 160; y += 8)
                    {

                        byte rr = bitmap.GetPixel(x, y).R;
                        byte gg = bitmap.GetPixel(x, y).G;
                        byte bb = bitmap.GetPixel(x, y).B;

                        sumR += rr;
                        sumG += gg;
                        sumB += bb;
                    }
                }
            }

            if (frame_num > NUM_LED/2)
            {
                 frame_num -= NUM_LED/2;

                for (int x = frame_num * 100 - 100; x < frame_num * 100; x += 5)
                {
                    for (int y = 740; y < 900; y += 8)
                    {
                        byte rr = bitmap.GetPixel(x, y).R;
                        byte gg = bitmap.GetPixel(x, y).G;
                        byte bb = bitmap.GetPixel(x, y).B;

                        sumR += rr;
                        sumG += gg;
                        sumB += bb;
                    }
                }
            }

            label31.Text = (frame_time.ElapsedMilliseconds.ToString());

            //textBox1.AppendText(" before return 4 ");

            frame[0] = (sumR / 400);
            frame[1] = (sumG / 400);
            frame[2] = (sumB / 400);

            //label1.BackColor = Color.FromArgb(frame[0], frame[1], frame[2]);
            frame_time.Stop();
            
            return frame;

        }


        public int[] GetScreen()
        {
            var screen_time = System.Diagnostics.Stopwatch.StartNew();
            int[] screen = new int[3 * NUM_LED];
            int[] screen_temp = new int[3];

            for (int i = 1; i <= NUM_LED; i++)
            {
                screen_temp = getFrame(i);
                textBox1.Text = "frame" + i;
                screen[i * 3 - 3] = screen_temp[0];
                screen[i * 3 - 2] = screen_temp[1];
                screen[i * 3 - 1] = screen_temp[2];

            }

            screen_time.Stop();
            label32.Text = (screen_time.ElapsedMilliseconds.ToString());

            return screen;
        }

        public void setAmbient()
        {
            int[] screen = GetScreen();
            var ready_time = System.Diagnostics.Stopwatch.StartNew();
            var label = new[] { label1, label2, label3,label4,label5,label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16, label17, label18, label19, label20, label21, label22, label23, label24, label25, label26, label27, label28, label29, label30 };
            //label1.ForeColor = Color.FromArgb(255,0,0);

            try
            {
                for(int i = 1; i<=NUM_LED; i++)
                {
                    label[i-1].BackColor = Color.FromArgb(screen[i * 3 - 3], screen[i * 3 - 2], screen[i * 3 - 1]); //.R(screen[i * 3 - 3]);
                }
                //port.Write(string.Join(" ", screen) + " A");

                ready_time.Stop();
               // label33.Text = (ready_time.ElapsedMilliseconds.ToString());
            }

            catch (Exception ex)
            {
               // timer1.Stop();
               // timer2.Stop();
                MessageBox.Show(ex.Message);
                //label3.Text = "Arduino's not responding...";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            //setAmbient();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            setAmbient();
        }
    }
}
