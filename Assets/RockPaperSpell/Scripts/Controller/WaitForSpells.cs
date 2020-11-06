using RockPaperSpell.Structs;
using UnityEngine;

namespace RockPaperSpell.Controller
{
    public class WaitForSpells : System.Collections.IEnumerator
    {
        private float currentTime;
        public object Current { get; set; }
        public SpellTarget[] SpellTargets { get; private set; }

        public WaitForSpells(int players)
        {
            SpellTarget empty = new SpellTarget { spell = -1, target = -1 };
            SpellTargets = new SpellTarget[players];
            for (int i = 0; i < players; ++i)
            {
                SpellTargets[i] = empty;
            }
            currentTime = 0;
        }

        public void SetSpellTarget(int index, SpellTarget spellTarget)
        {
            SpellTargets[index] = spellTarget;
        }

        public bool MoveNext()
        {
            bool next;
            currentTime += Time.deltaTime;
            if (currentTime >= GameController.TargetSelectionTime)
            {
                next = false;
            }
            else
            {
                bool allSpellsSelected = true;
                for (int i = 0; i < SpellTargets.Length && allSpellsSelected; ++i)
                {
                    allSpellsSelected = !SpellTargets[i].IsDefault();
                }
                next = !allSpellsSelected;
            }
            if (!next)
            {
                RandomSpells();
            }
            return next;
        }

        public void Reset() { }

        private void RandomSpells()
        {
            SpellTarget aux;
            int length = SpellTargets.Length;
            for (int i = 0; i < length; ++i)
            {
                aux = SpellTargets[i];
                SpellTargets[i] = aux.IsDefault() ? new SpellTarget(i, length) : aux;
            }
        }
    }
}