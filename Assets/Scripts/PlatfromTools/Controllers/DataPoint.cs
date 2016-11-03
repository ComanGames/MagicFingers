using UnityEngine;

namespace PlatfromTools.Controllers
{
    public class DataPoint
    {
        public Vector2 Postion;
        public bool IsActive { get; set; }

        public DataPoint(Vector2 postion, bool isActive)
        {
            Postion = postion;
            IsActive = isActive;
        }
    }
}