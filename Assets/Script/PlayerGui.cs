using UnityEngine;
using System.Collections;

public class PlayerGui : MonoBehaviour {
    Transform life, exp;
    UISlider lifeValue, expValue;
    UILabel lifeLabel, expLabel;
    PlayerManager playerManager; 

    void Start()
    {
        life = transform.FindChild("life");
        exp = transform.FindChild("exp");
        lifeValue = life.gameObject.GetComponent<UISlider>();
        expValue = exp.gameObject.GetComponent<UISlider>();
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        lifeLabel = life.gameObject.transform.FindChild("Label").GetComponent<UILabel>();
        expLabel = exp.gameObject.transform.FindChild("Label").GetComponent<UILabel>();
    }

    void Update()
    {
        lifeValue.value = (float)playerManager.cur_life / (float)playerManager.max_life;
        expValue.value = (float)playerManager.cur_exp / (float)playerManager.max_exp;

        lifeLabel.text = playerManager.cur_life + " / " + playerManager.max_life;
        expLabel.text = playerManager.cur_exp + " / " + playerManager.max_exp;
    }
}
