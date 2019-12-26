namespace RockPaperSpell.Model
{
    public class DimensionDoor : OffensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            int positionAux = target.Position;
            target.Position = source.Position;
            source.Position = positionAux;
        }
    }
}