using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelDesignTools
{
    public static class MeshGenerator
    {

        public static Mesh MeshFromCurve(Curve curve, float lineFatSize)
        {
            if (curve == null)
                return null;
            Mesh mesh = LinesFromDots(curve, lineFatSize);
            mesh.name = "MeshFromLine - " + mesh.GetHashCode();
            return mesh;
        }

        public static Mesh CircleMesh(float radius = 1f, float angle= 360,float steps = 16)
        {
            List<Vector3> dotsList = new List<Vector3>();
            List<int> vertices = new List<int>();

            //Circle center
            dotsList.Add(Vector3.zero);
            float currentAngle = 0;
            float stepAngle =( angle/(steps)) * Mathf.Deg2Rad;

            //Creating dots
            dotsList.AddRange(GetDotsAround(radius, steps, currentAngle, stepAngle));
            
            //Creating vertices
            for (int i = 2; i <= steps+1; i++)
            {
                vertices.Add(0);
                vertices.Add(i);
                vertices.Add(i-1);
            }



            Mesh resultMesh = new Mesh();
            resultMesh.vertices = dotsList.ToArray();
            resultMesh.triangles = vertices.ToArray();
            resultMesh.name = dotsList.Count+ Random.Range(-100000, 100000).ToString();

            return resultMesh;
        }

        public static List<Vector3> GetDotsAround(float radius, float steps, float startAngle, float stepAngle)
        {
            List<Vector3> dotsList = new List<Vector3>();
            for (int i = 0; i <= steps; i++)
            {
                Vector3 point = AngleToPoint(radius, startAngle);
                dotsList.Add(point);
                startAngle += stepAngle;
            }
            return dotsList;
        }

        public static Vector3 AngleToPoint(float radius, float angle)
        {
            return new Vector3(Mathf.Cos(angle),Mathf.Sin(angle),0) * radius;
        }

        public static Mesh LinesFromDots(Curve curve, float lineFatSize, bool autoRotate=true)
        {
            List<MeshSquare> squares =new List<MeshSquare>();
            if (curve.CurveDots == null)
                return null;
            CurveDot curveDot = curve.CurveDots[0];
            CurveDot curveDotNext = curve.CurveDots[1];
            squares.Add(new MeshSquare(curveDot.Point, curveDotNext.Point, curveDot.Angle, lineFatSize));
            for (int i = 1; i < curve.CurveDots.Length; i++)
            {
                 curveDot = curve.CurveDots[i-1];
                 curveDotNext = curve.CurveDots[i];
                squares.Add(new MeshSquare(curveDot.Point,curveDotNext.Point,curveDot.Angle,lineFatSize));
            }

            Mesh joinedSquares = JoinSquers(squares.ToArray(),lineFatSize);

            return joinedSquares;
        }

        private static Mesh JoinSquers(MeshSquare[] meshSquares,float linefat)
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
                firstDot = (firstDot - margeCenter).normalized*(linefat*0.5f);
                secondDot = (secondDot - margeCenter).normalized*(linefat*0.5f);

                dotsList.Add(firstDot+margeCenter);
                dotsList.Add(secondDot+margeCenter);

                Vector3 dot = Vector3.Lerp(meshSquares[i].RightDown,meshSquares[i].RightUp,0.5f);

                if (firstDot.x < dot.x)
                    vertices.AddRange(MeshSquare.SquareVerticesRight(i*2));
                else
                    vertices.AddRange(MeshSquare.SquareVerticesRight((i)*2));
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
        public static Mesh[] MeshLineToSquares(Mesh mesh)
        {
            List<Mesh> meshes = new List<Mesh>();
            for (int i = 1; i < mesh.vertices.Length/2; i++)
            {
                Mesh tempMesh = new Mesh();

                int r = i*2;
                //Vertices
                List<Vector3> vertices = new List<Vector3>(4);
                vertices.Add(mesh.vertices[r-2]);
                vertices.Add(mesh.vertices[r-1]);
                vertices.Add(mesh.vertices[r]);
                vertices.Add(mesh.vertices[r+1]);
                tempMesh.vertices = vertices.ToArray();

                tempMesh.triangles = mesh.triangles.SubArray(0, 6);

                tempMesh.name = "line piece " + (i-1);

                meshes.Add(tempMesh);
            }
            return meshes.ToArray();
        }

    }
}