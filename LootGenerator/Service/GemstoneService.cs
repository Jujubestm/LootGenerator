using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootGenerator.Model.Loot;
using LootGenerator.Interface;

namespace LootGenerator.Service;

internal class GemstoneService(IDiceService diceService) : IGemstoneService
{
    private readonly Dictionary<GemstoneType, Gemstone> gemstones = new()
    {
        {GemstoneType.Alexandrite, new Gemstone(GemstoneType.Alexandrite, GemstoneTier.Tier4) },
        {GemstoneType.Amber, new Gemstone(GemstoneType.Amber, GemstoneTier.Tier3) },
        {GemstoneType.Amethyst, new Gemstone(GemstoneType.Amethyst, GemstoneTier.Tier3) },
        {GemstoneType.Aquamarine, new Gemstone(GemstoneType.Aquamarine, GemstoneTier.Tier4) },
        {GemstoneType.Azurite, new Gemstone(GemstoneType.Azurite, GemstoneTier.Tier1) },
        {GemstoneType.BandedAgate, new Gemstone(GemstoneType.BandedAgate, GemstoneTier.Tier1) },
        {GemstoneType.BlackOpal, new Gemstone(GemstoneType.BlackOpal, GemstoneTier.Tier5) },
        {GemstoneType.BlackPearl, new Gemstone(GemstoneType.BlackPearl, GemstoneTier.Tier4) },
        {GemstoneType.BlackSapphire, new Gemstone(GemstoneType.BlackSapphire, GemstoneTier.Tier6) },
        {GemstoneType.Bloodstone, new Gemstone(GemstoneType.Bloodstone, GemstoneTier.Tier2) },
        {GemstoneType.BlueQuartz, new Gemstone(GemstoneType.BlueQuartz, GemstoneTier.Tier1) },
        {GemstoneType.BlueSapphire, new Gemstone(GemstoneType.BlueSapphire, GemstoneTier.Tier5) },
        {GemstoneType.BlueSpinel, new Gemstone(GemstoneType.BlueSpinel, GemstoneTier.Tier4) },
        {GemstoneType.Carnelian, new Gemstone(GemstoneType.Carnelian, GemstoneTier.Tier2) },
        {GemstoneType.Chalcedony, new Gemstone(GemstoneType.Chalcedony, GemstoneTier.Tier2) },
        {GemstoneType.Chrysoberyl, new Gemstone(GemstoneType.Chrysoberyl, GemstoneTier.Tier3) },
        {GemstoneType.Chrysoprase, new Gemstone(GemstoneType.Chrysoprase, GemstoneTier.Tier2) },
        {GemstoneType.Citrine, new Gemstone(GemstoneType.Citrine, GemstoneTier.Tier2) },
        {GemstoneType.Coral, new Gemstone(GemstoneType.Coral, GemstoneTier.Tier3) },
        {GemstoneType.Diamond, new Gemstone(GemstoneType.Diamond, GemstoneTier.Tier6) },
        {GemstoneType.Emerald, new Gemstone(GemstoneType.Emerald, GemstoneTier.Tier5) },
        {GemstoneType.EyeAgate, new Gemstone(GemstoneType.EyeAgate, GemstoneTier.Tier1) },
        {GemstoneType.FireOpal, new Gemstone(GemstoneType.FireOpal, GemstoneTier.Tier5) },
        {GemstoneType.Garnet, new Gemstone(GemstoneType.Garnet, GemstoneTier.Tier3) },
        {GemstoneType.Hematite, new Gemstone(GemstoneType.Hematite, GemstoneTier.Tier1) },
        {GemstoneType.Jacinth, new Gemstone(GemstoneType.Jacinth, GemstoneTier.Tier6) },
        {GemstoneType.Jade, new Gemstone(GemstoneType.Jade, GemstoneTier.Tier3) },
        {GemstoneType.Jasper, new Gemstone(GemstoneType.Jasper, GemstoneTier.Tier2) },
        {GemstoneType.Jet, new Gemstone(GemstoneType.Jet, GemstoneTier.Tier3) },
        {GemstoneType.LapisLazuli, new Gemstone(GemstoneType.LapisLazuli, GemstoneTier.Tier1) },
        {GemstoneType.Malachite, new Gemstone(GemstoneType.Malachite, GemstoneTier.Tier1) },
        {GemstoneType.Moonstone, new Gemstone(GemstoneType.Moonstone, GemstoneTier.Tier2) },
        {GemstoneType.MossAgate, new Gemstone(GemstoneType.MossAgate, GemstoneTier.Tier1) },
        {GemstoneType.Obsidian, new Gemstone(GemstoneType.Obsidian, GemstoneTier.Tier1) },
        {GemstoneType.Onyx, new Gemstone(GemstoneType.Onyx, GemstoneTier.Tier2) },
        {GemstoneType.Opal, new Gemstone(GemstoneType.Opal, GemstoneTier.Tier5) },
        {GemstoneType.Pearl, new Gemstone(GemstoneType.Pearl, GemstoneTier.Tier3) },
        {GemstoneType.Peridot, new Gemstone(GemstoneType.Peridot, GemstoneTier.Tier4) },
        {GemstoneType.Quartz, new Gemstone(GemstoneType.Quartz, GemstoneTier.Tier2) },
        {GemstoneType.Rhodochrosite, new Gemstone(GemstoneType.Rhodochrosite, GemstoneTier.Tier1) },
        {GemstoneType.Ruby, new Gemstone(GemstoneType.Ruby, GemstoneTier.Tier6) },
        {GemstoneType.Sardonyx, new Gemstone(GemstoneType.Sardonyx, GemstoneTier.Tier2) },
        {GemstoneType.Spinel, new Gemstone(GemstoneType.Spinel, GemstoneTier.Tier3) },
        {GemstoneType.StarRoseQuartz, new Gemstone(GemstoneType.StarRoseQuartz, GemstoneTier.Tier2) },
        {GemstoneType.StarRuby, new Gemstone(GemstoneType.StarRuby, GemstoneTier.Tier5) },
        {GemstoneType.StarSapphire, new Gemstone(GemstoneType.StarSapphire, GemstoneTier.Tier5) },
        {GemstoneType.TigerEye, new Gemstone(GemstoneType.TigerEye, GemstoneTier.Tier1) },
        {GemstoneType.Topaz, new Gemstone(GemstoneType.Topaz, GemstoneTier.Tier4) },
        {GemstoneType.Tourmaline, new Gemstone(GemstoneType.Tourmaline, GemstoneTier.Tier3) },
        {GemstoneType.Turquoise, new Gemstone(GemstoneType.Turquoise, GemstoneTier.Tier1) },
        {GemstoneType.YellowSapphire, new Gemstone(GemstoneType.YellowSapphire, GemstoneTier.Tier5) },
        {GemstoneType.Zircon, new Gemstone(GemstoneType.Zircon, GemstoneTier.Tier2) }
    };

