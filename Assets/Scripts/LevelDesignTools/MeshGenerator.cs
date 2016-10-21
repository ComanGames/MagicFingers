using System.Collections.Generic;
using UnityEngine;

namespace LevelDesignTools
{
    public static class MeshGenerator
    {

        public static Mesh MeshFromCurve(Vector3[] curvedDots)
        {
            Mesh mesh = LinesFromDots(curvedDots);
            mesh.name = "MeshFromLine - " + mesh.GetHashCode();
            return mesh;
        }
        public static Mesh LinesFromDots(Vector3[] dots, bool autoRotate=true)
        {
            List<MeshSquare> squares =new List<MeshSquare>();
            for (int i = 0; i < dots.Length-1; i++)
                squares.Add(new MeshSquare(dots[i],dots[i+1],GizmosSettings.LineFatSize));
            Mesh joinedSquares = JoinSquers(squares.ToArray());

            return joinedSquares;
        }

        private static Mesh JoinSquers(MeshSquare[] meshSquares)
        {
            List<Vector3> dotsList = new List<Vector3>();
            List<int> vertices = new List<int>();
            dotsList.Add(meshSquares[0].LeftDown);
            dotsList.Add(meshSquares[0].LeftUp);

            Vector3 fDot = Vector3.Lerp(meshSquares[0].LeftDown, meshSquares[0].LeftUp, 0.5f);
            Vector3 sDot = Vector3.Lerp(meshSquares[0].RightUp, meshSquares[0].RightDown, 0.5f);

            if(fDot.x < sDot.x)
            vertices.AddRange(MeshSquare.SquareVerticesRight(0));
            else
            vertices.AddRange(MeshSquare.SquareVerticesRight(0));



            for (int i = 1; i <meshSquares.Length ; i++)
            {


                Vector3 firstDot = Vector3.Lerp(meshSquares[i-1].RightDown,meshSquares[i].LeftDown,0.5f);
                Vector3 secondDot = Vector3.Lerp(meshSquares[i-1].RightUp,meshSquares[i].LeftUp,0.5f);


                Vector3 margeCenter=Vector3.Lerp(firstDot,secondDot,0.5f);
                firstDot = (firstDot - margeCenter).normalized*(GizmosSettings.LineFatSize*0.5f);
                secondDot = (secondDot - margeCenter).normalized*(GizmosSettings.LineFatSize*0.5f);

                dotsList.Add(firstDot+margeCenter);
                dotsList.Add(secondDot+margeCenter);

                Vector3 dot = Vector3.Lerp(meshSquares[i].RightDown,meshSquares[i].RightUp,0.5f);
                if (firstDot.x < dot.x)
                {
                    vertices.AddRange(MeshSquare.SquareVerticesRight(i*2));
                }
                else
                {
                    vertices.AddRange(MeshSquare.SquareVerticesRight((i)*2));
                }


            }
            int j = meshSquares.Length-1;
            vertices.AddRange(MeshSquare.SquareVerticesRight(j * 2));

            dotsList.Add((meshSquares[j].RightDown));
            dotsList.Add((meshSquares[j].RightUp));


            Mesh resultMesh = new Mesh();
            resultMesh.vertices = dotsList.ToArray();
            resultMesh.triangles = vertices.ToArray();
            return resultMesh;
        }
    }
}