using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //Variables
   Vector2 Movement; //How the user will move left and right
   private Rigidbody2D RB; //Applies gravity to the user
   public bool IsDashing; //Boolean value for dashing
   public bool IsJumping; //Boolean value for jumping
   public bool Airborn; //Boolean value for when the square is off of the ground
   public float Speed; //The speed at which the square moves
   public bool FacingRight; //Boolean value for when the square is moving right
   public float JumpForce; //The amount of force the square uses to jump
   public float DashForce; //The speed at which the square dashes
   public int Lives = 3; //The amount of lives the user has
   public Text ScoreText; //The display for the user's current score
   public Text HisScoreText; //The display for the user's high score
   public Text LivesText; //The display for the user's number of lives left
    public string GameOver; //A string used to terminate the program
    #region PlayerPrefs
    public int Score; //The user's current score
    public int HiScore; //The user's high score
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>(); //Assigns a Rigidbody2D to the player
        HiScore = PlayerPrefs.GetInt("TheScore", 0); //Creates save data for player
    }
    // Update is called once per frame
    void Update()
    {
        Flip(); //Calls the Flip function
        RB.velocity = new Vector2(Movement.x, 0) * Time.deltaTime * Speed; //Controls player's movement on the ground
        //Calls jump method
        if (IsJumping && !Airborn)
        {
            RB.AddForce(new Vector2(Movement.x, JumpForce) * Time.deltaTime); //Controls player's movement in the air
            IsJumping = false;
        }
        //Calls dash method
        if (IsDashing && FacingRight)
        {
            RB.AddForce(Vector2.right * Time.deltaTime);
            IsDashing = false;
        }
        else if (IsDashing && !FacingRight)
        {
            RB.AddForce(Vector2.left * Time.deltaTime);
            IsDashing = false;
        }
        //Updates high score
        if (Score > HiScore)
        {
            PlayerPrefs.SetInt("TheScore", Score);
            HiScore = Score; //Sets the high score to the current score
        }
        if (Lives == 0) //If the player has 0 lives left
        {
            SceneManager.LoadScene(GameOver); //Transfers the player to the game over screen when the player runs out of lives
        }
        ScoreText.text = "Score: " + Score.ToString(); //Displays player's current score
        HisScoreText.text = "High Score: " + HiScore.ToString(); //Displays player's high score
        LivesText.text = "Lives: " + Lives.ToString(); //Displays how many lives the player has left
    }
    #region InputActions
    //Sets up player controls
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
        //Flips the player in the correct direction
        if (Movement.x > 0 && !FacingRight || Movement.x < 0 && FacingRight)
        {
            FacingRight = !FacingRight;
            Vector3 Scale;
            Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
        }
    }
    //When the player leaves the ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Airborn = true; //Set the boolean value "Airborn" true
        }
    }
    //When the player collides with something
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") //If it's the ground
        {
            Airborn = false; //Set the boolean value "Airborn" false
        }
        if (collision.gameObject.tag == "Red") //If it's a red circle (object #0 in the list)
        {
            Lives--; //Subtract one of the user's lives by 1
        }
        if (collision.gameObject.tag == "Green") //If it's a green circle (object #1 in the list)
        {
            Score++; //Increase the user's score by 1
        }
    }
    //When the player touches a trigger object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Red") //If it's a red circle
        {
            Lives--; //Subtract one of the user's lives by 1
            Destroy(collision.gameObject); //Destroy the red circle
        }
        if (collision.gameObject.tag == "Green")
        {
            Score++; //Increase the user's score by 1
            Destroy(collision.gameObject); //Destroy the green circle
        }
    }
}
