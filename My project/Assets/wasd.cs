using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wasd : MonoBehaviour
{
    public float speed;//скорость перемещения
    public float jump_power;//сила прыжка

    private float gravity_force;
    private Vector3 move_vector;

    private CharacterController controller; 
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();//wasd
        animator = GetComponent<Animator>();//аниматор
    }

    void Update()
    {
        Control();
        Gravity();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Stair")
        {
            gravity_force = 0;
            Climb();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "platform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Stair")
        {
            gravity_force -= 20f * Time.deltaTime;
        }
        if (other.tag == "platform")
        {
            transform.parent = null;
        }
    }

    //взбирание по лестнице
    private void Climb()
    {
        move_vector = Vector3.zero;
        move_vector.x = Input.GetAxis("Horizontal") * speed;

        if (Vector3.Angle(Vector3.forward, move_vector) > 1f || Vector3.Angle(Vector3.forward, move_vector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, move_vector, speed, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        move_vector.y = Input.GetAxis("Vertical") * (speed - 2);

        controller.Move(move_vector * Time.deltaTime);
    }

    private void Control()//перемещение персонажа
    {
        if (controller.isGrounded)
        {
            move_vector = Vector3.zero;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                move_vector.x = Input.GetAxis("Horizontal") * (speed + 5);
            }
            else
            {
                move_vector.x = Input.GetAxis("Horizontal") * speed;
            }

            //поворот персонажа при передвижении
            if (Vector3.Angle(Vector3.forward, move_vector) > 1f || Vector3.Angle(Vector3.forward, move_vector) == 0)
            {
                Vector3 direct = Vector3.RotateTowards(transform.forward, move_vector, speed, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }
        }

        move_vector.y = gravity_force;

        controller.Move(move_vector * Time.deltaTime);
    }

    private void Gravity()
    {
        //гравитация
        if (!controller.isGrounded)
        {
            gravity_force -= 20f * Time.deltaTime;
        }
        else
        {
            gravity_force = -1;
        }

        //прыжок
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            gravity_force = jump_power;
        }
    }
}
