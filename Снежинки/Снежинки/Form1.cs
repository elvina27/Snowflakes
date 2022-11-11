using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Снежинки
{
    public partial class Form1 : Form
    {
        private readonly IList<Snowflake1> snowflakes;
        private readonly Timer timer;
        private readonly Bitmap snezhinka;
        private readonly Bitmap fon;
        private readonly int speed = 3;

        public Form1()
        {
            InitializeComponent();
            snowflakes = new List<Snowflake1>();
            AddSnowflakes();
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;

            fon = (Bitmap)Properties.Resources.фон;
            snezhinka = (Bitmap)Properties.Resources.снег;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            Print();
            timer.Start();
        }
       

        private void AddSnowflakes()
        {
            var rnd = new Random();
            for (int i = 0; i < 70; i++)
            {
                snowflakes.Add(new Snowflake1
                {
                    X = rnd.Next(Screen.PrimaryScreen.WorkingArea.Width),
                    Y = -rnd.Next(Screen.PrimaryScreen.WorkingArea.Height),
                    Size = rnd.Next(15, 30)
                });
            }
        }

        //Bitmap fon = Properties.Resources.фон;
        //Bitmap snezhinka = Properties.Resources.снег;
        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {
            Print();
        }

        private void Print()
        {
            var scene = new Bitmap(fon, ClientRectangle.Width, ClientRectangle.Height);
            for (var i = 0; i < snowflakes.Count; i++)
            {

                if (snowflakes[i].Y > Screen.PrimaryScreen.WorkingArea.Height)
                {
                    snowflakes[i].Y = -snowflakes[i].Size;
                }
                else
                {
                    snowflakes[i].Y += speed + snowflakes[i].Size;
                }
            }
            for (var i = 0; i < snowflakes.Count; i++)
            {
                if (snowflakes[i].Y > 0)
                {
                    var gr = Graphics.FromImage(scene);
                    gr.DrawImage(snezhinka,
                        new Rectangle(
                                snowflakes[i].X,
                        snowflakes[i].Y,
                        snowflakes[i].Size,
                        snowflakes[i].Size));
                }
            }
            var a = CreateGraphics();
            a.DrawImage(scene, 0, 0);
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

            if (timer.Enabled == true)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
        }
    }
}
