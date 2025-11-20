using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState (Player player) : base(player){}

    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    public override void Update()
    {
        base.Update();

        if (JumpPressed)
        {
            player.ChangeState(player.jumpState);
        }
        else if (Mathf.Abs(MoveInput.x) < .1f)
        {
            player.ChangeState(player.idleState);
        }
        else if (player.isGrounded && RunPressed && MoveInput.y < -.1f)
        {
            player.ChangeState(player.slideState);
        }
        else
        {
            anim.SetBool(IsWalking, !RunPressed);
            anim.SetBool(IsRunning, RunPressed);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        
        var speed = RunPressed ? player.runSpeed : player.walkSpeed;
        player.rb.linearVelocity = new Vector2(MoveInput.x * speed, player.rb.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool(IsWalking, false);
        anim.SetBool(IsRunning, false);
    }
}
