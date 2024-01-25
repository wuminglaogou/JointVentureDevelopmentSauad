using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SavePoint : MonoBehaviour
{
    public Canvas canvas;
    public SaveAnim anim;
    public Camera cam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canvas.enabled = true;
            SaveSystem.Instance.animPossbleToSet = anim;
            SaveSystem.Instance.cameraPossbleToSet= cam;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().canSave = true;          
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canvas.enabled = false;
            collision.GetComponent<PlayerController>().canSave = false;
        }
    }
}
