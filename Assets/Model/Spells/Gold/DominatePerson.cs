namespace RockPaperSpell.Model
{
    public class DominatePerson : GoldSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            target.Position -= 3;
            target.Gold -= 1;
            source.Gold += 1;
        }
    }
}