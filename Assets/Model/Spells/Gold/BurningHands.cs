namespace RockPaperSpell.Model
{
    public class BurningHands : GoldSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            target.Position -= 1;
            target.Gold -= 1;
        }
    }
}