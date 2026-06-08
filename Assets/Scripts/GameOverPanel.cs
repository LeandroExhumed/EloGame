using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Vector2 finalPosition;
    [SerializeField]
    private TextMeshProUGUI currentScoreText;
    [SerializeField]
    private TextMeshProUGUI highestScoreText;

    [SerializeField]
    private float duration = 1f;

    private Vector2 defaultPosition;

    private void Awake()
    {
        defaultPosition = rectTransform.anchoredPosition;
    }

    public void Open(int currentScore, int highestScore)
    {
        currentScoreText.text = currentScore.ToString();
        highestScoreText.text = highestScore.ToString();

        rectTransform.DOAnchorPos(finalPosition, duration).SetEase(Ease.OutBounce);
    }

    public void Close()
    {
        rectTransform.DOAnchorPos(defaultPosition, duration);
    }
}