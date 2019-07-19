using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldLaunch : MonoBehaviour
{

    public GameObject Shield;
    public float speed;
    private Rigidbody rb;
    private GameObject target;

    void Start()
    {
        
    }
    
    void Update()
    {
        rb.velocity = Vector3.Lerp(rb.velocity, (target.transform.position - transform.position).normalized * rb.velocity.magnitude, 0.5f);
    }

    void Launch(Vector3 direction,GameObject Target)
    {
        rb.AddForce(direction * speed,ForceMode.Impulse);
    }
}
