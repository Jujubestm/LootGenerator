using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootGenerator.Interface;
using LootGenerator.Model.Loot;
using LootGenerator.Model.Creature;
using LootGenerator.Repository;

namespace LootGenerator.Service;

internal class LootService(IDiceService diceService, IGoldService goldService, IGemstoneService gemstoneService, LootRepo lootRepo) : ILootService
{
    private readonly IDiceService _diceService = diceService;
    private readonly IGoldService _goldService = goldService;
    private readonly IGemstoneService _gemstoneService = gemstoneService;

    public Tuple<List<LootType>, Gold?, Gemstone?> Generate(Monster monster)
    {
        List<LootType> loot = new();
        Gold? gold = null;
        Gemstone? gemstone = null;
        ICreatureType? creatureType = lootRepo.GetCreatureType(monster.CreatureType);

        if (creatureType is not null)
        {
            if (creatureType.HeadOdds > 0 && creatureType.HeadOdds >= _diceService.Roll(1, 100))
            {
                loot.Add(LootType.Head);
            }

            if (creatureType.CheastOdds > 0 && creatureType.CheastOdds >= _diceService.Roll(1, 100))
            {
                loot.Add(LootType.Chest);
            }

            if (creatureType.HandsOdds > 0 && creatureType.HandsOdds >= _diceService.Roll(1, 100))
            {
                loot.Add(LootType.Hands);
            }

            if (creatureType.PocketsOdds > 0 && creatureType.PocketsOdds >= _diceService.Roll(1, 100))
            {
                loot.Add(LootType.Pockets);
            }

            if (monster.CR != ChallengeRating.None)
            {
                gold = _goldService.Generate(monster.CR);
            }

            if (monster.GemTier != GemstoneTier.None && monster.GemOdds >= _diceService.Roll(1, 100))
            {
                gemstone = _gemstoneService.Generate(monster.GemTier);
            }
        }
        return Tuple.Create(loot, gold, gemstone);
    }
}