using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]//针对相机的操纵
public class fps_FPCamera : MonoBehaviour {
    public Vector2 mouseLookSensitivity = new Vector2(5, 5);//调整鼠标灵敏度
    public Vector2 rotationXLimit = new Vector2(87, -87);//相机旋转的垂直方向的角度范围
    public Vector2 rotationYLimit = new Vector2(-360, 360);//相机旋转的水平方向的角度范围
    public Vector3 positionOffset = new Vector3(0, 2,-0.2f);//
    private Vector2 currentMouseLook = Vector2.zero;
    private float x_Angle = 0;//x轴旋转角度默认为0
    private float y_Angle = 0;
    private fps_PlayerParameter parameter;//获取鼠标的输入
    private Transform m_Transform;//避免重复调用
    private void Start()
    {
        parameter = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<fps_PlayerParameter>();
        m_Transform = transform; 
        m_Transform.localPosition = positionOffset;
    }
    private void Update()
    {
        UpdateInput();
    }
    private void LateUpdate()//旋转处理
    {
        Quaternion xQuaternion = Quaternion.AngleAxis(y_Angle, Vector3.up);//对应y轴的旋转
        Quaternion yQuaternino = Quaternion.AngleAxis(0, Vector3.left);//对应x轴的旋转
        m_Transform.parent.rotation = xQuaternion * yQuaternino;
        yQuaternino = Quaternion.AngleAxis(-x_Angle, Vector3.left);
        m_Transform.rotation = xQuaternion * yQuaternino;
    }
    private void UpdateInput()
    {
        if (parameter.inputSmoothLook == Vector2.zero)//表示没有用户输入
            return;
        GetMouseLock();//用户有输入获取用户输入
        y_Angle += currentMouseLook.x;//前者的y表示y轴的旋转，后者的x表示鼠标的输入轴向，鼠标的轴向对应视图中y轴的旋转
        x_Angle += currentMouseLook.y;
        y_Angle = y_Angle < -360 ? y_Angle += 360 : y_Angle;
        y_Angle = y_Angle > 360 ? y_Angle -= 360 : y_Angle;//保证y轴的旋转保持在一定范围内
        y_Angle = Mathf.Clamp(y_Angle, rotationYLimit.x, rotationYLimit.y);//
        x_Angle = x_Angle < -360 ? x_Angle += 360 : x_Angle;
        x_Angle = x_Angle > 360 ? x_Angle -= 360 : x_Angle;
        x_Angle = Mathf.Clamp(x_Angle, -rotationXLimit.x, -rotationXLimit.y);
    }
    private void GetMouseLock (){//对用户输入进行处理
        currentMouseLook.x = parameter.inputSmoothLook.x;
        currentMouseLook.y = parameter.inputSmoothLook.y;
        currentMouseLook.x *= mouseLookSensitivity.x;
        currentMouseLook.y *= mouseLookSensitivity.y;
        currentMouseLook.y *= -1;//修改y轴的方向
    }
}

