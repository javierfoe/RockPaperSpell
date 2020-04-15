using System.Collections;

namespace RockPaperSpell.Interface
{
    public interface View
    {
        IEnumerator SetView(int players);
        WizardView GetElement(int i);
        SpellBook SpellBook { get; }
        void EnableCast(bool value);
    }
}