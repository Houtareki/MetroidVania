using System;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [Header("References")]
    public Player player;
    public SpellUIManager spellUIManager;
    
    [Header("Spell State")]
    [SerializeField] private List<SpellSO> availableSpells = new ();
    [SerializeField] private int currentIndex = 0;
    private SpellSO CurrentSpell => availableSpells.Count > 0 ? availableSpells[currentIndex] : null;
    
    public bool CanCast => Time.time >= _nextCastTime;
    private float _nextCastTime;

    private void Start()
    {
        spellUIManager.ShowSpells(availableSpells);
        HightlightCurrentSpell();
    }

    public void NextSpell()
    {
        if (availableSpells.Count == 0) return;
        
        currentIndex = (currentIndex + 1) % availableSpells.Count;
        HightlightCurrentSpell();
    }

    public void PreviousSpell()
    {
        if (availableSpells.Count == 0) return;
        
        currentIndex = (currentIndex - 1 + availableSpells.Count) % availableSpells.Count;
        HightlightCurrentSpell();
    }

    private void HightlightCurrentSpell()
    {
        if (CurrentSpell != null)
        {
            spellUIManager.HighlightSpell(CurrentSpell);
        }
    }

    public void AnimationFinished()
    {
        player.AnimationFinished();
        CastSpell();
    }

    private void CastSpell()
    {
        if (!CanCast && CurrentSpell == null)
            return;
        
        
        if (CurrentSpell.isDot)
            StartCoroutine(CurrentSpell.CastCoroutine(player));
        else
            CurrentSpell.Cast(player);

        _nextCastTime = Time.time + CurrentSpell.cooldown;
    }
}
