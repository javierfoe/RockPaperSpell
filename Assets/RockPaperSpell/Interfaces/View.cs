namespace RockPaperSpell.Interface
{
    public interface View
    {
        void SetView(int players);
        Wizard this[int i] { get; }
        SpellBook SpellBook { get; }
    }
}