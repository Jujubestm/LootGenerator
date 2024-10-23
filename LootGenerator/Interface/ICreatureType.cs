using LootGenerator.Model.Loot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootGenerator.Interface;

internal interface ICreatureType
{
    public int HeadOdds { get; }
    public int CheastOdds { get; }
    public int HandsOdds { get; }
    public int PocketsOdds { get; }
}