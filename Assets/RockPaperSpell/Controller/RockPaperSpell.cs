using RockPaperSpell.Structs;
using System.Collections;
using UnityEngine;

namespace RockPaperSpell.Controller
{
    public class RockPaperSpell : MonoBehaviour
    {
        public static float WizardMovementTime { get; private set; }
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

        [Header("Wizard Movement Time")]
        [SerializeField] private float movementTime = 0;
        [Header("Highlighting")]
        [SerializeField] private float saturationOn = 0;
        [SerializeField] private float saturationOff = 0, brightnessOn = 0, brightnessOff = 0;
        [Header("Colors")]
        [SerializeField] private Color[] colors = null;
        [Header("Rock Paper Spell")]
        [SerializeField] private Component rockPaperSpell = null;
        [field: Header("Wizard Controller"), SerializeField] public Transform WizardControllers { get; private set; }
        [Header("Spell Book Controller")]
        [SerializeField] private GameObject spellBookGameObject = null;
        [Header("Offline settings")]
        [SerializeField] private bool offline = false;
        [SerializeField] private int offlinePlayers = 0;

        private SpellBook spellBook;
        private Interface.RockPaperSpell rockPaperSpellView;
        private int players;
        private Wizard[] wizardControllers;

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
            StartCoroutine(StartGame());
        }

        private void SetupWizards()
        {
            Model.Wizard[] wizards = Model.RockPaperSpell.Wizards;
            int length = wizards.Length;
            wizardControllers = new Wizard[length];
            for (int i = 0; i < length; i++)
            {
                wizards[i].Color = colors[i];
                wizardControllers[i] = WizardControllers.GetChild(i).GetComponent<Wizard>();
                wizardControllers[i].SetWizardModel(wizards[i]);
            }
        }

        private void SetupSpellBook()
        {
            GetDependencies();
            spellBook.SetSpellBook(Model.RockPaperSpell.SpellBook);
        }

        private IEnumerator StartGame()
        {
            yield return null;
            rockPaperSpellView.SetView(players);
            foreach (Wizard wizard in wizardControllers)
            {
                wizard.InitialState();
            }
            spellBook.InitialState();
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
            if (offline)
            {
                Setup(offlinePlayers);
                StartMatch();
            }
        }

        private void GetDependencies()
        {            
            spellBook = spellBookGameObject.GetComponent<SpellBook>();
            rockPaperSpellView = rockPaperSpell as Interface.RockPaperSpell;
        }

        private void OnValidate()
        {
            GetDependencies();
        }
    }
}