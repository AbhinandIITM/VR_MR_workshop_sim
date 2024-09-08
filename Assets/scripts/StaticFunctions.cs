using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticFunctions
{
    public static float NormalizeAngle1(float angle)
    {
        if (angle < 180)
        {
            return angle;
        }
        else
        {
            angle = angle - 360;
            return angle;
        }
    }

  /*  public static float NormalizeAngle2(float angle) 
    {
        angle = angle % 360;
    }*/
}
