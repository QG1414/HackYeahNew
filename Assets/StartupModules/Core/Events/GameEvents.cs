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

        public event Action OnGameOver;

        public void CallOnGameOver()
        {
            if (OnGameOver != null)
            {
                OnGameOver();
            }
        }

        public event Action<HackingEvent> OnEventStarted;
        public void CallOnEventStarted(HackingEvent hackingEvent)
        {
            if (OnEventStarted != null)
            {
                OnEventStarted(hackingEvent);
            }
        }


        public event Action<bool> OnEventStopped;

        public void CallOnEventStopped(bool win)
        {
            if (OnEventStopped != null)
            {
                OnEventStopped(win);
            }
        }

        public event Action<float,float> OnGeneratorChange;

        public void CallOnGeneratorChange(float generatorValueCurrent, float generatorValueMax)
        {
            if (OnGeneratorChange != null)
            {
                OnGeneratorChange(generatorValueCurrent, generatorValueMax);
            }
        }


        public event Action<DialogueOrder, int, int> OnDialogueChange;

        public void CallOnDialogueChange(DialogueOrder order, int mainOrder, int secondOrder)
        {
            if (OnDialogueChange != null)
            {
                OnDialogueChange(order, mainOrder, secondOrder);
            }
        }

        public event Action<DialogueOrder> OnDialogueEnd;

        public void CallOnDialogueEnd(DialogueOrder order)
        {
            if (OnDialogueEnd != null)
            {
                OnDialogueEnd(order);
            }
        }
    }
}
