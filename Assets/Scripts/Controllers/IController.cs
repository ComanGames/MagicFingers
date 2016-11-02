using UnityEngine;

namespace Controllers
{
    public interface IController
    {
        void Initialize(Camera main,float zDistance);
        bool IsActive();
        void Update();
        DataPoint[] GetInputs();
        
    }
}