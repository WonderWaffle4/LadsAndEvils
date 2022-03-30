using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    [Header("Settings:")]
    public float speed;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        var r = GetComponent<Renderer>();
        Color c = new Color(0, 1, 0, 0.5f);
        r.material.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 pos = transform.position;
        //pos.x += speed * Time.deltaTime;
        //transform.position = pos;

        //transform.position = Vector3.Lerp(transform.position, target.position, 0.01f);
    }
}
