using LootGenerator.Model.Creature;
using LootGenerator.Model.Creature.CreatureType;
using LootGenerator.Model.Loot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootGenerator.Interface;

internal interface ILootService
{
    public Tuple<List<LootType>, Gold?, Gemstone?> Generate(IMonster monster);
}