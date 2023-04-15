using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource fxSource;
    public AudioClip canHitFx, gameOverFx, gameCompleteFx, ballSpawnFx, throwFx;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayFx(FxTypes fxTypes)
    {
        switch (fxTypes)
        {
            case FxTypes.CANHITFX:
                fxSource.PlayOneShot(canHitFx);
                break;
            case FxTypes.GAMEOVERFX:
                fxSource.PlayOneShot(gameOverFx);
                break;
            case FxTypes.GAMECOMPLETEFX:
                fxSource.PlayOneShot(gameCompleteFx);
                break;
            case FxTypes.BALLSPAWNFX:
                fxSource.PlayOneShot(ballSpawnFx);
                break;
            case FxTypes.THROWFX:
                fxSource.PlayOneShot(throwFx);
                break;
        }
    }
}

public enum FxTypes
{
    CANHITFX,
    GAMEOVERFX,
    GAMECOMPLETEFX,
    BALLSPAWNFX,
    THROWFX
}
