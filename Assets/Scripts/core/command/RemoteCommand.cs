using game.service;
using Zenject;

namespace game.core.command
{
    public class RemoteCommand : BaseCommand
    {
        [Inject] protected readonly IRemoteDataService Service;
    }
}