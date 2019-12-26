using System;

namespace RockPaperSpell.Model
{
    [Flags]
    public enum SpellType
    {
        None = 0,
        Offensive = 1,
        Defensive = 2,
        Gold = 4,
        All = 7
    }

    public static class SpellTypeUtility
    {
        public static SpellType CreateSpellType(bool offensive, bool defensive, bool gold)
        {
            int off = offensive ? 1 : 0;
            int def = defensive ? 2 : 0;
            int gol = gold ? 4 : 0;
            return (SpellType)off + def + gol;
        }
    }
}