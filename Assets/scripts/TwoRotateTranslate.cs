using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoRotateTranslate : MonoBehaviour
{
    public enum Axis
    {
        X,
        Y,
        Z
    }

    [SerializeField]
    [Header("Handles")]
    private GameObject handle1;
    [SerializeField]
    private GameObject handle2;

    [SerializeField]
    private float translateMin;
    [SerializeField]
    private float translateMax;

    [SerializeField]
    private float rotateMin;
    [SerializeField]
    private float rotateMax;

    [SerializeField]
    private Axis rotateAxis;
    [SerializeField]
    private Axis translateAxis;

    private float handle1PrevVal;
    private float handle2PrevVal;

    private float currentHandle1Val;
    private float currentHandle2Val;

    private void Start()
    {
        handle1PrevVal = GetRotationValue(handle1.transform.localEulerAngles, rotateAxis);
        handle2PrevVal = GetRotationValue(handle2.transform.localEulerAngles, rotateAxis);
    }

    private void Update()
    {
        currentHandle1Val = StaticFunctions.NormalizeAngle1(GetRotationValue(handle1.transform.localEulerAngles, rotateAxis));
        currentHandle2Val = StaticFunctions.NormalizeAngle1(GetRotationValue(handle2.transform.localEulerAngles, rotateAxis));

        if (!Mathf.Approximately(handle1PrevVal, currentHandle1Val))
        {
            SyncHandlesAndPosition(currentHandle1Val, ref handle1PrevVal, handle2);
        }
        else if (!Mathf.Approximately(handle2PrevVal, currentHandle2Val))
        {
            SyncHandlesAndPosition(currentHandle2Val, ref handle2PrevVal, handle1);
        }
    }

    private void SyncHandlesAndPosition(float newYAngle, ref float prevVal, GameObject otherHandle)
    {
        // Sync the rotation of the other handle
        Vector3 otherHandleEulerAngles = otherHandle.transform.localEulerAngles;
        SetRotationValue(ref otherHandleEulerAngles, rotateAxis, newYAngle);
        otherHandle.transform.localEulerAngles = otherHandleEulerAngles;

        // Update the position of the main object
        float normalizedY = (newYAngle - rotateMin) / (rotateMax - rotateMin);
        float y = Mathf.Lerp(translateMin, translateMax, normalizedY);
        Vector3 newPosition = transform.localPosition;

        switch (translateAxis)
        {
            case Axis.X:
                newPosition.x = y;
                break;
            case Axis.Y:
                newPosition.y = y;
                break;
            case Axis.Z:
                newPosition.z = y;
                break;
        }

        if (currentHandle1Val < rotateMax && currentHandle1Val > rotateMin && currentHandle2Val < rotateMax && currentHandle2Val > rotateMin)
        {
            transform.localPosition = newPosition;
        }
        prevVal = newYAngle;
    }

    private float GetRotationValue(Vector3 eulerAngles, Axis axis)
    {
        switch (axis)
        {
            case Axis.X:
                return eulerAngles.x;
            case Axis.Y:
                return eulerAngles.y;
            case Axis.Z:
                return eulerAngles.z;
            default:
                return 0f; // Default case should never be hit
        }
    }

    private void SetRotationValue(ref Vector3 eulerAngles, Axis axis, float value)
    {
        switch (axis)
        {
            case Axis.X:
                eulerAngles.x = value;
                break;
            case Axis.Y:
                eulerAngles.y = value;
                break;
            case Axis.Z:
                eulerAngles.z = value;
                break;
        }
    }
}
