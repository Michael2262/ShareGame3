using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    Animator anim;
    PlayerController controller;
    PhysicsCheck physicsCheck;

    int groundID;
    
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        //GetComponentInParent��o���Ū�Component�A�z�ҷ�M�]������o�l�Ū�
        controller = GetComponentInParent<PlayerController>();
        physicsCheck = GetComponentInParent<PhysicsCheck>();

        //groundID�|�O�Ω�N�r�Ŧ��ഫ�����ƽX�]Hash Code�^�C���ƽX�q�`�Ω��u�Ʀr�Ŧ����A�]�����ƽX�񪽱�����r�Ŧ��֡C
        groundID = Animator.StringToHash("isOnGround");
    }

   
    void Update()
    {
        //anim.SetFloat("speed",Mathf.Abs(controller.))
        anim.SetBool("isCrouching", controller.isCrouch);

        //���եt�@�ؼg�k
        //anim.SetBool("isOnGround", physicsCheck.isGround);
        anim.SetBool(groundID, physicsCheck.isGround);

    }
}
