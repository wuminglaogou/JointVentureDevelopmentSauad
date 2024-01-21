using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("状态")]
    public float faceDir;
    public float PlayerSize;
    public bool isaddjump = false;
    public bool jumppressed;
    public Vector2 inputdirection;
    public bool isclimb=false;
    public bool issplit = false;
    public bool XAccelerate = false;
    public bool YAccelerate = false;
    public float XAcclerateSpeed;
    public float YAcclerateSpeed;

    // Start is called before the first frame update
    [Header("基本参数")]
    public float speed, Fast = 0;
    public float MinSpeed;
    public float jumpForce;
    public float hurtforce;
    public bool isHurt;
    public bool isdead;
    private int limit = 1;
    [Header("引用")]
    public Coroutine movingCoroutine;
    public AudioData jumpSFX;
    public GameObject Box;
    private Boolean isActive = true;
    private Playerinputcontrol inputcontrol;
    private Rigidbody2D rb;
    private Physicalcheck physicalcheck1;
    [Header("物理材质")]
    public PhysicsMaterial2D wall;
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D isattack1;
    //[Header("事件")]
    //public UnityAction onDieEvent = delegate { };

    private void Awake()
    {
        physicalcheck1 = GetComponent<Physicalcheck>();//获得文件内的公开变量
        rb = GetComponent<Rigidbody2D>();
        inputcontrol = new Playerinputcontrol();
        inputcontrol.GamePlay.Move.performed += Moving;
        inputcontrol.GamePlay.Move.canceled += StopMoving;
        inputcontrol.GamePlay.Jump.performed += Jump;
        inputcontrol.GamePlay.Split.performed += Split;
        inputcontrol.GamePlay.Switch.performed += Switch;
        inputcontrol.GamePlay.Delete.performed += DeleteCurrent;
    }

    private void StopMoving(InputAction.CallbackContext context)
    {

        if (isActive)
        {
            if (movingCoroutine != null)
            {
                StopCoroutine(movingCoroutine);
            }
            if (XAccelerate == false && YAccelerate == false)
                rb.velocity = new Vector2(0, rb.velocity.y);
            if (XAccelerate == true && YAccelerate == false)
                rb.velocity = new Vector2(0 + XAcclerateSpeed, rb.velocity.y/2);
            if (XAccelerate == false && YAccelerate == true)
                rb.velocity = new Vector2(rb.velocity.x/4, 0+YAcclerateSpeed);
            if (XAccelerate == true && YAccelerate == true)
                rb.velocity = new Vector2(0+XAcclerateSpeed, YAcclerateSpeed);
            inputdirection = Vector2.zero;
        }
        
    }

    private void Moving(InputAction.CallbackContext context)
    {
        if (isActive)
        {
            inputdirection = (context.ReadValue<Vector2>()).normalized;

            movingCoroutine = StartCoroutine(nameof(MovingCoroutine));
        }

    }
    IEnumerator MovingCoroutine()
    {
        while (true)
        {
           if(XAccelerate == false&&YAccelerate==false)
            rb.velocity = new Vector2(inputdirection.x * speed, rb.velocity.y);
           if(XAccelerate==true&&YAccelerate==false)
            rb.velocity = new Vector2(inputdirection.x * speed + XAcclerateSpeed, rb.velocity.y/2);
           if (XAccelerate == false && YAccelerate == true)
            rb.velocity = new Vector2(inputdirection.x * speed/4, 0+YAcclerateSpeed);
           if (XAccelerate == true && YAccelerate == true)
            rb.velocity = new Vector2(inputdirection.x * speed+XAcclerateSpeed, 0+YAcclerateSpeed);
            faceDir = transform.localScale.x;
            if (inputdirection.x > 0)
                faceDir =PlayerSize;
            if (inputdirection.x < 0)
                faceDir = -PlayerSize;
            transform.localScale = new Vector3(faceDir, PlayerSize, 1);
            yield return null;
        }
    }

    private void OnEnable()
    {
        inputcontrol.Enable();
    }
    private void OnDisable()
    {
        inputcontrol.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //inputdirection = (inputcontrol.GamePlay.Move.ReadValue<Vector2>()).normalized;
        rb.gravityScale = 3.5f;
        Trans(Box);
        issplit = false;
        //if(inputdirection.x==0&&(XAccelerate==true||YAccelerate==true))
        //AccelerateStopingmove();

    }
    private void DeleteCurrent(InputAction.CallbackContext context)
    {
        CharacterManager.Instance.DeleteCurrentCharacter();
    }

    public void Switch(InputAction.CallbackContext context)
    {
        if (CharacterManager.Instance.characters.Count==1)
        {
            return;
        }
        CharacterManager.Instance.SwitchToNextCharacter();
    }

    private void Split(InputAction.CallbackContext context)
    {
        issplit = true;
        Vector2 mousePositionViewportSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos=(mousePositionViewportSpace-new Vector2(transform.position.x,transform.position.y)).normalized;
        CharacterManager.Instance.Split(transform.position, mousePos);
        
    }
    private void FixedUpdate()
    {
        //Move();
    }
    //public void Move()
    //{
    //    rb.velocity = new Vector2(inputdirection.x * speed, rb.velocity.y);
    //    faceDir = transform.localScale.x;
    //    if (inputdirection.x > 0)
    //        faceDir = 4f;
    //    if (inputdirection.x < 0)
    //        faceDir = -4f;
    //    transform.localScale = new Vector3(faceDir, 4, 1);
    //}
    private void Jump(InputAction.CallbackContext context)
    {
        if (isActive)
        {
            if (physicalcheck1.isGround)
            {
                limit = 1;

            }
            if(isaddjump==true)//上墙后到落地任意时间点都额外都一段跳跃
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                limit--;
                AudioManager.Instance.PlayAudio(jumpSFX);
                isaddjump = false;
            }
            if (physicalcheck1.isGround || limit >= 0)
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                limit--;
                AudioManager.Instance.PlayAudio(jumpSFX);
            }
           
        }

    }
    public void Trans(GameObject gameObject)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.SetActive(isActive);
            rb.velocity = new Vector2(0, 0);
            if (isActive)
                speed = 0;
            else
                speed = MinSpeed;
            isActive = !isActive;
            inputdirection.x = 0;
        }
    }
    public void AddJump()
    {
        isaddjump = true;
        isclimb = true;
    }
    public void CancelClimb()
    {
        isclimb=false;
        //isaddjump=false;只在墙壁时多段跳跃，但手感不好
    }
    //public void AccelerateStopingmove()
    //{
    //    if (XAccelerate == false && YAccelerate == false)
    //        rb.velocity = new Vector2(0, rb.velocity.y);
    //    if (XAccelerate == true && YAccelerate == false)
    //        rb.velocity = new Vector2(0 + XAcclerateSpeed, rb.velocity.y / 2);
    //    if (XAccelerate == false && YAccelerate == true)
    //        rb.velocity = new Vector2(rb.velocity.x / 4, 0 + YAcclerateSpeed);
    //    if (XAccelerate == true && YAccelerate == true)
    //        rb.velocity = new Vector2(0 + XAcclerateSpeed, 0+YAcclerateSpeed);
    //}

    public void Die()
    {
        CharacterManager.Instance.CharacterDie(this);
        Destroy(gameObject);
    }

}
