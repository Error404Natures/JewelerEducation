using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingOld : MonoBehaviour
{
    public GameObject[] ZoneSanding;
    public int currentTouch = 0;

    private void Start()
    {
        for (int i = 0; i < ZoneSanding.Length; i++)
        {
            ZoneSanding[i].gameObject.SetActive(false);
        }
        ZoneSanding[currentTouch].gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Sanding")
        {
            ZoneSanding[currentTouch].gameObject.SetActive(false);
            currentTouch++;
            ZoneSanding[currentTouch].gameObject.SetActive(true);
             
            if (currentTouch == ZoneSanding.Length - 1)
            {
                StateManagerOld.instance.UpdateState();
            }
        }

    }
}
