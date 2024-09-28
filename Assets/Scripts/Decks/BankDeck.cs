
public class BankDeck : BaseDeck
{
    public override void Interact(Card card)
    {
        base.Interact(card);
        SoundManager.Instance.PlayBankCard();
    }
}
