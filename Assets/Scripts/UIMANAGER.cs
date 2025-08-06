using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UIMANAGER : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelCounter;

    [SerializeField] private RectTransform restartButton;

    private const string PlayCountKey = "PlayCount";

    void Awake()
    {
        UpdatePlayCount();
        UpdateLevelCounterUI();
    }

    public void RestartScene()
    {
        restartButton
            .DORotate(new Vector3(0, 0, 360), 0.75f, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutBack)
            .OnComplete(() =>
            {
                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentScene.buildIndex);
            });
    }

    private void UpdatePlayCount()
    {

        int playCount = PlayerPrefs.GetInt(PlayCountKey, 0);
        playCount++;
        PlayerPrefs.SetInt(PlayCountKey, playCount);
        PlayerPrefs.Save();
    }

    private void UpdateLevelCounterUI()
    {
        if (levelCounter != null)
        {
            int playCount = PlayerPrefs.GetInt(PlayCountKey, 1);
            levelCounter.text = $"Level {playCount}";
        }
    }
}
