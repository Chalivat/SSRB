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
        float x = Input.GetAxis("Strafe");
        float y = -Input.GetAxis("Forward");

        Vector3 forward = cam.transform.rotation * Vector3.forward;
        Vector3 Strafe = cam.transform.rotation * Vector3.right;

        Debug.Log(x +" ; " + y);
        rb.velocity = cam.transform.rotation * Vector3.forward + cam.transform.rotation * Vector3.right;
    }
}
