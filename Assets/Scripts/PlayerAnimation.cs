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
        //GetComponentInParent獲得父級的Component，理所當然也有能獲得子級的
        controller = GetComponentInParent<PlayerController>();
        physicsCheck = GetComponentInParent<PhysicsCheck>();

        //groundID會是用於將字符串轉換為哈希碼（Hash Code）。哈希碼通常用於優化字符串比較，因為哈希碼比直接比較字符串更快。
        groundID = Animator.StringToHash("isOnGround");
    }

   
    void Update()
    {
        //anim.SetFloat("speed",Mathf.Abs(controller.))
        anim.SetBool("isCrouching", controller.isCrouch);

        //嘗試另一種寫法
        //anim.SetBool("isOnGround", physicsCheck.isGround);
        anim.SetBool(groundID, physicsCheck.isGround);

    }
}
