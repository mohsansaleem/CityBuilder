using PG.CityBuilder.Model.Context;

namespace PG.CityBuilder.Context.Bootstrap
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
                        BootstrapModel.LoadingProgress.Value = Model.Context.BootstrapModel.ELoadingProgress.GamePlay;
                    }
                ).Catch(e =>
                {
                    BootstrapModel.LoadingProgress.Value = Model.Context.BootstrapModel.ELoadingProgress.CreateUserData;
                });
            }
        }
    }
}