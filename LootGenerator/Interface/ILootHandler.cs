using LootGenerator.Model.Creature;
using LootGenerator.Model.Loot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootGenerator.Interface;

internal interface ILootHandler
{
    public event EventHandler<string> NewLoot;

    public void GenerateGold(ChallengeRating cr);

    public void GenerateGemstone(GemstoneTier tier);

    public void GenerateLoot(int monsterCount, string monsterString);
}