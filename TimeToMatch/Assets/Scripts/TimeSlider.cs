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

    void Start()
    {
        currentTime = maxTime;
        slider.value = 1f;
    }

    void Update()
    {
        // 실제 시간 감소
        currentTime -= Time.deltaTime;
        currentTime = Mathf.Max(currentTime, 0f);

        float targetValue = currentTime / maxTime;

        // UI는 부드럽게 따라감
        slider.value = Mathf.Lerp(
            slider.value,
            targetValue,
            Time.deltaTime * smoothSpeed
        );
    }

    public void AddTime()
    {
        currentTime = Mathf.Clamp(currentTime + addTime, 0f, maxTime);
    }

    public void MinusTime()
    {
        currentTime = Mathf.Clamp(currentTime - minusTime, 0f, maxTime);
    }
}
