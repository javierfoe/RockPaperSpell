using System.Collections;
using UnityEngine;

namespace RockPaperSpell.Controller
{
    public class RockPaperSpell : MonoBehaviour
    {
        private static int Players;
        public int players;

        [Header("Rock Paper Spell View")]
        [SerializeField] private View.RockPaperSpell rockPaperSpell = null;
        [Header("Controllers")]
        [Header("Wizard Controllers")]
        [SerializeField] private Transform wizardControllers = null;
        [Header("Spell Book Controller")]
        [SerializeField] private Transform spellBook = null;

        public struct SpellTarget
        {
            public int spell, target;

            public SpellTarget(int player)
            {
                do
                {
                    target = Random.Range(0, Players);
                } while (target == player);
                spell = Random.Range(0, Players > 5 ? 5 : Players);
            }
        }

        private void Start()
        {
            Players = players;
            Model.RockPaperSpell.SetupBoard(players);
            rockPaperSpell.SetView(players);
            SetupWizards();
            SetupSpellBook();
            StartCoroutine(StartGame());
        }

        private void SetupWizards()
        {
            Model.Wizard[] wizards = Model.RockPaperSpell.Wizards;
            int length = wizards.Length;
            for (int i = 0; i < length; i++)
            {
                wizards[i].Color = rockPaperSpell.Colors[i];
                wizardControllers.GetChild(i).GetComponent<Wizard>().SetWizardModel(wizards[i]);
            }
        }

        private void SetupSpellBook()
        {
            spellBook.GetComponent<SpellBook>().SetSpellBook(Model.RockPaperSpell.SpellBook);
        }

        private IEnumerator StartGame()
        {
            SpellTarget[] roundSpells = new SpellTarget[players];
            bool win;
            int winner;
            int speedPotion = 0;
            do
            {
                for (int i = 0; i < players; i++)
                {
                    roundSpells[i] = new SpellTarget(i);
                }
                Model.RockPaperSpell.SetTargetsAndSpells(roundSpells);
                bool first = true;
                for (int i = speedPotion; i != speedPotion || first; i = i < players - 1 ? i + 1 : 0)
                {
                    yield return new WaitForSeconds(2);
                    first = false;
                    if (Model.RockPaperSpell.Wizards[i].CastSpell())
                    {
                        yield return new WaitForSeconds(2);
                    }
                }
                speedPotion = speedPotion < players - 1 ? speedPotion + 1 : 0;
                Model.RockPaperSpell.SplitLoot();
                win = Model.RockPaperSpell.CheckWin(out winner);
            } while (!win);
            Debug.Log(winner);
        }
    }
}