using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public bool canTakeDamage;
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        canTakeDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            gameObject.GetComponent<PlayerMove>().speed = 0;
           cam.GetComponent<MouseMove>().mouseSensitivity = 0;
            gameObject.GetComponent<PlayerMove>().StartCoroutine("Death");
            print("dead");
        }
    }
    IEnumerator takeDamge()
    {
        if (canTakeDamage)
        {
            health--;
            canTakeDamage = false;
        }
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;

    
    
    }
}
