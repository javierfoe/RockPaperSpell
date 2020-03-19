using RockPaperSpell.Structs;
using System.Collections;
using UnityEngine;

namespace RockPaperSpell.Controller
{
    public class RockPaperSpell : MonoBehaviour, Interface.Controller
    {
        public static float TargetSelectionTime { get; private set; }
        public static Interface.WizardController LocalPlayer { get; set; }
        internal static RockPaperSpell Controller { get; private set; }

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
        [Header("Spell & Target Select Time")]
        [SerializeField] private float targetSelectionTime = 0;
        [Header("Highlighting")]
        [SerializeField] private float saturationOn = 0;
        [SerializeField] private float saturationOff = 0, brightnessOn = 0, brightnessOff = 0;
        [Header("Colors")]
        [SerializeField] private Color[] colors = null;
        [Header("Interface.View RockPaperSpell")]
        [SerializeField] private GameObject rockPaperSpell = null;
        [Header("Offline settings")]
        [SerializeField] private bool offline = false;
        [SerializeField] private int localPlayerIndex = 0;
        [SerializeField] private int offlinePlayers = 0;

        private Interface.View rockPaperSpellView;
        private int players;
        private Wizard[] wizardControllers;
        private SpellBook spellBook;
        private WaitForSpells waitForSpells;

        private Wizard this[int index] => wizardControllers[index];        

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
                rockPaperSpellView[i].SetController(wizardControllers[i]);
            }
            spellBook.SetView(rockPaperSpellView.SpellBook);
        }

        public void SetTargetSpell(int player, SpellTarget spellTarget)
        {
            waitForSpells.SetSpellTarget(player, spellTarget);
            Model.Wizard playerWizard = Model.RockPaperSpell.Wizards[player];
            playerWizard.Target = Model.RockPaperSpell.Wizards[spellTarget.target];
            playerWizard.ChosenSpell = Model.RockPaperSpell.SpellBook[spellTarget.spell];
        }

        private void SetupWizards()
        {
            GetDependencies();
            Model.Wizard[] wizards = Model.RockPaperSpell.Wizards;
            int length = wizards.Length;
            for (int i = 0; i < length; i++)
            {
                wizards[i].Color = colors[i];
                this[i].SetWizardModel(wizards[i]);
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
            yield return new WaitForSeconds(movementTime);
            bool win;
            int winner;
            int speedPotion = 0;
            do
            {
                waitForSpells = new WaitForSpells(players);
                rockPaperSpellView.EnableCast(true);
                yield return waitForSpells;
                rockPaperSpellView.EnableCast(false);
                Model.RockPaperSpell.SetTargetsAndSpells(waitForSpells.SpellTargets);
                bool first = true;
                for (int i = speedPotion; i != speedPotion || first; i = i < players - 1 ? i + 1 : 0)
                {
                    yield return new WaitForSeconds(1.25f);
                    first = false;
                    if (Model.RockPaperSpell.Wizards[i].CastSpell())
                    {
                        yield return new WaitForSeconds(movementTime);
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
            TargetSelectionTime = targetSelectionTime;
            Controller = this;
            LocalPlayer = this[localPlayerIndex];
            SetViews();
            if (offline)
            {
                Setup(offlinePlayers);
                StartMatch();
                rockPaperSpell.GetComponent<View.RockPaperSpell>().SetLocalPlayer(localPlayerIndex);
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