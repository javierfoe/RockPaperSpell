using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RockPaperSpell.View
{
    public class WizardToken : Wizard, IPointerEnterHandler, IPointerExitHandler, Interface.Wizard
    {
        [SerializeField] private WizardRow wizardRow = null;
        [SerializeField] private Text gold = null, spell = null;
        [SerializeField] private Image target = null;

        public override void Highlight(bool on)
        {
            base.Highlight(on);
            wizardRow.Highlight(on);
        }

        public override void SetColor(Color color)
        {
            base.SetColor(color);
            wizardRow.SetColor(color);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Highlight(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Highlight(true);
        }

        public void SetGold(int gold)
        {
            this.gold.text = gold.ToString();
        }

        public void SetSpell(Structs.Spell spell)
        {
            this.spell.text = spell.name;
        }

        public void SetTarget(Structs.Wizard target)
        {
            this.target.color = target.color;
        }

        public void SetPosition(int position)
        {
            StartCoroutine(wizardRow.MoveTo(position));
        }

        public void SetInitialPosition(int position)
        {
            wizardRow.SetPosition(position);
        }
    }
}