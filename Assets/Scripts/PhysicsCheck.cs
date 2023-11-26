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
    //定義頭頂圓心，bounds 表示 Collider的邊界框， extents 則是 bounds 的一半尺寸
    


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
        //檢測地面，transform.position本來是一個三維向量，藉由前面加上(Vector2)強制變成2維，這才可以跟bottomOffset做相加
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);

        Vector2 HeadCenter = (Vector2)transform.position + new Vector2(0f, box.bounds.size.y);

        //檢測頭上有沒有東西
        isHeadBlock = Physics2D.OverlapCircle(HeadCenter ,  checkRaduis, groundLayer);

    }

    //Gizmos是輔助線
    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        //Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(0f, box.bounds.size.y), checkRaduis);
    }
}
