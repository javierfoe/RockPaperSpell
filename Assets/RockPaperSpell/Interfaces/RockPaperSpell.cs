namespace RockPaperSpell.Interface
{
    public interface RockPaperSpell
    {
        void SetView(int players);
        Wizard this[int i] { get; }
        SpellBook SpellBook { get; }
    }
}