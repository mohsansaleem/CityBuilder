using PG.Service;
using Zenject;

namespace PG.Core.command
{
    public class RemoteCommand : BaseCommand
    {
        [Inject] protected readonly IRemoteDataService Service;
    }
}