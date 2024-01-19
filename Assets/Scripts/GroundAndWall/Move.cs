using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("基本参数")]
    public float Xspeed;
    public float Yspeed;
    [Header("引用")]
    private Rigidbody2D rb;
    // Update is called once per frame

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        move();
    }
    public void move()
    {
        rb.velocity=new Vector2(Xspeed,Yspeed);
    }
}
