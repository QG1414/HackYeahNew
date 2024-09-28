using UnityEngine;
using NaughtyAttributes;
using SteelLotus.Core.SaveLoadSystem;
using System;
using System.Reflection;
using SteelLotus.Sounds;
using UnityEngine.SceneManagement;

namespace SteelLotus.Core
{
    public class MainGameController : Singleton<MainGameController>
    {

        [BoxGroup("Core"),SerializeField]
        private DataManager dataManager;

        [BoxGroup("Core"), SerializeField]
        private SoundManager soundManager;


        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            DontDestroyOnLoad(this.gameObject);
            dataManager.Init();
            SaveSystem.Init();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                SceneManager.LoadScene(1);
            }
        }
        public T GetPropertyByType<T>() where T : class
        {
            Type myType = this.GetType();
            FieldInfo[] allFields = myType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo field in allFields)
            {
                if(field.FieldType == typeof(T))
                {
                    return field.GetValue(this) as T;
                }
            }

            return null;
        }

        public T GetPropertyByName<T>(string name) where T : class
        {
            Type myType = this.GetType();
            FieldInfo[] allFields = myType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo field in allFields)
            {
                if (field.Name == name)
                {
                    return field.GetValue(this) as T;
                }
            }

            return null;
        }

        public void test()
        {
            Debug.LogError("test");
        }
    }
}
