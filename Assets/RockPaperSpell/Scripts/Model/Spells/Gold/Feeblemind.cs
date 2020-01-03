namespace RockPaperSpell.Model
{
    public class Feeblemind : GoldSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            int gold = source.Gold + target.Gold;
            target.Gold = gold / 2;
            source.Gold = gold - target.Gold;
        }
    }
}