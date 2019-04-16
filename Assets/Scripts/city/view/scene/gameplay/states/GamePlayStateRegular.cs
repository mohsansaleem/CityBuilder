using game.city.model.remote;
using UniRx;

namespace game.city.view
{
    public partial class GamePlayMediator
    {
        public class GamePlayStateRegular : GamePlayState
        {
            public GamePlayStateRegular(GamePlayMediator mediator) : base(mediator)
            {
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();
                
                Mediator._onMouseDownAtModule += OnMouseDownAtModule;
                View.GridView.OnMouseDownAtGrid += OnMouseDownAtGrid;

                View.ProduceButton.onClick.AddListener((() =>
                {
                    if (GamePlayModel.SelectedModule.Value != null)
                        GamePlayModel.SelectedModule.Value.Produce();
                }));
                
                GamePlayModel.SelectedModule.Subscribe(OnModuleSelected).AddTo(Disposables);
            }

            public void OnModuleSelected(ModuleRemoteDataModel model)
            {
                View.BuildingInfoPanel.SetActive(model != null);
                
                if (model != null)
                {
                    View.BuildingName.text = model.Data.moduleType.ToString();
                    
                    if (model.IsFull.Value)
                    {
                        // TODO: Do this through signal.
                        model.Collect();
                    }
                }
            }

            public override void Tick()
            {
                base.Tick();

                // TODO: Use Reactive Approach. For now going with this.
                if (GamePlayModel.SelectedModule.Value != null)
                {
                    View.ProduceButton.gameObject.SetActive(GamePlayModel.SelectedModule.Value.CanProduce);
                }
            }

            public override void OnStateExit()
            {
                GamePlayModel.SelectedModule.Value = null;
                
                base.OnStateExit();
                
                Mediator._onMouseDownAtModule -= OnMouseDownAtModule;
                View.GridView.OnMouseDownAtGrid -= OnMouseDownAtGrid;
            }

            private void OnMouseDownAtModule(EntityRemoteDataModel obj)
            {
                GamePlayModel.SelectedModule.SetValueAndForceNotify(obj as ModuleRemoteDataModel);
            }
            
            private void OnMouseDownAtGrid()
            {
                GamePlayModel.SelectedModule.Value = null;
            }
        }
    }
}