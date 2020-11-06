using RockPaperSpell.Structs;
using UnityEngine;
using UnityEngine.UI;

namespace RockPaperSpell.View
{
    public class Wizard : IndexBehaviour
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
            Highlight highlight = RetrieveHighlight(on);
            s = highlight.saturation;
            v = highlight.brightness;
            color = Color.HSVToRGB(h, s, v);
            wizardImage.color = color;
        }

        private Highlight RetrieveHighlight(bool on)
        {
            return new Highlight
            {
                saturation = on ? RockPaperSpell.SaturationOn : RockPaperSpell.SaturationOff,
                brightness = on ? RockPaperSpell.BrightnessOn : RockPaperSpell.BrightnessOff
            };
        }
    }
}