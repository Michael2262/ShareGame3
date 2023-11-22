using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    //�p����RD2D�ȡA�bawake���A�ǥ�GetComponent��o�~��<Rigidbody2D>�ۨ�����
    private Rigidbody2D rb;
    public Vector2 inputDirection;
    [Header("�򥻰Ѽ�")]
    public float speed;
    public float jumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputControl = new PlayerInputControl();
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
        //��{�H�����ʡA�ǥ�inputDirection�����쪺����O+1��-1�A���H�ڦ۳]���ܶqspeed�A�A���H�קK�q���]�ƾɭP���~�t�ȡCY�ȽƼg�^Rigibody����Y
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
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

}
