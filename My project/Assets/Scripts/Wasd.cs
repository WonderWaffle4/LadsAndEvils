using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasd : MonoBehaviour
{
    public float speed;//�������� �����������
    public float jump_power;//���� ������
    private Vector3 platformMoverLeft = new Vector3(-0.02f, 0f, 0f);//����������� ����������� ������ ��� �������� ��������� �����
    private Vector3 platformMoverRight = new Vector3(0.02f, 0f, 0f);//����������� ����������� ������ ��� �������� ��������� ������
    private float gravity_force;//����������
    private Vector3 move_vector;//����������� �����������
    private bool onLadder;//���� ����� �� ��������
    private CharacterController controller;//���������
    private Animator animator;

    void Start()
    {
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
        if(other.tag == "Stair")
            gravity_force = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Stair")
            onLadder = true;//���� ����� ��������� � ��������
        if(other.tag == "platform")
        {
            //controller.Move(other.GetComponent<Platform>().deltaPosition * test);
            if (!other.GetComponent<Platform>().moving_right)//����������� ������ �� ���������
                controller.Move(platformMoverLeft);//�����
            else
                controller.Move(platformMoverRight);//������
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

    //��������� �� ��������
    private void Climb()
    {
        //if (Vector3.Angle(Vector3.forward, move_vector) > 1f || Vector3.Angle(Vector3.forward, move_vector) == 0)
        //{
        //    Vector3 direct = Vector3.RotateTowards(transform.forward, move_vector, speed, 0.0f);
        //    transform.rotation = Quaternion.LookRotation(direct);
        //}
        move_vector.y = Input.GetAxis("Vertical") * (speed - 2);//����������� �� ��������� �� ��������
    }

    private void Control()//����������� ���������
    {
        if (controller.isGrounded || onLadder)
        {
            move_vector = Vector3.zero;
            if (Input.GetKey(KeyCode.LeftShift))
                move_vector.x = Input.GetAxis("Horizontal") * (speed + 5);
            else
                move_vector.x = Input.GetAxis("Horizontal") * speed;
            //������� ��������� ��� ������������
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
        //����������
        if (!controller.isGrounded && !onLadder)
        {
            gravity_force -= 20f * Time.deltaTime;
        }
        else
        {
            gravity_force = -1;
        }

        //������
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            gravity_force = jump_power;
        }
    }
}
