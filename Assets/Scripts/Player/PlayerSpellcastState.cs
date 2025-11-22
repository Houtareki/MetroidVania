using UnityEngine;

public class PlayerSpellcastState : PlayerState
{
    public PlayerSpellcastState (Player player) : base(player){}
    private static readonly int IsCasting = Animator.StringToHash("isCasting");

    public override void Enter()
    {
        base.Enter();
        
        anim.SetBool(IsCasting, true);
    }

    public override void AnimationFinished()
    {
        base.AnimationFinished();
        
        if (Mathf.Abs(MoveInput.x) > .1f)
            player.ChangeState(player.moveState);
        else
            player.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
        
        anim.SetBool(IsCasting, false);
    }
}
