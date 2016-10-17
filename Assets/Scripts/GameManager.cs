using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class GameManager:MonoBehaviour
{
    public bool TestTouch;
    public FingerPoolManager FingerPool;
    public Camera MainCamera;
    public float ZDistance;
    private IController[] _currentContrllers;

    public void Start()
    {
        _currentContrllers = ControllerUtilities.ControllerForCurrentPlatform(MainCamera, ZDistance,TestTouch);
    }

    public void Update()
    {
        ControllerUpdate();
    }

    private void ControllerUpdate()
    {
        List<InputData> inputs = new List<InputData>();
        //Collect all touches
        for (int j = 0; j < _currentContrllers.Length; j++)
        {
            _currentContrllers[j].Update();
            if (_currentContrllers[j].IsActive())
                inputs.AddRange(_currentContrllers[j].GetInputs());
        }

        //Making fingers for all touches
        if (inputs.Count > 0)
        {
            Transform[] fingers = FingerPool.GetFingerCount(inputs.Count);
            for (int i = 0; i < inputs.Count; i++)
                SetFingerToPoint(fingers[i], inputs[i].Postion);
        }
        else
            FingerPool.AllToSleep();
    }

    private void SetFingerToPoint(Transform activeFinger, Vector2 mousePosition)
    {
        activeFinger.transform.position = new Vector3(mousePosition.x,mousePosition.y,activeFinger.position.z);
    }
}