using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class physicalcheck : MonoBehaviour
{

    private Rigidbody2D rb;
    public Vector2 bottomOffset;//�ŵ׵�λ�Ʋ�ֵ  ������������������֮�⣬��Ȼ������Ļ���ⲻ�ˣ�����isground
    [Header("��������")]
    public float checkRaduis;
    public bool isGround;
    public Vector2 leftOffset;
    public Vector2 rightOffset;

    public LayerMask groundlayer;//����Ŀ���layer����ʲô
    [Header("״̬")]
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
        //������
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
