using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CardSO : ScriptableObject
{
    [SerializeField] private List<SuitSO> suitSOlist;
    [SerializeField] private string[] numberArray = new string[13];
    public (Color, string,Sprite) GetColorStringAndSprite(int number)
    {
        SuitSO suitSO = suitSOlist[Random.Range(0, suitSOlist.Count)];
        string text = numberArray[number-1];
        Sprite sprite = suitSO.sprite;
        Color color = suitSO.color;

        return (color, text, sprite);
    }
}
