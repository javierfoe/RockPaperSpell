using RockPaperSpell.Events;
using UnityEngine;
using UnityEngine.Events;

namespace RockPaperSpell.Model
{
    public class Wizard
    {
        private UnityEventWizard targetEvent;
        private UnityEventSpell spellEvent;
        private UnityEventInt positionEvent, goldEvent;

        private int position, gold;
        private Wizard target;
        private Spell chosenSpell;

        public bool WildSurge { get; set; }
        public Color Color;
        public int Position
        {
            get => position;
            set
            {
                if (value < 0)
                    position = 0;
                else if (value > 10)
                    position = 10;
                else
                    position = value;
                positionEvent.Invoke(position);
            }
        }
        public int Gold
        {
            get => gold;
            set
            {
                if (value < 0)
                    gold = 0;
                else
                    gold = value;
                goldEvent.Invoke(gold);
            }
        }
        public Wizard Target
        {
            get => target;
            set
            {
                target = value;
                targetEvent.Invoke(value.GetStruct());
            }
        }
        public Spell ChosenSpell
        {
            get => chosenSpell;
            set
            {
                chosenSpell = value;
                if (value != null)
                {
                    spellEvent.Invoke(value.GetStruct());
                }
                else
                {
                    spellEvent.Invoke(new Structs.Spell());
                }
            }
        }
        public Wizard Next { get; set; }
        public Wizard Previous { get; set; }

        public Structs.Wizard GetStruct()
        {
            return new Structs.Wizard
            {
                color = Color
            };
        }

        public Wizard() : this(5) { }

        public Wizard(int position)
        {
            targetEvent = new UnityEventWizard();
            spellEvent = new UnityEventSpell();
            goldEvent = new UnityEventInt();
            positionEvent = new UnityEventInt();
            Gold = 3;
            Position = position;
        }

        public void SetSpellAndTarget(Wizard target, Spell spell)
        {
            Target = target;
            ChosenSpell = spell;
        }

        public bool CastSpell()
        {
            if (ChosenSpell == null) return false;
            ChosenSpell.Cast(this, Target);
            ChosenSpell = null;
            return true;
        }

        public void Reposition()
        {
            if (Position < 3)
            {
                Position = 3;
            }
            else if (Position > 7)
            {
                Position = 7;
            }
            WildSurge = false;
        }

        public void AddPositionListener(UnityAction<int> action)
        {
            positionEvent.AddListener(action);
        }

        public void AddGoldListener(UnityAction<int> action)
        {
            goldEvent.AddListener(action);
        }

        public void AddTargetListener(UnityAction<Structs.Wizard> action)
        {
            targetEvent.AddListener(action);
        }

        public void AddSpellListener(UnityAction<Structs.Spell> action)
        {
            spellEvent.AddListener(action);
        }

        public void InvokeEvents()
        {
            Gold = Gold;
            Position = Position;
        }
    }
}