namespace LiftDiscord.PathOfBuilding.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class Summary
    {
        private static Dictionary<string, PropertyInfo> _props =
            typeof(Summary).GetProperties().ToDictionary(c => c.Name, c => c);

        public Summary(Dictionary<string, string> playerStats)
        {
            foreach (var item in playerStats)
            {
                if (!_props.TryGetValue(item.Key, out var prop))
                    continue;

                object value = null;

                if (prop.PropertyType == typeof(int))
                    value = Convert.ToInt32(item.Value);
                else if (prop.PropertyType == typeof(float))
                    value = Convert.ToSingle(item.Value);
                else
                    value = item.Value;

                if (value != null)
                    prop.SetValue(this, value);
            }
        }

        public int Str { get; private set; }
        public int Dex { get; private set; }
        public int Int { get; private set; }

        public float AverageDamage { get; private set; }
        public float Speed { get; private set; }
        public float CritChance { get; private set; }
        public float CritMultiplier { get; private set; }
        public float TotalDPS { get; private set; }
        public float TotalDot { get; private set; }
        public float BleedDPS { get; private set; }
        public float IgniteDPS { get; private set; }
        public float WithPoisonDPS { get; private set; }
        public int ManaCost { get; private set; }

        public float NetLifeRegen { get; private set; }

        public int Life { get; private set; }
        public int LifeUnreserved { get; private set; }

        public int Mana { get; private set; }
        public int ManaUnreserved { get; private set; }

        public int EnergyShield { get; private set; }

        public int Armour { get; private set; }
        public int Evasion { get; private set; }

        public int FireResist { get; private set; }
        public int ColdResist { get; private set; }
        public int LightningResist { get; private set; }
        public int ChaosResist { get; private set; }
    }
}
