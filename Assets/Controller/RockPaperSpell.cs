using System.Collections;
using UnityEngine;

namespace RockPaperSpell.Controller
{
    public class RockPaperSpell : MonoBehaviour
    {
        [Header("Rock Paper Spell View")]
        [SerializeField] private View.RockPaperSpell rockPaperSpell = null;
        [field: Header("Wizard Controller"), SerializeField] public Transform WizardControllers { get; private set; }
        [Header("Spell Book Controller")]
        [SerializeField] private Transform spellBook = null;
        private int players;

        public struct SpellTarget
        {
            public int spell, target;
            public SpellTarget(int player, int maxPlayers)
            {
                do
                {
                    target = Random.Range(0, maxPlayers);
                } while (target == player);
                spell = Random.Range(0, maxPlayers > 5 ? 5 : maxPlayers);
            }
        }

        public SpellTarget CreateSpellTarget(int player)
        {
            return new SpellTarget(player, players);
        }

        public void Setup(int players)
        {
            this.players = players;
            Model.RockPaperSpell.SetupBoard(players);
            SetView(players);
            SetupWizards();
            SetupSpellBook();
        }

        public void SetView(int players)
        {
            rockPaperSpell.SetView(players);
        }

        public void StartMatch()
        {
            StartCoroutine(StartGame());
        }

        private void SetupWizards()
        {
            Model.Wizard[] wizards = Model.RockPaperSpell.Wizards;
            int length = wizards.Length;
            for (int i = 0; i < length; i++)
            {
                wizards[i].Color = rockPaperSpell.Colors[i];
                WizardControllers.GetChild(i).GetComponent<Wizard>().SetWizardModel(wizards[i]);
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
                    roundSpells[i] = CreateSpellTarget(i);
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