using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image imageSuit;
    [SerializeField] private Text numberText;
    [SerializeField] private CardSO cardSO;
    private int number;
    private BaseDeck deck;
    private bool isActive = false;

    public Action OnActive;
    public Action<Card> OnDetachCard;
    public int Number {  get { return number; } }
    public void Initialize( int number, BaseDeck deck)
    {
        this.number = number;
        numberText.text = number.ToString();
        this.deck = deck;
        (Color color, string text, Sprite sprite) result = cardSO.GetColorStringAndSprite(number);
        numberText.color = result.color;
        numberText.text = result.text;
        imageSuit.sprite = result.sprite;
    }
    public void ActiveSelf()
    {
        isActive = true;
        OnActive?.Invoke();
    }
    public void DetachCard()
    {
        deck = null;
        Card previousCard = ComboLauncher.Instance.SetCard(this);
        OnDetachCard?.Invoke(previousCard);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(isActive && deck != null) deck.Interact(this);
    }
}
