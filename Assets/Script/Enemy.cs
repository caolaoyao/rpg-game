using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour { 
    //敌人名称
    public string name = "敌人";

    
    PlayerManager playerManager;
  
    //默认敌人血值
    public int cur_life = 60;
    public int max_life = 60;

    //敌人攻击
    public int attack = 20;

    //攻击延迟
    public float attack_delay = 0f;
    public float attack_max_delay = 2f;

    //敌人防御
    public int defense = 10;

    //杀死这个敌人可以获得的经验
    public int get_exp = 100;

    //是否已经死亡
    public bool isDead = false;
	

    //设置敌人属性
    public void setEnemyAttribute(int c_life, int m_life, int atk, float m_delay, string nm, int defs, int exp)
    {
        cur_life = c_life;
        max_life = m_life;
        attack = atk;
        attack_max_delay = m_delay;
        name = nm;
        defense = defs;
        get_exp = exp;
    }

    void Awake()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
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
                playerManager.Add_exp(get_exp);
                GameObject.Destroy(gameObject, 2);
            }
        }
    }
}
