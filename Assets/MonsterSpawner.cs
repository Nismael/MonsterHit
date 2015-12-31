using UnityEngine;
using System.Collections.Generic;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject Monster;
    
    private List<GameObject> monsters = new List<GameObject>();

    private const float SpawnRate = 1.0f;
    private float SpawnTimer = 0;

    void Awake()
    {
     //   Debug.Break();
    }

    void Start ()
    {
        SpawnTimer = 0;
    }
	
	void Update ()
    {
        SpawnTimer += Time.deltaTime;
        if (SpawnTimer >= SpawnRate)
        {
            SpawnTimer = 0;

            //todo : use bounds not hardcoded values
            var pos = Mathf.Lerp(-5, 5, Random.value);

            var monsterInstance = (GameObject)Instantiate(Monster, new Vector3(pos, 5, 0), Quaternion.identity);
            var Gremlin = monsterInstance.GetComponent<Gremlin>();

            Gremlin.Speed = Mathf.Lerp(2, 4, Random.value);
            Gremlin.Damage = 10;
            Gremlin.OnAttackCompleted += OnMonsterAttackFinished;
            Gremlin.Walk();
            monsters.Add(monsterInstance);
        }
    }

    public void OnMonsterAttackFinished(GameObject monster)
    {
        if (monsters.Find(x => x == monster))
        {
            Destroy(monster);
            monsters.Remove(monster);
        }
    }
}
