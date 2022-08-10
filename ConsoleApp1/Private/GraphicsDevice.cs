using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ConsoleApp1.Public;

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
            if (bytes[0] != color.B || bytes[1] != color.G || bytes[2] != color.R || bytes[3] != color.A)
            {
                for (var i = 0; i < bytes.Length; i += 4)
                {
                    bytes[i] = color.B;
                    bytes[i + 1] = color.G;
                    bytes[i + 2] = color.R;
                    bytes[i + 3] = color.A;
                }
            }
            var bits = canvas.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, canvas.PixelFormat);
            Marshal.Copy(bytes, 0, bits.Scan0, bytes.Length);
            canvas.UnlockBits(bits);
            //canvasGraphics.Clear(color);
        }

        public void DrawLine(Vector v0, Vector v1, Color color)
        {
            Point p0 = new Point((int)(v0.X * width), (int)(v0.Y * height));
            Point p1 = new Point((int)(v1.X * width), (int)(v1.Y * height));
            DrawLine(p0, p1, color);
        }

        public void DrawLine(Point p0, Point p1, Color color)
        {
            canvasGraphics.DrawLine(new Pen(color), p0, p1);
        }

        public void DrawString(string str, Font font, Brush brush, float x, float y)
        {
            canvasGraphics.DrawString(str, font, brush, x, y);
        }

        public void DrawMesh(Mesh mesh)
        {
            Matrix perspective = //Matrix.PerspectiveFovLH((float)(Math.PI / 2.0), width / height, 0.1f, 1.0f);
                Matrix.ReversedZPerspective(90 / 2.0f, width , height, 1.0f, 200.0f);

            Matrix view = Matrix.LookAtLH(Vector.Zero, Vector.Zero);

            Matrix matrix = view * perspective;

            RenderData meshData = mesh.GetRenderData();
            int numTriangles = meshData.indexBuffer.Length / 3;
            for(int i = 0; i < numTriangles; ++i)
            {
                Vector v0 = meshData.vertexBuffer[meshData.indexBuffer[i * 3 + 0]];
                Vector v1 = meshData.vertexBuffer[meshData.indexBuffer[i * 3 + 1]];
                Vector v2 = meshData.vertexBuffer[meshData.indexBuffer[i * 3 + 2]];

                v0 = matrix.Transform(v0);
                v0 = (v0 + Vector.One) / 2;
                v1 = matrix.Transform(v1);
                v1 = (v1 + Vector.One) / 2;
                v2 = matrix.Transform(v2);
                v2 = (v2 + Vector.One) / 2;

                DrawLine(v0, v1, Color.Black);
                DrawLine(v1, v2, Color.Black);
                DrawLine(v0, v2, Color.Black);
            }

        }

        private void SetPixel(int x, int y, Color color)
        {
            canvas.SetPixel(x, y, color);
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
