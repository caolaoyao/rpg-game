using UnityEngine;
using System.Collections;

public class EnemyGui : MonoBehaviour {
    //主摄像机对象
    private Camera camera;
    //敌人名称
    private string name = "敌人";

    //主角对象
    GameObject hero;
    //敌人本身
    GameObject enemy;
    //敌人模型高度
    float npcHeight;
    //红色血条贴图
    public Texture2D blood_red;
    //黑色血条贴图
    public Texture2D blood_black;

    void Start()
    {
        //根据Tag得到主角对象
        hero = GameObject.FindGameObjectWithTag("Player");
        //取出父对象
        enemy = transform.parent.gameObject;
        //得到摄像机对象
        camera = Camera.main;

        //注解1
        //得到模型原始高度
        float size_y = collider.bounds.size.y;
        //得到模型缩放比例
        float scal_y = transform.localScale.y;
        //它们的乘积就是高度
        npcHeight = (size_y * scal_y);
    }

    void Update()
    {
        //保持NPC一直面朝主角
        transform.LookAt(hero.transform);
    }

    void OnGUI()
    {
        //得到敌人头顶在3D世界中的坐标
        //默认敌人坐标点在脚底下，所以这里加上npcHeight它模型的高度即可 
        Vector3 worldPosition = new Vector3(transform.position.x, transform.position.y + npcHeight, transform.position.z);
        //根据敌人头顶的3D坐标换算成它在2D屏幕中的坐标
        Vector2 position = camera.WorldToScreenPoint(worldPosition);
        //得到真实敌人头顶的2D坐标
        position = new Vector2(position.x, Screen.height - position.y);

        //计算出血条的宽高
        Vector2 bloodSize = GUI.skin.label.CalcSize(new GUIContent(blood_red));

        //通过血值计算红色血条显示区域
        int blood_width = blood_red.width * enemy.GetComponent<Enemy>().cur_life / enemy.GetComponent<Enemy>().max_life;
        //先绘制黑色血条
        GUI.DrawTexture(new Rect(position.x - (bloodSize.x / 2), position.y - bloodSize.y, bloodSize.x, 3), blood_black);

        //在绘制红色血条
        GUI.DrawTexture(new Rect(position.x - (bloodSize.x / 2), position.y - bloodSize.y, blood_width, 3), blood_red);

        //计算敌人名称的宽高
        Vector2 nameSize = GUI.skin.label.CalcSize(new GUIContent(enemy.GetComponent<Enemy>().name));
        //设置显示颜色为黄色
        GUI.color = Color.yellow;
        //绘制敌人名称
        GUI.Label(new Rect(position.x - (nameSize.x / 2), position.y - nameSize.y - bloodSize.y, nameSize.x, nameSize.y), enemy.GetComponent<Enemy>().name);
    }
}
