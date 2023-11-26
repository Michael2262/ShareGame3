using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D;

    private void Awake()
    {
        //���
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void Start()
    {
        //Debug.LogError("1�����D");
        GetNewGameraBounds();
    }

    private void GetNewGameraBounds()
    {
        //var�гy�{���ܶqobj�CFind���ܦh�F��i�H��A�o���O�����Tag
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        //�ȧ�ܤ[�Areturn�N�|����N�X
        if (obj == null)
        {
            
            return;
        }

           

        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        
        //�M�w�s
        confiner2D.InvalidateCache();
    }

}
