using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayGenerator : MonoBehaviour {

    public GameObject template;
    // Use this for initialization
	void Start () {
        //creating waypoints via coords
		foreach (Vector3 v in Way.lCoord)
        {
           Way.left.Add(Instantiate(template, v, Quaternion.identity));        
        }
        foreach (Vector3 v in Way.cCoord)
        {
           Way.center.Add(Instantiate(template, v, Quaternion.identity));
        }
        foreach (Vector3 v in Way.rCoord)
        {
           Way.right.Add(Instantiate(template, v, Quaternion.identity));  
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
