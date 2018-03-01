namespace LiftDiscord.PathOfBuilding.Models
{
    public class Character
    {
        public Character(int level, string characterClass,
            string ascendancy, Summary summary, CharacterSkills skills)
        {
            Level = level;
            Class = characterClass;
            Ascendancy = ascendancy;
            Summary = summary;
            Skills = skills;
        }

        public string Ascendancy { get; }
        public string Class { get; }
        public int Level { get; }

        public Summary Summary { get; }
        public CharacterSkills Skills { get; }
    }
}
