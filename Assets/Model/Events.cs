using RockPaperSpell.Structs;
using UnityEngine.Events;

namespace RockPaperSpell.Events
{
    internal class UnityEventWizard : UnityEvent<Wizard> { }
    internal class UnityEventInt : UnityEvent<int> { }
    internal class UnityEventSpell : UnityEvent<Spell> { }
}