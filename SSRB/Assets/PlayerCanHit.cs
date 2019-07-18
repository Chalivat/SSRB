using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanHit : MonoBehaviour
{
    public BoxCollider swordCollider;

    public void canHit()
    {
        swordCollider.enabled = true;
    }

    public void cannontHit()
    {
        swordCollider.enabled = false;
    }
}
