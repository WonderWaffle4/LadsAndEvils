using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public float speed;
    public float jump_power;

    private bool is_grounded;//на земле или нет
    private Rigidbody rb;//rigidbody плеера
    private Vector3 move;//вектор передвижения

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Control();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Stair")
        {
            rb.useGravity = false;
            Climb();
        }

        if(other.transform.tag == "ground")
        {
            is_grounded = true;
            Debug.Log("Grounded");
        }
        else
        {
            is_grounded = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Stair")
        {
            rb.useGravity = true;
        }
    }

        private void Climb()
    {
        move.x = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.MovePosition(rb.position + move * (speed + 5) * Time.deltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + move * speed * Time.deltaTime);
        }

        move.y = Input.GetAxis("Vertical");

        rb.MovePosition(rb.position + move * speed * Time.deltaTime);
    }

    private void Control()
    {
        if (is_grounded)
        {
            move.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = speed * Time.deltaTime * move;
            }
            else
            {
                if(move.x > 1)
                {
                    rb.velocity = move;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && is_grounded)
        {
            rb.AddForce(Vector3.up * jump_power * 100, ForceMode.Impulse);
        }
    }
}
