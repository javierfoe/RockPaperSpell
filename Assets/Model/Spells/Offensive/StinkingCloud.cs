namespace RockPaperSpell.Model
{
    public class StinkingCloud : OffensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            target.Position -= 3;
            target.Next.Position -= 1;
            target.Previous.Position -= 1;
        }
    }
}
