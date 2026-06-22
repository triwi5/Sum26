using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image fillImage;


    private void Awake()
    {
        if (health == null)
        {
            health = GetComponentInParent<Health>();
        }
    }
    private void OnEnable()
    {
        if (health != null)
        {
            health.OnDamaged += HandleDamaged;
        }
    }

    private void OnDisabled()
    {
        if (health != null)
        {
            health.OnDamaged -= HandleDamaged;
        }
    }

    private void HandleDamaged(float amount, Vector3 hitPoint)
    {
        UpdateFill();
    }

   private void UpdateFill()
    {
        if (health == null || fillImage == null) return;
        fillImage.fillAmount = health.HealthPercent;
    }

    private void Start()
    {
        UpdateFill();
    }
}
