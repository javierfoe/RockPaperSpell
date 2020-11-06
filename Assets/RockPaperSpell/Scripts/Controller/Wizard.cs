using RockPaperSpell.Interface;
using RockPaperSpell.Structs;

namespace RockPaperSpell.Controller
{
    public class Wizard : Controller<WizardView>, WizardController
    {
        private Model.Wizard wizard;

        public void SetWizardModel(Model.Wizard wizard)
        {
            this.wizard = wizard;
        }

        public override void SetView(WizardView view)
        {
            base.SetView(view);
            wizard.AddGoldListener(view.SetGold);
            wizard.AddPositionListener(view.SetPosition);
            wizard.AddSpeedPotionListener(view.SetSpeedPotion);
            wizard.AddSpellListener(view.SetSpell);
            wizard.AddTargetListener(view.SetTarget);
        }

        public void InitialState()
        {
            view.SetColor(wizard.Color);
            wizard.InvokeEvents();
        }

        public void SetSpellTarget(int player, SpellTarget spellTarget)
        {
            GameController.SetTargetSpell(player, spellTarget);
        }
    }
}