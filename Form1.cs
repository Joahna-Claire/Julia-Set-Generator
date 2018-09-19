// Joahna Claire Golez
// CS 480 - 1001
// Assignment 2: A C# program that graphs Julia Set for arbitrary parameters
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap btm;

        // Function that graph Julia Set
        public void performJS(double c1, double c2)
        {
            int width = picCanvas.ClientSize.Width;
            int height = picCanvas.ClientSize.Height;
            const int zoom = 1;
            const int maxIterations = 255;
            const int moveX = 0;
            const int moveY = 0;
            double zx, zy, tmp;
            int i;

            //Creates bitmap or image
            btm = new Bitmap(picCanvas.ClientSize.Width, picCanvas.ClientSize.Height);

            using (Graphics g = Graphics.FromImage(btm))
            {
                g.Clear(Color.White);

                //Creates color range for graph
                var colors = (from c in Enumerable.Range(0, 256)
                              select Color.FromArgb((c >> 5) * 36, (c >> 3 & 7) * 36, (c & 3) * 85)).ToArray();

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        zx = 1.5 * (x - width / 2) / (0.5 * zoom * width) + moveX; //calculte real 
                        zy = 1.0 * (y - height / 2) / (0.5 * zoom * height) + moveY;//calculate imaginary
                        i = maxIterations; //iterations
                        while (zx * zx + zy * zy < 4 && i > 1)
                        {
                            tmp = zx * zx - zy * zy + c1;
                            zy = 2.0 * zx * zy + c2;
                            zx = tmp;
                            i -= 1;
                        }
                        btm.SetPixel(x, y, colors[i]); //set pixel
                    }
                }
                picCanvas.Image = btm;
            }
        }

        //Function for (X) exit button
        private void exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Function to get c1 and c2 values from textbox
        private void runJS(object sender, EventArgs e)
        {
            double c1, c2;

            c1 = Convert.ToDouble(c1txt.Text);
            c2 = Convert.ToDouble(c2txt.Text);

            //Check if c1 and c2 coordinates are in range
            if (!(c1 <= -2 && c2 <= -2) && !(c1 >= 2 && c2 >= 2))
            {
                MessageBox.Show("Please enter C1 and C2 value greater than -2 and less than 2.");
            }
            else
            {
                performJS(c1, c2);
            }
        }


        //Function only allows digits and at most one decimal point for c1 textbox
        private void c1txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if(ch==46 && c1txt.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if( !Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        //Function only allows digits and at most one decimal point for c2 textbox
        private void c2txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch == 46 && c1txt.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }
}
