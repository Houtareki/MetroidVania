using UnityEngine;

public class PlayerSlideState : PlayerState
{
    private float _slideTimer;
    private float _slideStopTimer;
    
    public PlayerSlideState(Player player) : base(player) {}

    private static readonly int IsSliding = Animator.StringToHash("isSliding");

    public override void Enter()
    {
        base.Enter();

        _slideTimer = player.slideDuration;
        _slideStopTimer = 0;
        player.SetColliderSlide();
        anim.SetBool(IsSliding, true);
    }

    public override void Update()
    {
        base.Update();
        
        if (_slideTimer > 0)
            _slideTimer -= Time.deltaTime;
        else if (_slideStopTimer >= 0)
        {
            _slideStopTimer -= player.slideStopDuration;
        }
        else
        {
            _slideStopTimer -= Time.deltaTime;

            if (_slideStopTimer <= 0)
            {
                if (player.CheckForCeiling() || MoveInput.y <= -.1f)
                    player.ChangeState(player.crouchState);
                else
                    player.ChangeState(player.idleState);
            }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_slideTimer > 0)
            player.rb.linearVelocity = new Vector2(player.slideSpeed * player.faceDirection, player.rb.linearVelocity.y);
        else
            player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        
        anim.SetBool(IsSliding, false);
    }
}
