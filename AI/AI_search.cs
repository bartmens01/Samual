using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class AI_search : MonoBehaviour
{
    public Animator PlayerAni;
    public  Animator ani;
    public Transform eyes;
    public bool canSearch;
    public Transform Target;
    public AudioSource scream;
    public  NavMeshAgent _NavAgent;
    public Transform[] searchPoints;
    public bool spotted;
    bool spotting;
    public float timeSearch;
   public float timeNextSearch;
    int A;
    public int searchAmount;
    // Start is called before the first frame update
    public event Action follow = delegate { };
    void Start()
    {
        _NavAgent = this.GetComponent<NavMeshAgent>();
        canSearch = true;
        searchAmount = 7;
     
    }
    private void Awake()
    {
        GetComponent<AI_FollowPlayer>().search += Search;
      
    }
    // Update is called once per frame
    void Update()
    {
        if (timeNextSearch <= 0)
        {
            A = UnityEngine.Random.Range(1, searchAmount);

            timeNextSearch = timeSearch;

        }
        if (spotted)
        {
            ani.SetBool("Run", true);
            follow();
        }
        Search();
        RayCast();
    }
    void RayCast()
    {
        Ray ray = new Ray();
        Ray ray2 = new Ray();
        RaycastHit hit;
        ray.origin = transform.forward;
        ray2.origin = transform.right;
        Debug.DrawRay(eyes.position, transform.forward * 20, Color.green);
        Debug.DrawRay(eyes.position, transform.forward * 20 +  transform.right - -transform.right * 10f, Color.green);
        Debug.DrawRay(eyes.position, transform.forward * 20 + -transform.right + -transform.right * 10f, Color.green);
        ray.direction = Vector3.up;
        ray.direction = Vector3.left;
        
        ray.direction = Vector3.right;
        if (Physics.Raycast(eyes.position, transform.forward * 20, out hit) || Physics.Raycast(eyes.position, transform.forward * 20 + transform.right - -transform.right * 10f, out hit) || Physics.Raycast(eyes.position, transform.forward * 20 + -transform.right + -transform.right * 10f, out hit))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                StartCoroutine("spot");
                Debug.Log("Hit");
                if (spotted == false)
                {
                   
                    StartCoroutine("spot");
                    spotting = true;
                }
                canSearch = false;
            }
        }
    }
    void Search()
    {
        if (canSearch)
        {
            _NavAgent.speed = 2f;
            ani.SetBool("Sneaking", true);
            ani.SetBool("Run", false);
            
            Target = searchPoints[A];
            Vector3 Point = Target.transform.position;
            float dist = Vector3.Distance(Target.transform.position, this.gameObject.transform.position);
            _NavAgent.SetDestination(Point);
            if (dist < 50)
            {
                transform.Rotate(0, 0 + 20 * Time.deltaTime, 0);


            }
            if (dist < 7)
            {
               
                timeNextSearch -= 1 * Time.deltaTime;
            }
        }
    }
    IEnumerator spot()
    {
        if (spotting == true)
        {
            _NavAgent.speed = 0;
            scream.Play();
            ani.SetBool("Intim", true);
            PlayerAni.SetBool("Spotted", true);
            yield return new WaitForSeconds(0.5f);
            PlayerAni.SetBool("Spotted", false);
            yield return new WaitForSeconds(4.5f);
            _NavAgent.speed = 6;
            ani.SetBool("Intim", false);
            ani.SetBool("Sneaking", false);
            scream.Stop();
            spotted = true;
            spotting = false;
        } 

    }
    
}
