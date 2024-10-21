using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootGenerator.Model.Loot;

internal class Gold
{
    public double Amount { get; set; }

    public override string ToString()
    { return Amount + " gp"; }
}