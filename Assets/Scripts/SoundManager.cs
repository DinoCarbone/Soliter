using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource bankCard;
    [SerializeField] private AudioSource correct;
    [SerializeField] private AudioSource wrong;
    public static SoundManager Instance;
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
    public void PlayBankCard()
    {
        bankCard.Play();
    }
    public void PlayCorrect()
    {
        correct.Play();
    }
    public void PlayWrong()
    {
        wrong.Play();
    }
}
