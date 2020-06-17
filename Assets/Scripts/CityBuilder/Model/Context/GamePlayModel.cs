using PG.CityBuilder.Model.Remote;
using UniRx;

namespace PG.CityBuilder.Model.Context
{
    public class GamePlayModel
    {
        public readonly ReactiveProperty<EGamePlayState> GamePlayState;
        public readonly ReactiveProperty<ModuleRemoteDataModel> SelectedModule;

        public GamePlayModel()
        {
            GamePlayState = new ReactiveProperty<EGamePlayState>(EGamePlayState.Load);
            SelectedModule = new ReactiveProperty<ModuleRemoteDataModel>(null);
        }
    }
    
    public enum EGamePlayState
    {
        Load = 0,
        Regular,
        Build,
        Pause
    }
}

