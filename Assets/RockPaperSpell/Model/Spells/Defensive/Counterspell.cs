namespace RockPaperSpell.Model
{
    public class Counterspell : DefensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            source.Position += 2;
            target.ChosenSpell = null;
        }
    }
}
