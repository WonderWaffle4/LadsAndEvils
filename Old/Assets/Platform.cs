using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float speed;
    public Transform first_pos;
    public Transform second_pos;
    public Vector3 lastPosition;
    public Vector3 deltaPosition;
    private bool moving_right = true;
    private Vector3 start;
    private Vector3 finish;

    public void Start()
    {
        start = first_pos.position;
        finish = second_pos.position;
    }

    //public void Update()
    //{
        
        
    //}

    public void FixedUpdate()
    {
        lastPosition = transform.position;
        PlatformMove();
        deltaPosition = transform.position - lastPosition;
        print(deltaPosition.x);
    }

    private void PlatformMove()
    {
        if(transform.position.x >= start.x)
        {
            moving_right = false;
        }
        else if(transform.position.x <= finish.x)
        {
            moving_right = true;
        }

        if (moving_right)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }
}
