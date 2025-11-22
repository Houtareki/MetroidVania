using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Fire Spell")]
public class FireSpellSO : SpellSO
{
    [Header("Fire Settings")]
    public int damage;
    public float duration;
    public float tickInterval = 1f;
    public float radius;
    public GameObject fireFXPrefab;
    public LayerMask enemyLayer;

    public override IEnumerator CastCoroutine(Player player)
    {
        var timer = 0f;
        var castPos = player.transform.position;
        var waitForSeconds = new WaitForSeconds(tickInterval);
        
        while (timer < duration)
        {
            var enemies = Physics2D.OverlapCircleAll(castPos, radius, enemyLayer);

            foreach (var enemy in enemies)
            {
                if (enemy.TryGetComponent(out Health health))
                {
                    health.ChangeHealth(-damage);
                }
                
                if (fireFXPrefab != null)
                {
                    var aoeFX = Instantiate(fireFXPrefab, enemy.transform.position, Quaternion.identity);
                    Destroy(aoeFX, 1f);
                }
            }

            timer += tickInterval;
            yield return waitForSeconds;
        }
    }

    public override void Cast(Player player)
    {
    }
}