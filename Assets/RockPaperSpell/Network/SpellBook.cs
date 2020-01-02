using Mirror;
using RockPaperSpell.Model;
using UnityEngine;

namespace RockPaperSpell.Network
{
    public class SpellBook : NetworkBehaviour, Interface.SpellBook
    {
        [SerializeField] private Component viewComponent = null;
        private Interface.SpellBook spellBook;

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
            if ((spellBook = viewComponent as Interface.SpellBook) == null)
                Debug.LogError("Interfaces.SpellBook is not set");
        }

        [ClientRpc]
        private void RpcAddSpell(Structs.Spell spell)
        {
            spellBook.AddSpell(spell);
        }
    }
}