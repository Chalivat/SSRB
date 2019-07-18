using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanHit : MonoBehaviour
{
    public BoxCollider swordCollider;
    public CharacterController controller;
    public TrailRenderer ps;

    void Start()
    {
        ps.emitting = false;
    }

    public void canHit()
    {
        swordCollider.enabled = true;
        ps.emitting = true;
    }

    public void cannontHit()
    {
        swordCollider.enabled = false;
        ps.emitting = false;
    }


    public void canMove()
    {
        controller.enabled = true;
    }

    public void cannotMove()
    {
        controller.enabled = false;
    }
}
