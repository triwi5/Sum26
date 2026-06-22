using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageNumber : MonoBehaviour
{
   [SerializeField] private TMP_Text text;
   [SerializeField] private float lifetime = 1f;
   [SerializeField] private float floatHeight = 1.5f;

   public void Show(float amount, Vector3 worldPosition)
   {
      transform.position = worldPosition;
      text.text = Mathf.RoundToInt(amount).ToString();

      Sequence seq = DOTween.Sequence();
      
      seq.Append(transform.DOMoveY(transform.position.y + floatHeight, lifetime).SetEase(Ease.OutCubic));

      seq.Join(text.DOFade(0f, lifetime).SetEase(Ease.InCubic));

      seq.OnComplete(() => Destroy(gameObject));
   }
}
