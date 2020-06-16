using PG.City.Model.Context;
using PG.Core.Installer;

namespace PG.City.Context.Bootstrap
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
                    () => { BootstrapModel.LoadingProgress.Value = BootstrapModel.ELoadingProgress.GamePlay; }
                );
                
                View.Hide();
            }
        }
    }
}