using UnityEngine;

[CreateAssetMenu(menuName = "Spells/TeleportSpell")]
public class TeleportSpellSO : SpellSO
{
    [Header("Teleport Settings")]
    public float range = 5;
    public float playerRadius = 5;
    public LayerMask obstacleLayer;
    
    public override void Cast(Player player)
    {
                
        var direction = new Vector2(player.faceDirection, 0);
        var targetPosition = (Vector2)player.transform.position + direction * range;
        
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
    }
}
