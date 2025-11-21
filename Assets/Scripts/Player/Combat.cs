using UnityEngine;

public class Combat : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damage;
    public float attackRadius = .5f;
    public float attackCooldown = 1.5f;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public Animator hitFX;
    
    public Player player;
    
    public bool CanAttack => Time.time >= _nextAttackTime;
    private float _nextAttackTime;
    private static readonly int HitFX = Animator.StringToHash("HitFX");

    public void AttackAnimationFinished()
    {
        player.AttackAnimationFinished();
    }

    public void Attack()
    {
        if (!CanAttack)
            return;

        _nextAttackTime = Time.time + attackCooldown;
        
        var enemy = Physics2D.OverlapCircle(attackPoint.position, attackRadius, enemyLayer);
        
        if (enemy != null)
        {
            hitFX.Play(HitFX);
            enemy.gameObject.GetComponent<Health>().ChangeHealth(-damage);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.aquamarine;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
