using System.Collections.Generic;
using UnityEngine;

namespace RockPaperSpell.Model
{
    public class SpellStack
    {
        private List<Spell> library, offensive, defensive, gold;

        public SpellStack()
        {
            library = new List<Spell>();
            offensive = new List<Spell>();
            defensive = new List<Spell>();
            gold = new List<Spell>();

            //Defensive Spells
            AddDefensiveSpell(new AntimagicField(), library);
            AddDefensiveSpell(new Confusion(), library);
            AddDefensiveSpell(new Counterspell(), library);
            AddDefensiveSpell(new Levitate(), library);
            AddDefensiveSpell(new MistyStep(), library);
            AddDefensiveSpell(new Passwall(), library);
            AddDefensiveSpell(new TeleportationCircle(), library);
            AddDefensiveSpell(new WallOfForce(), library);

            //Gold Spells
            AddGoldSpell(new BurningHands(), library);
            AddGoldSpell(new CharmPerson(), library);
            AddGoldSpell(new ColorSpray(), library);
            AddGoldSpell(new DominatePerson(), library);
            AddGoldSpell(new Feeblemind(), library);
            AddGoldSpell(new Imprisonment(), library);

            //Offensive Spells
            AddOffensiveSpell(new ChainLighting(), library);
            AddOffensiveSpell(new DimensionDoor(), library);
            AddOffensiveSpell(new Fear(), library);
            AddOffensiveSpell(new Fireball(), library);
            AddOffensiveSpell(new IceStorm(), library);
            AddOffensiveSpell(new MeteorSwarm(), library);
            AddOffensiveSpell(new Polymorph(), library);
            AddOffensiveSpell(new StinkingCloud(), library);
            AddOffensiveSpell(new Telekinesis(), library);
            AddOffensiveSpell(new VampiricTouch(), library);
        }

        public Spell WildSurge()
        {
            int random = Random.Range(0, library.Count);
            return library[random];
        }

        public Spell RandomSpell(SpellType spellType = SpellType.All)
        {
            List<Spell> spells;
            if (spellType == SpellType.All)
            {
                spells = library;
            }
            else
            {
                spells = new List<Spell>();
                if ((spellType & SpellType.Offensive) != 0)
                {
                    spells.AddRange(offensive);
                }
                if ((spellType & SpellType.Defensive) != 0)
                {
                    spells.AddRange(defensive);
                }
                if ((spellType & SpellType.Gold) != 0)
                {
                    spells.AddRange(gold);
                }
            }
            int random = Random.Range(0, spells.Count);
            Spell spell = spells[random];
            RemoveSpell(spell);
            return spell;
        }

        public void AddSpell(Spell spell)
        {
            AddRemoveSpell(spell, true);
        }

        private void RemoveSpell(Spell spell)
        {
            AddRemoveSpell(spell, false);
        }

        private void AddRemoveSpell(Spell spell, bool add)
        {
            AddRemoveSpell(spell, library, add);
            switch (spell.Type)
            {
                case SpellType.Offensive:
                    AddRemoveSpell(spell, offensive, add);
                    break;
                case SpellType.Defensive:
                    AddRemoveSpell(spell, defensive, add);
                    break;
                case SpellType.Gold:
                    AddRemoveSpell(spell, gold, add);
                    break;
            }
        }

        private void AddRemoveSpell(Spell spell, List<Spell> list, bool add)
        {
            if (add)
            {
                AddSpell(spell, list);
            }
            else
            {
                RemoveSpell(spell, list);
            }
        }

        private void AddSpell(Spell spell, List<Spell> list)
        {
            list.Add(spell);
        }

        private void RemoveSpell(Spell spell, List<Spell> list)
        {
            list.Remove(spell);
        }

        private void AddDefensiveSpell(Spell spell, List<Spell> list)
        {
            AddSpell(spell, list);
            defensive.Add(spell);
        }

        private void AddOffensiveSpell(Spell spell, List<Spell> list)
        {
            AddSpell(spell, list);
            offensive.Add(spell);
        }

        private void AddGoldSpell(Spell spell, List<Spell> list)
        {
            AddSpell(spell, list);
            gold.Add(spell);
        }
    }
}