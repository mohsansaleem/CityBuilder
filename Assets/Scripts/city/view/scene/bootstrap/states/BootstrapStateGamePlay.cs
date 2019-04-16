using game.city.model.scene;
using game.city.view.scene;
using game.core.installer;

namespace game.city.view
{
    public partial class BootstrapMediator
    {
        public class BootstrapStateGamePlay : BootstrapState
        {
            public BootstrapStateGamePlay(BootstrapMediator mediator):base(mediator)
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