using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionController : MonoBehaviour
{
    [Tooltip("Animation Settings")]
    [SerializeField] private CanvasGroup blackPanel;
    [SerializeField] private float timeShowOrHide = 0.5f;
    [SerializeField] private CanvasGroup gameOwerPanel;
    [SerializeField] private Text gameOverText;
    [Tooltip("Session Settings")]
    [SerializeField] private List<FieldDeck> fieldDeckList;
    [SerializeField] private BankDeck bankDeck;
    private void OnEnable()
    {
        blackPanel.alpha = 1;
        blackPanel.DOFade(0, timeShowOrHide);
        CardAnimation.OnCompleteMoving += CheckSession;
    }
    private void OnDisable()
    {
        CardAnimation.OnCompleteMoving -= CheckSession;
    }
    private void CheckSession()
    {
        bool isWin = false;
        foreach (FieldDeck field in fieldDeckList)
        {
            isWin = field.CheckIsWin();
            if (!isWin) break;
        }
        if (isWin)
        {
            gameOverText.text = "You've won!";
            ShowGameOverPanel();
        }
        else
        {
            bool isGameContinues = false;
            foreach (FieldDeck field in fieldDeckList)
            {
                isGameContinues = field.CheckCard();
                if (isGameContinues) break;
            }
            if(!isGameContinues) isGameContinues = bankDeck.CheckCard();
            if (!isGameContinues)
            {
                gameOverText.text = "You've lost";
                ShowGameOverPanel();
            }
        }
    }
    private void ShowGameOverPanel()
    {
        gameOwerPanel.DOFade(1, timeShowOrHide).OnComplete(() =>
        {
            gameOwerPanel.interactable = true;
            gameOwerPanel.blocksRaycasts = true;
        });
    }
    public void RestartGame()
    {
        gameOwerPanel.interactable = false;
        gameOwerPanel.blocksRaycasts = false;
        blackPanel.DOFade(1, timeShowOrHide).OnComplete(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }
}
