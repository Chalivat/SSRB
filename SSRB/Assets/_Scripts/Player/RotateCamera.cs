using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class RotateCamera : MonoBehaviour
{

    public float rotateSpeed;

    void Start()
    {
        
    }
    
    void Update()
    {
        float x = rotateSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        float y = rotateSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        transform.Rotate(-x, 0, 0);
        transform.Rotate(0, y, 0, Space.World);
    }
}
