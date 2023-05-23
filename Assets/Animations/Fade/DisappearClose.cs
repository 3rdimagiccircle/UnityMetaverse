using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearClose : MonoBehaviour
{
    public GameObject waypoints;
    public GameObject tutorial2;
    public GameObject startWaypoint;
    private void Awake()
    {
        waypoints.SetActive(false);
        if (tutorial2 != null)
        {
            tutorial2.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        //Debug.Log("Menu Destroyed");

        if (tutorial2 != null)
        {
            //spawn next menu
            tutorial2.SetActive(true);
            Debug.Log(tutorial2.activeSelf);
        }


        //spawn next waypoint
        waypoints.SetActive(true);
        //destroy OG waypoint
        Destroy(startWaypoint);
        //deactivate current menu
        gameObject.SetActive(false);

    }
}
