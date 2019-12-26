namespace RockPaperSpell.Model
{
    public class AntimagicField : DefensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            source.Position += 2;
            target.ChosenSpell = Dungeon.WildSurge();
        }
    }
}