using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SpritePlayerSwitcher : MonoBehaviour
{
    [SerializeField]
    private Image playerImage;

    [SerializeField]
    private List<Sprite> talkingSprites = new List<Sprite>();

    [SerializeField]
    private List<Sprite> idleSprites = new List<Sprite>();

    [SerializeField]
    private float talkingSwitchSpeed;

    [SerializeField]
    private float idleSwitchSpeed;

    [SerializeField]
    private float idleWaitTime;


    Coroutine spriteCoroutine = null;

    public void OnStartDialogoue()
    {
        if (spriteCoroutine != null)
        {
            StopCoroutine(spriteCoroutine);
            spriteCoroutine = null;
        }

        spriteCoroutine = StartCoroutine(SwitchSprites(talkingSprites, talkingSwitchSpeed, 0.1f));
    }

    public void OnStartIdle()
    {
        if (spriteCoroutine != null)
        {
            StopCoroutine(spriteCoroutine);
            spriteCoroutine = null;
        }


        spriteCoroutine = StartCoroutine(SwitchSprites(idleSprites, idleSwitchSpeed, idleWaitTime));
    }

    private IEnumerator SwitchSprites(List<Sprite> spritesToSwitch, float speed, float waitTime)
    {

        float time = 0;

        playerImage.sprite = spritesToSwitch[0];

        while(true)
        {
            time += Time.deltaTime;

            if(time >= waitTime)
            {
                foreach(Sprite sprite in spritesToSwitch)
                {
                    playerImage.sprite = sprite;
                    yield return new WaitForSeconds(speed);
                }
                playerImage.sprite = spritesToSwitch[0];

                time = 0;
            }

            yield return null;
        }
    }
}
