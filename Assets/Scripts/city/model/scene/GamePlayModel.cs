using game.city.model.remote;
using UniRx;

namespace game.city.model.scene
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

