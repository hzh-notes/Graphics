using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using ConsoleApp1.Public;

namespace ConsoleApp1.Private
{
    public class AppDelegate
    {
        public AppDelegate()
        {
            LoadContent();
        }

        private void LoadContent()
        {
            form = new Form
            {
                Size = new Size(width, height),
                StartPosition = FormStartPosition.CenterScreen
            };
            formGraphics = form.CreateGraphics();
            buffer = new GraphicsBuffer(width, height);
            defaultFont = new Font(new FontFamily("Comic Sans MS"), 14);
        }

        private void Present()
        {
            formGraphics.DrawImage(buffer.Current, Point.Empty);
        }

        public void Run()
        {
            var stopwatch = new Stopwatch();
            //第一帧消耗的时间
            var deltatime = TimeSpan.FromMilliseconds(1000.0 / 60);

            form.Show();

            while (!form.IsDisposed)
            {
                stopwatch.Start();

                Render(deltatime);
                buffer.SwapBuffers();
                Present();
                Application.DoEvents();

                if (stopwatch.Elapsed < maxElapsedTime)
                {
                    Thread.Sleep(maxElapsedTime - stopwatch.Elapsed);
                }
                else
                {
                    deltatime = maxElapsedTime;
                }

                stopwatch.Stop();
                deltatime = stopwatch.Elapsed;
                stopwatch.Reset();
            }
        }

        /// <summary>
        /// 渲染入口
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Render(TimeSpan deltaTime)
        {
            var graphics = buffer.BackgroundGraphics;

            graphics.Clear(Color.White);
            //graphics.DrawLine(new Point(0, 0), new Point(200, 200), Color.Black);
            graphics.DrawMesh(
                new CubeMesh(
                    new Transform(
                        new Vector(50, 0, 0),
                        new Vector(0, 0, 60),
                        new Vector(0.2f, 0.2f, 0.2f)
                        )
                    )
                );
            graphics.DrawString($"FPS: {1000.0 / deltaTime.Milliseconds:F2}",
                defaultFont, Brushes.Black, 0, 0);

        }

        private const int width = 1024;
        private const int height = 768;
        private GraphicsBuffer buffer;
        private Graphics formGraphics;

        private Form form;
        private Font defaultFont;

        //定义要稳定到的帧率为30
        private readonly TimeSpan maxElapsedTime = TimeSpan.FromMilliseconds(1000.0 / 30);
    }
}
