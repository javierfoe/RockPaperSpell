using UnityEngine;

namespace RockPaperSpell.Structs
{
    public struct Wizard
    {
        public static Wizard Default = new Wizard { color = Color.white };

        public Color color;
    }
}