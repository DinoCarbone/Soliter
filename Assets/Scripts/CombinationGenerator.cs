using System.Collections.Generic;
using UnityEngine;
using System;

public class CombinationGenerator : MonoBehaviour
{
    [SerializeField] private int maxCombination = 7;
    [SerializeField] private int minCombination = 2;
    [Range(0,1)]
    [SerializeField] private float probabilityGoingDown = 0.35f;
    [Range(0, 1)]
    [SerializeField] private float probabilitySwitching = 0.15f;
    [SerializeField] private List<FieldDeck> fieldDeckList;
    [SerializeField] private BankDeck bankDeck;
    [SerializeField] private ComboLauncher comboLauncher;
    void Start()
    {
        int totalCards = 0; 
        foreach (FieldDeck field in fieldDeckList)
        {
            totalCards += field.GetCountCard();
        }
        int numberOfCombinations = bankDeck.GetCountCard() + 1;// добавляю единицу, т.к в comboLauncher 1 карта
        totalCards += numberOfCombinations; 

        if(numberOfCombinations * maxCombination < totalCards)
        {
            Debug.LogWarning("Количество карт больше количества комбинаций * максимальную вместимость комбинации!");
        }
        else if (numberOfCombinations * minCombination > totalCards)
        {
            Debug.LogWarning("Количество карт меньше количества комбинаций * минимальную вместимость комбинации!");
        }

        GenerateCombinations(numberOfCombinations, totalCards);
    }

    private void GenerateCombinations(int numberOfCombinations, int totalCards)
    {
        List<List<int>> combinations = new List<List<int>>();
        int remainingCards = totalCards; 

        for (int i = 0; i < numberOfCombinations; i++)
        {
            int remainingCombinations = numberOfCombinations - i;

            int maxCombinationSize = Math.Min(maxCombination, remainingCards - (remainingCombinations - 1) * minCombination);

            int combinationSize = (i == numberOfCombinations - 1) ? remainingCards : UnityEngine.Random.Range(minCombination, maxCombinationSize + 1);

            List<int> combination = GenerateSingleCombination(combinationSize);
            combinations.Add(combination);

            remainingCards -= combinationSize;
        }

        for (int i = 0; i < combinations.Count; i++)
        {
            for (int b = 0; b < combinations[i].Count; b++)
            {
                if (b < combinations[i].Count - 1)
                {
                    AddCardNumber(combinations[i][b]);
                }
                else if(i < combinations.Count -1)
                {
                    bankDeck.AddCard(combinations[i][b]);
                }
                else
                {
                    comboLauncher.SetStartCard(combinations[i][b]);
                }
            }
        }
    }
    private void AddCardNumber(int number)
    {
        if (fieldDeckList != null)
        {
            FieldDeck fieldDeck = fieldDeckList[UnityEngine.Random.Range(0, fieldDeckList.Count)];
            if (fieldDeck.AddCard(number)) return;
            else
            {
                fieldDeckList.Remove(fieldDeck);
                AddCardNumber(number);
            }
        }
        else
        {
            Debug.LogError("fieldDeckList is empty!");
        }
    }

    private List<int> GenerateSingleCombination(int combinationSize)
    {
        List<int> combination = new List<int>();

        int firstCard = UnityEngine.Random.Range(1, 14);
        combination.Add(firstCard);

        bool goingDown = UnityEngine.Random.Range(0f, 1f) < probabilityGoingDown;

        for (int i = 1; i < combinationSize; i++)
        {
            int lastCard = combination[combination.Count - 1];

            if (UnityEngine.Random.Range(0f, 1f) < probabilitySwitching)
            {
                goingDown = !goingDown;
            }

            int nextCard;
            if (goingDown)
            {
                nextCard = (lastCard == 13) ? 1 : lastCard + 1;
            }
            else
            {
                nextCard = (lastCard == 1) ? 13 : lastCard - 1;
            }

            combination.Add(nextCard);
        }

        return combination;
    }
}
