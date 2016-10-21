using System.Linq;
using UnityEngine;

namespace LevelDesignTools
{
    public class MeshSquare
    {
        public Vector3 LeftUp;
        public Vector3 LeftDown;
        public Vector3 RightUp;
        public Vector3 RightDown;

        public int[] Vertices;
        public Vector3[] Dots;

        public MeshSquare(Vector3 startPoint,Vector3 finalPoint,float fat,bool withRot=true)
        {
            LeftDown = startPoint - (Vector3.up*fat/2f); //0
            LeftUp = startPoint + (Vector3.up*fat/2f); //1

            RightDown = finalPoint - (Vector3.up*fat/2f); //2
            RightUp = finalPoint + (Vector3.up*fat/2f); //3

            if (withRot)
            {
                Vector3 center = Vector3.Lerp(startPoint, finalPoint, 0.5f);
                Vector3 hulfOfWidth= Vector3.Distance(startPoint,finalPoint)*Vector3.right*0.5f;
                float angle =Vector3.Angle(new Vector3(0,1,0),(finalPoint-startPoint).normalized);
                Vector3 direction = new Vector3(0,0, ( (startPoint.x<finalPoint.x)?90-angle:angle+90));

                LeftDown = center -hulfOfWidth - (Vector3.up * fat / 2f); //0
                LeftUp = center -hulfOfWidth + (Vector3.up * fat / 2f); //0

                RightUp = center +hulfOfWidth + (Vector3.up * fat / 2f); //0
                RightDown = center +hulfOfWidth - (Vector3.up * fat / 2f); //0



                LeftDown = RotatePointAroundPivot(LeftDown, center, direction);
                LeftUp = RotatePointAroundPivot(LeftUp, center, direction);

                RightUp = RotatePointAroundPivot(RightUp, center, direction);
                RightDown = RotatePointAroundPivot(RightDown, center, direction);




            }

            Dots = new [] {LeftDown,LeftUp,RightDown,RightUp};
            Vertices = SquareVerticesRight(0);
        }
        public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
        {
            Vector3 dir = point - pivot; // get point direction relative to pivot
            dir = Quaternion.Euler(angles) * dir; // rotate it
            point = dir + pivot; // calculate rotated point
            return point; // return it
        }

        public static int[] SquareVerticesRight(int shift)
        {
            int[] vertices = new [] {0,1,3,0,3,2};
            vertices = (from x in vertices
                       select x + shift).ToArray();
            return vertices;
        }
        public static int[] SquareVerticesRightDown(int shift)
        {
            int[] vertices = new [] {0,2,3,0,3,1};
            vertices = (from x in vertices
                       select x + shift).ToArray();
            return vertices;
        }

        public static int[] SquareVerticesLeft(int shift)
        {
            int[] vertices = new [] {3,1,0,2,3,0};
            vertices = (from x in vertices
                       select x + shift).ToArray();
            return vertices;
        }
    }
}