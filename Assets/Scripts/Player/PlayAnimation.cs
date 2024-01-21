using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private Physicalcheck physicalcheck;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicalcheck = GetComponent<Physicalcheck>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Xspeed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("Yspeed", -(rb.velocity.y));
        anim.SetBool("isground", physicalcheck.isGround);
        anim.SetBool("isclimb", playerController.isclimb);
        anim.SetBool("issplit",playerController.issplit);
    }
    
    public void Die()
    {
        Debug.Log("die");
        anim.SetBool("isdead", true);
    }
    public void Save()
    {
        Debug.Log("respawn");
        anim.SetBool("isdead", false);
    }
}
