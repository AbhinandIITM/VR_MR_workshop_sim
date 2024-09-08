using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneRotateTranslate : MonoBehaviour
{
    public enum Axis
    {
        X,
        Y,
        Z
    }

    [SerializeField]
    private GameObject handle;

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

    private float handleVal;

    private void Update()
    {
        handleVal = StaticFunctions.NormalizeAngle1(GetRotationValue(handle.transform.localEulerAngles, rotateAxis));
        // Debug.Log(handleVal);

        MoveWorkspace(handleVal);
    }

    private void MoveWorkspace(float handleVal)
    {
        // Check to avoid division by zero
        float rotateRange = rotateMax - rotateMin;
        if (rotateRange == 0)
        {
            Debug.LogError("rotateMax and rotateMin are equal, causing division by zero.");
            return;
        }

        float val = ((handleVal - rotateMin) / rotateRange) * (translateMax - translateMin) + translateMin;
        // Debug.Log(handleVal + "  " + val);

        if (float.IsNaN(val))
        {
            Debug.LogError("Calculated val is NaN.");
            return;
        }

        Vector3 newPosition = transform.localPosition;

        switch (translateAxis)
        {
            case Axis.X:
                newPosition.x = val;
                break;
            case Axis.Y:
                newPosition.y = val;
                break;
            case Axis.Z:
                newPosition.z = val;
                break;
        }

        transform.localPosition = newPosition;
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
}
