using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public Vector2 bottomOffset;
    public float checkRaduis;
    public LayerMask groundLayer;    
    public bool isGround;
    public bool isHeadBlock;
    private BoxCollider2D box;
    //�w�q�Y����ߡAbounds ��� Collider����ɮءA extents �h�O bounds ���@�b�ؤo
    


    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        


}


    private void Update()
    {
        Check();
    }

    public void Check() 
    {
        //�˴��a���Atransform.position���ӬO�@�ӤT���V�q�A�ǥѫe���[�W(Vector2)�j���ܦ�2���A�o�~�i�H��bottomOffset���ۥ[
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);

        Vector2 HeadCenter = (Vector2)transform.position + new Vector2(0f, box.bounds.size.y);

        //�˴��Y�W���S���F��
        isHeadBlock = Physics2D.OverlapCircle(HeadCenter ,  checkRaduis, groundLayer);

    }

    //Gizmos�O���U�u
    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        //Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(0f, box.bounds.size.y), checkRaduis);
    }
}
