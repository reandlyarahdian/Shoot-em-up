using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    bool IsFacingRight;
    Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 movePos = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb2D.velocity = movePos * speed;

        if ((movePos.x > 0 && !IsFacingRight) || (movePos.x < 0 && IsFacingRight))
            Flip();

    }
    private void Flip()
    {
        IsFacingRight = !IsFacingRight;
        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
