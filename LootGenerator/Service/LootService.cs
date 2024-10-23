using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootGenerator.Interface;
using LootGenerator.Model.Creature;
using LootGenerator.Model.Loot;

namespace LootGenerator.Service;

internal class LootService(IDiceService diceService, IGoldService goldService, IGemstoneService gemstoneService) : ILootService
{
    private readonly IDiceService _diceService = diceService;
    private readonly IGoldService _goldService = goldService;
    private readonly IGemstoneService _gemstoneService = gemstoneService;

    public Tuple<List<LootType>, Gold?, Gemstone?> Generate(IMonster monster)
    {
        List<LootType> loot = new();
        Gold? gold;
        Gemstone? gemstone;

        if (monster.CreatureType.HeadOdds > 0 && monster.CreatureType.HeadOdds >= _diceService.Roll(1, 100))
        {
            loot.Add(LootType.Head);
        }

        if (monster.CreatureType.CheastOdds > 0 && monster.CreatureType.CheastOdds >= _diceService.Roll(1, 100))
        {
            loot.Add(LootType.Chest);
        }

        if (monster.CreatureType.HandsOdds > 0 && monster.CreatureType.HandsOdds >= _diceService.Roll(1, 100))
        {
            loot.Add(LootType.Hands);
        }

        if (monster.CreatureType.PocketsOdds > 0 && monster.CreatureType.PocketsOdds >= _diceService.Roll(1, 100))
        {
            loot.Add(LootType.Pockets);
        }

        if (monster.CR != ChallengeRating.None)
        {
            gold = _goldService.Generate(monster.CR);
        }
        else
        {
            gold = null;
        }

        if (monster.GemTier != GemstoneTier.None && monster.GemOdds >= _diceService.Roll(1, 100))
        {
            gemstone = _gemstoneService.Generate(monster.GemTier);
        }
        else
        {
            gemstone = null;
        }

        return Tuple.Create(loot, gold, gemstone);
    }
}