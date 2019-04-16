using game.core.installer;
using Zenject;

namespace game.core.command
{
    public class UnloadSceneCommand : BaseCommand
    {
        [Inject] private readonly ISceneLoader _sceneLoader;

        public void Execute(UnloadSceneSignal loadParams)
        {
            _sceneLoader.UnloadScene (loadParams.Scene).Done (
                () =>
                {
                    if (loadParams.OnComplete != null)
                    {
                        loadParams.OnComplete.Resolve();
                    }
                },
                exception =>
                {
                    if (loadParams.OnComplete != null)
                    {
                        loadParams.OnComplete.Reject(exception);
                    }
                }
            );
        }
    }
}
