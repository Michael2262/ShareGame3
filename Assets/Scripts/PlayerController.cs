using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    //�p����RD2D�ȡA�bawake���A�ǥ�GetComponent��o�~��<Rigidbody2D>�ۨ�����
    private Rigidbody2D rb;
    //�u����BoxCollider2D�̪��ȡA���n���Ψ����Collider
    private BoxCollider2D box;
    //��쪫�z�����̭�����
    private PhysicsCheck physicsCheck;
    public Vector2 inputDirection;
    [Header("�򥻰Ѽ�")]
    public float speed;
    public float crouchSpeed;
    public float jumpForce;
    public float crouchjumpForce;
    [Header("���A")]
    public bool isCrouch;
    //���a����}�ۤU��A���]���a�ΫO���ۤU���A
    public bool crouchHeld;



    //�I����ؤo
    Vector2 colliderStandSize;
    Vector2 colliderStandOffset;
    Vector2 colliderCrouchSize;
    Vector2 colliderCrouchOffset;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new PlayerInputControl();
        //Jump.�᭱�i�H���ܦh���P���ɾ��Astarted�O�䤤�@�ءA+= �O���U�@�Ө�ơA���U����Ʀb�U��
        inputControl.Gameplay.Jump.started += Jump;


        //���즳���j�p�P��m�]�w
        colliderStandSize = box.size;
        colliderStandOffset = box.offset;
        //�]�w�ۤU�᪺�j�p�P��m�]�w
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

    //�g���ʪ��C�հ���
    private void Update()
    {
        //inputDirection�|�ɮɰ����ڱ���������ʬ��� 
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();

    }

    //�g���ʪ��T�w����A�q�`�򪫲z�����|��b�o��
    private void FixedUpdate()
    {
        Move();
        
    }

    public void Move() 
    {

        //�U�ۧP�_�A�Y�H����@��false�A�hisCrouch�����Lfalse�F��̳�true�htrue
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

        //��{�H�����ʡA�ǥ�inputDirection�����쪺����O+1��-1�A���H�ڦ۳]���ܶqspeed�A�A���H�קK�q���]�ƾɭP���~�t�ȡCY�ȽƼg�^Rigibody����Y
        
        if (isCrouch || crouchHeld)
            rb.velocity = new Vector2(inputDirection.x * crouchSpeed * Time.deltaTime, rb.velocity.y);
        else if(!isCrouch)
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);


        //�{���ܶq�y�¦V�Aint�O��ơA��ransform.lossyScale.x���ӬO�B�I�ơA�ǥ�(int)�j��⥦�ܦ����
        int faceDir = (int)transform.lossyScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;

        //�H��½��(�ΨC��unity���鳣����transform�ӧ�A�]�����ݭnGetComponent)�A�NScale(�@�ӤT���ƭ�)�л\���s����
        transform.localScale = new Vector3(faceDir, 1, 1);

        

    }

     






    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("JUMP")
        //rigibody���@��transform.up(�@�ɮy�ЦV�W��V���O)*jumpForce�A�o�ˤw�g�g�n�F�A�᭱�i�H�W�[ForceMode2D�AImpulse�O���ɪ��O�BForce�O���q��
        if(physicsCheck.isGround && !isCrouch)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        if (physicsCheck.isGround && isCrouch)
            rb.AddForce(transform.up * crouchjumpForce, ForceMode2D.Impulse);
    }

   
}
