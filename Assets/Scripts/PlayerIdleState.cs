using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player) : base(player) { }
    
    private static readonly int IsIdle = Animator.StringToHash("isIdle");
    
    public override void Enter()
    {
        anim.SetBool(IsIdle, true);
        player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (JumpPressed)
        {
            JumpReleased = false;
            player.ChangeState(player.jumpState);
        }
        else if (Mathf.Abs(MoveInput.x) > .1f)
        {
            player.ChangeState(player.moveState);
        }
        else if (MoveInput.y < -.1f)
        {
            player.ChangeState(player.crouchState);
        }
    }
    
    public override void Exit()
    {
        anim.SetBool(IsIdle, false);
    }
}
