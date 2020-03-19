﻿namespace RockPaperSpell.Interface
{
    public interface WizardView
    {
        void SetColor(UnityEngine.Color color);
        void SetPosition(int position);
        void SetGold(int gold);
        void SetSpell(Structs.Spell spell);
        void SetTarget(Structs.Wizard target);
    }
}