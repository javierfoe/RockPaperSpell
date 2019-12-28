using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RockPaperSpell.View
{
    public class WizardToken : Wizard, IPointerEnterHandler, IPointerExitHandler
    {
        public WizardRow WizardRow { get; set; }

        [SerializeField] private Text gold = null, spell = null;
        [SerializeField] private Image target = null;

        public void SetGold(int gold)
        {
            this.gold.text = gold.ToString();
        }

        public void SetSpell(Model.Spell spell)
        {
            this.spell.text = spell.ToString();
        }

        public void SetTarget(int target)
        {
            this.target.color = RockPaperSpell.WizardColors[target];
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Highlight(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Highlight(true);
        }

        public override void Highlight(bool on)
        {
            base.Highlight(on);
            WizardRow.Highlight(on);
        }

        public override void SetColor(Color color)
        {
            base.SetColor(color);
            WizardRow.SetColor(color);
        }
    }
}