using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Ingredients
{
    public class OutlineShower : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Transform outlineObjectTransform;
        [SerializeField] private float outlineThickness = 0.05f;
    
        public void OnPointerEnter(PointerEventData eventData)
        {
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(outlineObjectTransform.DOScale(Vector3.one * (1 + outlineThickness), 0.2f));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(outlineObjectTransform.DOScale(Vector3.one, 0.2f));
        }
    }
}
