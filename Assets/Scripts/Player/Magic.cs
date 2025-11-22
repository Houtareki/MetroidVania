using UnityEngine;

public class Magic : MonoBehaviour
{
    public Player player;
    public SpellSO currentSpell;
    
    public bool CanCast => Time.time >= _nextCastTime;
    private float _nextCastTime;
    
    
    public void AnimationFinished()
    {
        player.AnimationFinished();
        CastSpell();
    }

    private void CastSpell()
    {
        if (!CanCast && currentSpell == null)
            return;
        
        
        if (currentSpell.isDot)
            StartCoroutine(currentSpell.CastCoroutine(player));
        else
            currentSpell.Cast(player);

        _nextCastTime = Time.time + currentSpell.cooldown;
    }
}
