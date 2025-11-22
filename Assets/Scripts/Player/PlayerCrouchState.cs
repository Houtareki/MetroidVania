using UnityEngine;

public class PlayerCrouchState : PlayerState
{
    public PlayerCrouchState (Player player) : base(player) {}
    
    private static readonly int IsCrouching = Animator.StringToHash("isCrouching");

    public override void Enter()
    {
        base.Enter();
        
        anim.SetBool(IsCrouching, true);
        player.SetColliderSlide();
    }

    public override void Update()
    {
        base.Update();

        if (player.CheckForCeiling())
            return;
        
        if (JumpPressed)
        {
            player.ChangeState(player.jumpState);
        }
        else if (MoveInput.y > -.1f && !player.CheckForCeiling())
        {
            player.ChangeState(player.idleState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Mathf.Abs(MoveInput.x) > .1f)
            rb.linearVelocity = new Vector2(player.faceDirection * player.walkSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        
        anim.SetBool(IsCrouching, false);
        player.SetColliderNormal();
    }
}