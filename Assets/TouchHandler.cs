using UnityEngine;
using System.Collections.Generic;

public class TouchHandler : MonoBehaviour
{
    public GameObject TouchFx;
    private List<GameObject> Particles = new List<GameObject>();

	// Use this for initialization
	void Start()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = Particles.Count-1; i >= 0; --i)
        {
            GameObject go = Particles[i];
            ParticleSystem ps = go.GetComponent<ParticleSystem>();
            if (!ps.isPlaying)
            {
                Destroy(go);
                Particles.Remove(go);
            }
        }
        foreach (GameObject go in Particles)
        {
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            var touchFx = (GameObject)Instantiate(TouchFx, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            Particles.Add(touchFx);
        }
        else if (Input.touchCount != 0)
        {
            var touchFx = (GameObject)Instantiate(TouchFx, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Quaternion.identity);
            Particles.Add(touchFx);
        }

	}
}
