using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Heal Spell")]
public class HealSpellSO : SpellSO
{
    [Header("Heal Settings")]
    public int healAmount;
    public GameObject healFXPrefab;
    
    public override void Cast(Player player)
    {
        var newHealFX = Instantiate(healFXPrefab, player.transform.position + Vector3.down * .5f, Quaternion.identity);
        Destroy(newHealFX, 2f);
        
        player.health.ChangeHealth(healAmount);
    }
}
