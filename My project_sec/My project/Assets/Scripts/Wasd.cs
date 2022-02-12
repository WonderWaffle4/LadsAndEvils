using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wasd : MonoBehaviour
{
    public float speed;//скорость перемещения
    public float jumpPower;//сила прыжка

    private Vector3 platformMoverLeft = new Vector3(-0.02f, 0f, 0f);//коэффициент перемещения игрока при движении платформы влево
    private Vector3 platformMoverRight = new Vector3(0.02f, 0f, 0f);//коэффициент перемещения игрока при движении платформы вправо

    private float gravityForce;//гравитация
    private Vector3 moveVector;//коэффициент перемещения

    private bool onLadder;//если игрок на лестнице
    private bool canJump;//если игрок может прыгать
    private bool withBox;//если игрок с коробкой

    private CharacterController controller;//котроллер
    private Animator animator;

    void Start()
    {
        withBox = false;
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
                gravityForce = 0;
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
        if (Input.GetKey(KeyCode.E) && controller.isGrounded)
        {
            print("Grabbed");
            speed = 2;
            canJump = false;
            other.transform.parent = transform;
            withBox = true;
        }
        else if (Input.GetKey(KeyCode.G))
        {
            print("Ungrabbed");
            canJump = true;
            speed = 7;
            other.transform.parent = null;
            withBox = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Stair")
        {
            gravityForce -= 20f * Time.deltaTime;
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
        moveVector.y = Input.GetAxis("Vertical") * (speed - 2);//перемещение по вертикали на лестнице
    }

    //перемещение персонажа
    private void Control()
    {
        if (controller.isGrounded || onLadder)
        {
            moveVector = Vector3.zero;
            if (Input.GetKey(KeyCode.LeftShift) && !(withBox))
                moveVector.x = Input.GetAxis("Horizontal") * (speed + 5);
            else if(Input.GetKey(KeyCode.LeftShift) && withBox)
                moveVector.x = Input.GetAxis("Horizontal") * (speed + 2);
            else
                moveVector.x = Input.GetAxis("Horizontal") * speed;

            //поворот персонажа при передвижении
            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speed, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }
        }

        moveVector.y = gravityForce;

        if (onLadder)
        {
            canJump = false;
            Climb();
        }

        controller.Move(moveVector * Time.deltaTime);
    }

    //немного физики
    private void Gravity()
    {
        //гравитация
        if (!controller.isGrounded && !onLadder)
        {
            canJump = false;
            gravityForce -= 20f * Time.deltaTime;
        }
        else
        {
            canJump = true;
            gravityForce = -1;
        }

        //прыжок
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded && !(withBox))
        {
            gravityForce = jumpPower;
        }
    }
}
