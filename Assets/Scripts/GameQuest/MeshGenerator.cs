using System.Collections.Generic;
using UnityEngine;

namespace GameQuest
{
    public class MeshGenerator
    {
        public static Mesh LinesFromDots(Vector3[] dots)
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

            for (int i = 0; i <meshSquares.Length ; i++)
            {
                if(meshSquares[i].LeftUp.x<meshSquares[i].RightUp.x)
                    vertices.AddRange(MeshSquare.SquareVerticesRight(i*2));
                else
                    vertices.AddRange(MeshSquare.SquareVerticesLeft(i*2));

                dotsList.Add(meshSquares[i].RightDown);
                dotsList.Add(meshSquares[i].RightUp);
            }

            Mesh resultMesh= new Mesh();
            resultMesh.vertices = dotsList.ToArray();
            resultMesh.triangles = vertices.ToArray();
            return resultMesh;
        }
    }
}