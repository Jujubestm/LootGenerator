using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootGenerator.Model.Loot;

public enum GemstoneType
{
    Alexandrite,

    Amber,

    Amethyst,

    Aquamarine,

    Azurite,

    BandedAgate,

    BlackOpal,

    BlackPearl,

    BlackSapphire,

    Bloodstone,

    BlueQuartz,

    BlueSapphire,

    BlueSpinel,

    Carnelian,

    Chalcedony,

    Chrysoberyl,

    Chrysoprase,

    Citrine,

    Coral,

    Diamond,

    Emerald,

    EyeAgate,

    FireOpal,

    Garnet,

    Hematite,

    Jacinth,

    Jade,

    Jasper,

    Jet,

    LapisLazuli,

    Malachite,

    Moonstone,

    MossAgate,

    Obsidian,

    Onyx,

    Opal,

    Pearl,

    Peridot,

    Quartz,

    Rhodochrosite,

    Ruby,

    Sardonyx,

    Spinel,

    StarRoseQuartz,

    StarRuby,

    StarSapphire,

    TigerEye,

    Topaz,

    Tourmaline,

    Turquoise,

    YellowSapphire,

    Zircon
}

public enum GemstoneTier
{
    Tier1 = 10,

    Tier2 = 50,

    Tier3 = 100,

    Tier4 = 500,

    Tier5 = 1000,

    Tier6 = 5000,

    None = 0
}

internal class Gemstone(GemstoneType type, GemstoneTier tier)
{
    public GemstoneType Type { get; set; } = type;

    public int Value { get; set; } = (int)tier;

    public override string ToString()
    {
        return $"{Type} ({Value} gp)";
    }
}