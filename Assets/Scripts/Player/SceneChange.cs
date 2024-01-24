using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneChange : MonoBehaviour
{
    public UnityEvent scenechange;
    public GameObject camera1;
    public GameObject camera2;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            
        }
    }
}
