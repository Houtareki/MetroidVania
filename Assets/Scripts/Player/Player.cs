using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerState _currentState;
    
    public PlayerIdleState idleState;
    public PlayerJumpState jumpState;
    public PlayerMoveState moveState;
    public PlayerCrouchState crouchState;
    public PlayerSlideState slideState;
    public PlayerAttackState attackState;
    public PlayerSpellcastState spellcastState;
    
    [Header("Core components")]
    public Combat combat;
    public Magic magic;
    public Health health;
    
    [Header("Components")]
    public Rigidbody2D rb;
    public PlayerInput playerInput;
    public Animator anim;
    public CapsuleCollider2D playerCollider;
    
    [Header("Movement variables")]
    public float walkSpeed;
    public float runSpeed = 8;
    public float jumpForce;
    public float jumpCutMultiplier = .5f;
    public float normalGravity;
    public float fallGravity;
    public float jumpGravity;
    
    public int faceDirection = 1;
    
    //Inputs
    public Vector2 moveInput;
    public bool runPressed;
    public bool jumpPressed;
    public bool jumpReleased;
    public bool attackPressed;
    public bool spellcastPressed;
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isGrounded;
    
    [Header("Crouch Check")]
    public Transform headCheck;
    public float headCheckRadius= .2f;
    
    [Header("Slide Settings")]
    public float slideDuration = .6f;
    public float slideSpeed = 12;
    public float slideStopDuration = .15f;

    public float slideHeight;
    public Vector2 slideOffset;
    public float normalHeight;
    public Vector2 normalOffset;
    
    public bool isSliding;
    
    [Header("Animation")]
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int YVelocity  = Animator.StringToHash("yVelocity");

    private void Awake()
    {
        idleState = new PlayerIdleState(this);
        jumpState = new PlayerJumpState(this);
        moveState = new PlayerMoveState(this);
        crouchState = new PlayerCrouchState(this);
        slideState = new PlayerSlideState(this);
        attackState = new PlayerAttackState(this);
        spellcastState = new PlayerSpellcastState(this);
    }
    
    private void Start()
    {
        rb.gravityScale = normalGravity;
        
        ChangeState(idleState);
    }

    private void Update()
    {
        _currentState.Update();
        
        if (!isSliding)
            Flip();
        HandleAnimations();
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdate();
        CheckGrounded();
    }

    public void ChangeState(PlayerState newState)
    {
        if (_currentState != null)
            _currentState.Exit();
        
        _currentState = newState;
        _currentState.Enter();
    }

    public void SetColliderNormal()
    {
        playerCollider.size = new Vector2(playerCollider.size.x, normalHeight);
        playerCollider.offset = normalOffset;
    }

    public void SetColliderSlide()
    {
        playerCollider.size = new Vector2(playerCollider.size.x, slideHeight);
        playerCollider.offset = slideOffset;
    }

    public void ApplyVariableGravity()
    {
        if (rb.linearVelocity.y < -0.1f)
        {
            rb.gravityScale = fallGravity;
        }
        else if (rb.linearVelocity.y > 0.1f)
        {
            rb.gravityScale = jumpGravity;
        }
        else
        {
            rb.gravityScale = normalGravity;
        }
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public bool CheckForCeiling()
    {
        return Physics2D.OverlapCircle(headCheck.position, headCheckRadius, groundLayer);
    }

    private void HandleAnimations()
    {
        anim.SetBool(IsGrounded, isGrounded);
        anim.SetFloat(YVelocity, rb.linearVelocity.y);
    }

    private void Flip()
    {
        if (moveInput.x > 0.1f)
            faceDirection = 1;
        else if (moveInput.x < -0.1f)
            faceDirection = -1;
        
        transform.localScale = new Vector3(faceDirection, 1, 1);
    }

    public void AnimationFinished()
    {
        _currentState.AnimationFinished();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        runPressed = value.isPressed;
    }

    public void OnAttack(InputValue value)
    {
        attackPressed = value.isPressed;
    }

    public void OnPrevious(InputValue value)
    {
        if (value.isPressed)
            magic.PreviousSpell();
    }

    public void OnNext(InputValue value)
    {
        if (value.isPressed)
            magic.NextSpell();
    }
    
    public void OnSpellcast(InputValue value)
    {
        spellcastPressed = value.isPressed;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            jumpPressed = true;
            jumpReleased = false;
        }
        else
        {
            jumpReleased = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(headCheck.position, headCheckRadius);
    }
}
