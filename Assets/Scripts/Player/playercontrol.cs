using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class playercontrol : MonoBehaviour
{
    private float faceDir;
    public Playerinputcontrol inputcontrol;
    private Rigidbody2D rb;
    public Vector2 inputdirection;
    // Start is called before the first frame update
    [Header("基本参数")]
    public float Speed, Fast = 0;
    public float JumpForce;
    public float hurtforce;
    public bool isHurt;
    public bool isdead;
    public bool isattack;
    [Header("物理材质")]
    public PhysicsMaterial2D wall;
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D isattack1;

    private void Awake()
    {
        //physicalcheck1 = GetComponent<physicalcheck>();//获得文件内的公开变量
        rb = GetComponent<Rigidbody2D>();
        inputcontrol = new Playerinputcontrol();
        inputcontrol.GamePlay.Jump.performed += Jump;

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
       inputdirection = inputcontrol.GamePlay.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
       
            Move();
    }
    public void Move()
    {
            rb.velocity = new Vector2(inputdirection.x * Speed * Time.deltaTime * 2, rb.velocity.y);
        faceDir = transform.localScale.x;
        if (inputdirection.x > 0)
            faceDir = 4f;
        if (inputdirection.x < 0)
            faceDir = -4f;
        transform.localScale = new Vector3(faceDir, 4, 1);
    }
    private void Jump(InputAction.CallbackContext context)
    {
        //if (physicalcheck1.isGround)
        //    limit = 1;
        //if (physicalcheck1.isGround || limit >= 0)
        //{
            rb.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
            //limit--;
        //}

    }
}
