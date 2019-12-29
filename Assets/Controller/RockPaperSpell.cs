using System.Collections;
using UnityEngine;

namespace RockPaperSpell.Controller
{
    public class RockPaperSpell : MonoBehaviour
    {
        private static int Players;
        public int players;

        [SerializeField] private View.RockPaperSpell rockPaperSpell = null;

        private void Start()
        {
            Players = players;
            Model.RockPaperSpell.SetupBoard(players);
            rockPaperSpell.SetView(Model.RockPaperSpell.Wizards);
            StartCoroutine(StartGame());
        }

        private struct SpellTarget
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

        private IEnumerator StartGame()
        {
            SpellTarget[] roundSpells = new SpellTarget[players];
            bool win = false;
            Model.Wizard winner = null;
            do
            {
                for(int i = 0; i < players; i++)
                {
                    roundSpells[i] = new SpellTarget(i);
                }
                yield return null;
            } while (!win);
            Debug.Log(winner);
        }
    }
}