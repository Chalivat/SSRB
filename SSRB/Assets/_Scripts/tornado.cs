using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tornado : MonoBehaviour
{
    
    void Start()
    {
        
    }
    
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 5,Space.Self);
    }
}
