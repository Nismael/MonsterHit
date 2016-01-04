using UnityEngine;
using System.Collections;

public class ScreenHitFx : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Debug.Log("PlayingFX");
        ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
        ps.Play();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
