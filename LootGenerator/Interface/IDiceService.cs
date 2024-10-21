using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootGenerator.Interface;

internal interface IDiceService
{
    public int Roll(int numberOfRolls, int sides);
}