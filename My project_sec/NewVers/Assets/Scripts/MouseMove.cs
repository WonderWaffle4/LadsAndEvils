using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public GameObject go;
    bool drag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                drag = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            drag = false;
        }

        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw > 0.1 && drag)
        {
            transform.position += transform.forward * Time.deltaTime * 100;/*Приближение*/
        }
        if (mw < -0.1 && drag)
        {
            transform.position -= transform.forward * Time.deltaTime * 100;/*Отдаление*/
        }

        if (drag)
        {
            Vector2 mouse = Input.mousePosition;
            transform.position = Camera.main.ScreenToWorldPoint(
                new Vector3(mouse.x, mouse.y,
                transform.position.z - Camera.main.transform.position.z));
        }
    }
}
