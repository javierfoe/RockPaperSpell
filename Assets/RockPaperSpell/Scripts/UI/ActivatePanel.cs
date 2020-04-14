using UnityEngine;

namespace RockPaperSpell.UI
{
    public class ActivatePanel : Button
    {
        [SerializeField] private GameObject panel = null;

        protected override void Click()
        {
            transform.parent.gameObject.SetActive(false);
            panel.SetActive(true);
        }
    }
}