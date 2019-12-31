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
        public Color color;

        public bool WildSurge { get; set; }
        public Color Color { get => color; set => color = value; }
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
                targetEvent.Invoke(value);
            }
        }
        public Spell ChosenSpell
        {
            get => chosenSpell;
            set
            {
                chosenSpell = value;
                spellEvent.Invoke(value);
            }
        }
        public Wizard Next { get; set; }
        public Wizard Previous { get; set; }

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
            goldEvent.Invoke(Gold);
        }

        public void AddTargetListener(UnityAction<Wizard> action)
        {
            targetEvent.AddListener(action);
            targetEvent.Invoke(Target);
        }

        public void AddSpellListener(UnityAction<Spell> action)
        {
            spellEvent.AddListener(action);
            spellEvent.Invoke(ChosenSpell);
        }
    }
}