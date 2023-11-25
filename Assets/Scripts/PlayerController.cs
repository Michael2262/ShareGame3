using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    //私有的RD2D值，在awake中，藉由GetComponent獲得外面<Rigidbody2D>自身的值
    private Rigidbody2D rb;
    //抓到物理模擬裡面的值
    private PhysicsCheck physicsCheck;
    public Vector2 inputDirection;
    [Header("基本參數")]
    public float speed;
    public float jumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new PlayerInputControl();
        //Jump.後面可以有很多不同的時機，started是其中一種，+= 是註冊一個函數，註冊的函數在下面
        inputControl.Gameplay.Jump.started += Jump;
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
        //實現人物移動，藉由inputDirection偵測到的控制器是+1或-1，乘以我自設的變量speed，再乘以避免電腦設備導致的誤差值。Y值複寫回Rigibody中的Y
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
        if(physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

}
