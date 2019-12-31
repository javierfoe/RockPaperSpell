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
            rockPaperSpell.SetView(players);
            //StartCoroutine(StartGame());
        }

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