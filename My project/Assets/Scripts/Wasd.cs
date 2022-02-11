using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasd : MonoBehaviour
{
    public float speed;//скорость перемещения
    public float jump_power;//сила прыжка
    private Vector3 platformMoverLeft = new Vector3(-0.02f, 0f, 0f);//коэффициент перемещения игрока при движении платформы влево
    private Vector3 platformMoverRight = new Vector3(0.02f, 0f, 0f);//коэффициент перемещения игрока при движении платформы вправо
    private float gravity_force;//гравитация
    private Vector3 move_vector;//коэффициент перемещения
    private bool onLadder;//если игрок на лестнице
    private CharacterController controller;//котроллер
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Stair")
            gravity_force = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Stair")
            onLadder = true;//если игрок находится в лестнице
        if(other.tag == "platform")
        {
            //controller.Move(other.GetComponent<Platform>().deltaPosition * test);
            if (!other.GetComponent<Platform>().moving_right)//перемещение игрока на платформе
                controller.Move(platformMoverLeft);//влево
            else
                controller.Move(platformMoverRight);//вправо
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Stair")
        {
            gravity_force -= 20f * Time.deltaTime;
            onLadder = false;
        }
    }

    //взбирание по лестнице
    private void Climb()
    {
        //if (Vector3.Angle(Vector3.forward, move_vector) > 1f || Vector3.Angle(Vector3.forward, move_vector) == 0)
        //{
        //    Vector3 direct = Vector3.RotateTowards(transform.forward, move_vector, speed, 0.0f);
        //    transform.rotation = Quaternion.LookRotation(direct);
        //}
        move_vector.y = Input.GetAxis("Vertical") * (speed - 2);//перемещение по вертикали на лестнице
    }

    private void Control()//перемещение персонажа
    {
        if (controller.isGrounded || onLadder)
        {
            move_vector = Vector3.zero;
            if (Input.GetKey(KeyCode.LeftShift))
                move_vector.x = Input.GetAxis("Horizontal") * (speed + 5);
            else
                move_vector.x = Input.GetAxis("Horizontal") * speed;
            //поворот персонажа при передвижении
            if (Vector3.Angle(Vector3.forward, move_vector) > 1f || Vector3.Angle(Vector3.forward, move_vector) == 0)
            {
                Vector3 direct = Vector3.RotateTowards(transform.forward, move_vector, speed, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            } 
        }
        move_vector.y = gravity_force;

        if (onLadder)
            Climb();

        controller.Move(move_vector * Time.deltaTime);
    }

    private void Gravity()
    {
        //гравитация
        if (!controller.isGrounded && !onLadder)
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
