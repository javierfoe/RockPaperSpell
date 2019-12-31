namespace RockPaperSpell.Model
{
    public abstract class Spell
    {
        public string Name;
        public SpellType Type { get; private set; }

        public Spell() : this(SpellType.None) { }

        public Spell(SpellType type)
        {
            Type = type;
            string toString = base.ToString();
            string[] splits = toString.Split('.');
            Name = splits[splits.Length - 1] + " " + Type;
        }

        public abstract void Cast(Wizard source, Wizard target);

        public override string ToString()
        {
            return Name;
        }
    }
}