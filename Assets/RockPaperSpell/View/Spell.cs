using UnityEngine;
using UnityEngine.UI;

namespace RockPaperSpell.View
{
    public class Spell : MonoBehaviour
    {
        [SerializeField] private Text spellText = null;

        public void SetSpell(Structs.Spell spell)
        {
            spellText.text = spell.name;
        }

        public void CopySpell(Spell spell)
        {
            spellText.text = spell.spellText.text;
        }
    }
}