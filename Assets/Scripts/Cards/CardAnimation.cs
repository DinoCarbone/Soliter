using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CardAnimation : MonoBehaviour
{
    [SerializeField] private Card card;
    [SerializeField] private GameObject activeSide;
    [SerializeField] private float timeRotate = 0.5f;
    [SerializeField] private float timeMoving = 1f;

    private Image mainCardImage;
    public static Action OnCompleteMoving;
    private void OnEnable()
    {
        mainCardImage = GetComponent<Image>();
        activeSide.SetActive(false);
        card.OnActive += ActiveSelf;
        card.OnDetachCard += MoveCard;
    }
    private void OnDisable()
    {
        card.OnActive -= ActiveSelf;
        card.OnDetachCard -= MoveCard;
    }
    private void ActiveSelf()
    {
        transform.DORotate(new Vector3(0, 90, 0), timeRotate).OnComplete(() =>
        {
            mainCardImage.enabled = false;
            activeSide.SetActive(true);
            transform.DORotate(Vector3.zero, timeRotate);
        });
    }
    private void MoveCard(Card previousCard)
    {
        Transform parent = ComboLauncher.Instance.transform;
        transform.SetParent(parent);
        transform.DOMove(parent.position, timeMoving).OnComplete(() =>
        {
            previousCard.gameObject.SetActive(false);
            OnCompleteMoving?.Invoke();
        });
    }
}
