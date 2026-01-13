using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isRunning;

    [SerializeField] private GameObject CardManager;
    [SerializeField] private GameObject StartButton;

    [SerializeField] CardMatchManager cardMatchManager;
    [SerializeField] CardGridManager cardGridManager;
    [SerializeField] CardRefillManager cardRefillManager;
    [SerializeField] TimeSlider timeSlider;

    [SerializeField] GameObject gameOverTimer;


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
    }

    public void GameOverTimerOn()
    {
        gameOverTimer.SetActive(true);

        Animator anim = gameOverTimer.GetComponent<Animator>();

        anim.Play("TimerOn", 0, 0f);
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
    }


}
