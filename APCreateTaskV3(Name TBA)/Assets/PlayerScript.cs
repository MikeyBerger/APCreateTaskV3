using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    Vector2 Movement;
    private Rigidbody2D RB;
    public bool IsDashing;
    public bool IsJumping;
    public bool Airborn;
    public float Speed;
    public bool FacingRight;
    public float JumpForce;
    public float DashForce;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        Flip();

        RB.velocity = new Vector2(Movement.x, 0) * Time.deltaTime * Speed;

        if (IsJumping && !Airborn)
        {
            RB.AddForce(new Vector2(Movement.x, JumpForce) * Time.deltaTime);
            //Airborn = true;
            IsJumping = false;
        }

        if (IsDashing)
        {
            RB.AddForce(new Vector2(Movement.x + DashForce, 0) * Time.deltaTime);
            IsDashing = false;
        }
    }

    #region InputActions

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Movement = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            IsJumping = true;
        }
    }

    public void OnDash(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            IsDashing = true;
        }
    }

    #endregion

    void Flip()
    {
        if (Movement.x > 0 && !FacingRight || Movement.x < 0 && FacingRight)
        {
            FacingRight = !FacingRight;

            Vector3 Scale;
            Scale = transform.localScale;

            Scale.x *= -1;

            transform.localScale = Scale;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Airborn = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Airborn = false;
        }
    }


}
