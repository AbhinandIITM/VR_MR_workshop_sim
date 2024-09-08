using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    private bool IsRotate;

    [SerializeField]
    private float speed;
    private void Start()
    {
        IsRotate = false;
    }

    private void Update()
    {   

        if (IsRotate)
        {
            transform.Rotate(new Vector3(0, 0, -1) *speed* Time.deltaTime);
        }
    }


    public void RotateSwitch()
    {
        IsRotate = !IsRotate;
    }


}
