using UnityEngine;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject menuPanel;     // Assign the Canvas/Panel that holds the menu
    public Text titleText;            // Optional: set to game name
    public Text levelText;            // Shows Easy/Hard
    public Image characterImage;      // Sprite for selected character
    public Sprite easySprite;
    public Sprite hardSprite;

    private void Start()
    {
        UpdateLevelUI();
        if (menuPanel != null) menuPanel.SetActive(true);
        if (GameManager.Instance != null) GameManager.Instance.ResetGame();
    }

    public void OnSelectLevel()
    {
        if (GameManager.Instance == null) return;
        var current = GameManager.Instance.SelectedDifficulty;
        var next = current == Difficulty.Easy ? Difficulty.Hard : Difficulty.Easy;
        GameManager.Instance.SetDifficulty(next);
        UpdateLevelUI();
    }

    public void OnPlay()
    {
        if (menuPanel != null) menuPanel.SetActive(false);
        if (GameManager.Instance != null) GameManager.Instance.StartGame();
        // Optionally reset time scale if you paused in menu; not needed if menu is overlay.
    }

    private void UpdateLevelUI()
    {
        if (GameManager.Instance == null) return;
        var diff = GameManager.Instance.SelectedDifficulty;
        if (levelText != null) levelText.text = diff == Difficulty.Easy ? "Level 1: Easy" : "Level 2: Hard";
        if (characterImage != null)
        {
            characterImage.sprite = diff == Difficulty.Easy ? easySprite : hardSprite;
        }
    }
}
