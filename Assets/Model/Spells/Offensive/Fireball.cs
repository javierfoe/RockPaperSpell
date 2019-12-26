namespace RockPaperSpell.Model
{
    public class Fireball : OffensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            target.Position -= 5;
        }
    }
}