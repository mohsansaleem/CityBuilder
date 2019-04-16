using UniRx;

namespace game.city.model.scene
{
    public class BootstrapModel
    {
        public enum ELoadingProgress
        {
            NotLoaded = -1,
            Zero = 0,
            MetaNotFound = 25,
            StaticDataLoaded = 30,
            UserNotFound = 50,
            DataSeeded = 70,
            GamePlay = 110
        }
        
        public ReactiveProperty<ELoadingProgress> LoadingProgress;

        public BootstrapModel()
        {
            LoadingProgress = new ReactiveProperty<ELoadingProgress>(ELoadingProgress.Zero);
        }
    }
    
    
}

