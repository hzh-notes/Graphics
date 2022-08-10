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

    public abstract class Mesh
    {
        public Mesh(Transform transform)
        {
            this.transform = transform;
            renderData = new RenderData();
            Init();
        }

        public RenderData GetRenderData()
        { 
            return renderData; 
        }

        abstract public void Init();

        protected RenderData renderData;

        public Transform transform;
    }

    public class CubeMesh : Mesh
    {
        public CubeMesh(Transform transform) 
            : base(transform)
        {

        }

        public override void Init()
        {
            Vector[] vertices =
            {
                new Vector(-50,-50,50),
                new Vector(-50,-50,-50),
                new Vector(50,-50,-50),
                new Vector(50,-50,50),
                new Vector(-50,50,50),
                new Vector(50,50,50),
                new Vector(50,50,-50),
                new Vector(-50,50,-50),
            };

            int[] indices = 
            { 
                1,3,0,1,2,3,
                0,5,4,0,3,5,
                7,0,4,7,1,0,
                2,5,3,2,6,5,
                7,4,5,7,5,6,
                1,6,2,1,7,6
            };

            renderData.vertexBuffer = vertices;
            renderData.indexBuffer = indices;

            for(int i = 0; i < vertices.Length; i++)
            {
                renderData.vertexBuffer[i] = transform.GetMatrixWithScale().Transform(vertices[i]);
            }
        }
    }
}
