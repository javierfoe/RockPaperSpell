namespace RockPaperSpell.Interfaces
{
    public interface Wizard
    {
        void SetColor(UnityEngine.Color color);
        void SetInitialPosition(int position);
        void SetPosition(int position);
        void SetGold(int gold);
        void SetSpell(Model.Spell spell);
        void SetTarget(Model.Wizard target);
    }
}