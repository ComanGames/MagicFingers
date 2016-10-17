using UnityEngine;

namespace Controllers
{
    public class InputData
    {
        public Vector2 Postion;
        public bool IsActive { get; set; }

        public InputData(Vector2 postion, bool isActive)
        {
            Postion = postion;
            IsActive = isActive;
        }
    }
}