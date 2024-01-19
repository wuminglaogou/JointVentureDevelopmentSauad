using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("״̬")]
    public float faceDir;
    public bool jumppressed;
    public Vector2 inputdirection;
    // Start is called before the first frame update
    [Header("��������")]
    public float speed, Fast = 0;
    public float PlayerScale;//��ɫ�ߴ�
    public float MaxSpeed;//����ٶ�
    public float MinSpeed;//��ʼ�ٶ�
    public float ChangeSpeed;//�ٶȱ仯����
    public float jumpForce;
    //public float hurtforce;
    //public bool isHurt;
    //public bool isdead;
    //public bool isattack;
    public int limit = 1;
    public bool add;
    public float ExitWallSpeed;//�뿪ǽ�ڵ�����
    private BoxTrans boxTrans;

    [Header("����")]
    public AudioData jumpSFX;
    private Playerinputcontrol inputcontrol;
    private Rigidbody2D rb;
    private Physicalcheck physicalcheck1;
    [Header("�������")]
    public PhysicsMaterial2D wall;
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D isattack1;

    private void Awake()
    {
        physicalcheck1 = GetComponent<Physicalcheck>();//����ļ��ڵĹ�������
        rb = GetComponent<Rigidbody2D>();
        inputcontrol = new Playerinputcontrol();
        inputcontrol.GamePlay.Jump.performed += Jump;
        add = false;
        boxTrans=GetComponent<BoxTrans>();
     
      
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
        inputdirection = (inputcontrol.GamePlay.Move.ReadValue<Vector2>()).normalized;
        boxTrans = GetComponent<BoxTrans>();
        rb.gravityScale = 3.5f;
    }
    private void FixedUpdate()
    {
        if (boxTrans.boxtrans == false)
        { 
            Move();
        }
    }
    public void Move()
    {
        if(inputdirection.x==0)
        {
            speed = MinSpeed;
        }
        if(inputdirection.x!=0)
        {
            if (speed < MaxSpeed)
                speed += 2*Time.deltaTime * ChangeSpeed;
        }    
        rb.velocity = new Vector2(inputdirection.x * speed, rb.velocity.y);
        faceDir = transform.localScale.x;
        if (inputdirection.x > 0)
            faceDir = PlayerScale;
        if (inputdirection.x < 0)
            faceDir = -PlayerScale;
        transform.localScale = new Vector3(faceDir, PlayerScale, 1);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (boxTrans.boxtrans == false)
        {
            if (physicalcheck1.isGround)
            {
                limit = 1;

            }
            if (physicalcheck1.isGround || limit >= 0 || add == true)
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                limit--;
                AudioManager.Instance.PlayAudio(jumpSFX);
                if (add == true)
                    add = false;
            }
        }

    }
    public void addjump()
    {
        Debug.Log("1");
        add = true;
        
    }
    public void exitwall()
    {
        speed = ExitWallSpeed;

    }

}
