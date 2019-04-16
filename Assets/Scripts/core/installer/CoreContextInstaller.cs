using game.core.command;
using Zenject;

namespace game.core.installer
{
    public class CoreContextInstaller : MonoInstaller<CoreContextInstaller>
    {
        public override void InstallBindings()
        { 
            // Installing Signal Bus Once and for all.
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<LoadSceneSignal>().RunAsync();
            Container.BindSignal<LoadSceneSignal>()
                .ToMethod<LoadSceneCommand>((x, loadParams) => x.Execute(loadParams))
                .FromNew();

            Container.DeclareSignal<LoadUnloadScenesSignal>().RunAsync();
            Container.BindSignal<LoadUnloadScenesSignal>()
                .ToMethod<LoadUnloadScenesCommand>((x, loadParams) => x.Execute(loadParams))
                .FromNew();

            Container.DeclareSignal<UnloadSceneSignal>().RunAsync();
            Container.BindSignal<UnloadSceneSignal>()
                .ToMethod<UnloadSceneCommand>((x, loadParams) => x.Execute(loadParams))
                .FromNew();

            Container.DeclareSignal<UnloadAllScenesExceptSignal>().RunAsync();
            Container.BindSignal<UnloadAllScenesExceptSignal>()
                .ToMethod<UnloadAllScenesExceptCommand>((x, loadParams) => x.Execute(loadParams))
                .FromNew();

            Container.DeclareSignal<RequestStateChangeSignal>().RunAsync();
            
            Container.BindInterfacesTo<AsyncSceneLoader>().AsTransient();
        }
    }
}