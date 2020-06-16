using PG.City.Model.scene;
using PG.Core.thirdparty.RSG.Promise.v1._3._0._0;
using Sirenix.Utilities;

namespace PG.City.Context.Gameplay
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
                        Mediator._gamePlayModel.GamePlayState.SetValueAndForceNotify(GamePlayModel.EGamePlayState.Regular);
                    }));
                }
                else
                {
                    Mediator._gamePlayModel.GamePlayState.SetValueAndForceNotify(GamePlayModel.EGamePlayState.Regular);
                }
            }

            private IPromise<ModuleView> SpawnModules()
            {
                Promise<ModuleView> promise = null;

                RemoteDataModel.ModuleRemoteDatas.ForEach((animalData) =>
                {
                    if (promise == null)
                    {
                        promise = (Promise<ModuleView>) Mediator.SpawnModuleAndAdd(animalData.Value);
                    }
                    else
                    {
                        promise = (Promise<ModuleView>)promise.Then((v) => Mediator.SpawnModuleAndAdd(animalData.Value));
                    }
                });

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