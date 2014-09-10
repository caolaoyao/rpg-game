using UnityEngine;
using System.Collections;

public class Mask : MonoBehaviour {
    UISprite uis;
    PlayerManager playerManager;
	// Use this for initialization
	void Start () {
        uis = GetComponent<UISprite>();
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.parent.gameObject.name == "Skill_1")
        {
            uis.fillAmount = playerManager.skill_1_delay / playerManager.skill_1_max_delay;
        }

        else if (transform.parent.gameObject.name == "Skill_2")
        {
            uis.fillAmount = playerManager.skill_2_delay / playerManager.skill_2_max_delay;
        }
	}
}
