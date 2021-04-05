using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float maxSpeed, minSpeed, crouchSpeed;
    public float maxStamina;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float groundDistance = 0.4f;
    public float stamina;
    public Transform GroundCheck;
    public LayerMask groundMask;
    public GameObject crabKill;
    public GameObject crab;
    Vector3 velocity;
    bool isGrounded;
    bool keyIsUp;
    bool canSprint;
    bool isDiying;
    public event Action Crouch = delegate { };
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
        stamina = maxStamina;
        speed = minSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;

        }
      
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        crouch();
        stam();
        sprint();
    }
    void crouch()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
            speed = crouchSpeed;
            GetComponent<Crouch>().IsCrouch = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Crouch();
            speed = minSpeed;
            GetComponent<Crouch>().IsCrouch = false;
        }


    }
    void stam()
    {
        if (stamina >= 50)
        {
            canSprint = true;
            
        }
        if (stamina <= 0)
        {
            canSprint = false;
            keyIsUp = true;

        }
        if (stamina >= maxStamina)
        {
            stamina = maxStamina;


        }

    }
    void sprint()
    {
       if (canSprint)
            {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (GetComponent<Crouch>().IsCrouch == false)
                {
                    
                        stamina -= 25 * Time.deltaTime;
                        keyIsUp = false;
                        speed += 5f * Time.deltaTime;
                        if (speed >= maxSpeed)
                        {

                            speed = maxSpeed;

                        }
                    
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            keyIsUp = true;
        }
        if (keyIsUp == true)
        {
            if (GetComponent<Crouch>().IsCrouch == false)
            {
             speed -= 10f * Time.deltaTime;
                
            }
            if (speed <= minSpeed)
            {
                stamina += 15 * Time.deltaTime;
                if (GetComponent<Crouch>().IsCrouch == false)
                {
                    speed = minSpeed;
                }
            }

        }

    }
  
    IEnumerator Death()
    {
        if (isDiying == false)
        {
            isDiying = true;
            Destroy(crab);
            Animator crabani = crabKill.GetComponent<Animator>();
            crabKill.SetActive(true);
            crabani.SetBool("Kill", true);
            yield return new WaitForSeconds(1f);
            crabani.SetBool("Kill", false);
        }
    }
}
