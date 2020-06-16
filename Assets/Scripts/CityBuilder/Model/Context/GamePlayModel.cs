using PG.City.Model.Remote;
using UniRx;

namespace PG.City.Model.Context
{
    public class GamePlayModel
    {
        public enum EGamePlayState
        {
            Load = 0,
            Regular,
            Build,
            Pause
        }

        public readonly ReactiveProperty<EGamePlayState> GamePlayState;
        public readonly ReactiveProperty<ModuleRemoteDataModel> SelectedModule;

        public GamePlayModel()
        {
            GamePlayState = new ReactiveProperty<EGamePlayState>(EGamePlayState.Load);
            SelectedModule = new ReactiveProperty<ModuleRemoteDataModel>(null);
        }
    }
}

