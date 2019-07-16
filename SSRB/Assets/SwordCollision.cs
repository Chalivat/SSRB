using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    public EnnemiEpee epee;

    void Start()
    {
        epee.GetComponent<EnnemiEpee>();
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerAttack.canDeflect)
        {
            epee.asBeenDeflected = true;
        }
    }
}
