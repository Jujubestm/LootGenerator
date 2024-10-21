﻿using LootGenerator.Interface;
using LootGenerator.Model.Creature;
using LootGenerator.Model.Loot;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootGenerator;

internal class GraphicUserInterface(IHostApplicationLifetime hostApplicationLifetime, ILootHandler lootHandler, IMenuHandler menuHandler) : BackgroundService, IGraphicUserInterface
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime = hostApplicationLifetime;
    private readonly ILootHandler _lootHandler = lootHandler;
    private readonly IMenuHandler _menuHandler = menuHandler;
    private ConsoleKey _key;
    private MenuStates _menuStates;

    private enum Menu
    {
        MainMenu,
        GenerateLoot,
        GenerateGold,
        GenerateGemstone
    }

    private Menu currentMenu = Menu.MainMenu;
    private readonly List<string> mainMenu = ["Main Menu:", "Generate Loot", "Exit"];
    private readonly List<string> lootMenu = ["Loot Menu:", "Generate Gold", "Generate Gemstone", "Back"];
    private readonly List<string> goldMenu = ["Choose CR:", "0 - 4", "5 - 10", "11 - 16", "17+"];
    private readonly List<string> gemstoneMenu = ["Choose Tier:", "1", "2", "3", "4", "5", "6"];
    private bool toggle = false;
    private bool lastKey = false;
    private const int maxWidth = 114;
    private const int maxHeight = 26;
    private const int minWidth = 4;
    private const int minHeight = 3;
    private const int maxBillboardLength = 42;
    private const int maxBillboardHeight = 25;
    private const int maxInfoWidth = maxWidth - 41 - infX - 1;
    private const int billboardX = maxWidth - 39;
    private const int billboardY = minHeight;
    private const int curX = minWidth + 1;
    private const int infX = curX + 4;
    private const string cur = ">>> ";
    private const string newLine = "\n\t\t";
    private int curY = minHeight;
    private int curIndex = 0;
    private int curFrame = maxBillboardHeight;
    private int billboardIndex = 0;
    private readonly string[] billboard = new string[maxBillboardHeight];

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        LoadTemplate();
        _menuStates = new MenuStates(this);
        _lootHandler.NewLoot += (sender, loot) => ToBillboard(loot);
        _menuHandler.KeyPress += (sender, key) =>
        {
            _key = key;
            toggle = !toggle;
        };
        _menuHandler.StartPolling();
        while (!stoppingToken.IsCancellationRequested)
        {
            if (toggle == lastKey)
            {
                Task.Delay(100, stoppingToken).Wait(stoppingToken);
            }
            else
            {
                lastKey = toggle;
                switch (currentMenu)
                {
                    case Menu.MainMenu:
                        _menuStates.MainMenu(_key);
                        break;

                    case Menu.GenerateLoot:
                        _menuStates.GenerateLoot(_key);
                        break;

                    case Menu.GenerateGold:
                        _menuStates.GenerateGold(_key);
                        break;

                    case Menu.GenerateGemstone:
                        _menuStates.GenerateGemstone(_key);
                        break;
                }
            }
        }
        return Task.CompletedTask;
    }

    private void LoadTemplate()
    {
        Console.Title = "Loot Generator";
        Console.SetWindowSize(120, 30);
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, 0);
        for (var i = 0; i < 29; i++)
        {
            Console.CursorLeft = 2;
            Console.WriteLine("|");
        }

        Console.SetCursorPosition(3, 0);
        for (var i = 0; i < 116; i++)
        {
            Console.Write("-");
        }

        for (var i = 0; i < 29; i++)
        {
            Console.CursorLeft = 119;
            Console.WriteLine("|");
        }

        Console.CursorLeft = 3;
        for (var i = 0; i < 116; i++)
        {
            Console.Write("-");
        }

        Console.SetCursorPosition(55, 1);
        Console.Write("Loot Generator");
        Console.SetCursorPosition(50, 28);
        Console.Write("Created by: Jujubestm");
        Console.SetCursorPosition(maxWidth - 40, minWidth - 2);
        for (var i = 0; i < maxHeight + 2; i++)
        {
            Console.CursorLeft = maxWidth - 41;
            Console.Write("|");
            Console.CursorTop = minWidth - 2 + i;
        }

        EraseInfo();
        BillboardClear();
        MenuBuilder(mainMenu);
    }

    private void BillboardClear()
    {
        billboardIndex = 0;
        for (var i = 0; i < maxBillboardHeight; i++)
        {
            billboard[i] = new string(' ', maxBillboardLength);
        }
    }

    private void ToBillboard(string input)
    {
        if (input.Length > maxBillboardLength)
        {
            input = input[..maxBillboardLength];
        }
        billboardIndex %= maxBillboardHeight;
        Console.SetCursorPosition(billboardX, billboardY + billboardIndex);
        EraseBillboardLine();
        billboardIndex++;
        Console.Write(input);
        NewBillboardDot();
    }

    private void SetCursorPos(int yPos, int offest = 1)
    {
        if (yPos <= minHeight)
        {
            yPos = minHeight + offest;
            curIndex++;
        }
        else if (yPos > curFrame)
        {
            yPos = curFrame;
            curIndex--;
        }
        Console.SetCursorPosition(curX, curY);
        Console.Write(new string(' ', cur.Length));
        curY = yPos;
        Console.SetCursorPosition(curX, curY);
        Console.Write(cur);
    }

    private void EraseBillboardLine()
    {
        Console.Write(new string(' ', maxBillboardLength));
        Console.SetCursorPosition(billboardX, billboardY + billboardIndex);
    }

    private void NewBillboardDot()
    {
        if (billboardIndex != 1)
        {
            Console.SetCursorPosition(billboardX + maxBillboardLength + 1, billboardY + billboardIndex - 2);
        }
        else
        {
            Console.SetCursorPosition(billboardX + maxBillboardLength + 1, maxBillboardHeight + 2);
        }

        Console.Write(" ");
        Console.SetCursorPosition(billboardX + maxBillboardLength + 1, billboardY + billboardIndex - 1);
        Console.Write("*");
    }

    private static void EraseInfo()
    {
        Console.SetCursorPosition(infX, minHeight);
        for (var i = 0; i < maxBillboardHeight; i++)
        {
            Console.Write(new string(' ', maxInfoWidth));
            Console.CursorTop++;
            Console.CursorLeft = infX;
        }
    }

    private void MenuBuilder(List<string> menuOptions, int offset = 1)
    {
        EraseInfo();
        SetCursorPos(minHeight + offset);
        for (var i = 0; i < menuOptions.Count; i++)
        {
            Console.SetCursorPosition(infX, minHeight + i);
            Console.Write(menuOptions[i]);
        }

        curFrame = minHeight + menuOptions.Count - 1;
        curIndex = 0;
    }

    internal void ArrowUp()
    {
        curIndex--;
        SetCursorPos(curY - 1);
    }

    internal void ArrowDown()
    {
        curIndex++;
        SetCursorPos(curY + 1);
    }

    private sealed class MenuStates(GraphicUserInterface gui)
    {
        private readonly GraphicUserInterface _gui = gui;

        public void MainMenu(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    _gui.ArrowUp();
                    break;

                case ConsoleKey.DownArrow:
                    _gui.ArrowDown();
                    break;

                case ConsoleKey.Enter:
                    switch (_gui.curIndex)
                    {
                        case 0:
                            _gui.currentMenu = Menu.GenerateLoot;
                            _gui.MenuBuilder(_gui.lootMenu);
                            break;

                        case 1:
                            _gui._hostApplicationLifetime.StopApplication();
                            break;
                    }
                    break;
            }
        }

        public void GenerateLoot(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Backspace:
                    _gui.currentMenu = Menu.MainMenu;
                    _gui.MenuBuilder(_gui.mainMenu);
                    break;

                case ConsoleKey.UpArrow:
                    _gui.ArrowUp();
                    break;

                case ConsoleKey.DownArrow:
                    _gui.ArrowDown();
                    break;

                case ConsoleKey.Enter:
                    switch (_gui.curIndex)
                    {
                        case 0:
                            _gui.currentMenu = Menu.GenerateGold;
                            _gui.MenuBuilder(_gui.goldMenu);
                            break;

                        case 1:
                            _gui.currentMenu = Menu.GenerateGemstone;
                            _gui.MenuBuilder(_gui.gemstoneMenu);
                            break;

                        case 2:
                            _gui.currentMenu = Menu.MainMenu;
                            _gui.MenuBuilder(_gui.mainMenu);
                            break;
                    }
                    break;
            }
        }

        public void GenerateGold(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Backspace:
                    _gui.currentMenu = Menu.GenerateLoot;
                    _gui.MenuBuilder(_gui.lootMenu);
                    break;

                case ConsoleKey.UpArrow:
                    _gui.ArrowUp();
                    break;

                case ConsoleKey.DownArrow:
                    _gui.ArrowDown();
                    break;

                case ConsoleKey.Enter:
                    switch (_gui.curIndex)
                    {
                        case 0:
                            _gui._lootHandler.GenerateGold(ChallengeRating.Zero);
                            break;

                        case 1:
                            _gui._lootHandler.GenerateGold(ChallengeRating.Five);
                            break;

                        case 2:
                            _gui._lootHandler.GenerateGold(ChallengeRating.Eleven);
                            break;

                        case 3:
                            _gui._lootHandler.GenerateGold(ChallengeRating.Seventeen);
                            break;
                    }
                    break;
            }
        }

        public void GenerateGemstone(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Backspace:
                    _gui.currentMenu = Menu.GenerateLoot;
                    _gui.MenuBuilder(_gui.lootMenu);
                    break;

                case ConsoleKey.UpArrow:
                    _gui.ArrowUp();
                    break;

                case ConsoleKey.DownArrow:
                    _gui.ArrowDown();
                    break;

                case ConsoleKey.Enter:
                    switch (_gui.curIndex)
                    {
                        case 0:
                            _gui._lootHandler.GenerateGemstone(GemstoneTier.Tier1);
                            break;

                        case 1:
                            _gui._lootHandler.GenerateGemstone(GemstoneTier.Tier2);
                            break;

                        case 2:
                            _gui._lootHandler.GenerateGemstone(GemstoneTier.Tier3);
                            break;

                        case 3:
                            _gui._lootHandler.GenerateGemstone(GemstoneTier.Tier4);
                            break;

                        case 4:
                            _gui._lootHandler.GenerateGemstone(GemstoneTier.Tier5);
                            break;

                        case 5:
                            _gui._lootHandler.GenerateGemstone(GemstoneTier.Tier6);
                            break;
                    }
                    break;
            }
        }
    }
}