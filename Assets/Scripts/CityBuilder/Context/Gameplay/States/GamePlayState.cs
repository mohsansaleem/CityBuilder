using System.Collections.Generic;
using PG.CityBuilder.Installer;
using PG.CityBuilder.Model.Context;
using PG.CityBuilder.Model.Remote;
using PG.CityBuilder.Views.Gameplay;
using PG.Core.FSM;

namespace PG.CityBuilder.Context.Gameplay
{
    public partial class GamePlayMediator
    {
        public class GamePlayState : StateBehaviour
        {
            protected readonly Gameplay.GamePlayMediator Mediator;
            
            protected readonly GamePlayModel GamePlayModel;
            protected readonly GamePlayView View;

            protected readonly ProjectContextInstaller.Settings ProjectSettings;

            protected readonly RemoteDataModel RemoteDataModel;
            protected readonly Dictionary<long, ModuleView> Modules;
            
            public GamePlayState(Gameplay.GamePlayMediator mediator)
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
