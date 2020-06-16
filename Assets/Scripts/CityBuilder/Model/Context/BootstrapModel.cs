using UniRx;

namespace PG.CityBuilder.Model.Context
{
    public class BootstrapModel
    {
        public enum ELoadingProgress
        {
            NotLoaded = -1,
            LoadStaticData = 0,
            CreateMetaData = 25,
            LoadUserData = 30,
            CreateUserData = 50,
            GamePlay = 70,
            Running = 110
        }
        
        public ReactiveProperty<ELoadingProgress> LoadingProgress;

        public BootstrapModel()
        {
            LoadingProgress = new ReactiveProperty<ELoadingProgress>(ELoadingProgress.LoadStaticData);
        }
    }
    
    
}

