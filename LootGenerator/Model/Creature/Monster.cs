using LootGenerator.Interface;
using LootGenerator.Model.Loot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootGenerator.Model.Creature;

internal class Monster(string name, ICreatureType creatureType, Dictionary<LootType, string> flavor) : IMonster
{
    public string Name { get; } = name;
    public ICreatureType CreatureType { get; } = creatureType;
    public Dictionary<LootType, string> Flavor { get; } = flavor;
    public ChallengeRating CR { get; } = ChallengeRating.None;
    public GemstoneTier GemTier { get; } = GemstoneTier.None;
    public int GemOdds { get; } = 0;

    public Monster(string name, ICreatureType creatureType, Dictionary<LootType, string> flavor, ChallengeRating cr) : this(name, creatureType, flavor)
    {
        CR = cr;
    }

    public Monster(string name, ICreatureType creatureType, Dictionary<LootType, string> flavor, GemstoneTier gemTier, int gemOdds) : this(name, creatureType, flavor)
    {
        GemTier = gemTier;
        GemOdds = gemOdds;
    }

    public Monster(string name, ICreatureType creatureType, Dictionary<LootType, string> flavor, ChallengeRating cr, GemstoneTier gemTier, int gemOdds) : this(name, creatureType, flavor)
    {
        CR = cr;
        GemTier = gemTier;
        GemOdds = gemOdds;
    }
}