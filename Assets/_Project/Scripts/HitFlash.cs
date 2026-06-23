using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Health))]
public class HitFlash : MonoBehaviour
{
    [Header("Flash")] 
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashDuration = 0.1f;

    [Header("Renderer")] 
    [SerializeField] private Renderer targetRenderer;

    private Health health;
    private MaterialPropertyBlock propertyBlock;
    private Color originalColor;
    private Color currentColor;
    private Tween activeFlashTween;
    
    private static readonly int BaseColorProperty = Shader.PropertyToID("_BaseColor");

    private void Awake()
    {
        health = GetComponent<Health>();

        if (targetRenderer == null)
        {
            targetRenderer = GetComponentInChildren<Renderer>();
        }
        
        propertyBlock = new MaterialPropertyBlock();

        originalColor = targetRenderer.sharedMaterial.GetColor(BaseColorProperty);
        currentColor = originalColor;
    }

    private void OnEnable()
    {
        health.OnDamaged += HandleDamaged;
    }

    private void OnDisable()
    {
        health.OnDamaged -= HandleDamaged;
        activeFlashTween?.Kill();
    }

    private void HandleDamaged(DamageInfo info)
    {
        Flash();
    }

    private void Flash()
    {
        activeFlashTween?.Kill();
        currentColor = flashColor;
        ApplyColor();

        activeFlashTween = DOTween.To(
            () => currentColor,
            value =>
            {
                currentColor = value;
                ApplyColor();
            },
            originalColor,
            flashDuration
            ).SetEase(Ease.OutQuad);
    }

    private void ApplyColor()
    {
        targetRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor(BaseColorProperty, currentColor);
        targetRenderer.SetPropertyBlock(propertyBlock);
    }

}
