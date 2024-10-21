using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootGenerator.Interface;

internal interface IMenuHandler
{
    public event EventHandler<ConsoleKey> KeyPress;

    public void StartPolling();
}