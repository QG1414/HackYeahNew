using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CablesMinigame : Minigame
{
    [SerializeField]
    private List<Button> inputButtons = new List<Button>();

    [SerializeField]
    private List<Button> outputButtons = new List<Button>();

    List<Button> shoufledButtons = new List<Button>();

    private int? focusButtonID = null;

    private int numbersOfcables = 0;
    public override void StartMinigame()
    {
        base.StartMinigame();
        numbersOfcables = 0;
        shoufledButtons = new List<Button>(Shuffle(outputButtons));

        for(int i=0; i< inputButtons.Count; i++)
        {
            var l1 = i;
            inputButtons[i].onClick.RemoveAllListeners();
            inputButtons[i].onClick.AddListener(() => StartStopFocus(l1,true));
            shoufledButtons[i].onClick.RemoveAllListeners();
            shoufledButtons[i].onClick.AddListener(() => ConnectCable(l1));
            shoufledButtons[i].image.color = inputButtons[i].image.color;
        }
    }

    public void StartStopFocus(int button, bool active)
    {
        if(focusButtonID != null)
        {
            var val = focusButtonID.Value;
            inputButtons[focusButtonID.Value].image.color = inputButtons[focusButtonID.Value].image.color * 2f;
            inputButtons[focusButtonID.Value].onClick.RemoveAllListeners();
            inputButtons[focusButtonID.Value].onClick.AddListener(() => StartStopFocus(val, true));

            if (focusButtonID == button)
            {
                focusButtonID = null;
                return;
            }
        }


        var l1 = button;
        var newActive = !active;

        focusButtonID = button;

        if (active)
            inputButtons[button].image.color = inputButtons[button].image.color * 0.5f;
        else
            inputButtons[button].image.color = inputButtons[button].image.color * 2f;

        inputButtons[button].onClick.RemoveAllListeners();
        inputButtons[button].onClick.AddListener(() => StartStopFocus(l1, newActive));
    }

    public void ConnectCable(int buttonID)
    {
        if (focusButtonID == null)
            return;

        if(focusButtonID == buttonID)
        {
            //Connect cables
            inputButtons[buttonID].onClick.RemoveAllListeners();
            shoufledButtons[buttonID].onClick.RemoveAllListeners();
            shoufledButtons[buttonID].image.color = shoufledButtons[buttonID].image.color * 0.5f;
            focusButtonID = null;
            numbersOfcables += 1;
            if (numbersOfcables >= inputButtons.Count)
            {
                EndMinigame();
                foreach(Button inputButton in inputButtons)
                {
                    inputButton.image.color = inputButton.image.color * 2f;
                }
            }
        }
        else
        {
            var val = focusButtonID.Value;
            inputButtons[focusButtonID.Value].image.color = inputButtons[focusButtonID.Value].image.color * 2f;
            inputButtons[focusButtonID.Value].onClick.RemoveAllListeners();
            inputButtons[focusButtonID.Value].onClick.AddListener(() => StartStopFocus(val, false));
        }

    }

    public List<T> Shuffle<T>(List<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }

        return ts;

    }


}
