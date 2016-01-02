using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour
{
    public UnityEngine.UI.Text GameHUDScoreText;
    public UnityEngine.UI.Text GameOverScoreText;

    private Animator animator;

    void Start ()
    {
        animator = GetComponent<Animator>();
	}
	
    public void UpdateScore(int score)
    {
        GameHUDScoreText.text = score.ToString();
        GameOverScoreText.text = string.Format("SCORE: {0}", score);
    }

    public void ShowGameOverScreen()
    {
        Debug.Log("ShowGameOverScreen");
        animator.SetTrigger("GameOver");
    }

    public void ResetGameOverScreen()
    {
        animator.Play("Idle");
    }
}
