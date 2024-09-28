using System.IO;
using UnityEngine;
using System.Security.Cryptography;
using Newtonsoft.Json;
using SteelLotus.Core;

namespace SteelLotus.Core.SaveLoadSystem
{
    public static class SaveSystem
    {
        private const string savingFolder = "Saves/";
        private const string ending = ".xml";

        private static string absoluteSavingPath = Application.persistentDataPath + "/" + savingFolder;
        
        
        private const string savingLevel = "CurrentLevelData/";
        private const string savingPlayer = "PlayerData/";
        private const string savingCollectibles = "CollectiblesData/";

        public static string SavingLevel { get => savingLevel; }
        public static string SavingCollectibles { get => savingCollectibles; }
        public static string SavingPlayer { get => savingPlayer; }

        private static DataManager dataManager;

        public static void Init()
        {
            dataManager = null;
            dataManager = MainGameController.Instance.GetPropertyByType<DataManager>();
        }


        #region LoadTypes

        private static string GetRawData(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            CryptoStream oStream = dataManager.Encrypter.CreateDecryptoCryptoStream(fs, CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(oStream);

            string rawData = reader.ReadToEnd();

            reader.Close();
            oStream.Close();
            fs.Close();

            return rawData;
        }

        public static T Load<T>(string savingPath, T ifFileNotExistValue)
        {
            if (!CheckIfFileExists(savingPath))
                return ifFileNotExistValue;

            string path = CreatePath(savingPath);

            T data = JsonConvert.DeserializeObject<T>(GetRawData(path));

            return data;
        }


        public static T LoadClass<T>(string savingPath) where T : class
        {
            if (!CheckIfFileExists(savingPath))
                return null;

            string path = CreatePath(savingPath);

            T data = JsonConvert.DeserializeObject<T>(GetRawData(path));

            return data;
        }

        #endregion LoadTypes


        #region SaveTypes

        public static void Save<T>(T savingValue, string savingPath)
        {
            string path = CreatePath(savingPath);
            CreateDirectoryForSaves(path);

            FileStream fs = new FileStream(path, FileMode.Create);
            CryptoStream iStream = dataManager.Encrypter.CreateEncryptoCryptoStream(fs, CryptoStreamMode.Write);
            StreamWriter sWriter = new StreamWriter(iStream);

            string JsonFile = JsonConvert.SerializeObject(savingValue);
            sWriter.Write(JsonFile);

            sWriter.Close();
            iStream.Close();
            fs.Close();
        }


        public static void SaveClass<T>(T savingValue, string savingPath) where T : new()
        {
            string path = CreatePath(savingPath);
            CreateDirectoryForSaves(path);

            FileStream fs = new FileStream(path, FileMode.Create);
            CryptoStream iStream = dataManager.Encrypter.CreateEncryptoCryptoStream(fs, CryptoStreamMode.Write);
            StreamWriter sWriter = new StreamWriter(iStream);

            T customData = new T();
            customData = savingValue;
            string JsonFile = JsonConvert.SerializeObject(customData);
            sWriter.Write(JsonFile);

            sWriter.Close();
            iStream.Close();
            fs.Close();
        }


        #endregion SaveTypes

        public static bool CheckIfFileExists(string savingPath)
        {
            string path = CreatePath(savingPath);
            return File.Exists(path);
        }

        public static void DeleteAllSaves()
        {
            if(Directory.Exists(absoluteSavingPath))
            {
                Directory.Delete(absoluteSavingPath, true);
            }
        }

        public static void DeleteOneSave(string savingPath)
        {
            string path = CreatePath(savingPath);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        private static void CreateDirectoryForSaves(string path)
        {
            int lastIndicator = path.LastIndexOf("/");
            int numberOfWordsToDelete = (path.Length - lastIndicator);
            string newPath = path.Remove(lastIndicator, numberOfWordsToDelete);

            if (Directory.Exists(newPath))
                return;

            Directory.CreateDirectory(newPath);
        }

        private static string CreatePath(string rawPath)
        {
            string finalPath = absoluteSavingPath + rawPath + ending;
            return finalPath;
        }
    }
}
