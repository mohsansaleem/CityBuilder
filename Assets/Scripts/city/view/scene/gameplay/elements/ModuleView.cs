using game.city.model.remote;
using pg.core.assets;
using RSG;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace game.city.view
{
    public class ModuleView : Movable
    {
        public Slider ProgressBar;
        public Text FullText;

        protected CompositeDisposable Disposables;

        public override void Reinitialize<T>(FactoryObjectParams assetParams, Promise<T> assetReadyPromise)
        {
            Disposables = new CompositeDisposable();

            ModuleViewParams para = assetParams as ModuleViewParams;

            // DO some stuff.
            transform.SetParent(para.parent);

            Model = para.ModuleModel;
            var data = ModuleModel.RemoteData;

            para.ModuleModel.CurrentPosition.Subscribe(pos => transform.position = pos).AddTo(Disposables);
            para.ModuleModel.IsInConstruction.Subscribe(OnProgressStatusChange).AddTo(Disposables);
            para.ModuleModel.IsInProduction.Subscribe(OnProgressStatusChange).AddTo(Disposables);
            para.ModuleModel.ProgressValue.Subscribe(OnProgressChange).AddTo(Disposables);
            para.ModuleModel.IsFull.Subscribe(IsFull).AddTo(Disposables);


            assetReadyPromise.Resolve(this as T);
        }

        public void OnProgressStatusChange(bool status)
        {
            ProgressBar.gameObject.SetActive(ModuleModel.RemoteData.IsInProduction ||
                                             ModuleModel.RemoteData.IsInConstruction);
        }

        private void OnProgressChange(float value)
        {
            ProgressBar.value = value;
        }

        private void IsFull(bool isFull)
        {
            FullText.gameObject.SetActive(isFull);
        }

        public override void OnDespawned()
        {
            base.OnDespawned();

            Disposables.Dispose();
        }

        protected ModuleRemoteDataModel ModuleModel => Model as ModuleRemoteDataModel;
    }

    public class ModuleViewParams : FactoryObjectParams
    {
        public Transform parent;
        public ModuleRemoteDataModel ModuleModel;
    }
}