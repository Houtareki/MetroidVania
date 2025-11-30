using TMPro;
using UnityEngine;

public class Loot : MonoBehaviour
{
    private Player _player;
    [SerializeField] private CollectibleSO collectibleSo;

    public Animator anim;
    public TMP_Text itemMessage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<Player>();

        if (_player == null)
            return;

        CollectItem();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _player = null;
    }

    private void CollectItem()
    {
        itemMessage.text = "Found " + collectibleSo.itemName;
        anim.Play("CollectLoot");
        collectibleSo.Collect(_player);
        Destroy(gameObject, 1);
    }
}
