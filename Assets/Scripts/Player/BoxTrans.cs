using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrans : MonoBehaviour
{
    public GameObject Box;
    private Boolean isActive = true;
    private Rigidbody2D playerRb;
    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Trans(Box);
    }
    public void Trans(GameObject gameObject)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.SetActive(isActive);
            isActive = !isActive;
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);
        }
    }
}
