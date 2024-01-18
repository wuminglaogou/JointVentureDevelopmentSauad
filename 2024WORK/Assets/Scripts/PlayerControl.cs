using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private int faceDir;
    public PlayerInputControl inputcontrol;
    private Rigidbody2D rb;
    public Vector2 inputdirection;
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
        rb = GetComponent<Rigidbody2D>();
        inputcontrol = new PlayerInputControl();
    }
    void Start()
    {
        
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
        inputdirection = inputcontrol.Player.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
      rb.velocity=new Vector2(inputdirection.x*Speed*Time.deltaTime*2,inputdirection.y);
        faceDir = (int)transform.localScale.x;
        if (inputdirection.x > 0)
            faceDir = 3;
        if (inputdirection.x < 0)
            faceDir = -3;
        transform.localScale = new Vector3(faceDir, 3, 1);
    }
}
