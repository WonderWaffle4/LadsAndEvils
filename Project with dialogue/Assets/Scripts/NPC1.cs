using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1 : MonoBehaviour
{
    private new Collider collider;
    private void Start()
    {
        collider = GameObject.Find("NPC1").GetComponent<Collider>();
    }
    private void FixedUpdate()
    {
    }
    public void OnTriggerEnter(Collider other)
    {
        
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {

        }
    }

}
