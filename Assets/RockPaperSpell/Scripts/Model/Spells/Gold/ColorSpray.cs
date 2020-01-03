namespace RockPaperSpell.Model
{
    public class ColorSpray : GoldSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            int poorer = RockPaperSpell.PoorerWizardCount(target);
            target.Gold -= poorer;
        }
    }
}