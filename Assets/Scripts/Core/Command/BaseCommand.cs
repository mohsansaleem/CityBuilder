using Zenject;

namespace PG.Core.Command
{
    public class BaseCommand
    {
        [Inject]
        protected SignalBus SignalBus;
    }
}