using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootGenerator.Interface;
using LootGenerator.Model.Creature;
using LootGenerator.Model.Loot;

namespace LootGenerator.Handler;

internal class LootHandler(IGoldService goldService, IGemstoneService gemstoneService, ILootService lootService) : ILootHandler
{
    public event EventHandler<string>? NewLoot;

    public void GenerateGold(ChallengeRating cr)
    {
        NewLoot?.Invoke(this, goldService.Generate(cr).ToString());
    }

    public void GenerateGemstone(GemstoneTier tier)
    {
        NewLoot?.Invoke(this, gemstoneService.Generate(tier).ToString());
    }

    public void GenerateLoot(int monsterCount, IMonster monster)
    {
        for (int i = 0; i < monsterCount; i++)
        {
            var loot = lootService.Generate(monster);
            if (loot.Item2 is not null)
            {
                NewLoot?.Invoke(this, loot.Item2.ToString());
            }
            if (loot.Item3 is not null)
            {
                NewLoot?.Invoke(this, loot.Item3.ToString());
            }
            foreach (var item in loot.Item1)
            {
                NewLoot?.Invoke(this, monster.Flavor[item]);
            }
        }
    }
}