using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class RotateCamera : MonoBehaviour
{

    public float rotateSpeed;
    public float autoAimSpeed;

    void Start()
    {
        
    }
    
    void Update()
    {
        Aim();
    }

    void Aim()
    {
        if (Lock.Target)
        {
            Quaternion newRot = Quaternion.LookRotation(Lock.Target.transform.position + Vector3.up * 2 - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation,newRot,autoAimSpeed * Time.deltaTime);
        }
        else
        {
            float x = rotateSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            float y = rotateSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
            transform.Rotate(-x, 0, 0);
            transform.Rotate(0, y, 0, Space.World);
        }
    }
}
