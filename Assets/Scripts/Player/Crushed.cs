using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.Events;

public class Crushed : MonoBehaviour
{
    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Õ½¶·£¬Ë¬£¡");
            Rigidbody2D rb=other.GetComponent<Rigidbody2D>();
            if(rb.velocity.x==0&&rb.velocity.y>=-1)
            Destroy(other.gameObject);
        }
    }
}
