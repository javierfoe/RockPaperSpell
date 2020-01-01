namespace RockPaperSpell.Model
{
    public class VampiricTouch : OffensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            source.Position += 2;
            target.Position -= 2;
        }
    }
}