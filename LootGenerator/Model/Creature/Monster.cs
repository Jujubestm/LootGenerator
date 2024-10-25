using LootGenerator.Interface;
using LootGenerator.Model.Creature.CreatureType;
using LootGenerator.Model.Loot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootGenerator.Model.Creature;

internal class Monster()
{
    public string Name { get; set; } = null!;
    public CreatureTypeList CreatureType { get; set; }
    public Dictionary<LootType, string> Flavor { get; set; } = null!;
    public ChallengeRating CR { get; set; }
    public GemstoneTier GemTier { get; set; }
    public int GemOdds { get; set; }
}