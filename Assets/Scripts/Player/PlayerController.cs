using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("状态")]
    public float faceDir;
    public bool jumppressed;
    public Vector2 inputdirection;
    // Start is called before the first frame update
    [Header("基本参数")]
    public float speed, Fast = 0;
    public float jumpForce;
    public float hurtforce;
    public bool isHurt;
    public bool isdead;
    public bool isattack;
    private int limit = 1;
    [Header("引用")]
    public AudioData jumpSFX;
    private Playerinputcontrol inputcontrol;
    private Rigidbody2D rb;
    private Physicalcheck physicalcheck1;
    [Header("物理材质")]
    public PhysicsMaterial2D wall;
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D isattack1;

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
        rb.velocity = new Vector2(0,rb.velocity.y);
    }

    private void Moving(InputAction.CallbackContext context)
    {
        inputdirection=(context.ReadValue<Vector2>()).normalized;
        rb.velocity = new Vector2(inputdirection.x * speed, rb.velocity.y);
        faceDir = transform.localScale.x;
        if (inputdirection.x > 0)
            faceDir = 4f;
        if (inputdirection.x < 0)
            faceDir = -4f;
        transform.localScale = new Vector3(faceDir, 4, 1);
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
    }
    private void DeleteCurrent(InputAction.CallbackContext context)
    {
        CharacterManager.Instance.DeleteCurrentCharacter();
    }

    private void Switch(InputAction.CallbackContext context)
    {
        if (CharacterManager.Instance.characters.Count==1)
        {
            return;
        }
        CharacterManager.Instance.SwitchToNextCharacter();
        this.enabled = false;
    }

    private void Split(InputAction.CallbackContext context)
    {
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

        if (physicalcheck1.isGround)
        {
            limit = 1;

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
