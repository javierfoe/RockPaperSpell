namespace RockPaperSpell.Controller
{
    public class Wizard : Controller<Interface.Wizard>
    {
        private Model.Wizard wizard;

        public void SetWizardModel(Model.Wizard wizard)
        {
            this.wizard = wizard;
            wizard.AddGoldListener(view.SetGold);
            wizard.AddPositionListener(view.SetPosition);
            wizard.AddSpellListener(view.SetSpell);
            wizard.AddTargetListener(view.SetTarget);
        }

        public void InitialState()
        {
            view.SetInitialPosition(wizard.Position);
            view.SetColor(wizard.Color);
            wizard.InvokeEvents();
        }
    }
}