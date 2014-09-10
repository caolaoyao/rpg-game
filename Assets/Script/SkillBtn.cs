using UnityEngine;
using System.Collections;

public class SkillBtn : MonoBehaviour {
    PlayerManager playerManager;
	// Use this for initialization
	void Start () {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
	}
	
    void OnClick()
    {
        Debug.Log(gameObject.name);
        playerManager.SendMessage(gameObject.name, SendMessageOptions.DontRequireReceiver);
    }
}
