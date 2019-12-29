﻿namespace RockPaperSpell.Model
{
    public class WallOfForce : DefensiveSpell
    {
        public override void Cast(Wizard source, Wizard target)
        {
            source.Position += 2;
            if(target.ChosenSpell != null)
                target.Target = target;
        }
    }
}