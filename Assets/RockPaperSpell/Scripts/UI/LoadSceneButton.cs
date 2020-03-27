using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RockPaperSpell.UI
{
    public class LoadSceneButton : Button
    {
        [Scene][SerializeField] private string scene = null;

        protected override void Click()
        {
            SceneManager.LoadScene(scene);
        }
    }
}