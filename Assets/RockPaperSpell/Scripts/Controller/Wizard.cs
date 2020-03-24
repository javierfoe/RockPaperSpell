using RockPaperSpell.Structs;

namespace RockPaperSpell.Controller
{
    public class Wizard : Controller<Interface.WizardView>, Interface.WizardController
    {
        private Model.Wizard wizard;

        public void SetWizardModel(Model.Wizard wizard)
        {
            this.wizard = wizard;
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
            RockPaperSpell.Controller.SetTargetSpell(player, spellTarget);
        }
    }
}