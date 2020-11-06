using RockPaperSpell.Interface;
using RockPaperSpell.Structs;
using System.Collections;
using UnityEngine;

namespace RockPaperSpell.Controller
{
    public static class GameController
    {
        public static int PlayerAmount { get; set; }
        public const float TargetSelectionTime = 10.0f;
        public const float WizardMovementTime = 2.0f;

        private static Interface.View rockPaperSpellView;
        private static Wizard[] wizardControllers;
        private static SpellBook spellBook;
        private static WaitForSpells waitForSpells;

        public static void SetViews()
        {
            FindView();

            WizardView viewAux;
            for (int i = 0; i < wizardControllers.Length; i++)
            {
                viewAux = rockPaperSpellView.GetElement(i);
                wizardControllers[i].SetView(viewAux);
                viewAux.SetController(wizardControllers[i]);
            }
            spellBook.SetView(rockPaperSpellView.SpellBook);
        }

        public static void SetTargetSpell(int player, SpellTarget spellTarget)
        {
            waitForSpells.SetSpellTarget(player, spellTarget);
        }

        public static void SetWizardColors(Color[] colors)
        {
            Model.Wizard[] wizards = Model.RockPaperSpell.Wizards;
            if (wizards == null) return;
            int length = wizards.Length;
            for (int i = 0; i < length; i++)
            {
                wizards[i].Color = colors[i];
            }
        }

        public static IEnumerator StartGame()
        {
            Model.RockPaperSpell.SetupBoard(PlayerAmount);
            SetupWizards();
            SetupSpellBook();
            SetViews();
            yield return rockPaperSpellView.SetView(PlayerAmount);
            SetInitialState();
            yield return new WaitForSeconds(WizardMovementTime);
            bool win;
            int winner;
            int speedPotion = 0;
            do
            {
                waitForSpells = new WaitForSpells(PlayerAmount);
                rockPaperSpellView.EnableCast(true);
                yield return waitForSpells;
                rockPaperSpellView.EnableCast(false);
                Model.RockPaperSpell.SetTargetsAndSpells(waitForSpells.SpellTargets);
                bool first = true;
                for (int i = speedPotion; i != speedPotion || first; i = i < PlayerAmount - 1 ? i + 1 : 0)
                {
                    yield return new WaitForSeconds(1.25f);
                    first = false;
                    if (Model.RockPaperSpell.Wizards[i].CastSpell())
                    {
                        yield return new WaitForSeconds(WizardMovementTime);
                    }
                }
                speedPotion = speedPotion < PlayerAmount - 1 ? speedPotion + 1 : 0;
                win = Model.RockPaperSpell.CheckWin(out winner);
                yield return new WaitForSeconds(1);
            } while (!win);
            Debug.Log(winner);
        }

        private static void FindView()
        {
            View.RockPaperSpell viewRPP = GameObject.FindObjectOfType<View.RockPaperSpell>();
            Transform parent = viewRPP.transform.parent;
            if (parent.childCount > 1)
            {
                Transform network = parent.GetChild(1);
                rockPaperSpellView = network.GetComponent<Network.RockPaperSpell>();
            }
            else
            {
                rockPaperSpellView = viewRPP;
            }
        }

        private static void SetupWizards()
        {
            Model.Wizard[] wizards = Model.RockPaperSpell.Wizards;
            int length = wizards.Length;
            wizardControllers = new Wizard[length];
            for (int i = 0; i < length; i++)
            {
                wizardControllers[i] = new Wizard();
                wizardControllers[i].SetWizardModel(wizards[i]);
            }
        }

        private static void SetupSpellBook()
        {
            spellBook = new SpellBook();
            spellBook.SetSpellBook(Model.RockPaperSpell.SpellBook);
        }

        private static void SetInitialState()
        {
            for (int i = 0; i < PlayerAmount; i++)
            {
                wizardControllers[i].InitialState();
            }
            spellBook.InitialState();
        }
    }
}