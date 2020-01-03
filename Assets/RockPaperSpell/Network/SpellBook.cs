using Mirror;
using RockPaperSpell.Model;
using UnityEngine;

namespace RockPaperSpell.Network
{
    public class SpellBook : NetworkBehaviour, Interface.SpellBook
    {
        [SerializeField] private View.SpellBook spellBookGo = null;
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
            spellBook = spellBookGo as Interface.SpellBook;
        }

        [ClientRpc]
        private void RpcAddSpell(Structs.Spell spell)
        {
            spellBook.AddSpell(spell);
        }
    }
}