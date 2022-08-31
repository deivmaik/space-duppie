using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    //Player movement variables
    public float jumpForce = 6f;
    public float runningSpeed = 2f;

    Rigidbody2D rigidBody;
    Animator animator;
    Vector3 startPosition;

    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    public LayerMask groundMask;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
        
        startPosition = this.transform.position;
    }

    public void StartGame()
    {
        this.transform.position = startPosition;
        this.rigidBody.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());

        // Debug.DrawRay(this.transform.position, Vector2.down*1.5f, Color.red);
    }
    
    void FixedUpdate()
    {
        if(gameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (rigidBody.velocity.x < runningSpeed)
            {
            rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);

        } else{
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
        }
    }

    void Jump()
    {
        if(IsTouchingTheGround())
        {
            rigidBody.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
        }
    }

    // Method indicates if the player is touching the floor or not
    bool IsTouchingTheGround()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 2.0f, groundMask)){
            return true;
        } else {
            return false;
        }
    }

    public void Die(){
        this.animator.SetBool(STATE_ALIVE, false);
        gameManager.sharedInstance.gameOver();
    }
}
