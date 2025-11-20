using UnityEngine;

public abstract class PlayerState
{
    protected readonly Player player;
    protected readonly Animator anim;

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
    protected Vector2 MoveInput => player.moveInput;

    protected PlayerState(Player player)
    {
        this.player = player;
        anim = player.anim;
    }
    
    public virtual void Enter(){}
    public virtual void Exit(){}
    
    public virtual void Update(){}
    public virtual void FixedUpdate(){}
}
