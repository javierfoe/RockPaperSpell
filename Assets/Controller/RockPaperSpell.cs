using UnityEngine;

namespace RockPaperSpell.Controller
{
    public class RockPaperSpell : MonoBehaviour
    {
        public int players;

        [SerializeField] private View.RockPaperSpell rockPaperSpell;

        private void Start()
        {
            Model.Dungeon.SetupBoard(players);
            rockPaperSpell.SetView(players);
        }
    }
}