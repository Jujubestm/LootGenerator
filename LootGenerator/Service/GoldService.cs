using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootGenerator.Interface;
using LootGenerator.Model.Creature;
using LootGenerator.Model.Loot;
using LootGenerator.Service;

namespace LootGenerator.Service;

internal class GoldService(IDiceService diceService) : IGoldService
{
    public Gold Generate(ChallengeRating CR)
    {
        var gold = new Gold();

        double roll;
        double secondRoll = 0;
        const double cp = 0.01;
        const double sp = 0.1;
        const double ep = 0.5;
        const double gp = 1;
        const double pp = 10;

        switch (CR)
        {
            case ChallengeRating.Zero:
            case ChallengeRating.OneEighth:
            case ChallengeRating.OneQuarter:
            case ChallengeRating.OneHalf:
            case ChallengeRating.One:
            case ChallengeRating.Two:
            case ChallengeRating.Three:
            case ChallengeRating.Four:
                switch (diceService.Roll(1, 100))
                {
                    case <= 30:
                        roll = diceService.Roll(5, 6) * cp; // 5d6 cp
                        gold.Amount = roll + secondRoll;
                        break;

                    case <= 60:
                        roll = diceService.Roll(4, 6) * sp; // 4d6 sp
                        gold.Amount = roll + secondRoll;
                        break;

                    case <= 70:
                        roll = diceService.Roll(3, 6) * ep; // 3d6 ep
                        gold.Amount = roll + secondRoll;
                        break;

                    case <= 95:
                        roll = diceService.Roll(3, 6) * gp; // 3d6 gp
                        gold.Amount = roll + secondRoll;
                        break;

                    case <= 100:
                        roll = diceService.Roll(1, 6) * pp; // 1d6 pp
                        gold.Amount = roll + secondRoll;
                        break;
                }

                break;

            case ChallengeRating.Five:
            case ChallengeRating.Six:
            case ChallengeRating.Seven:
            case ChallengeRating.Eight:
            case ChallengeRating.Nine:
            case ChallengeRating.Ten:
                switch (diceService.Roll(1, 100))
                {
                    case <= 30:
                        roll = diceService.Roll(4, 6) * cp * 100;      // 4d6 cp * 100
                        secondRoll = diceService.Roll(1, 6) * ep * 10; // 1d6 ep * 10
                        gold.Amount = roll + secondRoll;
                        break;

                    case <= 60:
                        roll = diceService.Roll(6, 6) * sp * 10;       // 6d6 sp * 10
                        secondRoll = diceService.Roll(2, 6) * gp * 10; // 2d6 gp * 10
                        gold.Amount = roll + secondRoll;
                        break;

                    case <= 70:
                        roll = diceService.Roll(3, 6) * ep * 10;       // 3d6 ep * 10
                        secondRoll = diceService.Roll(2, 6) * gp * 10; // 2d6 gp * 10
                        gold.Amount = roll + secondRoll;
                        break;

                    case <= 95:
                        roll = diceService.Roll(4, 6) * gp * 10; // 4d6 gp * 10
                        gold.Amount = roll + secondRoll;
                        break;

                    case <= 100:
                        roll = diceService.Roll(2, 6) * gp * 10;  // 2d6 gp * 10
                        secondRoll = diceService.Roll(3, 6) * pp; // 3d6 pp
                        gold.Amount = roll + secondRoll;
                        break;
                }

                break;

            case ChallengeRating.Eleven:
            case ChallengeRating.Twelve:
            case ChallengeRating.Thirteen:
            case ChallengeRating.Fourteen:
            case ChallengeRating.Fifteen:
            case ChallengeRating.Sixteen:
                switch (diceService.Roll(1, 100))
                {
                    case <= 20:
                        roll = diceService.Roll(4, 6) * sp * 100;       // 4d6 sp * 100
                        secondRoll = diceService.Roll(1, 6) * gp * 100; // 1d6 gp * 100
                        gold.Amount = roll + secondRoll;
                        break;

                    case <= 35:
                        roll = diceService.Roll(1, 6) * ep * 100;       // 1d6 ep * 100
                        secondRoll = diceService.Roll(1, 6) * gp * 100; // 1d6 gp * 100
                        gold.Amount = roll + secondRoll;
                        break;

                    case <= 75:
                        roll = diceService.Roll(2, 6) * gp * 100;      // 2d6 gp * 100
                        secondRoll = diceService.Roll(1, 6) * pp * 10; // 1d6 pp * 10
                        gold.Amount = roll + secondRoll;
                        break;

                    case <= 100:
                        roll = diceService.Roll(2, 6) * gp * 100;      // 2d6 gp * 100
                        secondRoll = diceService.Roll(2, 6) * pp * 10; // 2d6 pp * 10
                        gold.Amount = roll + secondRoll;
                        break;
                }

                break;

            default:
                switch (diceService.Roll(1, 100))
                {
                    case <= 15:
                        roll = diceService.Roll(2, 6) * ep * 1000;      // 2d6 ep * 1000
                        secondRoll = diceService.Roll(8, 6) * gp * 100; // 8d6 gp * 100
                        gold.Amount = roll + secondRoll;
                        break;

                    case <= 55:
                        roll = diceService.Roll(1, 6) * gp * 1000;      // 1d6 gp * 1000
                        secondRoll = diceService.Roll(1, 6) * pp * 100; // 1d6 pp * 100
                        gold.Amount = roll + secondRoll;
                        break;

                    case <= 100:
                        roll = diceService.Roll(1, 6) * gp * 1000;      // 1d6 gp * 1000
                        secondRoll = diceService.Roll(2, 6) * pp * 100; // 2d6 pp * 100
                        gold.Amount = roll + secondRoll;
                        break;
                }

                break;
        }

        return gold;
    }

    public Gold Create(double value)
    { return new Gold { Amount = value }; }
}