    private readonly List<GemstoneType> tier1 =
    [
        GemstoneType.Azurite,
        GemstoneType.BandedAgate,
        GemstoneType.BlueQuartz,
        GemstoneType.EyeAgate,
        GemstoneType.Hematite,
        GemstoneType.LapisLazuli,
        GemstoneType.Malachite,
        GemstoneType.MossAgate,
        GemstoneType.Obsidian,
        GemstoneType.Rhodochrosite,
        GemstoneType.TigerEye,
        GemstoneType.Turquoise
    ];

    private readonly List<GemstoneType> tier2 =
    [
        GemstoneType.Bloodstone,
        GemstoneType.Carnelian,
        GemstoneType.Chalcedony,
        GemstoneType.Chrysoprase,
        GemstoneType.Citrine,
        GemstoneType.Jasper,
        GemstoneType.Moonstone,
        GemstoneType.Onyx,
        GemstoneType.Quartz,
        GemstoneType.Sardonyx,
        GemstoneType.StarRoseQuartz,
        GemstoneType.Zircon
    ];

    private readonly List<GemstoneType> tier3 =
    [
        GemstoneType.Amber,
        GemstoneType.Amethyst,
        GemstoneType.Chrysoberyl,
        GemstoneType.Coral,
        GemstoneType.Garnet,
        GemstoneType.Jade,
        GemstoneType.Jet,
        GemstoneType.Pearl,
        GemstoneType.Spinel,
        GemstoneType.Tourmaline
    ];

    private readonly List<GemstoneType> tier4 =
    [
        GemstoneType.Alexandrite,
        GemstoneType.Aquamarine,
        GemstoneType.BlackPearl,
        GemstoneType.BlueSpinel,
        GemstoneType.Peridot,
        GemstoneType.Topaz
    ];

    private readonly List<GemstoneType> tier5 =
    [
        GemstoneType.BlackOpal,
        GemstoneType.BlueSapphire,
        GemstoneType.Emerald,
        GemstoneType.FireOpal,
        GemstoneType.Opal,
        GemstoneType.StarRuby,
        GemstoneType.StarSapphire,
        GemstoneType.YellowSapphire
    ];

    private readonly List<GemstoneType> tier6 =
    [
        GemstoneType.BlackSapphire,
        GemstoneType.Diamond,
        GemstoneType.Jacinth,
        GemstoneType.Ruby
    ];

    private Gemstone GetGemstone(GemstoneType type)
    {
        return gemstones[type];
    }

    public Gemstone Generate(GemstoneTier tier)
    {
        return tier switch
        {
            GemstoneTier.Tier1 => GetGemstone(tier1[diceService.Roll(1, 12) - 1]),
            GemstoneTier.Tier2 => GetGemstone(tier2[diceService.Roll(1, 12) - 1]),
            GemstoneTier.Tier3 => GetGemstone(tier3[diceService.Roll(1, 10) - 1]),
            GemstoneTier.Tier4 => GetGemstone(tier4[diceService.Roll(1, 6) - 1]),
            GemstoneTier.Tier5 => GetGemstone(tier5[diceService.Roll(1, 8) - 1]),
            GemstoneTier.Tier6 => GetGemstone(tier6[diceService.Roll(1, 4) - 1]),
            _ => throw new NotImplementedException()
        };
    }

    public Gemstone Create(GemstoneType type)
    {
        return GetGemstone(type);
    }
}