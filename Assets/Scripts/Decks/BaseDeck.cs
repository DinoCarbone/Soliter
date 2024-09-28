using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDeck : MonoBehaviour
{
    [SerializeField] protected List<Card> cardList;
    protected Stack<Card> cardStack = new Stack<Card>();
    public int GetCountCard()
    {
        return cardList.Count;
    }
    public bool AddCard(int value)
    {
        if (cardStack.Count < cardList.Count)
        {
            Card addedCard = cardList[cardStack.Count];
            cardStack.Push(addedCard);
            addedCard.Initialize(value, this);

            if (cardStack.Count == cardList.Count) addedCard.ActiveSelf();
            return true;
        }
        return false;
    }
    public virtual void Interact(Card card)
    {
        if (cardStack.Pop() != card)
        {
            Debug.LogWarning("Error in BankDeck!");
        }
        card.DetachCard();
        ActiveUpperCard();
    }
    protected void ActiveUpperCard()
    {
        if(cardStack.Count > 0)
        {
            Card card = cardStack.Peek();
            card.ActiveSelf();
        }
    }
    public virtual bool CheckCard()
    {
        if (cardStack.Count > 0) return true;
        return false;
    }
}
