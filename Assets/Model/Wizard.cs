using UnityEngine.Events;

namespace RockPaperSpell.Model
{
    internal class UnityEventInt : UnityEvent<int> { }

    public class Wizard
    {
        private UnityEventInt positionEvent, goldEvent;
        private int position, gold;

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
        public Wizard Target { get; set; }
        public Spell ChosenSpell { get; set; }
        public Wizard Next { get; set; }
        public Wizard Previous { get; set; }
        
        public Wizard() : this(5) { }

        public Wizard(int position)
        {
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
    }
}