using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    public Animator ani;
    public bool open;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void Open()
    {
        ani.SetBool("Open", true);
    
    
    }
    public void Close()
    {

        ani.SetBool("Open", false);

    }
}
