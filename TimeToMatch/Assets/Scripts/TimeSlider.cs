using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    
    [SerializeField] private Slider slider;
    [SerializeField] private float maxTime = 5f;
    [SerializeField] private float smoothSpeed = 8f;

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

    public void AddTime(float amount)
    {
        currentTime = Mathf.Clamp(currentTime + amount, 0f, maxTime);
    }

    public void MinusTime(float amount)
    {
        currentTime = Mathf.Clamp(currentTime - amount, 0f, maxTime);
    }
}
