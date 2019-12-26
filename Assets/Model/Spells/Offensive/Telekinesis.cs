namespace RockPaperSpell.Model
{
    public class Telekinesis : OffensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            target.Position = source.Position - 1;
        }
    }
}
