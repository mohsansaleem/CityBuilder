using game.city.installer;
using game.city.model.scene;

namespace game.city.view
{
    public partial class BootstrapMediator
    {
        public class BootstrapStateLoadStaticData : BootstrapState
        {
            public BootstrapStateLoadStaticData(BootstrapMediator mediator) : base(mediator)
            {
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();

                View.Show();
                
                LoadStaticDataSignal.LoadStaticData(SignalBus).Then(
                    () =>
                    {
                        BootstrapModel.LoadingProgress.Value = model.scene.BootstrapModel.ELoadingProgress.StaticDataLoaded;
                    }
                ).Catch(e =>
                {
                    BootstrapModel.LoadingProgress.Value = BootstrapModel.ELoadingProgress.MetaNotFound;
                });
            }
        }
    }
}