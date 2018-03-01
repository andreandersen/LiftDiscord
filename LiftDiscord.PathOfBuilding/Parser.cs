namespace LiftDiscord.PathOfBuilding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using Ionic.Zlib;
    using LiftDiscord.PathOfBuilding.Models;

    public class Parser
    {
        public Character ParseCode(string base64)
        {
            var xml = XDocument.Parse(FromBase64ToXml(base64));

            var summary = new Summary(GetPlayerStats(xml));
            var skills = GetCharacterSkills(xml);

            var build = xml.Root.Element("Build");
            var level = int.Parse(build.Attribute("level").Value);
            var className = build.Attribute("className").Value;
            var ascendancy = build.Attribute("ascendClassName").Value;

            return new Character(level, className, ascendancy, summary, skills);
        }

        private static CharacterSkills GetCharacterSkills(XDocument doc)
        {
            var mainGroup = int.Parse(doc.Root.Element("Build").Attribute("mainSocketGroup").Value) - 1;

            var skills = doc.Root.Element("Skills")
                .Elements("Skill")
                .Select((c, i) =>
                    new SkillGroup(
                        GetGemsFromSkill(c), c.Attribute("slot")?.Value,
                        bool.Parse(c.Attribute("enabled").Value), i == mainGroup))
                .ToList();

            return new CharacterSkills(skills, mainGroup);
        }

        private static IEnumerable<Gem> GetGemsFromSkill(XElement skillElement)
        {
            return skillElement.Elements("Gem")
                .Select(c => {
                    var level = int.Parse(c.Attribute("level").Value);
                    var quality = int.Parse(c.Attribute("quality").Value);
                    var skillId = c.Attribute("skillId").Value;
                    var name = c.Attribute("nameSpec").Value;
                    var enabled = bool.Parse(c.Attribute("enabled").Value);
                    return new Gem(skillId, name, level, quality, enabled);
                });
        }
        
        private static Dictionary<string, string> GetPlayerStats(XDocument doc)
        {
            return doc.Root
                .Element("Build")
                .Elements("PlayerStat")
                .Select(c => new StatObject(c))
                .Where(c => c.IsValid)
                .GroupBy(c => c.Name, c => c.Value)
                .ToDictionary(c => c.Key, c => c.First());
        }

        private static string FromBase64ToXml(string base64)
        {
            var dec = Convert.FromBase64String(base64.Replace("-", "+").Replace("_", "/"));
            using (var input = new MemoryStream(dec))
            using (var deflate = new ZlibStream(input, CompressionMode.Decompress))
            using (var output = new MemoryStream())
            {
                deflate.CopyTo(output);
                return Encoding.UTF8.GetString(output.ToArray());
            }
        }

        private class StatObject
        {
            public StatObject(XElement element)
            {
                var attributes = element.Attributes().ToList();
                var nameAttribute = attributes.FirstOrDefault(c => c.Name == "stat");
                var valueAttribute = attributes.FirstOrDefault(c => c.Name == "value");
                Name = nameAttribute?.Value;
                Value = valueAttribute?.Value;
            }

            public string Name { get; }
            public string Value { get; }
            public bool IsValid => Name != null && Value != null;
        }
    }
}
