namespace RockPaperSpell.Model
{
    public class IceStorm : OffensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            target.Position -= 4;
            target.Next.Position += 2;
            target.Previous.Position += 2;
        }
    }
}
