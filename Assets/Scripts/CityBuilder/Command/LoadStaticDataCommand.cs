using System;
using PG.CityBuilder.Context.Bootstrap;
using PG.CityBuilder.Model;
using UnityEngine;
using Zenject;
using PG.Core.Command;
using RSG;

namespace PG.CityBuilder.Command
{
    public class LoadStaticDataCommand : RemoteCommand
    {
        [Inject] private readonly StaticDataModel _staticDataModel;

        public void Execute(LoadStaticDataSignal loadParam)
        {
            var sequence = Promise.Sequence(
                () => LoadMetaJson()
                // Add other Jsons or asset bundles etc.
            );

            sequence
                .Then(() =>
                    {
                        Debug.Log(string.Format("{0} , static data load completed!", this));
                        loadParam.DataLoadPromise.Resolve();
                    }
                )
                .Catch(loadParam.DataLoadPromise.Reject);
        }

        // For now just loading everything from StreamingAssets. Proper way would be loading it from AssetBudles.
        private IPromise LoadMetaJson()
        {
            Promise promiseReturn = new Promise();

            try
            {
                Service.GetMetaData()
                    .Catch(promiseReturn.Reject)
                    .Then(_staticDataModel.SeedMetaData)
                    .Done(metaData => promiseReturn.Resolve());
            }
            catch(Exception ex)
            {
                promiseReturn.Reject(ex);
            }

            return promiseReturn;
        }
    }
}