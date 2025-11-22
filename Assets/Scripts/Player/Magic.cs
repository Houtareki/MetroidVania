using UnityEngine;

public class Magic : MonoBehaviour
{
    public Player player;
    public float spellRange;
    public float spellCooldown;
    public LayerMask obstacleLayer;

    public float playerRadius = 1.5f;

    public bool CanCast => Time.time >= _nextCastTime;
    private float _nextCastTime;
    
    
    public void AnimationFinished()
    {
        player.AnimationFinished();
        CastSpell();
    }

    private void CastSpell()
    {
        Teleport();
    }

    private void Teleport()
    {
        var direction = new Vector2(player.faceDirection, 0);
        var targetPosition = (Vector2)player.transform.position + direction * spellRange;
        
        var hit = Physics2D.OverlapCircle(targetPosition, playerRadius, obstacleLayer);

        if (hit != null)
        {
            var step = .1f;
            var adjustedPosition = targetPosition;

            while (hit != null && Vector2.Distance(adjustedPosition, player.transform.position) > 0)
            {
                adjustedPosition -= direction * step;
                hit = Physics2D.OverlapCircle(adjustedPosition, playerRadius, obstacleLayer);
            }
            targetPosition = adjustedPosition;
        }
        
        player.transform.position = targetPosition;
        _nextCastTime = Time.time + spellCooldown;
    }
}
