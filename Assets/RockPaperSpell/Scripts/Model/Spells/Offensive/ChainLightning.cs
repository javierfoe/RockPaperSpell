namespace RockPaperSpell.Model
{
    public class ChainLighting : OffensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            target.Position -= 3;
            target.Target.Position -= 2;
        }
    }
}
