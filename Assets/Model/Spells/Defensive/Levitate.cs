namespace RockPaperSpell.Model
{
    public class Levitate : DefensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            target.Position += 1;
            int forward = Dungeon.WizardCountCloserToLoot(source);
            source.Position += forward;
        }
    }
}