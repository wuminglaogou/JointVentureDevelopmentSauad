using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public class AddJump : MonoBehaviour
{
    // Start is called before the first frame update

    public UnityEvent addjump;//额外跳跃
    public UnityEvent Minspeed;//离开墙壁时最低速度

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            addjump?.Invoke();
           

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Minspeed?.Invoke();
          

        }
    }
}
