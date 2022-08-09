using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleApp1.Public;

namespace ConsoleApp1.Private
{
    public struct RenderData
    {
        public void Append(RenderData data)
        {
            int numVertices = vertexBuffer.Length;
            data.vertexBuffer.CopyTo(vertexBuffer, numVertices);

            for(int i = 0; i < data.indexBuffer.Length; i++)
            {
                indexBuffer.Append(data.indexBuffer[i] + numVertices);
            }
        }

        public Vector[] vertexBuffer;
        public int[] indexBuffer;
    }

    public class Mesh
    {
        public RenderData GetRenderData()
        { 
            return new RenderData(); 
        }

        private RenderData renderData;

        public Transform transform;
    }
}
