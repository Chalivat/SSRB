using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public GameObject target;

    public void SpawnProjectile()
    {
        Instantiate(projectile, target.transform.position, Quaternion.identity);
    }
}
