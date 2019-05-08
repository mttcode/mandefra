using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using NUnit.Framework;
using MathNet.Numerics;


namespace Mandefra
{
    public partial class MandelForm : Form
    {
        Complex P1, P2, oldP1, oldP2;
        Size oldSize;
        int maxIter;
        double escR;
        Color[] colors = new Color[768];
        Bitmap mandelBitmap;
        List<RadioButton> vyber_d;


        public MandelForm()
        {
            InitializeComponent();
        }


        public void genColors()
        {
            for (int i = 0; i < 256; i++)
            {
                int colorValue_R = 0;
                int colorValue_G = 0;

                int colorValue_B = i;

                colors[i] = Color.FromArgb(colorValue_R, colorValue_G, colorValue_B);
            }

            for (int i = 256; i < 512; i++)
            {
                int colorValue_R = 0;

                int colorValue_G = i - 256;
                int colorValue_B = 255 - colorValue_G;

                colors[i] = Color.FromArgb(colorValue_R, colorValue_G, colorValue_B);
            }

            for (int i = 512; i < 768; i++)
            {
                int colorValue_B = 0;

                int colorValue_R = i - 512;
                int colorValue_G = 255 - colorValue_R;

                colors[i] = Color.FromArgb(colorValue_R, colorValue_G, colorValue_B);
            }
        }

        public void cyklusVyber()
        {
            for (int i = 0; i < vyber_d.Count; i++)
            {
                if (vyber_d[i].Checked == true)
                {
                    MandelbrotMnozina(maxIter, i);
                }

            }

        }

