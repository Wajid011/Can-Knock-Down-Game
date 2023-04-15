using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Text canText, ballText;
    [SerializeField] private GameObject mainMenu, gameMenu, gameOverPanel, retryBtn, nextBtn;
    [SerializeField] private GameObject container, lvlBtnPrefab;

    public Text CanText { get { return canText; } }
    public Text BallText { get { return ballText; } }

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
       if(GameManager.singleton.gameStatus == GameStatus.None)
       {
            CreateLevelButtons();
       }else if(GameManager.singleton.gameStatus == GameStatus.Failed ||
            GameManager.singleton.gameStatus == GameStatus.Complete)
        {
            mainMenu.SetActive(false);
            gameMenu.SetActive(true);
            LevelManager.instance.SpawnLevel(GameManager.singleton.currentLevelIndex);
        }
    }

    void CreateLevelButtons()
    {
        for (int i = 0; i < LevelManager.instance.levelDatas.Length; i++)
        {
            GameObject buttonObj = Instantiate(lvlBtnPrefab, container.transform);
            buttonObj.transform.GetChild(0).GetComponent<Text>().text = "" + (i + 1);
            Button button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(() => OnClick(button));
        }
    }

    void OnClick(Button btn)
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        GameManager.singleton.currentLevelIndex = btn.transform.GetSiblingIndex();
        LevelManager.instance.SpawnLevel( GameManager.singleton.currentLevelIndex);
    }

    public void GameResult()
    {
        switch (GameManager.singleton.gameStatus)
        {
            case GameStatus.Failed:
                gameOverPanel.SetActive(true);
                retryBtn.SetActive(true);
                SoundManager.instance.PlayFx(FxTypes.GAMEOVERFX);
                break;
            case GameStatus.Complete:
                gameOverPanel.SetActive(true);
                nextBtn.SetActive(true);
                SoundManager.instance.PlayFx(FxTypes.GAMECOMPLETEFX);
                break;

        }
    }

    public void HomeBtn()
    {
        GameManager.singleton.gameStatus = GameStatus.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextRetryBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
