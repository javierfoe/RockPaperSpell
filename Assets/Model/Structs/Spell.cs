namespace RockPaperSpell.Structs
{
    public struct Spell
    {
        public string name;

        public static Spell CreateDefault()
        {
            return new Spell
            {
                name = ""
            };
        }
    }
}