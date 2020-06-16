using PG.Service;
using Zenject;

namespace PG.Core.Command
{
    public class RemoteCommand : BaseCommand
    {
        [Inject] protected readonly IRemoteDataService Service;
    }
}