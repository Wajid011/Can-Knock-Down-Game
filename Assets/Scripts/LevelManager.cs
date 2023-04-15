using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public LevelData[] levelDatas;
    public GameObject ballPrefab;
    public Transform ballSpawnPos;

    private int availableCans, availableBalls;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SpawnBall()
    {
        if(availableBalls > 1)
        {
            GameObject ball = Instantiate(ballPrefab, ballSpawnPos.position, Quaternion.identity);
            SwipeController.instance.TargetObj = ball;
            availableBalls--; 
            UIManager.instance.BallText.text = availableBalls.ToString();
            SoundManager.instance.PlayFx(FxTypes.BALLSPAWNFX);
        }
        else
        {
            if(GameManager.singleton.gameStatus == GameStatus.Playing)
            {
                GameManager.singleton.gameStatus = GameStatus.Failed;
                UIManager.instance.GameResult();
            }
        }
    }

    public void SpawnLevel(int levelIndex)
    {
        LevelData level = Instantiate(levelDatas[levelIndex], new Vector3(0,1.5f,8.5f), Quaternion.identity).GetComponent<LevelData>();
        availableCans = level.canCount;
        availableBalls = level.ballCount;
        UIManager.instance.CanText.text = availableCans.ToString();
        UIManager.instance.BallText.text = availableBalls.ToString();

        SpawnBall();
        GameManager.singleton.gameStatus = GameStatus.Playing;
    }

    public void ReduceCan()
    {
        availableCans--;
        UIManager.instance.CanText.text = availableCans.ToString();
    
        if(GameManager.singleton.gameStatus == GameStatus.Playing)
        {
            if(availableCans <= 0)
            {
                GameManager.singleton.gameStatus = GameStatus.Complete;
                
                if(GameManager.singleton.currentLevelIndex < levelDatas.Length)
                    GameManager.singleton.currentLevelIndex++;
                
                UIManager.instance.GameResult();
           }
        }
    }
}
