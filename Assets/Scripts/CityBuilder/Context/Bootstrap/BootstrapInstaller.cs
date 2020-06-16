using PG.CityBuilder.Command;
using UnityEngine;
using Zenject;

namespace PG.CityBuilder.Context.Bootstrap
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField]
        private BootstrapView _view;

        public override void InstallBindings()
        {
            Container.DeclareSignal<LoadStaticDataSignal>().RunAsync();
            Container.BindSignal<LoadStaticDataSignal>()
                .ToMethod<LoadStaticDataCommand>(cmd => cmd.Execute)
                .FromNew();

            Container.DeclareSignal<CreateMetaDataSignal>().RunAsync();
            Container.BindSignal<CreateMetaDataSignal>()
                .ToMethod<CreateMetaDataCommand>(cmd => cmd.Execute)
                .FromNew();
            
            Container.DeclareSignal<LoadUserDataSignal>().RunAsync();
            Container.BindSignal<LoadUserDataSignal>()
                .ToMethod<LoadUserDataCommand>(cmd => cmd.Execute)
                .FromNew();

            Container.DeclareSignal<CreateUserDataSignal>().RunAsync();
            Container.BindSignal<CreateUserDataSignal>()
                .ToMethod<CreateUserDataCommand>(cmd => cmd.Execute)
                .FromNew();

            Container.BindInstances(_view);
            Container.BindInterfacesTo<BootstrapMediator>().AsSingle();
        }
    }
}
