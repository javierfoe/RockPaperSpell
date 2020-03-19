namespace RockPaperSpell.Interface
{
    public interface View
    {
        void SetView(int players);
        WizardView this[int i] { get; }
        SpellBook SpellBook { get; }
        void EnableCast(bool value);
    }
}