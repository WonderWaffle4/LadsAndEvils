using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int type;//0 - horizontal, 1 - vertical, 2 - angle
    public float wait_time;
    public float speed;
    public Transform first_pos;
    public Transform second_pos;
    public bool moving_right = true;
    private bool moving_up = true;
    private bool moving = true;
    private bool timer_on = false;
    private Vector3 start;
    private Vector3 finish;
    private float timer;

    public void Start()
    {
        start = first_pos.position;
        finish = second_pos.position;
        timer = wait_time;
    }

    public void Update()
    {
        switch (type) {
            case 0:
                PlatformMoveHorizontal();
                break;
            case 1:
                PlatformMoveVertical();
                break;
            case 2:
                PlatformMoveAngle();
                break;
        }
    }

    private void PlatformMoveHorizontal()
    {
        if(transform.position.x >= start.x)
        {
            moving_right = false;
            Timer();       
        }
        else if(transform.position.x <= finish.x)
        {
            moving_right = true;
            Timer();
        }
        if (moving_right && !timer_on)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else if(!timer_on)
        {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

    private void PlatformMoveVertical()
    {
        if (transform.position.y <= start.y)
        {
            moving_up = true;
            Timer();
        }
        else if (transform.position.y >= finish.y)
        {
            moving_up = false;
            Timer();
        }
        if (moving_up && !timer_on)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
        }
        else if (!timer_on)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
        }
    }

    private void PlatformMoveAngle()
    {
        if (transform.position.y <= start.y && transform.position.x <= start.x)
        {
            moving = true;
            Timer();
        }
        else if (transform.position.y >= finish.y && transform.position.x >= finish.x)
        {
            moving = false;
            Timer();
        }
        if (moving && !timer_on)
        {
            transform.position = new Vector3(
                transform.position.x + (speed / 2) * Time.deltaTime, 
                transform.position.y + (speed / 2) * Time.deltaTime, 
                transform.position.z);
        }
        else if (!timer_on)
        {
            transform.position = new Vector3(
                transform.position.x - (speed / 2) * Time.deltaTime, 
                transform.position.y - (speed / 2) * Time.deltaTime, 
                transform.position.z);
        }
    }

    private void Timer()
    {
        timer_on = true;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = wait_time;
            timer_on = false;
        }
    }
}
