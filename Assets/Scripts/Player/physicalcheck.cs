using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class physicalcheck : MonoBehaviour
{

    private Rigidbody2D rb;
    public Vector2 bottomOffset;//脚底的位移差值  必须在人物物理引擎之外，不然在里面的话检测不了，例如isground
    [Header("基本参数")]
    public float checkRaduis;
    public bool isGround;
    public Vector2 leftOffset;
    public Vector2 rightOffset;

    public LayerMask groundlayer;//就是目标的layer得是什么
    [Header("状态")]
    public bool TouchLeftWall;
    public bool TouchRightWall;

    // Start is called before the first frame update
    // Update is called once per frame
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        check();


    }
    private void FixedUpdate()
    {
        check();
     
    }

    public void check()
    {
        //检测地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis);
        TouchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundlayer);
        TouchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundlayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
 

}