        public void MandelbrotMnozina(int iter, int set_d)
        {
            P1 = new Complex(1, 1.5);
            P2 = new Complex(-2, -1.5);
            oldP1 = new Complex(1, 1.5);
            oldP2 = new Complex(-2, -1.5);
            oldSize = new Size(picMandel.Size.Width, picMandel.Size.Height);

            maxIter = iter;
            escR = 2;

            mandelBitmap = new Bitmap(picMandel.Width, picMandel.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            System.Drawing.Imaging.BitmapData bd = mandelBitmap.LockBits(new Rectangle(0, 0, picMandel.Width, picMandel.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            IntPtr pointerBmp = bd.Scan0;

            int pxs_max = picMandel.Width * picMandel.Height * 4;
            byte[] pxs = new byte[pxs_max];

            double xStep = (P1.Real - P2.Real) / (picMandel.Width - 1);
            double yStep = (P1.Imag - P2.Imag) / (picMandel.Height - 1);
            double logEscR = Math.Log(escR);

            Complex z = P2;


            for (int x = 0; x < picMandel.Width; x++)
            {
                z.Imag = P1.Imag;
                z.Real += xStep;


                switch (set_d)
                {
                    case 0:
                        for (int y = 0; y < picMandel.Height; y++)
                        {
                            z.Imag -= yStep;
                            Complex C = z;

                            //alg. wikipedia
                            int iteration = 0;

                            while (z.Modulus < escR && iteration < maxIter)
                            {
                                z = C;
                                iteration++;
                            }

                            int colorIndex = 0;
                            int index = (y * picMandel.Width + x) * 4;

                            if (iteration < maxIter)
                            {
                                z = C;
                                iteration++;

                                z = C;
                                iteration++;

                                double smooth = iteration - (Math.Log(Math.Log(z.Modulus))) / logEscR;
                                colorIndex = (int)(smooth / maxIter * 768);

                                if (colorIndex >= 768)
                                {
                                    colorIndex = 0;
                                }

                                if (colorIndex < 0)
                                {
                                    colorIndex = 0;
                                }
                            }
                            pxs[index] = colors[colorIndex].B;
                            pxs[index + 1] = colors[colorIndex].G;
                            pxs[index + 2] = colors[colorIndex].R;
                            pxs[index + 3] = 255;

                            z = C;
                        }
                        break;

                    case 1:
                        for (int y = 0; y < picMandel.Height; y++)
                        {
                            z.Imag -= yStep;
                            Complex C = z;

                            //alg. wikipedia
                            int iteration = 0;

                            while (z.Modulus < escR && iteration < maxIter)
                            {
                                z = z + C;
                                iteration++;
                            }

                            int colorIndex = 0;
                            int index = (y * picMandel.Width + x) * 4;

                            if (iteration < maxIter)
                            {
                                z = z + C;
                                iteration++;

                                z = z + C;
                                iteration++;

                                double smooth = iteration - (Math.Log(Math.Log(z.Modulus))) / logEscR;
                                colorIndex = (int)(smooth / maxIter * 768);

                                if (colorIndex >= 768)
                                {
                                    colorIndex = 0;
                                }

                                if (colorIndex < 0)
                                {
                                    colorIndex = 0;
                                }
                            }
                            pxs[index] = colors[colorIndex].B;
                            pxs[index + 1] = colors[colorIndex].G;
                            pxs[index + 2] = colors[colorIndex].R;
                            pxs[index + 3] = 255;

                            z = C;
                        }
                        break;

                    case 2:
                        for (int y = 0; y < picMandel.Height; y++)
                        {
                            z.Imag -= yStep;
                            Complex C = z;

                            //alg. wikipedia
                            int iteration = 0;

                            while (z.Modulus < escR && iteration < maxIter)
                            {
                                z = z * z + C;
                                iteration++;
                            }

                            int colorIndex = 0;
                            int index = (y * picMandel.Width + x) * 4;

                            if (iteration < maxIter)
                            {
                                z = z * z + C;
                                iteration++;

                                z = z * z + C;
                                iteration++;

                                double smooth = iteration - (Math.Log(Math.Log(z.Modulus))) / logEscR;
                                colorIndex = (int)(smooth / maxIter * 768);

                                if (colorIndex >= 768)
                                {
                                    colorIndex = 0;
                                }

                                if (colorIndex < 0)
                                {
                                    colorIndex = 0;
                                }
                            }
                            pxs[index] = colors[colorIndex].B;
                            pxs[index + 1] = colors[colorIndex].G;
                            pxs[index + 2] = colors[colorIndex].R;
                            pxs[index + 3] = 255;

                            z = C;
                        }
                        break;

                    case 3:
                        for (int y = 0; y < picMandel.Height; y++)
                        {
                            z.Imag -= yStep;
                            Complex C = z;

                            //alg. wikipedia
                            int iteration = 0;

                            while (z.Modulus < escR && iteration < maxIter)
                            {
                                z = z * z *z + C;
                                iteration++;
                            }

                            int colorIndex = 0;
                            int index = (y * picMandel.Width + x) * 4;

                            if (iteration < maxIter)
                            {
                                z = z * z * z + C;
                                iteration++;

                                z = z * z *z + C;
                                iteration++;

                                double smooth = iteration - (Math.Log(Math.Log(z.Modulus))) / logEscR;
                                colorIndex = (int)(smooth / maxIter * 768);

                                if (colorIndex >= 768)
                                {
                                    colorIndex = 0;
                                }

                                if (colorIndex < 0)
                                {
                                    colorIndex = 0;
                                }
                            }
                            pxs[index] = colors[colorIndex].B;
                            pxs[index + 1] = colors[colorIndex].G;
                            pxs[index + 2] = colors[colorIndex].R;
                            pxs[index + 3] = 255;

                            z = C;
                        }
                        break;

                    case 4:
                        for (int y = 0; y < picMandel.Height; y++)
                        {
                            z.Imag -= yStep;
                            Complex C = z;

                            //alg. wikipedia
                            int iteration = 0;

                            while (z.Modulus < escR && iteration < maxIter)
                            {
                                z = z * z * z * z+ C;
                                iteration++;
                            }

                            int colorIndex = 0;
                            int index = (y * picMandel.Width + x) * 4;

                            if (iteration < maxIter)
                            {
                                z = z * z * z * z+ C;
                                iteration++;

                                z = z * z * z * z + C;
                                iteration++;

                                double smooth = iteration - (Math.Log(Math.Log(z.Modulus))) / logEscR;
                                colorIndex = (int)(smooth / maxIter * 768);

                                if (colorIndex >= 768)
                                {
                                    colorIndex = 0;
                                }

                                if (colorIndex < 0)
                                {
                                    colorIndex = 0;
                                }
                            }
                            pxs[index] = colors[colorIndex].B;
                            pxs[index + 1] = colors[colorIndex].G;
                            pxs[index + 2] = colors[colorIndex].R;
                            pxs[index + 3] = 255;

                            z = C;
                        }
                        break;

                    case 5:
                        for (int y = 0; y < picMandel.Height; y++)
                        {
                            z.Imag -= yStep;
                            Complex C = z;

                            //alg. wikipedia
                            int iteration = 0;

                            while (z.Modulus < escR && iteration < maxIter)
                            {
                                z = z * z * z * z * z+ C;
                                iteration++;
                            }

                            int colorIndex = 0;
                            int index = (y * picMandel.Width + x) * 4;

                            if (iteration < maxIter)
                            {
                                z = z * z * z * z * z + C;
                                iteration++;

                                z = z * z * z * z * z + C;
                                iteration++;

                                double smooth = iteration - (Math.Log(Math.Log(z.Modulus))) / logEscR;
                                colorIndex = (int)(smooth / maxIter * 768);

                                if (colorIndex >= 768)
                                {
                                    colorIndex = 0;
                                }

                                if (colorIndex < 0)
                                {
                                    colorIndex = 0;
                                }
                            }
                            pxs[index] = colors[colorIndex].B;
                            pxs[index + 1] = colors[colorIndex].G;
                            pxs[index + 2] = colors[colorIndex].R;
                            pxs[index + 3] = 255;

                            z = C;
                        }
                        break;

                    case 6:
                        for (int y = 0; y < picMandel.Height; y++)
                        {
                            z.Imag -= yStep;
                            Complex C = z;

                            //alg. wikipedia
                            int iteration = 0;

                            while (z.Modulus < escR && iteration < maxIter)
                            {
                                z = 1/z + C;
                                iteration++;
                            }

                            int colorIndex = 0;
                            int index = (y * picMandel.Width + x) * 4;

                            if (iteration < maxIter)
                            {
                                z = 1/z + C;
                                iteration++;

                                z = 1/z + C;
                                iteration++;

                                double smooth = iteration - (Math.Log(Math.Log(z.Modulus))) / logEscR;
                                colorIndex = (int)(smooth / maxIter * 768);

                                if (colorIndex >= 768)
                                {
                                    colorIndex = 0;
                                }

                                if (colorIndex < 0)
                                {
                                    colorIndex = 0;
                                }
                            }
                            pxs[index] = colors[colorIndex].B;
                            pxs[index + 1] = colors[colorIndex].G;
                            pxs[index + 2] = colors[colorIndex].R;
                            pxs[index + 3] = 255;

                            z = C;
                        }
                        break;

                    case 7:
                        for (int y = 0; y < picMandel.Height; y++)
                        {
                            z.Imag -= yStep;
                            Complex C = z;

                            //alg. wikipedia
                            int iteration = 0;

                            while (z.Modulus < escR && iteration < maxIter)
                            {
                                z = 1/(z*z) + C;
                                iteration++;
                            }

                            int colorIndex = 0;
                            int index = (y * picMandel.Width + x) * 4;

                            if (iteration < maxIter)
                            {
                                z = 1/(z*z) + C;
                                iteration++;

                                z = 1/(z*z) + C;
                                iteration++;

                                double smooth = iteration - (Math.Log(Math.Log(z.Modulus))) / logEscR;
                                colorIndex = (int)(smooth / maxIter * 768);

                                if (colorIndex >= 768)
                                {
                                    colorIndex = 0;
                                }

                                if (colorIndex < 0)
                                {
                                    colorIndex = 0;
                                }
                            }
                            pxs[index] = colors[colorIndex].B;
                            pxs[index + 1] = colors[colorIndex].G;
                            pxs[index + 2] = colors[colorIndex].R;
                            pxs[index + 3] = 255;

                            z = C;
                        }
                        break;

                    case 8:
                        for (int y = 0; y < picMandel.Height; y++)
                        {
                            z.Imag -= yStep;
                            Complex C = z;

                            //alg. wikipedia
                            int iteration = 0;

                            while (z.Modulus < escR && iteration < maxIter)
                            {
                                z = 1/(z*z*z) + C;
                                iteration++;
                            }

                            int colorIndex = 0;
                            int index = (y * picMandel.Width + x) * 4;

                            if (iteration < maxIter)
                            {
                                z = 1/(z*z*z) + C;
                                iteration++;

                                z = 1/(z*z*z) + C;
                                iteration++;

                                double smooth = iteration - (Math.Log(Math.Log(z.Modulus))) / logEscR;
                                colorIndex = (int)(smooth / maxIter * 768);

                                if (colorIndex >= 768)
                                {
                                    colorIndex = 0;
                                }

                                if (colorIndex < 0)
                                {
                                    colorIndex = 0;
                                }
                            }
                            pxs[index] = colors[colorIndex].B;
                            pxs[index + 1] = colors[colorIndex].G;
                            pxs[index + 2] = colors[colorIndex].R;
                            pxs[index + 3] = 255;

                            z = C;
                        }
                        break;

                    case 9:
                        for (int y = 0; y < picMandel.Height; y++)
                        {
                            z.Imag -= yStep;
                            Complex C = z;

                            //alg. wikipedia
                            int iteration = 0;

                            while (z.Modulus < escR && iteration < maxIter)
                            {
                                z = 1/(z*z*z*z) + C;
                                iteration++;
                            }

                            int colorIndex = 0;
                            int index = (y * picMandel.Width + x) * 4;

                            if (iteration < maxIter)
                            {
                                z = 1/(z*z*z*z) + C;
                                iteration++;

                                z = 1/(z*z*z*z) + C;
                                iteration++;

                                double smooth = iteration - (Math.Log(Math.Log(z.Modulus))) / logEscR;
                                colorIndex = (int)(smooth / maxIter * 768);

                                if (colorIndex >= 768)
                                {
                                    colorIndex = 0;
                                }

                                if (colorIndex < 0)
                                {
                                    colorIndex = 0;
                                }
                            }
                            pxs[index] = colors[colorIndex].B;
                            pxs[index + 1] = colors[colorIndex].G;
                            pxs[index + 2] = colors[colorIndex].R;
                            pxs[index + 3] = 255;

                            z = C;
                        }
                        break;

                    case 10:
                        for (int y = 0; y < picMandel.Height; y++)
                        {
                            z.Imag -= yStep;
                            Complex C = z;

                            //alg. wikipedia
                            int iteration = 0;

                            while (z.Modulus < escR && iteration < maxIter)
                            {
                                z = 1/(z*z*z*z*z) + C;
                                iteration++;
                            }

                            int colorIndex = 0;
                            int index = (y * picMandel.Width + x) * 4;

                            if (iteration < maxIter)
                            {
                                z = 1 / (z * z * z * z * z) + C;
                                iteration++;

                                z = 1 / (z * z * z * z * z) + C;
                                iteration++;

                                double smooth = iteration - (Math.Log(Math.Log(z.Modulus))) / logEscR;
                                colorIndex = (int)(smooth / maxIter * 768);

                                if (colorIndex >= 768)
                                {
                                    colorIndex = 0;
                                }

                                if (colorIndex < 0)
                                {
                                    colorIndex = 0;
                                }
                            }
                            pxs[index] = colors[colorIndex].B;
                            pxs[index + 1] = colors[colorIndex].G;
                            pxs[index + 2] = colors[colorIndex].R;
                            pxs[index + 3] = 255;

                            z = C;
                        }
                        break;

                    default:
                        for (int y = 0; y < picMandel.Height; y++)
                        {
                            z.Imag -= yStep;
                            Complex C = z;

                            //alg. wikipedia
                            int iteration = 0;

                            while (z.Modulus < escR && iteration < maxIter)
                            {
                                z = z*z + C;
                                iteration++;
                            }

                            int colorIndex = 0;
                            int index = (y * picMandel.Width + x) * 4;

                            if (iteration < maxIter)
                            {
                                z = z*z + C;
                                iteration++;

                                z = z*z + C;
                                iteration++;

                                double smooth = iteration - (Math.Log(Math.Log(z.Modulus))) / logEscR;
                                colorIndex = (int)(smooth / maxIter * 768);

                                if (colorIndex >= 768)
                                {
                                    colorIndex = 0;
                                }

                                if (colorIndex < 0)
                                {
                                    colorIndex = 0;
                                }
                            }
                            pxs[index] = colors[colorIndex].B;
                            pxs[index + 1] = colors[colorIndex].G;
                            pxs[index + 2] = colors[colorIndex].R;
                            pxs[index + 3] = 255;

                            z = C;
                        }
                        break;
                }
                
            }

            System.Runtime.InteropServices.Marshal.Copy(pxs, 0, pointerBmp, pxs.Length);
            mandelBitmap.UnlockBits(bd);

            picMandel.Invalidate();

            oldP1 = P1;
            oldP2 = P2;
            oldSize = picMandel.Size;
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }

        private void MandelForm_Load(object sender, EventArgs e)
        {
            genColors();
            MandelbrotMnozina(15, 2);
            numericUpDown1.Value = 15;
            radioButton3.Checked = true;

            vyber_d = new List<RadioButton>(new RadioButton[] { radioButton1, radioButton2, radioButton3, 
                radioButton4, radioButton5, radioButton6, radioButton7, radioButton8, radioButton9, 
                radioButton10, radioButton11});
        }


        private void picMandel_Paint(object sender, PaintEventArgs e)
        {
            int x = (picMandel.Width - oldSize.Width) / 2;
            int y = (picMandel.Height - oldSize.Height) / 2;

            e.Graphics.DrawImage(mandelBitmap, x, y);
        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                maxIter = (int)numericUpDown1.Value;
                cyklusVyber();
            }
            catch { }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                mandelBitmap.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            cyklusVyber();
        }
    }
}
