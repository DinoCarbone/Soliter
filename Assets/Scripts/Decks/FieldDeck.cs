
public class FieldDeck : BaseDeck
{
    public override void Interact(Card card)
    {
        int numberLauncher = ComboLauncher.Instance.Number;
        if( numberLauncher +1 == card.Number || numberLauncher -1 == card.Number)
        {
            base.Interact(card);
            SoundManager.Instance.PlayCorrect();
        }
        else if (numberLauncher == 13 && card.Number == 1)
        {
            base.Interact(card);
            SoundManager.Instance.PlayCorrect();
        }
        else if (numberLauncher == 1 && card.Number == 13)
        {
            base.Interact(card);
            SoundManager.Instance.PlayCorrect();
        }
        else
        {
            SoundManager.Instance.PlayWrong();
        }
    }
    public override bool CheckCard()
    {
        if (cardStack.Count > 0)
        {
            Card card = cardStack.Peek();
            int numberLauncher = ComboLauncher.Instance.Number;
            if (numberLauncher + 1 == card.Number || numberLauncher - 1 == card.Number)
            {
                return true;
            }
            else if (numberLauncher == 13 && card.Number == 1)
            {
                return true;
            }
            else if (numberLauncher == 1 && card.Number == 13)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public bool CheckIsWin()
    {
        if(cardStack.Count > 0)
        {
            return false;
        }
        return true;
    }
}
