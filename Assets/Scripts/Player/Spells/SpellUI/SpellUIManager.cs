using System.Collections.Generic;
using UnityEngine;

public class SpellUIManager : MonoBehaviour
{
    [SerializeField] private List<SpellSlot> slots = new ();

    public void ShowSpells(List<SpellSO> spells)
    {
        for (var i = 0; i < spells.Count; i++)
        {
            if (i < slots.Count)
                slots[i].SetSpell(spells[i]);
            else
                slots[i].SetSpell(null);
        }
    }

    public void HighlightSpell(SpellSO activeSpell)
    {
        foreach (var slot in slots)
            slot.SetHighLight(slot.AssignedSpell == activeSpell);
    }
}
