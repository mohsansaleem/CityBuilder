using System;
using Zenject;

namespace PG.Core.command
{
    public class BaseCommand
    {
        [Inject]
        protected SignalBus SignalBus;
    }
}