using System.Collections;
using UnityEngine;

public abstract class SpellSO : CollectibleSO
{
    [Header("General")]
    public float cooldown;

    public bool isDot;

    public override void Collect(Player player)
    {
        player.magic.LearnSpell(this);
    }

    public abstract void Cast(Player player);

    public virtual IEnumerator CastCoroutine(Player player)
    {
        yield break;
    }
}
