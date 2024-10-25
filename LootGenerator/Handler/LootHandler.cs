using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootGenerator.Interface;
using LootGenerator.Model.Creature;
using LootGenerator.Model.Loot;
using LootGenerator.Repository;

namespace LootGenerator.Handler;

internal class LootHandler(IGoldService goldService, IGemstoneService gemstoneService, ILootService lootService, LootRepo lootRepo) : ILootHandler
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

    private void GenerateLoot(int monsterCount, Monster monster)
    {
        double totalgold = 0;
        List<Gemstone> gemstones = new();
        Dictionary<LootType, int> lootTypes = new();
        for (int i = 0; i < monsterCount; i++)
        {
            var loot = lootService.Generate(monster);
            if (loot.Item2 is not null)
            {
                totalgold += loot.Item2.Amount;
            }
            if (loot.Item3 is not null)
            {
                gemstones.Add(loot.Item3);
            }
            foreach (var item in loot.Item1)
            {
                lootTypes[item] = lootTypes.TryGetValue(item, out int value) ? value + 1 : 1;
            }
        }

        NewLoot?.Invoke(this, goldService.Create(totalgold).ToString());
        foreach (var gemstone in gemstones)
        {
            NewLoot?.Invoke(this, gemstone.ToString());
        }
        foreach (var lootType in lootTypes)
        {
            NewLoot?.Invoke(this, monster.Flavor[lootType.Key] + " x" + lootType.Value);
        }
    }

    public void GenerateLoot(int monsterCount, string monsterString)
    {
        Monster? monster = lootRepo.GetMonster(monsterString);
        if (monster is not null)
        {
            GenerateLoot(monsterCount, monster);
        }
    }
}