using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    
    [SerializeField] private Slider slider;
    [SerializeField] private float maxTime = 5f;
    [SerializeField] private float smoothSpeed = 8f;


    [SerializeField] private float addTime = 0.5f;
    [SerializeField] private float minusTime = 0.5f;

    private float currentTime;

    private float baseSpeed = 0.5f;
    private float accel = 0.035f;
    private float maxSpeed = 2.2f;

    float elapsedTime = 0f;

    private float minAdd = 0.25f;
    private float maxAdd = 1f;
    private float growDuration = 45f;

    void Start()
    {
        currentTime = maxTime;
        slider.value = 1f;
    }

    void Update()
    {
        if (!GameManager.Instance.isRunning)
            return;

        // 실제 시간 감소
        elapsedTime += Time.deltaTime;
        float speed = Mathf.Min(
            baseSpeed + elapsedTime * accel,
            maxSpeed
        );
        currentTime -= Time.deltaTime * speed;
        currentTime = Mathf.Max(currentTime, 0f);

        float targetValue = currentTime / maxTime;

        if (targetValue <= 0.001f)
        {
            slider.value = 0f;
            GameManager.Instance.PreGameOver();
        }

        // UI는 부드럽게 따라감
        slider.value = Mathf.Lerp(
            slider.value,
            targetValue,
            Time.deltaTime * smoothSpeed
        );
    }

    public void AddTime()
    {
        float t = Mathf.Clamp01(elapsedTime / growDuration);
        float gain = Mathf.Lerp(minAdd, maxAdd, Mathf.Sqrt(t));
        currentTime = Mathf.Clamp(currentTime + gain, 0f, maxTime);
    }

    public void MinusTime()
    {
        currentTime = Mathf.Clamp(currentTime - minusTime, 0f, maxTime);
    }

    public void ResetSlider()
    {
        currentTime = maxTime;
        slider.value = 1f;
        elapsedTime = 0f;
    }
}
