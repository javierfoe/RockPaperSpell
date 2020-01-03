namespace RockPaperSpell.Model
{
    public class Fear : OffensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            int poorer = RockPaperSpell.PoorerWizardCount(target);
            target.Position -= 2 * poorer;
        }
    }
}