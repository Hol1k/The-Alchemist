using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Cauldron
{
    public class CookController : MonoBehaviour
    {
        [SerializeField] private Button cookButton;
        public bool IsCookButtonActive
        {
            get => cookButton.interactable;
            set => cookButton.interactable = value;
        }

        private void Start()
        {
            IsCookButtonActive = false;
        }

        public void AddIngredient()
        {
            IsCookButtonActive = true;
        }

        public void Cook()
        {
            IsCookButtonActive = false;
        }
    }
}
