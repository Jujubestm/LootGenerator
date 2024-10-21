using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootGenerator.Interface;
using LootGenerator.Model.Creature;
using LootGenerator.Model.Loot;

namespace LootGenerator.Handler;

internal class LootHandler(IGoldService goldService, IGemstoneService gemstoneService) : ILootHandler
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
}