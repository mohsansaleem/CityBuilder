using PG.CityBuilder.Model.Context;

namespace PG.CityBuilder.Context.Bootstrap
{
    public partial class BootstrapMediator
    {
        public class BootstrapStateLoadStaticData : BootstrapState
        {
            public BootstrapStateLoadStaticData(Bootstrap.BootstrapMediator mediator) : base(mediator)
            {
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();

                View.Show();
                
                LoadStaticDataSignal.LoadStaticData(SignalBus).Then(
                    () =>
                    {
                        BootstrapModel.LoadingProgress.Value = Model.Context.BootstrapModel.ELoadingProgress.LoadUserData;
                    }
                ).Catch(e =>
                {
                    BootstrapModel.LoadingProgress.Value = Model.Context.BootstrapModel.ELoadingProgress.CreateMetaData;
                });
            }
        }
    }
}