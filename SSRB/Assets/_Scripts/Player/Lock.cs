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
    private bool lastInput;
    private bool isLocked;
    void Start()
    {
        ennemies = GameObject.FindGameObjectsWithTag("Swordman").ToList();
        ennemies = new List<GameObject>(ennemies.Concat(GameObject.FindGameObjectsWithTag("Distance").ToList()));
    }
    
    void Update()
    {
        
        for (int i = 0; i < ennemies.Count; i++)
        {
            if (ennemies[i] == null)
            {
                if (index >= ennemies.Count - 1)
                {
                    index = 0;
                }
                else index++;
                ennemies.RemoveAt(i);
            }
        }
        if (Input.GetButtonDown("Lock"))
        {
            isLocked = !isLocked;
        }

        if (ennemies.Count != 0)
        {
            ChangeSelection();
        }
        if (ennemies.Count != 0 && isLocked)
        {
            Target = ennemies[index];
            cursor.transform.position = Camera.main.WorldToScreenPoint(Target.transform.position + Vector3.up);
            
        }
        else Target = null;
    }

    void ChangeSelection()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && !lastInput)
        {
            lastInput = true;

            if (Input.GetAxisRaw("Horizontal") > 0) //up
            {
                if (index == 0)
                {
                    index = ennemies.Count - 1;
                }
                else index--;
            }

            if (Input.GetAxisRaw("Horizontal") < 0) //down
            {
                if (index >= ennemies.Count - 1)
                {
                    index = 0;
                }
                else index++;
            }
        }

        if (Input.GetAxisRaw("Horizontal") == 0 && lastInput)
        {
            lastInput = false;
        }
    }

    
}
