namespace RockPaperSpell.Model
{
    public class MistyStep : DefensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            source.Position = target.Position;
        }
    }
}