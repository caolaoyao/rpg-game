using UnityEngine;
using System.Collections;

public class Magic : MonoBehaviour {
    PlayerManager playerManager;

    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "enemy")
        {
            StartCoroutine(TakeDamaged(1f, other, playerManager.attack * 5));
        }
    }

    IEnumerator TakeDamaged(float timer, Collider other, int damaged)
    {
        yield return new WaitForSeconds(timer);
        if (other)
        {
            other.gameObject.GetComponent<EnemyFox>().Damaged(damaged);
        }
    }
}
