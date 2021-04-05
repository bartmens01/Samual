using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class AI_FollowPlayer : MonoBehaviour
{
   public GameObject Target;
    NavMeshAgent _NavAgent;
    public event Action search = delegate { };
    float wait;
   public Animator ani;
    public float dist;
    public Transform eyes;
    bool audioPlaying;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        wait = 2f;
        _NavAgent = this.GetComponent<NavMeshAgent>();
        
    }
    private void Awake()
    {
        GetComponent<AI_search>().follow += FollowPlayer;
        
    }
        // Update is called once per frame
    void Update()
    {
      
     
        if (Target.GetComponent<Hide>().hiding == true)
        {
            if (dist <= 12)
            {
                
                ani.SetBool("Attack", false);
                if (GetComponent<AI_search>().canSearch == false)
                {
                    transform.Rotate(0, 0 + 20 * Time.deltaTime, 0);
                }
                wait -= 1 * Time.deltaTime;
                if (wait <= 0)
                {
                    audioPlaying = false;
                    Target.GetComponent<AudioManerget>().nostress();
                    search();
                    GetComponent<AI_search>().canSearch = true;
                    GetComponent<AI_search>().spotted = false;
                    wait = 2f;
                }
            }

        }
    }
    void FollowPlayer()
    {
        if (!audioPlaying)
        {
            Target.GetComponent<AudioManerget>().Instress();
            audioPlaying = true;
        }
        dist = Vector3.Distance(Target.transform.position, transform.position);
        Vector3 Player = Target.transform.position;
        _NavAgent.SetDestination(Player);
        print("follow");
        if (Physics.Raycast(eyes.position, transform.forward * 20, out hit))
        {
            if (dist <= 6)
            {
                print("Close");
                if (Target.GetComponent<Hide>().hiding == false)
                {
                    StartCoroutine("Damage");
                }
            }
        }
        if (dist >= 6)
        {
            
            ani.SetBool("Attack", false);
        }
    }
    IEnumerator Damage()
    {
        yield return new WaitForSeconds(0.6f);
        if (dist <= 6)
        {
            print("Close");
            if (Target.GetComponent<Hide>().hiding == false)
            {
                ani.SetBool("Attack", true);

                if (Target.GetComponent<Health>().canTakeDamage)
                {
                    Target.GetComponent<Health>().StartCoroutine("takeDamge");
                }
            }
        }

    }
}
