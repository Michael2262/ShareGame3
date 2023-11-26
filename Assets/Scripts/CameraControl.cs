using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D;

    private void Awake()
    {
        //賦值
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void Start()
    {
        //Debug.LogError("1有問題");
        GetNewGameraBounds();
    }

    private void GetNewGameraBounds()
    {
        //var創造臨時變量obj。Find有很多東西可以找，這次是找標籤Tag
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        //怕找很久，return就會停止代碼
        if (obj == null)
        {
            
            return;
        }

           

        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        
        //清緩存
        confiner2D.InvalidateCache();
    }

}
