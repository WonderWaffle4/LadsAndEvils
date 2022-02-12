using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wasd : MonoBehaviour
{
    public float speed;//скорость перемещения
    public float jump_power;//сила прыжка

    private Vector3 platformMoverLeft = new Vector3(-0.02f, 0f, 0f);//коэффициент перемещения игрока при движении платформы влево
    private Vector3 platformMoverRight = new Vector3(0.02f, 0f, 0f);//коэффициент перемещения игрока при движении платформы вправо

    private float gravity_force;//гравитация
    private Vector3 move_vector;//коэффициент перемещения

    private bool onLadder;//если игрок на лестнице
    private bool canJump;//если игрок может прыгать
    private bool WithBox;//если игрок с коробкой

    private CharacterController controller;//котроллер
    private Animator animator;

    void Start()
    {
        WithBox = false;
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
        switch (other.tag)
        {
            case "Stair":
                gravity_force = 0;
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "Stair":
                onLadder = true;
                break;
            case "platform":
                //перемещение игрока на платформе
                if (!other.GetComponent<Platform>().moving_right)
                    controller.Move(platformMoverLeft);//влево
                else
                    controller.Move(platformMoverRight);//вправо
                break;
            case "BoxTrigger":
                MoveBox(other);
                break;
        }
    }

    private void MoveBox(Collider other)
    {
        print("In Box");
        if (Input.GetKey(KeyCode.E))
        {
            print("Grabbed");
            speed = 2;
            canJump = false;
            other.transform.parent = transform;
            WithBox = true;
        }
        else if (Input.GetKey(KeyCode.G))
        {
            print("Ungrabbed");
            canJump = true;
            speed = 7;
            other.transform.parent = null;
            WithBox = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Stair")
        {
            gravity_force -= 20f * Time.deltaTime;
            onLadder = false;
        }
        if(other.tag == "BoxTrigger")
        {
            speed = 7;
            canJump = true;
        }
    }

    //взбирание по лестнице
    private void Climb()
    {
        move_vector.y = Input.GetAxis("Vertical") * (speed - 2);//перемещение по вертикали на лестнице
    }

    //перемещение персонажа
    private void Control()
    {
        if (controller.isGrounded || onLadder)
        {
            move_vector = Vector3.zero;
            if (Input.GetKey(KeyCode.LeftShift) && !(WithBox))
                move_vector.x = Input.GetAxis("Horizontal") * (speed + 5);
            else if(Input.GetKey(KeyCode.LeftShift) && WithBox)
                move_vector.x = Input.GetAxis("Horizontal") * (speed + 2);
            else
                move_vector.x = Input.GetAxis("Horizontal") * speed;

            //поворот персонажа при передвижении
            if (!(WithBox) && Vector3.Angle(Vector3.forward, move_vector) > 1f || Vector3.Angle(Vector3.forward, move_vector) == 0)
            {
                Vector3 direct = Vector3.RotateTowards(transform.forward, move_vector, speed, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }
        }

        move_vector.y = gravity_force;

        if (onLadder)
        {
            canJump = false;
            Climb();
        }

        controller.Move(move_vector * Time.deltaTime);
    }

    //немного физики
    private void Gravity()
    {
        //гравитация
        if (!controller.isGrounded && !onLadder)
        {
            canJump = false;
            gravity_force -= 20f * Time.deltaTime;
        }
        else
        {
            canJump = true;
            gravity_force = -1;
        }

        //прыжок
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded && !(WithBox))
        {
            gravity_force = jump_power;
        }
    }
}
