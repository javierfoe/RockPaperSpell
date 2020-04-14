namespace RockPaperSpell.Interface
{
    public interface View
    {
        void SetView(int players);
        WizardView GetElement(int i);
        SpellBook SpellBook { get; }
        void EnableCast(bool value);
    }
}