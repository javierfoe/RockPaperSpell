using UnityEngine;
using UnityEngine.UI;

namespace RockPaperSpell.View
{
    public class Wizard : MonoBehaviour
    {
        [SerializeField] protected Image wizardImage;

        public virtual void SetColor(Color color)
        {
            wizardImage.color = color;
            Highlight(false);
        }

        public virtual void Highlight(bool on)
        {
            Color color = wizardImage.color;
            float h, s, v;
            Color.RGBToHSV(color, out h, out s, out v);
            s = on ? RockPaperSpell.SaturationOn : RockPaperSpell.SaturationOff;
            v = on ? RockPaperSpell.BrightnessOn : RockPaperSpell.BrightnessOff;
            color = Color.HSVToRGB(h, s, v);
            wizardImage.color = color;
        }
    }
}