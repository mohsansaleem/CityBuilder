using PG.Core.Command;
using Zenject;

namespace PG.Core.Installer
{
    public class CoreContextInstaller : MonoInstaller<CoreContextInstaller>
    {
        public override void InstallBindings()
        { 
            // Installing Signal Bus Once and for all.
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<LoadSceneSignal>().RunAsync();
            Container.BindSignal<LoadSceneSignal>()
                .ToMethod<LoadSceneCommand>(command => command.Execute)
                .FromNew();

            Container.DeclareSignal<LoadUnloadScenesSignal>().RunAsync();
            Container.BindSignal<LoadUnloadScenesSignal>()
                .ToMethod<LoadUnloadScenesCommand>(command => command.Execute)
                .FromNew();

            Container.DeclareSignal<UnloadSceneSignal>().RunAsync();
            Container.BindSignal<UnloadSceneSignal>()
                .ToMethod<UnloadSceneCommand>(command => command.Execute)
                .FromNew();

            Container.DeclareSignal<UnloadAllScenesExceptSignal>().RunAsync();
            Container.BindSignal<UnloadAllScenesExceptSignal>()
                .ToMethod<UnloadAllScenesExceptCommand>(command => command.Execute)
                .FromNew();

            Container.DeclareSignal<RequestStateChangeSignal>().RunAsync();
            
            Container.BindInterfacesTo<AsyncSceneLoader>().AsTransient();
        }
    }
}