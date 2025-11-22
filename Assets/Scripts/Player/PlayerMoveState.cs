using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState (Player player) : base(player){}

    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    public override void Update()
    {
        base.Update();

        if (SpellcastPressed && magic.CanCast)
            player.ChangeState(player.spellcastState);
        else if (AttackPressed && combat.CanAttack)
            player.ChangeState(player.attackState);
        else if (JumpPressed)
            player.ChangeState(player.jumpState);
        else if (Mathf.Abs(MoveInput.x) < .1f)
            player.ChangeState(player.idleState);
        else if (player.isGrounded && MoveInput.y < -.1f)
        {
            if (RunPressed)
                player.ChangeState(player.slideState);
            else
                player.ChangeState(player.crouchState);
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
        rb.linearVelocity = new Vector2(MoveInput.x * speed, rb.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool(IsWalking, false);
        anim.SetBool(IsRunning, false);
    }
}
