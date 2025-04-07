using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Label scoreLabel;
    private VisualElement gameOverPanel;
    private Label finalScoreLabel;
    private Label highScoreLabel;
    private Button restartButton;

    void Awake()
    {
        Instance = this;
        var root = GetComponent<UIDocument>().rootVisualElement;

        scoreLabel = root.Q<Label>("ScoreLabel");
        gameOverPanel = root.Q<VisualElement>("GameOverPanel");
        finalScoreLabel = root.Q<Label>("FinalScoreLabel");
        highScoreLabel = root.Q<Label>("HighScoreLabel");
        restartButton = root.Q<Button>("RestartButton");

        restartButton.clicked += () => ScoreManager.Instance.RestartGame();
    }

    void Update()
    {
        scoreLabel.text = "Score: " + ScoreManager.Instance.score;
    }

    public void ShowGameOverUI(int finalScore, int highScore)
    {
        gameOverPanel.style.display = DisplayStyle.Flex;
        finalScoreLabel.text = $"Final Score: {finalScore}";
        highScoreLabel.text = $"High Score: {highScore}";
    }
}
