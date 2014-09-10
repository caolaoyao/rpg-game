using UnityEngine;
using System.Collections;

public class EnemySwap : MonoBehaviour {
    public GameObject enemy_prefab;
    float delay = 0f;
    int enemy_count = 0;
    GameObject enemy;
	// Update is called once per frame
	void Update () {
        delay -= Time.deltaTime;
        if (delay < 0)
        {
            delay = 0;
        }

        if (delay <= 0 && enemy_count < 1)
        {
            delay = 5f;
            enemy_count += 1;
            enemy = Instantiate(enemy_prefab, transform.position, transform.rotation) as GameObject;
        }

        if (enemy_count > 0)
        {
            if (!enemy)
            {
                enemy_count = 0;
            }
        }
	}
}
