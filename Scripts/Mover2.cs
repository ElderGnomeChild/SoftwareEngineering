using System.Collections;
//using System.Collections.Generic;
using UnityEngine;


public class Mover2 : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = rb.transform.forward * speed;
    }
}
