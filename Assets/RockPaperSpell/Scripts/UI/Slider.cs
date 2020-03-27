using UnityEngine;
using UnityEngine.UI;

namespace RockPaperSpell.UI
{
    public class Slider : MonoBehaviour
    {
        [SerializeField] private string displayText = "";
        [SerializeField] private Text staticText = null, amountText = null;
        [SerializeField] private int amount = 0;
        [SerializeField] private UnityEngine.UI.Slider slider = null;

        public int Amount => (int)slider.value;

        private void UpdateAmountText(float value)
        {
            amountText.text = value.ToString();
        }

        private void Awake()
        {
            slider.onValueChanged.AddListener(UpdateAmountText);
            slider.onValueChanged.Invoke(amount);
        }

        private void OnValidate()
        {
            staticText.text = displayText;
        }
    }
}