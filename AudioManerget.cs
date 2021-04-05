using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManerget : MonoBehaviour
{
    public AudioSource heartRest;
    public AudioSource stress;
   
    // Start is called before the first frame update
    void Start()
    {
        nostress();
    }

    // Update is called once per frame
    void Update()
    {
           

          
        
        
            
    
        
        
        
        
    }
    public void Instress()
    {

        stress.Play();
        heartRest.Stop();
    }
   public  void nostress()
    {
        stress.Stop();
        heartRest.Play();

    }
}
