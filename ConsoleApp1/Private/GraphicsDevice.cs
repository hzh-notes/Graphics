using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ConsoleApp1.Private
{
    public class GraphicsDevice : IDisposable
    {
        public GraphicsDevice(Bitmap bitmap)
        {
            width = bitmap.Width;
            height = bitmap.Height;
            bytes = new byte[width * height * 4];
            canvas = bitmap;
            canvasGraphics = Graphics.FromImage(canvas);
        }

        public void Clear(Color color)
        {
            //if (bytes[0] != color.B || bytes[1] != color.G || bytes[2] != color.R || bytes[3] != color.A)
            //{
            //    for (var i = 0; i < bytes.Length; i += 4)
            //    {
            //        bytes[i] = color.B;
            //        bytes[i + 1] = color.G;
            //        bytes[i + 2] = color.R;
            //        bytes[i + 3] = color.A;
            //    }
            //}
            //var bits = canvas.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, canvas.PixelFormat);
            //Marshal.Copy(bytes, 0, bits.Scan0, bytes.Length);
            //canvas.UnlockBits(bits);
            canvasGraphics.Clear(color);
        }
        public void DrawLine(Point p0, Point p1, Color color)
        {
            canvasGraphics.DrawLine(new Pen(color), p0, p1);
        }

        public void DrawString(string str, Font font, Brush brush, float x, float y)
        {
            canvasGraphics.DrawString(str, font, brush, x, y);
        }

        public void Dispose()
        {
            canvasGraphics.Dispose();
        }

        private int width;
        private int height;

        private byte[] bytes;

        private readonly Bitmap canvas;
        private readonly Graphics canvasGraphics;


    }
}
