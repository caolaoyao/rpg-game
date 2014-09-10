using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
    [HideInInspector]
    public int max_life = 500; //最大生命值
    [HideInInspector]
    public int cur_life = 500; //当前生命值

    [HideInInspector]
    public int max_magic = 200; //最大魔法值
    [HideInInspector]
    public int cur_magic = 200; //当前魔法值

    //升级要的经验
    [HideInInspector]
    public int max_exp = 200;
    //当的的经验
    [HideInInspector]
    public int cur_exp = 100;

    //等级
    [HideInInspector]
    public int level = 1;

    public int attack = 100; //攻击力
    //攻击延迟
    [HideInInspector]
    public float attack_delay = 2f;

    //防御
    [HideInInspector]
    public int defense = 20;

    //玩家是否已经死亡
    public bool isDead = false;

    //Skill_1的特效和攻击延迟
    public GameObject skill_1_particle;
    public float skill_1_delay = 0;
    public float skill_1_max_delay = 2f;

    //Skill_2的特效和攻击延迟
    public GameObject skill_2_particle;
    public float skill_2_delay = 0;
    public float skill_2_max_delay = 2f;

    //升级特效
    public GameObject level_up_particle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        skill_1_delay -= Time.deltaTime;
        if (skill_1_delay < -5)
        {
            skill_1_delay = 0;
        }

        skill_2_delay -= Time.deltaTime;
        if (skill_2_delay < -5)
        {
            skill_2_delay = 0;
        }
	}

    public void Skill_1()
    {
        if (animation.IsPlaying("MagicShotStraight"))
        {
            return;
        }

        skill_1_delay = skill_1_max_delay;

        GameObject skill_1_show = Instantiate(skill_1_particle, transform.position + Vector3.up, transform.rotation) as GameObject;
        Destroy(skill_1_show, 4);
        animation.Play("MagicShotStraight");
    }

    public void Skill_2()
    {
        if (animation.IsPlaying("FireBallSpell"))
        {
            return;
        }

        skill_2_delay = skill_2_max_delay;

        GameObject skill_2_show = Instantiate(skill_2_particle, transform.position, Quaternion.identity) as GameObject;
        skill_2_show.transform.Rotate(-90, 0, 0);
        Destroy(skill_2_show, 4);
        animation.Play("FireBallSpell");
    }

    public void Add_exp(int exp)
    {
        cur_exp += exp;
        if (cur_exp >= max_exp)
        {
            Level_up();
        }
    }

    void Level_up()
    {
        max_life += 100;
        cur_life = max_life;

        cur_exp = 0;
        max_exp += 100;

        attack += 50;

        GameObject level_up_show = Instantiate(level_up_particle, transform.position, Quaternion.identity) as GameObject;
        Destroy(level_up_show, 2);
    }

    public void Damaged(int damage)
    {
        if (cur_life > 0)
        {
            int realAttack = (int)Random.Range(damage - damage * 0.25f, damage + damage * 0.25f);
            if (realAttack - defense > 0)
            {
                cur_life -= (realAttack - defense);
            }
            else
            {
                cur_life -= 1;
            }

            if (cur_life <= 0)
            {
                cur_life = 0;
                isDead = true;
            }
        }
    }
}
