using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    public Image iconImage;
    public GameObject highlight;
    
    public SpellSO AssignedSpell {get; private set;}
    
    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightColor = Color.white;
    private readonly Vector3 _normalScale = Vector3.one;
    private readonly Vector3 _highlightScale = Vector3.one * 1.2f;

    public void SetSpell(SpellSO spellSo)
    {
        AssignedSpell = spellSo;

        if (spellSo != null)
        {
            iconImage.sprite = spellSo.icon;
            iconImage.gameObject.SetActive(true);
        }
        else
        {
            AssignedSpell = null;
            iconImage.sprite = null;
            iconImage.gameObject.SetActive(false);
        }
        
        SetHighLight(false);
    }

    public void SetHighLight(bool active)
    {
        highlight.SetActive(active);
        
        iconImage.color = active ? highlightColor : normalColor;
        iconImage.rectTransform.localScale = active ? _highlightScale : _normalScale;
    }
}
