using Mirror;
using RockPaperSpell.Model;
using UnityEngine;

namespace RockPaperSpell.Network
{
    public class SpellBook : NetworkBehaviour, Interfaces.SpellBook
    {
        [SerializeField] private Component viewComponent = null;
        private Interfaces.SpellBook spellBook;

        public void AddSpell(Structs.Spell spell)
        {
            RpcAddSpell(spell);
        }

        public override void OnStartClient()
        {
            GetDependencies();
        }

        private void OnValidate()
        {
            GetDependencies();
        }

        private void GetDependencies()
        {
            if ((spellBook = viewComponent as Interfaces.SpellBook) == null)
                Debug.LogError("Interfaces.SpellBook is not set");
        }

        [ClientRpc]
        private void RpcAddSpell(Structs.Spell spell)
        {
            spellBook.AddSpell(spell);
        }
    }
}