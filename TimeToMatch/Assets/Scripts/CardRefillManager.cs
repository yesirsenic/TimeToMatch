using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRefillManager : MonoBehaviour
{
    [Header("All Cards (Grid children)")]
    public Card[] cards;

    [Header("Sprites (CardType order)")]
    public Sprite[] cardSprites;

    // ============================
    // 내부 상태
    // ============================

    // 사라진 "쌍"을 기억 (1쌍 = CardType 1개)
    private Queue<CardType> removedPairQueue = new Queue<CardType>();

    // 현재 누적 사라진 쌍 수
    private int removedPairCount = 0;

    // 리필 중복 방지
    private bool isRefilling = false;

    // ============================
    // 외부에서 호출
    // ============================

    /// <summary>
    /// 매치 성공 후 호출
    /// (Fade 끝나고 IsMatched = true 된 이후)
    /// </summary>
    public void OnMatchSuccess(CardType type)
    {
        removedPairQueue.Enqueue(type);
        removedPairCount++;

        TryRefill();
    }

    // ============================
    // 리필 진입 판단
    // ============================

    void TryRefill()
    {
        if (isRefilling)
            return;

        int refillPairCount = DecideRefillPairCount();

        if (refillPairCount <= 0)
            return;

        isRefilling = true;

        for (int i = 0; i < refillPairCount; i++)
        {
            bool success = RefillOnePair();
            if (!success)
                break;

            removedPairCount--;
        }

        isRefilling = false;
    }

    // ============================
    // "이번에 몇 쌍 채울지" 결정
    // ============================

    int DecideRefillPairCount()
    {
        // 5쌍 이상 → 무조건 3쌍
        if (removedPairCount >= 5)
            return 3;

        // 4쌍 → 75% 확률로 2~3쌍
        if (removedPairCount == 4)
        {
            if (Random.value < 0.75f)
                return Random.Range(2, 4); // 2 or 3
            return 0;
        }

        // 3쌍 → 50% 확률로 1~2쌍
        if (removedPairCount == 3)
        {
            if (Random.value < 0.5f)
                return Random.Range(1, 3); // 1 or 2
            return 0;
        }

        return 0;
    }

    // ============================
    // 쌍 하나 리필 
    // ============================

    bool RefillOnePair()
    {
        // 쌍 데이터 없으면 불가
        if (removedPairQueue.Count == 0)
            return false;

        // 빈 슬롯 2개 필요
        List<Card> emptyCards = GetEmptyCards();
        if (emptyCards.Count < 2)
            return false;

        Shuffle(emptyCards);

        CardType type = removedPairQueue.Dequeue();

        Card a = emptyCards[0];
        Card b = emptyCards[1];

        a.SetCard(type, cardSprites[(int)type]);
        b.SetCard(type, cardSprites[(int)type]);

        return true;
    }

    // ============================
    // 유틸
    // ============================

    List<Card> GetEmptyCards()
    {
        List<Card> list = new List<Card>();
        foreach (var card in cards)
        {
            if (card.IsMatched)
                list.Add(card);
        }
        return list;
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }

    public void ResetRefill()
    {
        removedPairQueue.Clear();
        removedPairCount = 0;
        isRefilling = false;
    }
}
