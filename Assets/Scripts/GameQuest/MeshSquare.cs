using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameQuest
{
    public class MeshSquare
    {
        public Vector3 LeftUp;
        public Vector3 LeftDown;
        public Vector3 RightUp;
        public Vector3 RightDown;

        public int[] Vertices;
        public Vector3[] Dots;

        public MeshSquare(Vector3 startPoint,Vector3 finalPoint,float fat)
        {

            LeftDown = startPoint - (Vector3.up*fat/2f);    //0
            LeftUp = startPoint + (Vector3.up*fat/2f);      //1

            RightDown = finalPoint - (Vector3.up*fat/2f);   //2
            RightUp = finalPoint + (Vector3.up*fat/2f);     //3

            Dots = new [] {LeftDown,LeftUp,RightDown,RightUp};
            Vertices = SquareVerticesRight(0);
        }

        public static int[] SquareVerticesRight(int shift)
        {
            int[] vertices = new [] {0,1,3,0,3,2};
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