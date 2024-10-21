using LootGenerator.Model.Creature;
using LootGenerator.Model.Loot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootGenerator.Interface;

internal interface IGoldService
{
    public Gold Generate(ChallengeRating CR);

    public Gold Create(double value);
}