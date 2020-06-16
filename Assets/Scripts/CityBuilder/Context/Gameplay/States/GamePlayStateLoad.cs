using PG.CityBuilder.Model.Context;
using RSG;
using System.Linq;

namespace PG.CityBuilder.Context.Gameplay
{
    public partial class GamePlayMediator
    {
        public class GamePlayStateLoad : GamePlayState
        {
            public GamePlayStateLoad(Gameplay.GamePlayMediator mediator) : base(mediator)
            {
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();

                // TODO: MS: Reset next State to Regular. For now changing to Build.
                if (RemoteDataModel.ModuleRemoteDatas != null)
                {
                    // Spawning Modules
                    SpawnModules().Done(((v) =>
                    {
                        Mediator._gamePlayModel.GamePlayState.SetValueAndForceNotify(Model.Context.GamePlayModel.EGamePlayState.Regular);
                    }));
                }
                else
                {
                    Mediator._gamePlayModel.GamePlayState.SetValueAndForceNotify(Model.Context.GamePlayModel.EGamePlayState.Regular);
                }
            }

            private IPromise<ModuleView> SpawnModules()
            {
                Promise<ModuleView> promise = null;

                foreach (var moduleRemoteData in RemoteDataModel.ModuleRemoteDatas)
                {
                    if (promise == null)
                    {
                        promise = (Promise<ModuleView>) Mediator.SpawnModuleAndAdd(moduleRemoteData.Value);
                    }
                    else
                    {
                        promise = (Promise<ModuleView>)promise.Then((v) => Mediator.SpawnModuleAndAdd(moduleRemoteData.Value));
                    }
                }

                if (promise == null)
                {
                    promise = new Promise<ModuleView>();
                    promise.Resolve(null);
                }
                
                return promise;
            }

            
        }
    }
}