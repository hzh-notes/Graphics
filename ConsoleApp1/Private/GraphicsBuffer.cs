using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConsoleApp1.Private
{
    public class GraphicsBuffer
    {
        public GraphicsBuffer(int width, int height)
        {
            Current = new Bitmap(width, height);
            Background = new Bitmap(width, height);

            CurrentGraphics = new GraphicsDevice(Current);
            BackgroundGraphics = new GraphicsDevice(Background);
        }

        public void SwapBuffers()
        {
            var t = Current;
            Current = Background;
            Background = t;
        }

        public Bitmap Current { get; private set; }
        public Bitmap Background { get; private set; }

        public GraphicsDevice CurrentGraphics { get; private set; }
        public GraphicsDevice BackgroundGraphics { get; private set; }

    }
}
