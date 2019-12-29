namespace RockPaperSpell.Model
{
    public abstract class Spell
    {
        public SpellType Type { get; private set; }

        public Spell(SpellType type)
        {
            Type = type;
        }

        public abstract void Cast(Wizard source, Wizard target);

        public override string ToString()
        {
            string toString = base.ToString();
            string[] splits = toString.Split('.');
            return splits[splits.Length - 1] + " " +Type;
        }
    }
}