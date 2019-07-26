using System;
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
    private Camera cam;
    private CameraShake cameraShake;

    private Quaternion initialRot;
    void Start()
    {
        ennemies = GameObject.FindGameObjectsWithTag("Swordman").ToList();
        ennemies = new List<GameObject>(ennemies.Concat(GameObject.FindGameObjectsWithTag("Distance").ToList()));
        cam = Camera.main;
        cameraShake = cam.gameObject.GetComponent<CameraShake>();
        initialRot = cam.transform.localRotation;
    }
    
    void Update()
    {

        cursor.gameObject.SetActive(isLocked);
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
        else
        {
            cursor.gameObject.SetActive(false);
        }
        if (ennemies.Count != 0 && isLocked)
        {
            if (ennemies[index] == null)
            {
                if (index >= ennemies.Count)
                {
                    index = 0;
                }
                else index++;
            }
            Target = ennemies[index];
            cursor.transform.position = Camera.main.WorldToScreenPoint(Target.transform.position + Vector3.up);
            cam.gameObject.transform.localRotation = Quaternion.Lerp(cam.gameObject.transform.localRotation, Quaternion.Euler(-10, ChooseSide() * 100, 0), 0.01f * Time.deltaTime);
            //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 50, .1f * Time.deltaTime);
            cameraShake.offset = Vector3.Lerp(cameraShake.offset,new Vector3(-ChooseSide(),-1f,1f), 1f * Time.deltaTime);
            //cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation,Quaternion.LookRotation(Target.transform.position - cam.transform.position),0.01f * Time.deltaTime);
        }
        else
        {
            cam.gameObject.transform.localRotation = Quaternion.Lerp(cam.gameObject.transform.localRotation, Quaternion.Euler(10, 0, 0),0.01f * Time.deltaTime);
            //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,60,.1f * Time.deltaTime);
            cameraShake.offset = Vector3.Lerp(cameraShake.offset, new Vector3(0, 0, -3f), 1f * Time.deltaTime);
            //cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, initialRot, 0.1f * Time.deltaTime);
            Target = null;
        }
    }

    void ChangeSelection()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && !lastInput)
        {
            lastInput = true;
            Sorting();

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

    private float sideOffset = 2f;
    float ChooseSide()
    {
        return Input.GetAxisRaw("Strafe") * sideOffset;
    }
    
    void Sorting()
    {
        ennemies.Sort(SortByScreenPosition);
        ennemies.Reverse();
    }
    int SortByScreenPosition(GameObject a, GameObject b)
    {
        float posA = cam.WorldToScreenPoint(a.transform.position).x;
        float posB = cam.WorldToScreenPoint(b.transform.position).x;
        return posA.CompareTo(posB);
    }


}
