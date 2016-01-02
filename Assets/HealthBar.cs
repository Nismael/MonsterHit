using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    private UnityEngine.UI.Image HealthImage;
    private Vector2 InitialSize;

	void Start ()
    {
        HealthImage = transform.Find("Health").GetComponent<UnityEngine.UI.Image>();
        InitialSize = HealthImage.rectTransform.sizeDelta;
    }
	
    public void UpdateHealth(float healthRatio)
    {
        HealthImage.rectTransform.sizeDelta = new Vector2(InitialSize.x * healthRatio, InitialSize.y);
    }
}
