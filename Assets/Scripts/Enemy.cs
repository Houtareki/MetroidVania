using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public Health health;
    
    [Header("Death FX Pieces")]
    [SerializeField] private GameObject[] deathParts;

    [SerializeField] private float spawnForce = 5;
    [SerializeField] private float torque = 5;
    [SerializeField] private float lifeTime = 2;
    
    
    [Header("Animation")]
    private static readonly int IsDamaged = Animator.StringToHash("isDamaged");

    private void OnEnable()
    {
        if (health != null)
        {
            health.OnDamaged += HandleDamage;
            health.OnDeath += HandleDeath;
        }
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.OnDamaged -= HandleDamage;
            health.OnDeath -= HandleDeath;
        }
    }

    private void HandleDamage()
    {
        anim.SetTrigger(IsDamaged);
    }

    private void HandleDeath()
    {
        foreach (var prefab in deathParts)
        {
            var rotation = Quaternion.Euler(0, 0, Random.Range(0.5f, 1));
            var part = Instantiate(prefab, transform.position, rotation);
            
            var rb = part.GetComponent<Rigidbody2D>();
            
            var randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(.5f, 1f)).normalized;
            rb.linearVelocity = randomDirection * spawnForce;
            rb.AddTorque(Random.Range(-torque, torque), ForceMode2D.Impulse);
            
            Destroy(part, lifeTime);
        }
        
        Destroy(gameObject);
    }
}
