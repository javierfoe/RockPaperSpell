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

        public Color Color
        {
            get; set;
        }
        public int Position
        {
            get => position;
            set
            {
                if (value < 0)
                    position = 0;
                else if (value > 11)
                    position = 11;
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

        public void CastSpell()
        {
            ChosenSpell?.Cast(this, Target);
        }

        public void Reposition()
        {
            if (Position < 4)
            {
                Position = 4;
            }
            else if (Position > 7)
            {
                Position = 7;
            }
        }

        public void AddPositionListener(UnityAction<int> action)
        {
            positionEvent.AddListener(action);
            positionEvent.Invoke(Position);
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