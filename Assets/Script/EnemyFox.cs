using UnityEngine;
using System.Collections;

public class EnemyFox : Enemy{
    public enum FoxSatus
    {
        Run,
        BeAttak,
        Attak,
        Rest,
        Dead
    }

    //玩家的位置
    Transform transform_Player;

    //玩具
    GameObject player;

    //自动寻路
    NavMeshAgent navmeshagent;

    //狐狸的旧位置
    Vector3 posOriginal;

    //狐狸的状态
    [HideInInspector]
    public FoxSatus foxSatus;

    AudioSource miao;

    //自动寻路的坐标点
    Vector3 transform_Player_fix = new Vector3();
    bool flag = true;

	// Use this for initialization
	void Start () {
        foxSatus = FoxSatus.Rest;

        navmeshagent = gameObject.GetComponent<NavMeshAgent>();

        posOriginal = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        navmeshagent.speed = 2;

        miao = GetComponent<AudioSource>();

        player = GameObject.FindWithTag("Player");

        transform_Player = player.transform;

        setEnemyAttribute(600, 600, 20, 2.0f, "狐狸", 10, 50);
	}
	
	// Update is called once per frame
	void Update () {
        attack_delay -= Time.deltaTime;
        if (attack_delay < 0)
        {
            attack_delay = 0;
        }

        if (isDead)
        {
            foxSatus = FoxSatus.Dead;
            navmeshagent.Stop();
        }
        else
        {
            transform_Player_fix.x = transform_Player.position.x + 1.4f;
            transform_Player_fix.y = transform_Player.position.y;
            transform_Player_fix.z = transform_Player.position.z + 1.4f;
            float distance = Vector3.Distance(transform.position, transform_Player.position);

            if (distance < 15)
            {
                if (distance < 2.4)
                {
                    navmeshagent.Stop();
                    transform.LookAt(transform_Player);
                    if (attack_delay <= 0)
                    {
                        foxSatus = FoxSatus.Attak;
                        attack_delay = attack_max_delay;
                    }
                    else
                    {
                        foxSatus = FoxSatus.Rest;
                    }
                }
                else
                {
                    if (flag)
                    {
                        miao.Play();
                        flag = false;
                    }
                    navmeshagent.SetDestination(transform_Player_fix);
                    foxSatus = FoxSatus.Run;
                }
            }

            else
            {
                navmeshagent.SetDestination(posOriginal);
                if (Vector3.Distance(transform.position, posOriginal) < 1)
                {
                    foxSatus = FoxSatus.Rest;
                }
            }
        }

        switch (foxSatus)
        {
            case FoxSatus.Run:
                animation.Play("Run");
                break;
            case FoxSatus.BeAttak:
                animation.Play("");
                break;
            case FoxSatus.Attak:
                if (player.GetComponent<PlayerManager>().isDead)
                {
                    animation.Play("Rest");
                }
                else
                {
                    animation.Play("Attak");
                    player.GetComponent<PlayerManager>().Damaged(attack);
                }               
                break;

            case FoxSatus.Dead:
                animation.Play("Dead");
                break;
        }

        if (!animation.isPlaying)
        {
            animation.Play("Rest");
        }
	}
}
