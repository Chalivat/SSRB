using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    public int vie;
    public Slider healthBar;
    void Start()
    {
        vie = 10;
    }
    
    void Update()
    {
        healthBar.value = vie;
    }
}
