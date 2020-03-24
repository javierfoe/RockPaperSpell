namespace RockPaperSpell.Interface
{
    public interface WizardView
    {
        void SetController(WizardController controller);
        void SetSpeedPotion(bool speedPotion);
        void SetColor(UnityEngine.Color color);
        void SetPosition(int position);
        void SetGold(int gold);
        void SetSpell(Structs.Spell spell);
        void SetTarget(Structs.Wizard target);
        void SetSpellTarget(int player, Structs.SpellTarget spellTarget);
    }
}