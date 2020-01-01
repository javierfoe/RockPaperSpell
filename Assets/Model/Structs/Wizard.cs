using UnityEngine;

namespace RockPaperSpell.Structs
{
    public struct Wizard
    {
        public Color color;

        public static Wizard CreateDefault()
        {
            return new Wizard
            {
                color = new Color(0, 0, 0, 0)
            };
        }
    }
}