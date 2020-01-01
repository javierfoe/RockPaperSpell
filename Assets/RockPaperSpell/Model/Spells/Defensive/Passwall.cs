namespace RockPaperSpell.Model
{
    public class Passwall : DefensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            source.Position += 3;
            target.Position += 2;
        }
    }
}