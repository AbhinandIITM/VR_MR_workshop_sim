using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnAngleAchieve : MonoBehaviour
{

    
    private enum Axis{
        X,Y,Z
    }
    [SerializeField]
    private Axis axis;

    [SerializeField]
    private float angle;


    [SerializeField]
    private GameObject objectInteractable;

    private void Start()
    {

    }

    private void Update()
    {
        if (CheckAngleAchieve())
        {
            objectInteractable.SetActive(true);
        }
        else 
        { 
            objectInteractable.SetActive(false);
        }

    }

    private bool CheckAngleAchieve()
    {
        switch (axis)
        {
            case Axis.X:
                if(Mathf.Abs(transform.localEulerAngles.x-angle) < 1)
                {
                    return true;
                }
                break;
            case Axis.Y:
                if (Mathf.Abs(transform.localEulerAngles.y - angle) < 1)
                {

                    return true;
                }
                break;
            case Axis.Z:
                if (Mathf.Abs(transform.localEulerAngles.z - angle) < 1)
                { 
                    return true;
                }
                break;
        }
        return false;
    }
}
