namespace RockPaperSpell.Model
{
    public class TeleportationCircle : DefensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            int forward = target.Gold - source.Gold;
            if (forward < 0)
                forward = -forward;
            source.Position += forward;
        }
    }
}