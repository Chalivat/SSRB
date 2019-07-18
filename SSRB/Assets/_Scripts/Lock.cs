using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Lock : MonoBehaviour
{
    public List<GameObject> ennemies;
    public static GameObject Target;
    public Image cursor;
    private int index;
    void Start()
    {
        ennemies = GameObject.FindGameObjectsWithTag("Swordman").ToList();
    }
    
    void Update()
    {

        for (int i = 0; i < ennemies.Count; i++)
        {
            if (ennemies[i] == null)
            {
                ennemies.RemoveAt(i);
            }
        }
        if (Input.GetButtonDown("Lock"))
        {
            if (index >= ennemies.Count -1)
            {
                index = 0;
            }
            else index++;
        }

        if (ennemies.Count != 0)
        {
            Target = ennemies[index];
            cursor.transform.position = Camera.main.WorldToScreenPoint(Target.transform.position) + Vector3.up;
        }

    }
}
