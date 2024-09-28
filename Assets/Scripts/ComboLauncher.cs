using UnityEngine;

public class ComboLauncher : MonoBehaviour
{
    [SerializeField] private Card startCard;
    private Card currentCard;
    public static ComboLauncher Instance;
    public int Number { get { return currentCard.Number; } }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("ComboLauncher is Destroy!");
            Destroy(gameObject);
        }
    }
    public void SetStartCard(int value)
    {
        startCard.Initialize(value,null);
        startCard.ActiveSelf();
        currentCard = startCard;
    }
    public Card SetCard(Card card)
    {
        Card previousCard = currentCard;
        currentCard = card;
        return previousCard; // передаю предыдущую карту для отключения
    }
}
