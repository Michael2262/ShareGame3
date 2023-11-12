using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    [Header("移動參數")]
    public float speed = 8f;
    public float crouchSpeedDivisor = 3f;

    [Header("狀態")]
    public bool isCrouch;

    float xVelocity;

    //碰撞體尺寸
    Vector2 colliderStandSize;
    Vector2 colliderStandOffset;
    Vector2 colliderCrouchSize;
    Vector2 colliderCrouchOffset;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent< Rigidbody2D >();
        coll = GetComponent< BoxCollider2D >();

        colliderStandSize = coll.size;
        colliderStandOffset = coll.offset;
        colliderCrouchSize = new Vector2(coll.size.x, coll.size.y / 2f);
        colliderCrouchOffset = new Vector2(coll.offset.x, coll.offset.y / 2f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GroundMovement();
    }

    void GroundMovement() 
    {
        if (Input.GetButton("Crouch"))
            Crouch();
        else if (!Input.GetButton("Crouch") && isCrouch)
            StandUp();

        xVelocity = Input.GetAxis("Horizontal"); //-1f~1f

        if (isCrouch)
            xVelocity /= crouchSpeedDivisor;

        rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);

        FilpDirction();

    }

    void FilpDirction()
    {
        if (xVelocity < 0)
            transform.localScale = new Vector2(-1, 1);
        if (xVelocity > 0)
            transform.localScale = new Vector2(1, 1);
    }

    void Crouch()
    {
        isCrouch = true;
        coll.size = colliderCrouchSize;
        coll.offset = colliderCrouchOffset;
    }

    void StandUp()
    {
        isCrouch = false;
        coll.size = colliderStandSize;
        coll.offset = colliderStandOffset;
    }
}
