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
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private float duration = 1f;

    private Vector2 defaultPosition;

    private void Awake()
    {
        defaultPosition = rectTransform.anchoredPosition;
    }

    public void Open()
    {
        rectTransform.DOAnchorPos(finalPosition, duration).SetEase(Ease.OutBounce);
    }

    public void SetScoreText(string text)
    {
        scoreText.text = text;
    }

    public void Close()
    {
        rectTransform.DOAnchorPos(defaultPosition, duration);
    }
}