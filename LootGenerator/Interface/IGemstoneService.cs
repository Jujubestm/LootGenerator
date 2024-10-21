using LootGenerator.Model.Loot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootGenerator.Interface
{
    internal interface IGemstoneService
    {
        public Gemstone Generate(GemstoneTier tier);

        public Gemstone Create(GemstoneType type);
    }
}