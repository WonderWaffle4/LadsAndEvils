using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wasd : MonoBehaviour
{
    public float speed;//�������� �����������
    public float jump_power;//���� ������

    private Vector3 platformMoverLeft = new Vector3(-0.02f, 0f, 0f);//����������� ����������� ������ ��� �������� ��������� �����
    private Vector3 platformMoverRight = new Vector3(0.02f, 0f, 0f);//����������� ����������� ������ ��� �������� ��������� ������

    private float gravity_force;//����������
    private Vector3 move_vector;//����������� �����������

    private bool onLadder;//���� ����� �� ��������
    private bool canJump;//���� ����� ����� �������
    private bool WithBox;//���� ����� � ��������

    private CharacterController controller;//���������
    private Animator animator;

    void Start()
    {
        WithBox = false;
        controller = GetComponent<CharacterController>();//wasd
        animator = GetComponent<Animator>();//��������
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
                //����������� ������ �� ���������
                if (!other.GetComponent<Platform>().moving_right)
                    controller.Move(platformMoverLeft);//�����
                else
                    controller.Move(platformMoverRight);//������
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

    //��������� �� ��������
    private void Climb()
    {
        move_vector.y = Input.GetAxis("Vertical") * (speed - 2);//����������� �� ��������� �� ��������
    }

    //����������� ���������
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

            //������� ��������� ��� ������������
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

    //������� ������
    private void Gravity()
    {
        //����������
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

        //������
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded && !(WithBox))
        {
            gravity_force = jump_power;
        }
    }
}
