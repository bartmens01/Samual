using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    public Transform head;
    bool key2;
    bool key1;
    GameObject door;
    public GameObject crab;
    // Start is called before the first frame update
    void Start()
    {
        crab = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.E))
        {

            RayCast();
        
        }
    }
    void RayCast()
    {
      
        RaycastHit hit;
        
       Ray ray = new Ray(head.transform.position, head.transform.forward);
        Debug.DrawRay(head.transform.position, head.transform.forward * 20, Color.green);

        if (Physics.Raycast(ray, out hit, 10))
        {
            if (hit.collider.gameObject.tag == "Key1")
            {
                Destroy(hit.collider.gameObject);
                key1 = true;
              
            }
            if (hit.collider.gameObject.tag == "Key2")
            {
                
                Destroy(hit.collider.gameObject);
                key2 = true;
                
            }
            if (hit.collider.gameObject.tag == "door")
            {
                if (key1 == true)
                {
                    door = hit.collider.gameObject;
                    door.GetComponent<openDoor>().Open();
                    Debug.Log("open");
                    crab.GetComponent<AI_search>().searchAmount = 10;
                    crab.GetComponent<AI_search>().timeSearch = 5;
                    crab.GetComponent<AI_search>()._NavAgent.speed = 4f;
                }
                
               
            }
            if (hit.collider.gameObject.tag == "door1")
            {
                if (key2 == true)
                {
                    door = hit.collider.gameObject;
                    door.GetComponent<openDoor>().Open();
                    Debug.Log("open");

                }


            }

        }
    }
}
