﻿using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    private Rigidbody rb;
    private AudioSource audioSource;

    public float spread;


    void Start() 
    { 
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //GameObject clone = 
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // as GameObject;
            audioSource.Play ();
        }
    }

    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        //rb.velocity = movement * speed;

        //rb.position = new Vector3
        //(   Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
        //    0.0f,
        //    Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        //);

        if ( Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rb.position = new Vector3(-spread, 0.0f, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) )
        {
            rb.position = new Vector3(0.0f, 0.0f, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rb.position = new Vector3(spread, 0.0f, 0.0f);
        }

        rb.rotation = Quaternion.Euler (0, 0, rb.velocity.x * -tilt);
    }
}
