using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Private
{
    public class Scene
    {
        public static Scene Instance 
        { 
            get
            {
                if (Instance == null)
                {
                    return new Scene();
                }
                else
                {
                    return Instance;
                }
            }
            private set { Instance = value; }
        }

        private Scene()
        {

        }

        public RenderData GetRenderData()
        {
            RenderData renderData = new RenderData();

            foreach(var mesh in meshs)
            {
                RenderData meshData = mesh.GetRenderData();

                renderData.Append(meshData);
            }

            return renderData;
        }

        public Mesh[] meshs;
    }
}
