using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootGenerator.Interface;

namespace LootGenerator.Handler;

internal class MenuHandler() : IMenuHandler
{
    public event EventHandler<ConsoleKey> KeyPress;

    public void StartPolling()
    {
        Task.Run(() =>
        {
            while (true)
            {
                KeyPress?.Invoke(this, Console.ReadKey(true).Key);
            }
        });
    }
}