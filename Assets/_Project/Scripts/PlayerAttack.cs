using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack State")]
    [SerializeField] private float attackRadius = 3f;
    [SerializeField] private float attackDamage = 25f;
    [SerializeField] private float attackCooldown = 0.5f;

    [Header("Targeting")] 
    [SerializeField] private LayerMask enemyLayer;
    
    private PlayerInputActions inputActions;
    private float cooldownTimer;

    private void Awake()
    {
        inputActions=new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Attack.performed += OnAttack;

    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (cooldownTimer > 0f) return;

        PerformAttack();

        cooldownTimer = attackCooldown;
    }

    private void PerformAttack()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius, enemyLayer);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(attackDamage, hit.transform.position);
            }
        }

        Debug.Log($"Attack! Hit {hits.Length} target(s).");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }


}
