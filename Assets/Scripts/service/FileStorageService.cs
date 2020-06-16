using System;
using System.IO;
using Newtonsoft.Json;
using PG.CityBuilder.Context;
using PG.CityBuilder.Model;
using PG.CityBuilder.Model.Data;
using PG.CityBuilder.Model.Remote;
using RSG;
using UnityEngine;
using Zenject;

namespace PG.Service
{
    public class FileStorageService : IRemoteDataService
    {
        // Needed these to have the data to Authorize. 
        // For Server state it will be authorized on Server.
        [Inject] private RemoteDataModel _remoteDataModel;
        [Inject] private StaticDataModel _staticDataModel;
        
        public IPromise CreateMetaData(MetaData metaData)
        {
            Promise promiseReturn = new Promise();

            try
            {
                // TODO: MS: Encrypt the Data. For now saving plain to read and change.
                string path = Path.Combine(Application.streamingAssetsPath, Constants.MetaDataFile);

                StreamWriter writer = new StreamWriter(path);
                writer.Write(JsonConvert.SerializeObject(metaData, Formatting.Indented));
                writer.Flush();
                writer.Close();

                promiseReturn.Resolve();
            }
            catch (Exception ex)
            {
                promiseReturn.Reject(ex);
            }

            return promiseReturn;
        }

        public IPromise<MetaData> GetMetaData()
        {
            Promise<MetaData> promiseReturn = new Promise<MetaData>();

            try
            {
                // TODO: MS: Encrypt the Data. For now saving plain to read and change.
                string path = Path.Combine(Application.streamingAssetsPath, Constants.MetaDataFile);

                if (File.Exists(path))
                {
                    StreamReader reader = new StreamReader(path);
                    MetaData metaData = JsonConvert.DeserializeObject<MetaData>(reader.ReadToEnd());
                    reader.Close();

                    promiseReturn.Resolve(metaData);
                }
                else
                {
                    promiseReturn.Reject(new FileNotFoundException());
                }
            }
            catch (Exception ex)
            {
                promiseReturn.Reject(ex);
            }

            return promiseReturn;
        }

        public IPromise SaveUserData(UserData userData)
        {
            Promise promise = new Promise();

            try
            {
                // TODO: MS: Encrypt the Data. For now saving plain to read and change.
                string path = Path.Combine(Application.streamingAssetsPath, Constants.GameStateFile);

                StreamWriter writer = new StreamWriter(path);
                writer.Write(JsonConvert.SerializeObject(userData, Formatting.Indented));
                writer.Flush();
                writer.Close();

                promise.Resolve();
            }
            catch (Exception e)
            {
                promise.Reject(e);
            }

            return promise;
        }

        public IPromise<UserData> GetUserData()
        {
            Promise<UserData> promise = new Promise<UserData>();

            try
            {
                // TODO: MS: Encrypt the Data. For now saving plain to read and change.
                string path = Path.Combine(Application.streamingAssetsPath, Constants.GameStateFile);

                if (File.Exists(path))
                {
                    StreamReader reader = new StreamReader(path);
                    UserData userData = JsonConvert.DeserializeObject<UserData>(reader.ReadToEnd());
                    reader.Close();

                    promise.Resolve(userData);
                }
                else
                {
                    promise.Reject(new FileNotFoundException());
                }
            }
            catch (Exception e)
            {
                promise.Reject(e);
            }

            return promise;
        }

        public IPromise<ModuleRemoteData> AddNewModule(EModuleType moduleType)
        {
            Promise<ModuleRemoteData> promise = new Promise<ModuleRemoteData>();

            try
            {
                Vector3 pos;
                ModuleData moduleData = _staticDataModel.GetModuleData(moduleType);
            
                // We will only use the Data. Remote Data will be updated in commands.
                UserData userData = _remoteDataModel.UserData;
            
                if (moduleData.CostGold > userData.Gold ||
                    moduleData.CostWood > userData.Wood ||
                    moduleData.CostIron > userData.Iron)
                {
                    promise.Reject(new Exception("Not Enough Resources."));
                    return promise;
                }
            
                if (_remoteDataModel.OccupySpaceForModule(moduleData, out pos))
                {
                    ModuleRemoteData moduleRemoteData = new ModuleRemoteData()
                    {
                        Id = DateTime.Now.Ticks,
                        moduleType = moduleType,
                        IsInConstruction = true,
                        IsInProduction = false,
                        IsFull = false,
                        LastCommandTime = DateTime.Now,
                        CurrentPosition = pos
                    };
                
                    userData.ModulesInGrid.Add(moduleRemoteData);
                    
                    // Updating the Data in UserData.
                    userData.Gold -= moduleData.CostGold;
                    userData.Wood -= moduleData.CostWood;
                    userData.Iron -= moduleData.CostIron;
                    
                    SaveUserData(userData);
                    
                    promise.Resolve(moduleRemoteData);
                }
                else
                {
                    promise.Reject(new Exception("No Space for new Module."));
                }
            }
            catch (Exception e)
            {
                promise.Reject(e);
            }

            return promise;
        }

        public IPromise UpdateModulePosition(ModuleRemoteDataModel model, Vector3 old, Vector3 pos)
        {
            Promise promise = new Promise();
            
            try
            {
                var grid = _remoteDataModel.UserData.Grid;
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

                SaveUserData(_remoteDataModel.UserData);
                
                promise.Resolve();
            }
            catch (Exception e)
            {
                promise.Reject(e);
            }

            return promise;
        }
    }
}