using System.Collections;
using UnityEngine;

public abstract class SpellSO : ScriptableObject
{
    [Header("General")]
    public string spellName;
    public float cooldown;
    public Sprite icon;

    public bool isDot;
    
    public abstract void Cast(Player player);

    public virtual IEnumerator CastCoroutine(Player player)
    {
        yield break;
    }
}
