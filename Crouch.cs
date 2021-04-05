using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    public Transform t_mesh;                    // Player Transform
    public CharacterController ccr_controller;  // Get the character controller
    public bool IsCrouch = false;               // 
    public float LocalScaleY;                   // Y scale of "t_mesh"
    public float ControllerHeight;              // Y scale of the character controller

    // PRIVATE VARS //

    void Start()
    {
        //nothing here yet
    }
    private void Awake()

    {
        GetComponent<PlayerMove>().Crouch += CrouchFunction;
       
    }
        void Update()
    {
    }

    void CrouchFunction()
    {
        IsCrouch = !IsCrouch;
        if (IsCrouch == true)
        {
            t_mesh.localScale = new Vector3(1, LocalScaleY, 1);
            ccr_controller.height = ControllerHeight;
            
        }
        else
        {
            Ray ray = new Ray();
            RaycastHit hit;
            ray.origin = transform.position;
            ray.direction = Vector3.up;
            if (!Physics.Raycast(ray, out hit, 1))
            {
                t_mesh.localScale = new Vector3(1, 1, 1);
                ccr_controller.height = 1.8f;
             
            }
            else
            {
                Debug.Log("Not enough space to stand up!");
              }
        }
    }
}
