using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isRunning;

    [SerializeField] private GameObject CardManager;
    [SerializeField] private GameObject StartButton;
    [SerializeField] GameObject gameOverTimer;
    [SerializeField] private GameObject NOADButton;

    [SerializeField] CardMatchManager cardMatchManager;
    [SerializeField] CardGridManager cardGridManager;
    [SerializeField] CardRefillManager cardRefillManager;
    [SerializeField] TimeSlider timeSlider;
    [SerializeField] GameObject Tutorials;


    [SerializeField] Text score_Text;
    [SerializeField] Text bestScore_Text;

    int score;
    int bestscore;
    int combo;

#if UNITY_ANDROID
    private string gameId = "1234567";
#elif UNITY_IOS
    private string gameId = "7654321";
#endif


    private void Awake()
    {
        // 이미 존재하면 자신 제거
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        isRunning = false;

        bestscore = PlayerPrefs.GetInt("BestScore");
        bestScore_Text.text = bestscore.ToString();
        score = 0;
        score_Text.text = score.ToString();
    }

    void Start()
    {
        Advertisement.Initialize(gameId, true);
    }

    private int comboScore()
    {
        int res = 10;

        if(combo > 10)
        {
            res = 100;
        }

        else if(combo > 6)
        {
            res = 50;
        }

        else if(combo > 4)
        {
            res = 20;
        }

        return res;
    }

    private void SetBestScore()
    {
        if(bestscore < score)
        {
            bestscore = score;
            bestScore_Text.text = bestscore.ToString();
            PlayerPrefs.SetInt("BestScore", bestscore);
        }
    }

    public void GameOverTimerOn()
    {
        gameOverTimer.SetActive(true);

        Animator anim = gameOverTimer.GetComponent<Animator>();

        anim.Play("TimerOn", 0, 0f);

        SoundEffectManager.Instance.Play(SFXType.GameOver);
    }

    public void __Init__()
    {
        isRunning = true;
        StartButton.SetActive(false);
        CardManager.SetActive(true);
        cardMatchManager.ResetMatch();
        cardRefillManager.ResetRefill();
        cardGridManager.ResetGrid();
        timeSlider.ResetSlider();

    }

    public void GameStart()
    {
        if(PlayerPrefs.GetInt("Tutorial") == 0)
        {
            Tutorials.SetActive(true);
        }

        else
        {
            __Init__();
        }
    }

    public void PreGameOver()
    {
        isRunning = false;
        GameOverTimerOn();

    }

    public void AfterGameOver()
    {
        gameOverTimer.SetActive(false);
        CardManager.SetActive(false);
        StartButton.SetActive(true);
        SetBestScore();
        score = 0;
        score_Text.text = "0";
        combo = 0;

        AdsManager.Instance.OnPlayerDied();
    }

    public void ScoreUp()
    {
        combo++;
        score += comboScore();
        score_Text.text = score.ToString();
        
    }

    public void Combo_Broken()
    {
        combo = 0;
    }

    public void TutorialOff()
    {
        PlayerPrefs.SetInt("Tutorial", 1);
        Tutorials.SetActive(false);
        __Init__();

    }


}
