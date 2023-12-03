using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControllerNew : MonoBehaviour
{
    public PlayerInputControl inputControl;
    //私有的RD2D值，在awake中，藉由GetComponent獲得外面<Rigidbody2D>自身的值
    private Rigidbody2D rb;
    //只取用BoxCollider2D裡的值，不要取用到全部Collider
    private CapsuleCollider2D box;
    //抓到物理模擬裡面的值
    private PhysicsCheck physicsCheck;
    public Vector2 inputDirection;
    public float xVelocity;
    [Header("基本參數")]
    public float speed;
    public float crouchSpeed;
    public float jumpForce;
    public float crouchjumpForce;
    [Header("狀態")]
    public bool isCrouch;
    //玩家雖放開蹲下鍵，但因為地形保持蹲下狀態
    public bool crouchHeld;



    //碰撞體尺寸
    Vector2 colliderStandSize;
    Vector2 colliderStandOffset;
    Vector2 colliderCrouchSize;
    Vector2 colliderCrouchOffset;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<CapsuleCollider2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new PlayerInputControl();
        //Jump.後面可以有很多不同的時機，started是其中一種，+= 是註冊一個函數，註冊的函數在下面
        inputControl.Gameplay.Jump.started += Jump;


        //抓到原有的大小與位置設定
        colliderStandSize = box.size;
        colliderStandOffset = box.offset;
        //設定蹲下後的大小與位置設定
        colliderCrouchSize = new Vector2(box.size.x, box.size.y / 2f);
        colliderCrouchOffset = new Vector2(box.offset.x, box.offset.y / 2f);

    }



    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    //週期性的每禎執行
    private void Update()
    {
        //inputDirection會時時偵測我控制器中的移動相關 
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();

    }

    //週期性的固定執行，通常跟物理有關會放在這裡
    private void FixedUpdate()
    {
        Move();

    }

    public void Move()
    {

        //下蹲判斷，若以後任一為false，則isCrouch的布林false；兩者都true則true
        isCrouch = inputDirection.y < -0.5f && physicsCheck.isGround;


        if (isCrouch && physicsCheck.isGround)
        {
            crouchHeld = true;
            box.size = colliderCrouchSize;
            box.offset = colliderCrouchOffset;
        }

        if (crouchHeld = true && physicsCheck.isGround && physicsCheck.isHeadBlock)
        {
            box.size = colliderCrouchSize;
            box.offset = colliderCrouchOffset;

        }

        else if (!isCrouch)
        {
            crouchHeld = false;
            box.size = colliderStandSize;
            box.offset = colliderStandOffset;
        }

        //實現人物移動，藉由inputDirection偵測到的控制器是+1或-1，乘以我自設的變量speed，再乘以避免電腦設備導致的誤差值。Y值複寫回Rigibody中的Y

        if (isCrouch || crouchHeld)
            rb.velocity = new Vector2(inputDirection.x * crouchSpeed * Time.deltaTime, rb.velocity.y);
        else if (!isCrouch)
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);


        //臨時變量臉朝向，int是整數，但ransform.lossyScale.x本來是浮點數，藉由(int)強制把它變成整數
        int faceDir = (int)transform.lossyScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;

        //人物翻轉(用每個unity物體都有的transform來改，因此不需要GetComponent)，將Scale(一個三維數值)覆蓋成新的值
        transform.localScale = new Vector3(faceDir, 1, 1);



    }








    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("JUMP")
        //rigibody給一個transform.up(世界座標向上方向的力)*jumpForce，這樣已經寫好了，後面可以增加ForceMode2D，Impulse是瞬時的力、Force是普通的
        if (physicsCheck.isGround && !isCrouch)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        if (physicsCheck.isGround && isCrouch)
            rb.AddForce(transform.up * crouchjumpForce, ForceMode2D.Impulse);
    }


}
