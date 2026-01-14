using UnityEngine;

public class CardMatchManager : MonoBehaviour
{
    public static CardMatchManager Instance;

    private Card firstCard;
    private Card secondCard;
    private bool isChecking;

    [SerializeField] TimeSlider timeSlider;
    [SerializeField] CardRefillManager refillManager;

    private void Awake()
    {
        Instance = this;
    }

    public void OnCardClicked(Card card)
    {
        if (isChecking)
            return;

        if (firstCard == card)
            return;

        if (firstCard == null)
        {
            firstCard = card;
            return;
        }

        secondCard = card;
        isChecking = true;

        CheckMatch();
    }

    void CheckMatch()
    {
        if (firstCard.cardType == secondCard.cardType)
        {
            // ✅ 같은 카드
            firstCard.Match();
            secondCard.Match();

            timeSlider.AddTime();
            refillManager.OnMatchSuccess(firstCard.cardType);
            GameManager.Instance.ScoreUp();
        }
        else
        {
            // ❌ 다른 카드
            timeSlider.MinusTime();
            GameManager.Instance.Combo_Broken();
        }

        ResetSelection();
    }

    void ResetSelection()
    {
        firstCard = null;
        secondCard = null;
        isChecking = false;
    }

    public void ResetMatch()
    {
        firstCard = null;
        secondCard = null;
        isChecking = false;
    }
}
