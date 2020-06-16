using PG.City.Model.data;
using PG.City.Model.remote;
using PG.City.Model.scene;
using UnityEngine;

namespace PG.City.Context.Bootstrap
{
    public partial class BootstrapMediator
    {
        public class BootstrapStateCreateUserData : BootstrapState
        {
            private readonly RemoteDataModel _remoteDataModel;

            public BootstrapStateCreateUserData(Bootstrap.BootstrapMediator mediator) : base(mediator)
            {
                _remoteDataModel = mediator._remoteDataModel;
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();

                UserData userData = GameSettings.DefaultGameState.User;

                CreateUserDataSignal.CreateUserData(SignalBus, userData).Then(
                    () => {
                        _remoteDataModel.SeedUserData(userData);
                        BootstrapModel.LoadingProgress.Value = BootstrapModel.ELoadingProgress.DataSeeded;
                    }
                ).Catch(e =>
                {
                    Debug.LogError("Exception Creating new User: " + e);
                });
            }
        }
    }
}