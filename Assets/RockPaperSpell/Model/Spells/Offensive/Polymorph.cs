namespace RockPaperSpell.Model
{
    public class Polymorph : OffensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            int back = target.Gold - source.Gold;
            if (back < 0)
                back = -back;
            target.Position -= back;
        }
    }
}