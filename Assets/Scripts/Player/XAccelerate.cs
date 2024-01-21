using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XAccelerate : MonoBehaviour
{
    // Start is called before the first frame update
    public bool X;
    public bool Y;
    public float Xspeed;
    public float Yspeed;
 
    // Update is called once per frame

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            PlayerController controller = other.gameObject.GetComponent<PlayerController>();

            if (X == true)
            {
                controller.XAccelerate = true;
                controller.XAcclerateSpeed = Xspeed;
            }
            if (Y == true)
            {
                controller.YAccelerate = true;
                controller.YAcclerateSpeed = Yspeed;
            }
        }
        
        
    }


    public void OnTriggerStay2D(Collider2D other)
    {
        if (X == true)
        { 
            if (other.gameObject.CompareTag("Player"))
            {
                Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
                PlayerController controller = other.gameObject.GetComponent<PlayerController>();
            }
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            PlayerController controller = other.gameObject.GetComponent<PlayerController>();
            if (X == true)
            {
                controller.XAccelerate = false;
                controller.XAcclerateSpeed = 0;
            }
            if (Y == true)
            {
                controller.YAccelerate = false;
                controller.YAcclerateSpeed = 0;
            }
        }

    }
}
