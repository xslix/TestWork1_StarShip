using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 30f;
    public enum tWayType { l = 0, c = 1, r = 2 };
    public float delta = 0.5f;
    public float rotationSpeed = 3f;
    public float acceleration = 5f;

    public tWayType curWayNum = tWayType.c;
    public tWayType redirectWayNum = tWayType.c;
    private int curPoint = 1; //moving target point index
    private float speed = 10f;
    private float redirectingProcess = 0;


    Vector3 GetTargetPoint()
    {
        if (isRedirecting()) redirectingProcess+= rotationSpeed * Time.deltaTime * speed/10; //next step
        return Vector3.Lerp(Way.byNum((int)curWayNum)[curPoint + 1], Way.byNum((int)redirectWayNum)[curPoint + 1], 0.1f * redirectingProcess);
        
        // returs a point betweeen (on) ways
    }


	void Start () {
        transform.position = Way.cCoord[0];
        transform.LookAt(Way.cCoord[1]);
        // start position 'n rotation
	}

    bool isRedirecting()
    {
        
        return (curWayNum != redirectWayNum);
    }


    void Update() {
        Vector3 target = GetTargetPoint();
        if (Vector3.Distance(transform.position, target) < delta*speed/5)
        {
            curPoint++;
            target = GetTargetPoint();
        } //move to next point
  
        transform.position += Vector3.Normalize(target - transform.position) * Time.deltaTime * speed; //motion
        if (isRedirecting())  transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target - transform.position), 0.02f*speed/20); //rotation
  
        if (Vector3.Distance(transform.position, Way.byNum((int)redirectWayNum)[curPoint+1]) < delta * speed / 5)
        {
            curWayNum = redirectWayNum;
        } //finish of redirecting
   
        if (!isRedirecting() && Input.GetKeyDown(KeyCode.D))
        {
            redirectWayNum = (tWayType) Mathf.Min((int)redirectWayNum + 1, 2);
            redirectingProcess = 0;
        } //redirect right
        if (!isRedirecting() && Input.GetKeyDown(KeyCode.A))
        {
            redirectWayNum = (tWayType)Mathf.Max((int)redirectWayNum - 1, 0);
            redirectingProcess = 0;
        } //redirect left

        if (!isRedirecting() && Input.GetKey(KeyCode.W))
        {
            speed = Mathf.Min(maxSpeed, speed + acceleration * Time.deltaTime);
        } // + speed

        if (!isRedirecting() && Input.GetKey(KeyCode.S))
        {
            speed = Mathf.Max(0, speed - acceleration * Time.deltaTime);
        } // - speed
    }
    


}
