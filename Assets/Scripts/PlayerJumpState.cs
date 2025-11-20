using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player) : base(player){ }
    
    private static readonly int IsJumping = Animator.StringToHash("isJumping");

    public override void Enter()
    {
        base.Enter();
        anim.SetBool(IsJumping, true);
        
        player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, player.jumpForce);
        
        JumpPressed = false;
        JumpReleased = false;
    }

    public override void Update()
    {
        base.Update();

        if (player.isGrounded && player.rb.linearVelocity.y <= 0)
        {
            if (Mathf.Abs(MoveInput.x) > .1f)
                player.ChangeState(player.moveState);
            else
                player.ChangeState(player.idleState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.ApplyVariableGravity();

        if (JumpReleased && player.rb.linearVelocity.y > 0)
        {
            player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, player.rb.linearVelocity.y * player.jumpCutMultiplier);
            JumpReleased = false;
        }

        var speed = RunPressed ? player.runSpeed : player.walkSpeed;
        var targetSpeed = speed * MoveInput.x;
        player.rb.linearVelocity = new Vector2(targetSpeed, player.rb.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool(IsJumping, false);
    }
}