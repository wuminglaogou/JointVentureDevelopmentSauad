using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClimbWall : MonoBehaviour
{
    //public UnityEvent addjump;
    //public UnityEvent cancelclimb;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("jump");
        if (other.gameObject.CompareTag("Player"))
            other.GetComponent<PlayerController>().AddJump();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("jump");
        if (other.gameObject.CompareTag("Player"))
            other.GetComponent<PlayerController>().CancelClimb();
    }
}
