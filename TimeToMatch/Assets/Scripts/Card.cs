using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardType cardType;

    [Header("UI")]
    public Image iconImage;
    public Button button;
    public CanvasGroup canvasGroup;

    public bool IsMatched { get; private set; }

    public void SetCard(CardType type, Sprite sprite)
    {
        cardType = type;
        iconImage.sprite = sprite;

        ResetCard();
    }

    public void OnClick()
    {
        if (IsMatched)
            return;

        CardMatchManager.Instance.OnCardClicked(this);
    }

    public void ResetCard()
    {
        IsMatched = false;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        button.interactable = true;
    }

    public void Match()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        button.interactable = false;

        float t = 0f;
        float duration = 0.25f;

        while (t < duration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / duration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        IsMatched = true;
    }
}
