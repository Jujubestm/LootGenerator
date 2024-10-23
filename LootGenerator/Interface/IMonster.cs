using LootGenerator.Model.Creature.CreatureType;
using LootGenerator.Model.Creature;
using LootGenerator.Model.Loot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LootGenerator.Interface;

internal interface IMonster
{
    public string Name { get; }
    public ICreatureType CreatureType { get; }

    public Dictionary<LootType, string> Flavor { get; }

    public ChallengeRating CR { get; }
    public GemstoneTier GemTier { get; }
    public int GemOdds { get; }
}