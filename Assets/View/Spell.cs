using UnityEngine;
using UnityEngine.UI;

namespace RockPaperSpell.View
{
    public class Spell : MonoBehaviour
    {
        [SerializeField] private Text spellText;

        public void SetSpell(Model.Spell spell)
        {
            spellText.text = spell.ToString();
        }
    }
}