using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootGenerator.Interface;

namespace LootGenerator.Service;

internal class DiceService : IDiceService
{
    private readonly Random random = new();

    private int Roll(int sides)
    { return random.Next(1, sides + 1); }

    public int Roll(int numberOfRolls, int sides)
    {
        int total = 0;
        for (int i = 0; i < numberOfRolls; i++)
        {
            total += Roll(sides);
        }
        return total;
    }
}