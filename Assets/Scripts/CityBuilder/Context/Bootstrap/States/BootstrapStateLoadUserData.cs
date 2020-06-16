using PG.City.Model.scene;

namespace PG.City.Context.Bootstrap
{
    public partial class BootstrapMediator
    {
        public class BootstrapStateLoadUserData : BootstrapState
        {
            public BootstrapStateLoadUserData(Bootstrap.BootstrapMediator mediator) : base(mediator)
            {
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();
                
                LoadUserDataSignal.LoadUserData(SignalBus).Then(
                    () =>
                    {
                        BootstrapModel.LoadingProgress.Value = BootstrapModel.ELoadingProgress.DataSeeded;
                    }
                ).Catch(e =>
                {
                    BootstrapModel.LoadingProgress.Value = BootstrapModel.ELoadingProgress.UserNotFound;
                });
            }
        }
    }
}