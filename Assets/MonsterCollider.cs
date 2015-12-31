using UnityEngine;
using System.Collections;

public class MonsterCollider : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Gremlin>().Attack();
    }
}
