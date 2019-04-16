using System.Collections.Generic;
using game.city.installer;
using game.city.model.remote;
using game.city.model.scene;
using game.core;

namespace game.city.view
{
    public partial class GamePlayMediator
    {
        public class GamePlayState : StateBehaviour
        {
            protected readonly GamePlayMediator Mediator;
            
            protected readonly GamePlayModel GamePlayModel;
            protected readonly GamePlayView View;

            protected readonly ProjectContextInstaller.Settings ProjectSettings;

            protected readonly RemoteDataModel RemoteDataModel;
            protected readonly Dictionary<long, ModuleView> Modules;
            
            public GamePlayState(GamePlayMediator mediator)
            {
                this.Mediator = mediator;
                
                this.GamePlayModel = mediator._gamePlayModel;
                this.View = mediator._view;

                this.ProjectSettings = mediator._projectSettings;

                this.RemoteDataModel = mediator._remoteDataModel;
                this.Modules = mediator._buildingViews;
            }
        }
    }
}
