using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private float duration = 1f;

    public void Open()
    {
        rectTransform.DOAnchorPos(Vector2.zero, duration).SetEase(Ease.OutBounce);
    }

    public void SetScoreText(string text)
    {
        scoreText.text = text;
    }
}