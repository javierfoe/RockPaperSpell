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

        public void SetWizardModel(Model.Wizard wizard)
        {
            wizard.AddGoldListener(SetGold);
            wizard.AddSpellListener(SetSpell);
            wizard.AddTargetListener(SetTarget);
            wizard.AddPositionListener(SetPosition);
            WizardRow.SetOnPosition(wizard.Position);
            SetColor(wizard.Color);
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

        public void OnPointerExit(PointerEventData eventData)
        {
            Highlight(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Highlight(true);
        }

        private void SetGold(int gold)
        {
            this.gold.text = gold.ToString();
        }

        private void SetSpell(Model.Spell spell)
        {
            this.spell.text = spell == null ? "" : spell.ToString();
        }

        private void SetTarget(Model.Wizard target)
        {
            this.target.color = target == null ? Color.white : target.Color;
        }

        private void SetPosition(int position)
        {
            WizardRow.MoveTo(position);
        }

    }
}