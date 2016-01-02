using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour
{
    public UnityEngine.UI.Text GameHUDScoreText;
    public UnityEngine.UI.Text GameOverScoreText;
    public HealthBar _HealthBar;

    private Animator GameOverAnimator;

    void Start ()
    {
        GameOverAnimator = GetComponent<Animator>();
	}
	
    public void UpdateScore(int score)
    {
        GameHUDScoreText.text = score.ToString();
        GameOverScoreText.text = string.Format("SCORE: {0}", score);
    }

    public void ShowGameOverScreen()
    {
        GameOverAnimator.SetTrigger("GameOver");
    }

    public void ResetGameOverScreen()
    {
        GameOverAnimator.Play("Idle");
    }
}
