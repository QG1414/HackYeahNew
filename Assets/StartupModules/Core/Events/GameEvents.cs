using SteelLotus.Core.SaveLoadSystem;
using System;

namespace SteelLotus.Core.Events
{
    public class GameEvents : Singleton<GameEvents>
    {
        public event Action OnGameStart;

        public void CallOnGameStart()
        {
            if (OnGameStart != null)
                OnGameStart();
        }

        public event Action OnGameEnd;

        public void CallOnGameEnd()
        {
            if (OnGameEnd != null)
                OnGameEnd();
        }

        public event Action OnSecondUpdate;

        public void CallOnSecondUpdate()
        {
            if(OnSecondUpdate != null)
            {
                OnSecondUpdate();
            }
        }
    }
}
