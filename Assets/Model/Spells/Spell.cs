using System;

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
    }
}