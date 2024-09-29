using UnityEngine;
using NaughtyAttributes;
using SteelLotus.Core.SaveLoadSystem;
using System;
using System.Reflection;
using SteelLotus.Sounds;
using UnityEngine.SceneManagement;
using UnityEditor.Build;
using System.Collections;

namespace SteelLotus.Core
{
    public class MainGameController : Singleton<MainGameController>
    {

        [BoxGroup("Core"),SerializeField]
        private DataManager dataManager;

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

        [SerializeField]
        private HealthController healthController;

        [SerializeField]
        private TimeController timeController;

        [SerializeField]
        private TutorialScript tutorials;


        private float generatorMultiplaier = 1f;

        public bool GameStarted { get; set; }

        public bool GeneratorCritical { get; set; }
        public float GeneratorMultiplaier { get => generatorMultiplaier; set => generatorMultiplaier = value; }

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

            switchScenes.StopMovement(() => tutorials.StartTutorial());
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

        public void DecreaseHealth()
        {
            healthController.UpdateHealth();
            SoundManager.Instance.PlayOneShoot(SoundManager.Instance.AlertSource, SoundManager.Instance.AlertCollection.clips[3], 0.5f);
        }


        public void BlockUnlockInteractions(bool value)
        {
            interactionsBlocker.blocksRaycasts = value;
        }
    }
}
