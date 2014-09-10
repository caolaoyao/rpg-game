using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
    PlayerManager playerManager;
    float speed = 0.1f;
    float delay = 1f;

    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    void Update()
    {
        delay -= Time.deltaTime;
        if (delay < 0)
        {
            transform.Translate(Vector3.forward * speed);
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "enemy")
        {
            StartCoroutine(TakeDamaged(1f, other, playerManager.attack * 8));
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
