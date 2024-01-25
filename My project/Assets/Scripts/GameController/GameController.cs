using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityEvent AllDead;
    void Update()
    {
        if(GameObject.FindWithTag("Player")==null)
        {
            AllDead.Invoke();
        }
    }
}
