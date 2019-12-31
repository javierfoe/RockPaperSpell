namespace RockPaperSpell.Controller
{
    public class Wizard : Controller<Interfaces.Wizard>
    {
        public void SetWizardModel(Model.Wizard wizard)
        {
            wizard.AddGoldListener(view.SetGold);
            wizard.AddPositionListener(view.SetPosition);
            wizard.AddSpellListener(view.SetSpell);
            wizard.AddTargetListener(view.SetTarget);
            view.SetInitialPosition(wizard.Position);
            view.SetColor(wizard.Color);
        }
    }
}