using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootGenerator.Interface;
using LootGenerator.Model.Loot;

namespace LootGenerator.Model.Creature.CreatureType;

internal class Undead : ICreatureType
{
    public int HeadOdds => 30;
    public int CheastOdds => 30;
    public int HandsOdds => 70;
    public int PocketsOdds => 70;
}