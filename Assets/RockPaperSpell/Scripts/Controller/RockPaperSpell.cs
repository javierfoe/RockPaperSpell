using RockPaperSpell.Structs;
using System.Collections;
using UnityEngine;

namespace RockPaperSpell.Controller
{
    public class RockPaperSpell : MonoBehaviour, Interface.Controller
    {
        private static float WizardMovementTime;
        private static float SaturationOn;
        private static float SaturationOff;
        private static float BrightnessOn;
        private static float BrightnessOff;

        public static Highlight Highlight(bool on)
        {
            return new Highlight
            {
                saturation = on ? SaturationOn : SaturationOff,
                brightness = on ? BrightnessOn : BrightnessOff
            };
        }

        public static IEnumerator Lerp(RectTransform wizard)
        {
            yield return null;
            Vector2 left = wizard.offsetMin;
            Vector2 right = wizard.offsetMax;
            float time = 0f;
            while (time < 1)
            {
                time += Time.deltaTime / WizardMovementTime;
                wizard.offsetMin = Vector3.Lerp(left, Vector2.zero, time);
                wizard.offsetMax = Vector3.Lerp(right, Vector2.zero, time);
                yield return null;
            }
        }

        [Header("Wizard Movement Time")]
        [SerializeField] private float movementTime = 0;
        [Header("Highlighting")]
        [SerializeField] private float saturationOn = 0;
        [SerializeField] private float saturationOff = 0, brightnessOn = 0, brightnessOff = 0;
        [Header("Colors")]
        [SerializeField] private Color[] colors = null;
        [Header("Interface.RockPaperSpell")]
        [SerializeField] private GameObject rockPaperSpell = null;
        [Header("Offline settings")]
        [SerializeField] private bool offline = false;
        [SerializeField] private int offlinePlayers = 0;

        private Interface.View rockPaperSpellView;
        private int players;
        private Wizard[] wizardControllers;
        private SpellBook spellBook;

        public SpellTarget CreateSpellTarget(int player)
        {
            return new SpellTarget(player, players);
        }

        public void Setup(int players)
        {
            this.players = players;
            Model.RockPaperSpell.SetupBoard(players);
            SetupWizards();
            SetupSpellBook();
        }

        public void StartMatch()
        {
            SetInitialState();
            StartCoroutine(StartGame());
        }

        public void SetViews()
        {
            for (int i = 0; i < wizardControllers.Length; i++)
            {
                wizardControllers[i].SetView(rockPaperSpellView[i]);
            }
            spellBook.SetView(rockPaperSpellView.SpellBook);
        }

        private void SetupWizards()
        {
            GetDependencies();
            Model.Wizard[] wizards = Model.RockPaperSpell.Wizards;
            int length = wizards.Length;
            for (int i = 0; i < length; i++)
            {
                wizards[i].Color = colors[i];
                wizardControllers[i].SetWizardModel(wizards[i]);
            }
        }

        private void SetupSpellBook()
        {
            GetDependencies();
            spellBook.SetSpellBook(Model.RockPaperSpell.SpellBook);
        }

        private void SetInitialState()
        {
            rockPaperSpellView.SetView(players);
            for (int i = 0; i < players; i++)
            {
                wizardControllers[i].InitialState();
            }
            spellBook.InitialState();
        }

        private IEnumerator StartGame()
        {
            yield return null;

            SpellTarget[] roundSpells = new SpellTarget[players];
            bool win;
            int winner;
            int speedPotion = 0;
            do
            {
                for (int i = 0; i < players; i++)
                {
                    roundSpells[i] = CreateSpellTarget(i);
                }
                Model.RockPaperSpell.SetTargetsAndSpells(roundSpells);
                bool first = true;
                for (int i = speedPotion; i != speedPotion || first; i = i < players - 1 ? i + 1 : 0)
                {
                    yield return new WaitForSeconds(2);
                    first = false;
                    if (Model.RockPaperSpell.Wizards[i].CastSpell())
                    {
                        yield return new WaitForSeconds(2);
                    }
                }
                speedPotion = speedPotion < players - 1 ? speedPotion + 1 : 0;
                Model.RockPaperSpell.SplitLoot();
                win = Model.RockPaperSpell.CheckWin(out winner);
                yield return new WaitForSeconds(1);
            } while (!win);
            Debug.Log(winner);
        }

        private void Awake()
        {
            GetDependencies();
            WizardMovementTime = movementTime;
            SaturationOff = saturationOff;
            SaturationOn = saturationOn;
            BrightnessOff = brightnessOff;
            BrightnessOn = brightnessOn;
            SetViews();
            if (offline)
            {
                Setup(offlinePlayers);
                StartMatch();
            }
        }

        private void GetDependencies()
        {
            wizardControllers = transform.GetChild(0).GetComponentsInChildren<Wizard>();
            spellBook = transform.GetChild(1).GetComponentInChildren<SpellBook>();
            rockPaperSpellView = rockPaperSpell.GetComponent<Interface.View>();
        }
    }
}