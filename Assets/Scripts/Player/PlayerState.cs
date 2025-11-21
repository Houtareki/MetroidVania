using UnityEngine;

public abstract class PlayerState
{
    protected readonly Player player;
    protected readonly Animator anim;
    protected readonly Rigidbody2D rb;
    protected readonly Combat combat;

    protected bool JumpPressed
    {
        get => player.jumpPressed; 
        set => player.jumpPressed = value;
    }

    protected bool JumpReleased
    {
        get => player.jumpReleased;
        set => player.jumpReleased = value;
    }

    protected bool RunPressed => player.runPressed;
    protected bool AttackPressed => player.attackPressed;
    protected Vector2 MoveInput => player.moveInput;

    protected PlayerState(Player player)
    {
        this.player = player;
        anim = player.anim;
        rb = player.rb;
        combat = player.combat;
    }
    
    public virtual void Enter(){}
    public virtual void Exit(){}
    
    public virtual void Update(){}
    public virtual void FixedUpdate(){}
    
    public virtual void AttackAnimationFinished(){}
}
