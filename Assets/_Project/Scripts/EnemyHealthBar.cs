using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health health;
    [SerializeField] private Image fillImage;
    [SerializeField] private Image delayedFillImage;

    [Header("Delayed Bar Behavior")] 
    [SerializeField] private float delayBeforeDeplete = 0.4f;
    [SerializeField] private float depleteDuration = 0.3f;

    private Tween delayedTween;

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

    private void OnDisable()
    {
        if (health != null)
        {
            health.OnDamaged -= HandleDamaged;
        }

        delayedTween?.Kill();
    }
    
    

    private void Start()
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = health.HealthPercent;
        }

        if (delayedFillImage != null)
        {
            delayedFillImage.fillAmount = health.HealthPercent;
        }
    }
    
    private void HandleDamaged(DamageInfo info)
    {
        float newPercent = health.HealthPercent;

        if (fillImage != null)
        {
            fillImage.fillAmount = newPercent;
        }

        delayedTween?.Kill();

        delayedTween = DOTween.Sequence()
            .AppendInterval(delayBeforeDeplete)
            .Append(delayedFillImage.DOFillAmount(newPercent, depleteDuration).SetEase(Ease.InQuad));
    }
}
