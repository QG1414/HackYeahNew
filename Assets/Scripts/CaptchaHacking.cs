using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaptchaHacking : HackingEvent
{
    [SerializeField]
    List<Button> buttonList = new List<Button>();

    [SerializeField]
    private Sprite correctSprite;

    [SerializeField]
    private List<Sprite> incorrectSprites;

    [SerializeField]
    private TMP_Text howManySolve;

    [SerializeField]
    private int howManyToSolve;

    private int correctElementNumber = 0;
    private int currentSolved;

    public override void StartEvent()
    {
        howManySolve.text = "0 / " + howManyToSolve.ToString();

        currentSolved = howManyToSolve;

        ShuffleNewDeck();

        base.StartEvent();
    }

    private void ShuffleNewDeck()
    {
        List<Button> tmpButton = new List<Button>(buttonList);

        correctElementNumber = Random.Range(0, buttonList.Count);
        tmpButton[correctElementNumber].image.sprite = correctSprite;
        Button selectedButton = tmpButton[correctElementNumber];


        tmpButton = new List<Button>(Shuffle<Button>(tmpButton));

        for (int i = 0; i < tmpButton.Count; i++)
        {
            var i1 = i;
            tmpButton[i].onClick.RemoveAllListeners();
            tmpButton[i].onClick.AddListener(() => CheckSelectedCaptcha(i1));
            if (tmpButton[i] == selectedButton)
            {
                correctElementNumber = i;
            }
            else
            {
                tmpButton[i].image.sprite = incorrectSprites[i % incorrectSprites.Count];
            }

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

    public void CheckSelectedCaptcha(int number)
    {

        currentSolved -= 1;

        howManySolve.text = (howManyToSolve - currentSolved).ToString() + " / " + howManyToSolve.ToString();

        if (correctElementNumber != number)
        {
            FinishHacking(false);
            return;
        }

        if (currentSolved <= 0)
            FinishHacking(true);
        else
        {
            ShuffleNewDeck();
        }
    }
}
