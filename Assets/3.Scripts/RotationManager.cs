using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    public bool isRotate = false;

    public Vector2 initRot = Vector2.zero;
    public Vector2 diffRot = Vector2.zero;
    public Transform cameraTrans;

    private void Update()
    {
        if (isRotate)
        {
            RotateObject();
        }
    }
    
    public void StartAdjustment()
    {
        isRotate = true;
        initRot = new Vector2(SensorReadManager.instance.fYaw, SensorReadManager.instance.fRoll);
    }

    void RotateObject()
    {
        diffRot.x = SensorReadManager.instance.fYaw - initRot.x;
        diffRot.y = SensorReadManager.instance.fRoll - initRot.y;

        cameraTrans.localEulerAngles = new Vector3(-diffRot.y, diffRot.x, 0);
    }
}
