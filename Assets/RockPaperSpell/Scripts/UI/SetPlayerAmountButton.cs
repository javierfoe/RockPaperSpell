using UnityEngine;

namespace RockPaperSpell.UI
{
    public class SetPlayerAmountButton : Button
    {
        [SerializeField] private Slider slider = null;

        protected override void Click()
        {
            Controller.RockPaperSpell.PlayerAmount = slider.Amount;
        }
    }
}