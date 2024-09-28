using UnityEngine;
using NaughtyAttributes;
using SteelLotus.Core.SaveLoadSystem;
using System;
using System.Reflection;
using SteelLotus.Sounds;
using UnityEngine.SceneManagement;
using UnityEditor.Build;

namespace SteelLotus.Core
{
    public class MainGameController : Singleton<MainGameController>
    {

        [BoxGroup("Core"),SerializeField]
        private DataManager dataManager;

        [BoxGroup("Core"), SerializeField]
        private SoundManager soundManager;

        [BoxGroup("Notification"), SerializeField]
        private NotificationController notificationController;

        [BoxGroup("UI"), SerializeField]
        private Canvas canvas;

        [BoxGroup("UI"), SerializeField]
        private SwitchScenes switchScenes;

        [BoxGroup("Minigame"), SerializeField]
        private MinigameController minigameController;

        [BoxGroup("UI"), SerializeField]
        private CanvasGroup interactionsBlocker;

        public bool GameStarted { get; set; }


        [ContextMenu("start game")]
        public void startGame()
        {
            GameStarted = true;
        }

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


        public void BlockUnlockInteractions(bool value)
        {
            interactionsBlocker.blocksRaycasts = value;
        }
    }
}
