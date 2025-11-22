using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Fire Spell")]
public class FireSpellSO : SpellSO
{
    [Header("Fire Settings")]
    public int damage;
    public float duration;
    public float radius;
    public GameObject fireFXPrefab;
    public LayerMask enemyLayer;
    
    public override void Cast(Player player)
    {
        var enemies = Physics2D.OverlapCircleAll(player.transform.position, radius, enemyLayer);

        foreach (var enemy in enemies)
        {
            var health = enemy.GetComponent<Health>();
            if (health != null)
            {
                health.ChangeHealth(-damage);
            }

            if (fireFXPrefab != null)
            {
                var newFX = Instantiate(fireFXPrefab, enemy.transform.position, Quaternion.identity);
                Destroy(newFX, 2);
            }
        }
    }
}
