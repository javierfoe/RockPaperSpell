using UnityEngine;

namespace RockPaperSpell.Structs
{
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
}