namespace RockPaperSpell.Model
{
    public class Counterspell : DefensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            target.ChosenSpell = null;
        }
    }
}
