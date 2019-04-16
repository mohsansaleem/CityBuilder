using game.city.installer;
using game.city.model.remote;
using UnityEngine;

namespace game.city.view
{
    public partial class GamePlayMediator
    {
        public class GamePlayStateBuild : GamePlayState
        {
            // Raycast stuff to handle the Grid Position & Movement.
            private int layer_mask = LayerMask.GetMask("Grid");
            private Vector3 _startMousePosition;
            private Vector3 _tmpVector;
            private Vector3 _originalPosition;
            
            public GamePlayStateBuild(GamePlayMediator mediator) : base(mediator)
            {
                
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();
             
                Mediator._onMouseDownAtModule += OnMouseDownAtModule;
                Mediator._onMouseUpFromModule += OnMouseUpFromModule;
                Mediator._onDragModule += OnDragModule;                
            }

            public override void OnStateExit()
            {
                base.OnStateExit();
                
                Mediator._onMouseDownAtModule -= OnMouseDownAtModule;
                Mediator._onMouseUpFromModule -= OnMouseUpFromModule;
                Mediator._onDragModule -= OnDragModule;    
            }

            // Module events
            private void OnMouseDownAtModule(EntityRemoteDataModel model)
            {
                if (GetGridPosition(ref _tmpVector))
                {
                    _startMousePosition = _tmpVector;
                    
                    // TODO: MS: Use Selected Module.
                    var view = Mediator.GetModule((ModuleRemoteDataModel)model);
                    _originalPosition = view.transform.position;
                }
            }

            private void OnDragModule(EntityRemoteDataModel model)
            {
                if (GetGridPosition(ref _tmpVector))
                {
                    var res = _tmpVector - _startMousePosition;
                    res += _originalPosition;
                    
                    // TODO: MS: Use Selected Module.
                    var view = Mediator.GetModule((ModuleRemoteDataModel)model);
                    var pos = new Vector3(Mathf.Round(res.x / 10) * 10,
                                          0,
                                          Mathf.Round(res.z / 10) * 10);
                    
                    if (view != null && 
                        !view.transform.position.Equals(pos) && 
                        IsValidPosition((ModuleRemoteDataModel)model, pos))
                    {
                        UpdatePosition(view, (ModuleRemoteDataModel)model, pos);
                    }
                }
            }

            private void OnMouseUpFromModule(EntityRemoteDataModel model)
            {
                if (GetGridPosition(ref _tmpVector))
                {
                    var res = _tmpVector - _startMousePosition;
                    res += _originalPosition;

                    var view = Mediator.GetModule((ModuleRemoteDataModel) model);
                    var pos = new Vector3(Mathf.Round(res.x / 10) * 10,
                        0,
                        Mathf.Round(res.z / 10) * 10);
                    
                    UpdateModulePositionSignal.UpdateModulePosition(Mediator.SignalBus, (ModuleRemoteDataModel)model, pos)
                        .Catch(e => Debug.LogError("Unable to Update Position"))
                        .Done((() => view.transform.position = pos));
                }
            }
        

            private bool GetGridPosition(ref Vector3 ret)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                
                if (Physics.Raycast (ray, out hit, 1000f, layer_mask))
                {
                    ret = hit.point;
                    return true;
                }

                return false;
            }

            private bool IsValidPosition(ModuleRemoteDataModel model, Vector3 pos)
            {
                var grid = RemoteDataModel.UserData.Grid;
                
                int oldi = (int)model.RemoteData.CurrentPosition.x/10;
                int oldy = -(int)model.RemoteData.CurrentPosition.z/10;
                
                int posi = (int)pos.x / 10;
                int posy = -(int) pos.z / 10;

                if (posi < 0 || posi + model.Data.Width > Constants.GridWidth ||
                    posy < 0 || posy + model.Data.Length > Constants.GridLength)
                {
                    return false;
                }
                
                for (int i = posi; i < posi + model.Data.Width; i++)
                {
                    for (int r = posy; r < posy + model.Data.Length; r++)
                    {
                        if (grid[i][r] && 
                            !((i >= oldi && r >= oldy) && 
                            (i < oldi + model.Data.Width && r < oldy + model.Data.Length))
                            )
                            return false;
                    }
                }
                
                return true;
            }

            
            private void UpdatePosition(ModuleView view, ModuleRemoteDataModel model, Vector3 pos)
            {
                var grid = RemoteDataModel.UserData.Grid;
                var old = view.transform.position;
                int posi = (int)old.x / 10;
                int posy = -(int)old.z / 10;

                for (int i = posi; i < posi + model.Data.Width; i++)
                {
                    for (int r = posy; r < posy + model.Data.Length; r++)
                    {
                        grid[i][r] = false;
                    }
                }
                
                posi = (int)pos.x / 10;
                posy = -(int)pos.z / 10;
                
                for (int i = posi; i < posi + model.Data.Width; i++)
                {
                    for (int r = posy; r < posy + model.Data.Length; r++)
                    {
                        grid[i][r] = true;
                    }
                }
                
                // TODO: MS: Use Reactive Property and Set from Command.
                model.RemoteData.CurrentPosition = pos;
                model.CurrentPosition.Value = pos;
                
                // Set on Reactive Change.
                //view.transform.position = pos;
            }
        }
    }
}