using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelection : MonoBehaviour
{
    Canvas canva;
    void Start()
    {
        canva = GetComponent<Canvas>();
        canva.worldCamera = Camera.main;
    }
    
    void Update()
    {
        
    }
}
