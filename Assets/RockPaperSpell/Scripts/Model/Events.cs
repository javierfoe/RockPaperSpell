using RockPaperSpell.Structs;
using UnityEngine.Events;

namespace RockPaperSpell.Events
{
    internal class UnityEventInt : UnityEvent<int> { }
    internal class UnityEventBool : UnityEvent<bool> { }
    internal class UnityEventWizard : UnityEvent<Wizard> { }
    internal class UnityEventSpell : UnityEvent<Spell> { }
}