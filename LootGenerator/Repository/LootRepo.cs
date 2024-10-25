using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootGenerator.Interface;
using LootGenerator.Model.Creature;
using LootGenerator.Model.Creature.CreatureType;
using LootGenerator.Model.Loot;
using System.Text.Json;
using System.Reflection;

namespace LootGenerator.Repository;

internal class LootRepo
{
    private static readonly string programPath = Path.GetDirectoryName(Environment.ProcessPath) ?? throw new InvalidOperationException("Unable to determine the program path.");
    private readonly string creatureTypePath = Path.Combine(programPath, "CreatureType.json");
    private readonly string monsterPath = Path.Combine(programPath, "Monster.json");

    private readonly Dictionary<Model.Creature.CreatureType.CreatureTypeList, Interface.ICreatureType> creatureTypes = new();
    private readonly Dictionary<string, Monster> monsters = new();

    public LootRepo()
    {
        if (!File.Exists(creatureTypePath))
        {
            Dictionary<Model.Creature.CreatureType.CreatureTypeList, Interface.ICreatureType> dummy = new()
            {
                { Model.Creature.CreatureType.CreatureTypeList.Undead, new Undead() }
            };
            File.WriteAllText(creatureTypePath, JsonSerializer.Serialize(dummy));
        }
        if (!File.Exists(monsterPath))
        {
            Dictionary<LootType, string> dummy = new()
            {
                { LootType.Head, "Skull" },
                { LootType.Chest, "Rib Cage Bone" },
                { LootType.Hands, "Shorsword" },
                { LootType.Pockets, "Lump of Coal" }
            };

            Dictionary<string, Monster> dummy2 = new()
            {
                {
                    "skeleton", new Monster()
                    {
                        Name = "skeleton",
                        CreatureType = CreatureTypeList.Undead,
                        Flavor = dummy,
                        CR = ChallengeRating.OneQuarter,
                        GemTier = GemstoneTier.None,
                        GemOdds = 0
                    }
                }
            };

            File.WriteAllText(monsterPath, JsonSerializer.Serialize(dummy2));
        }

        var jsonParse = JsonSerializer.Deserialize<Dictionary<CreatureTypeList, object>>(File.ReadAllText(creatureTypePath)) ?? throw new InvalidCastException();
        var creatureTypeKeys = jsonParse.Select(kvp => kvp.Key);
        foreach (var creatureType in creatureTypeKeys)
        {
            var type = Array.Find(Assembly.GetExecutingAssembly().GetTypes(), t => t.Name == creatureType.ToString());

            if (type is not null)
            {
                var instance = Activator.CreateInstance(type) as ICreatureType;
                if (instance is not null)
                {
                    creatureTypes.Add(creatureType, instance);
                }
            }
        }

        monsters = JsonSerializer.Deserialize<Dictionary<string, Monster>>(File.ReadAllText(monsterPath)) ?? new();
    }

    public ICreatureType? GetCreatureType(CreatureTypeList creatureType)
    {
        return creatureTypes.TryGetValue(creatureType, out ICreatureType? value) ? value : null;
    }

    public Monster? GetMonster(string monster)
    {
        return monsters.TryGetValue(monster, out Monster? value) ? value : null;
    }
}