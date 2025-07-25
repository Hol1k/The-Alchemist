using DG.Tweening;
using Game.Scripts.Cauldron;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.Scripts.Ingredients
{
    public class IngredientSpawner : MonoBehaviour, IPointerClickHandler
    {
        private CookController _cookController;
        
        [SerializeField] private bool developmentMode = false;
        [SerializeField] private float spawnOffsetY = -0.1f;
        
        [SerializeField] private GameObject ingredientPrefab;
        
        private bool _spawnRequested;
        [SerializeField] private Transform moveTarget;

        [Inject]
        private void Construct(CookController cookController)
        {
            _cookController = cookController;
        }
        
        private void FixedUpdate()
        {
            SpawnIngredient();
        }

        private void OnDrawGizmos()
        {
            if (developmentMode)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(transform.position + Vector3.up * spawnOffsetY, 0.07f);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _spawnRequested = true;
        }

        private void SpawnIngredient()
        {
            if (_spawnRequested)
            {
                _spawnRequested = false;
                
                MoveIngredient();
            }
        }

        private void MoveIngredient()
        {
            var ingredientTransform = Instantiate(ingredientPrefab, transform.position + Vector3.up * spawnOffsetY, Quaternion.identity).transform;
            
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(ingredientTransform.DOMove(ingredientTransform.position + Vector3.up * 0.3f, 0.5f));
            sequence.Append(ingredientTransform.DOMoveX(moveTarget.position.x, 1));
            sequence.Join(ingredientTransform.DOMoveZ(moveTarget.position.z, 1));
            sequence.Append(ingredientTransform.DOMove(moveTarget.position, 1f));
            
            sequence.OnComplete(() =>
            {
                Destroy(ingredientTransform.gameObject);
                _cookController.AddIngredient();
            });
        }
    }
}
