using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

        [Space]
        [SerializeField] private TextMeshProUGUI resultLabel;
        public float resultShowTime;
        private readonly string[] _possiblePotions = { "Зелье исцеления", "Зелье щита", "Зелье силы" };
        private float _currResultShowTime;

        private void Start()
        {
            IsCookButtonActive = false;
        }

        private void FixedUpdate()
        {
            ResultShowTimer();
        }

        public void AddIngredient()
        {
            IsCookButtonActive = true;
        }

        public void Cook()
        {
            IsCookButtonActive = false;

            _currResultShowTime = resultShowTime;
            resultLabel.text = $"Результат: \n {_possiblePotions[Random.Range(0, 3)]}";
        }

        private void ResultShowTimer()
        {
            if (_currResultShowTime < 0f)
            {
                resultLabel.text = "Результат:";
            }
            else
            {
                _currResultShowTime -= Time.fixedDeltaTime;
            }
        }
    }
}
