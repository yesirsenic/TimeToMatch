using System.Collections.Generic;
using UnityEngine;

public class CardGridManager : MonoBehaviour
{
    [Header("카드 설정")]
    public Card[] cards;              // GridLayout 자식 카드 20개
    public Sprite[] cardSprites;       // 5개 (enum 순서와 동일)

    private void Start()
    {
        SetupCards();
    }

    void SetupCards()
    {
        List<CardType> cardPool = new List<CardType>();

        // 5종류 × 4장 = 20장 생성
        foreach (CardType type in System.Enum.GetValues(typeof(CardType)))
        {
            for (int i = 0; i < 4; i++)
            {
                cardPool.Add(type);
            }
        }

        // 셔플
        Shuffle(cardPool);

        // 카드에 할당
        for (int i = 0; i < cards.Length; i++)
        {
            CardType type = cardPool[i];
            cards[i].SetCard(type, cardSprites[(int)type]);
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }

    public void ResetGrid()
    {
        SetupCards();
    }
}
