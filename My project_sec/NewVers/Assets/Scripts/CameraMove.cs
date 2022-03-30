using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed;
    public Transform charactor;

    private Vector3 camera_pos;

    void Start()
    {
        camera_pos = transform.position;
    }

    
    void Update()
    {
        camera_pos.y = charactor.position.y + 4;
        camera_pos.x = charactor.position.x;
        transform.position = Vector3.Lerp(transform.position, camera_pos, speed * Time.deltaTime);
    }
}
