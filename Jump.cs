using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Jump : MonoBehaviour

{
    public CharacterController controller;
    Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float speed = 3f;
    // Start is called before the first frame update
    void Awake()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
       
       // GetComponent<PlayerMove>().Jump += Jumpe;
    }

    // Update is called once per frame
    private void Update()
    {

    
    }

    private void Jumpe()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        Debug.Log("JUMP");
    }

}
