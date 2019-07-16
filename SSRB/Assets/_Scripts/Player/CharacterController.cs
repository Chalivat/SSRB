using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public Camera cam;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }
    
    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        float x = Input.GetAxis("Strafe");
        float z = -Input.GetAxis("Forward");


        Vector3 direction = new Vector3(x, 0, z);
        Quaternion newRot = Quaternion.LookRotation(cam.transform.forward);
        Vector3 nextRot = newRot.eulerAngles;
        nextRot.x = 0;
        nextRot.z = 0;
        newRot = Quaternion.Euler(nextRot);

        direction = newRot * direction;
        Debug.Log(direction.magnitude);
        if (direction.magnitude >= 1f)
        {
            rb.velocity = direction * speed;
        }
    }

    void Rotate()
    {
        Quaternion newRot = Quaternion.LookRotation(rb.velocity);
        Vector3 nextRot = newRot.eulerAngles;
        nextRot.x = 0;
        nextRot.z = 0;
        newRot = Quaternion.Euler(nextRot);

       
        transform.rotation = Quaternion.Lerp(transform.rotation,newRot,0.5f);
    }
}
