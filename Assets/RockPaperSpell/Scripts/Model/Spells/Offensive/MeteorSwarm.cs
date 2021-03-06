﻿namespace RockPaperSpell.Model
{
    public class MeteorSwarm : OffensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            int spellsBefore = RockPaperSpell.SpellsBefore(this);
            target.Position -= 1 + 2 * spellsBefore;
        }
    }
}
