using PG.CityBuilder.Installer;
using PG.Core.Installer;
using UnityEngine;

namespace PG.CityBuilder.Context.Bootstrap
{
    public partial class BootstrapMediator
    {
        public class BootstrapStateGamePlay : BootstrapState
        {
            public BootstrapStateGamePlay(Bootstrap.BootstrapMediator mediator):base(mediator)
            {
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();
                
                LoadUnloadScenesSignal.Load(SignalBus, new[] { Scenes.GamePlay }).Done
                (
                    () =>
                    {
                        Debug.LogError("Going to Fire Event");
                        BootstrapModel.LoadingProgress.Value = Model.Context.BootstrapModel.ELoadingProgress.Running;
                    }
                );
                
                View.Hide();
            }
        }
    }
}