using UnityEngine;
using System.Collections;
using System;

public class Controller : MonoBehaviour
{
    public AudioSource workVoice;

    float speed = 10.0f;
    Transform m_transform;
    CharacterController m_controller;

    float Quaternion_Y;
    float touchKey_x = 0;
    float touchKey_y = 0;
    Vector3 transformValue = Vector3.zero;

    float distance = 7;

    //记录上一次手机触摸位置判断用户是在左放大还是缩小手势
    Vector2 oldPosition1;
    Vector2 oldPosition2;    

    void Start()
    {
        m_controller = GetComponent<CharacterController>();
        m_transform = this.transform;
    }

    void Update()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + distance - 1, transform.position.z - distance);

        //判断触摸数量为多点触摸
        if (Input.touchCount > 1)
        {
            //前两只手指触摸类型都为移动触摸
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                //计算出当前两点触摸点的位置
                var tempPosition1 = Input.GetTouch(0).position;
                var tempPosition2 = Input.GetTouch(1).position;
                //函数返回真为放大，返回假为缩小
                if (isEnlarge(oldPosition1, oldPosition2, tempPosition1, tempPosition2))
                {
                    if (distance > 4)
                        distance -= 0.1f;

                    Camera.main.transform.Translate(Vector3.forward * -0.1f);

                }
                else
                {
                    if (distance < 7)
                        distance += 0.1f;

                }
                //备份上一次触摸点的位置，用于对比
                oldPosition1 = tempPosition1;
                oldPosition2 = tempPosition2;
            }
        }

        if (animation.IsPlaying("SpellCastA"))
        {
            return;
        }
        if (animation.IsPlaying("MagicShotStraight"))
        {
            return;
        }

        if (touchKey_y == 0 && touchKey_x == 0)
        {
            touchKey_y = Input.GetAxis("Vertical");
            touchKey_x = Input.GetAxis("Horizontal");
        }
        

        if ((touchKey_x != 0 || touchKey_y != 0))
        {
            Quaternion_Y = float.Parse(Math.Atan(touchKey_y / touchKey_x) * 180 / Math.PI + "");

            if (touchKey_x > 0 && touchKey_y > 0)
            {

            }
            else if (touchKey_x < 0 && touchKey_y > 0)
            {
                Quaternion_Y = Quaternion_Y + 180;
            }
            else if (touchKey_x < 0 && touchKey_y < 0)
            {
                Quaternion_Y = Quaternion_Y + 180;
            }
            else if (touchKey_x > 0 && touchKey_y < 0)
            {
                Quaternion_Y = Quaternion_Y + 360;
            }

            transform.rotation = Quaternion.Euler(0.0f, -Quaternion_Y + 90, 0.0f);

            if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0)
            {
                transform.rotation = Quaternion.Euler(0.0f, -Quaternion_Y - 90, 0.0f);
            }

            transformValue.x = touchKey_x * Time.deltaTime * 4;

            transformValue.z = touchKey_y * Time.deltaTime * 4;
        }
        else
        {

            transformValue.x = 0 * Time.deltaTime * 4;

            transformValue.z = 0 * Time.deltaTime * 4;
        }

        if (transformValue.x == 0 && transformValue.z == 0)
        {
            if (animation.IsPlaying("Run"))
                animation.Play();

            workVoice.Stop();
        }
        else
        {

            animation.Play("Run");
            if (!workVoice.isPlaying)
            {
                workVoice.Play();
            }
        }

        m_controller.Move(transformValue);
    }

    //函数返回真为放大，返回假为缩小
    bool isEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        //函数传入上一次触摸两点的位置与本次触摸两点的位置计算出用户的手势
        var leng1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        var leng2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));
        if (leng1 < leng2)
        {
            //放大手势
            return true;
        }
        else
        {
            //缩小手势
            return false;
        }
    }

    void OnEnable()
    {
        EasyJoystick.On_JoystickMove += OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd; 
    }

    void OnDisable()
    {
        EasyJoystick.On_JoystickMove -= OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd; 
    }

    void OnDestroy()
    {
        EasyJoystick.On_JoystickMove -= OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd; 
    }

    void OnJoystickMove(MovingJoystick move)
    {
        touchKey_y = move.joystickAxis.y;
        touchKey_x = move.joystickAxis.x;
    }

    void OnJoystickMoveEnd(MovingJoystick move)
    {
        touchKey_y = move.joystickAxis.y;
        touchKey_x = move.joystickAxis.x;
    }
}
