using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Public
{
    public struct Transform
    {
        public Transform(Vector position)
        {
            this.position = position;
            rotation = Vector.Zero;
            scale = Vector.One;
        }

        public Transform(Vector position,Vector rotation)
        {
            this.position = position;
            this.rotation = rotation;
            scale = Vector.One;
        }

        public Transform(Vector position, Vector rotation, Vector scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }

        public Matrix GetMatrixWithScale()
        {
            var translateMat = Matrix.Translation(position);
            var rotateMat = Matrix.Rotation(rotation);
            var scaleMat = Matrix.Scale(scale);

            return scaleMat * rotateMat * translateMat;
        }

        public Matrix GetMatrixNoScale()
        {
            var translateMat = Matrix.Translation(position);
            var rotateMat = Matrix.Rotation(rotation);
            var scaleMat = Matrix.Scale(Vector.One);

            return scaleMat * rotateMat * translateMat;
        }

        public Vector position;
        public Vector rotation;
        public Vector scale;


    }
}
