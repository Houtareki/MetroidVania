using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Spark Spell")]
public class SparkSpellSO : SpellSO
{
    [Header("Spark Settings")]
    public int damage = 3;
    public float radius = 5;
    public GameObject sparkFXPrefab;
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

            if (sparkFXPrefab != null)
            {
                var newFX = Instantiate(sparkFXPrefab, enemy.transform.position, Quaternion.identity);
                Destroy(newFX, 2);
            }
        }

    }
}
