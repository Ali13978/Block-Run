using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class player : MonoBehaviour
{
  // Config
  [SerializeField] float MovementSpeed =5f;
  [SerializeField] int JumpSpeed = 5;
  [SerializeField] int ClimbingSpeed = 5;
    Vector2 RespawnPos;
    bool Abc = true;
  float CurrentGravity = 0f;
  // States
  // Cache
    Rigidbody2D MyRigidBody;
    Animator MyAnimator;
    BoxCollider2D MyBoxCollider;
    CapsuleCollider2D MyCapsuleCollider;
    bool CanWalk;

    // Buttons
    bool JumpButtonDown = false;

    private void Start()
    {
        CanWalk = true;
        RespawnPos = new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y);
        MyAnimator = GetComponent<Animator>();
        MyRigidBody = GetComponent<Rigidbody2D>();
        MyBoxCollider = GetComponent<BoxCollider2D>();
        MyCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        Jump();
        Movement();
        FlipSprite();
        Death();
        ClimbLadders();
    }

    public void JumpButtonPressed()
    {
        StartCoroutine(JumpButtonPressedCorutine());
    }

    private IEnumerator JumpButtonPressedCorutine()
    {
        JumpButtonDown = true;
        yield return new WaitForEndOfFrame();
        JumpButtonDown = false;
    }

    private void Movement()
    {
        
        float ControlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 PlayerVelocity = new Vector2(ControlThrow*MovementSpeed, MyRigidBody.velocity.y);
        MyRigidBody.velocity = PlayerVelocity;
        bool IfPlayerHasHorizontalSpeed = Mathf.Abs(MyRigidBody.velocity.x) > Mathf.Epsilon;
        MyAnimator.SetBool("IsRunning", IfPlayerHasHorizontalSpeed);
    }

    private void StopWalking()
    {
        if(CanWalk == false)
        {
            Debug.Log("ABC");
            MyRigidBody.velocity = new Vector2(0,-9.8f);
        }
    }

    public void ContinueMovement()
    {
        CanWalk = true;
    }

    public void StopMoving()
    {
        CanWalk = false;
    }

    private void FlipSprite()
    {
        bool IfPlayerHasHorizontalSpeed = Mathf.Abs(MyRigidBody.velocity.x) > Mathf.Epsilon;

        if (IfPlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(MyRigidBody.velocity.x), 1f);
        }
    }

    private void ClimbLadders()
    {
        if (!MyBoxCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        { 
            MyRigidBody.gravityScale = 1f;
            MyAnimator.SetBool("IsClimbing", false);
            return;
        }
        MyRigidBody.gravityScale = CurrentGravity;
        float ControlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 ClimbVelocity = new Vector2(MyRigidBody.velocity.x, ControlThrow * ClimbingSpeed);
        MyRigidBody.velocity = ClimbVelocity;
        bool PlayerHasVerticalSpeed = Mathf.Abs(MyRigidBody.velocity.y) > Mathf.Epsilon;
        MyAnimator.SetBool("IsClimbing", PlayerHasVerticalSpeed);
    }

    private void Jump()
    {
        if (!MyBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        { return; }
        if (Input.GetButtonDown("Jump") || JumpButtonDown == true)
        {
            GetComponent<AudioSource>().Play();
            Vector2 JumpVelocity = new Vector2(0f, JumpSpeed);
            MyRigidBody.velocity += JumpVelocity;
        }
    }

    private void Death()
    {

        if (MyCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            if (Abc == true)
            {
                StartCoroutine(ABC());
                FindObjectOfType<DeathCollider>().DecrementLives();
            }
            Respawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (MyBoxCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            {
                collision.gameObject.GetComponent<EnemyDeath>().EnemyDied();

            }
        }
    }

    private IEnumerator ABC()
    {
        Abc = false;
        yield return new WaitForSeconds(0.2f);
        Abc = true;
    }

    public void Respawn()
    {
        transform.position = RespawnPos;
    }
}