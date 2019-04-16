using game.city.installer;
using game.city.model.scene;
using game.core;
using Zenject;

namespace game.city.view
{
    public partial class BootstrapMediator
    {
        public class BootstrapState : StateBehaviour
        {
            protected readonly BootstrapMediator Mediator;
            protected readonly BootstrapModel BootstrapModel;
            protected readonly BootstrapView View;
            protected readonly SignalBus SignalBus;

            protected readonly ProjectContextInstaller.Settings GameSettings;

            public BootstrapState(BootstrapMediator mediator)
            {
                this.Mediator = mediator;
                this.BootstrapModel = mediator._bootstrapModel;
                this.View = mediator._view;
                this.SignalBus = mediator.SignalBus;

                this.GameSettings = mediator._gameSettings;
            }
        }
    }
}
