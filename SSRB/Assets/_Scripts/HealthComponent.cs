using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    public int vie;
    public Slider healthBar;
    public Animator anim;
    
    void Start()
    {
        vie = 10;
        healthBar.value = vie;
        
    }
    
    void Update()
    {
        
    }

    public void PlayerGetHit(int damage)
    {
        vie -= damage;
        healthBar.value = vie;
        anim.Play("Knocked");
        CameraShake.ShakeCamera(0.2f,0.2f);
    }

    public void PlayerGetDeflected()
    {
        anim.Play("Knocked");
        CameraShake.FreezeTime();
        CameraShake.ShakeCamera(0.1f, 0.1f);
    }

    public void PlayerDeflected()
    {

    }

    public void PlayerHit()
    {

    }

    
}
