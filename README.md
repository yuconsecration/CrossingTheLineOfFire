*~~<font color=red>@2019072103d</font>~~ *
# 1.导入所需的资源
> 链接：https://pan.baidu.com/s/1kLnu28aPiZ7ak6j4rADqsw  提取码：m71u
# 2.项目开发
## 2.1标签的封装
> 创建一个名为Tags的脚本，将项目中所有将要使用的标签以const常量的方式进行分装。
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags{
    public const string player = "Player";//代表玩家
    public const string gameController = "GameController";//代表游戏控制器
    public const string enemy = "Enemy";//代表敌人
    public const string fader = "Fader";//代表淡入淡出的画布
    public const string mainCamera = "MainCamera";//代表主摄像机
}
```
## 2.2实现屏幕淡入淡出的效果
> 1.在场景面板中创建一个空物体，命名为FadeInOut，加入组件GUI Texture，color设置为黑色，Texture设置为swatch_black_dff(在导入的资源中搜索)
> 2.为物体FadeInOut创建一个脚本FadeInOut用来实现屏幕的渐隐渐现

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//SceneManager使用的命名空间

public class FadeInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;//设置渐隐渐现的速度
    private bool sceneStarting = true;//设置场景开始，当场景开始时实现渐隐渐现的效果
    private GUITexture tex;//设置一个GUITexture类型的组件名为tex
    private void Start()
    {
        tex = this.GetComponent<GUITexture>();//给组件进行赋值
        tex.pixelInset = new Rect(0, 0, Screen.width, Screen.height);//给组件初始化，位置为0，0,大小和屏幕一致
    }
    private void Update()
    {
        if (sceneStarting)
        {
            StartScene();//实现场景的渐现
        }
    }
    private void FadeToClear()//实现渐现
    {
        tex.color = Color.Lerp(tex.color, Color.clear, fadeSpeed * Time.deltaTime);//fadeSpeed * Time.deltaTime表示速度
    }
    private void FadeToBlack()//实现渐隐
    {
        tex.color = Color.Lerp(tex.color, Color.black, fadeSpeed * Time.deltaTime);//tex.color表示当前的颜色，Color.black表示渐变后的颜色
    }
    private void StartScene()//渐现开始
    {
        FadeToClear();//场景开始实现渐现效果
        if (tex.color.a <= 0.05f)//判断是否变为透明
        {
            tex.color = Color.clear;//将当前的颜色变为彻底透明
            tex.enabled = false;//禁用GUI组件
            sceneStarting = true;//将标识位设置为false
        }
    }
    public void EndScene()//渐隐开始
    {
        tex.enabled = true;
        FadeToBlack();
        if (tex.color.a >= 0.95f)
        {
            SceneManager.LoadScene("Demo");//进行场景跳转到当前场景，进行重复加载
        }

    }
}
```
## 2.3按键输入封装

> 在场景面板中创建一个空物体，命名为GameController，将其Tag设置为GameController，同时设置脚本fps_Input

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps_Input : MonoBehaviour {
    public class fps_InputAxis//自定义轴辅助类,存储自定义轴的信息
    {
        public KeyCode positive;//定义一个正向按键
        public KeyCode negative;//定义一个负向按键
    }
    public Dictionary<string, KeyCode> buttons = new Dictionary<string, KeyCode>();//定义一个集合
    public Dictionary<string, fps_InputAxis> axis = new Dictionary<string, fps_InputAxis>();//定义一个自定义轴集合
    public List<string> unityAxis = new List<string>();//定义一个unity内部轴的集合
    private void Start()
    {
        SetupDefault();
    }

    private void SetupDefault(string type = "")//设置默认的按键，type中的内容表示你要设置什么类型的东西，是按钮类型的、自定义轴还是uinty默认自带的轴
    {
        if(type == ""||type == "buttons")
        {
            if (buttons.Count == 0)//说明按钮没有进行过初始化
            {
                AddButton("Fire", KeyCode.Mouse0);//鼠标左键设置为开火键
                AddButton("Reload", KeyCode.R);
                AddButton("Jump", KeyCode.Space);
                AddButton("Crouch", KeyCode.C);//蹲下按键
                AddButton("Sprint", KeyCode.LeftShift);//加速按键
            }
        }
        if (type == "" || type == "Axis")//初始化自定义轴
        {
            if (axis.Count == 0)
            {
                AddAxis("Horizontal", KeyCode.W, KeyCode.S);
                AddAxis("Vertical", KeyCode.A, KeyCode.D);
            }
        }
        if (type == "" || type == "UnityAxis")//初始化unity自定义的轴
        {
            if (unityAxis.Count == 0)
            {
                AddUnityAxis("Mouse X");
                AddUnityAxis("Mouse Y");
                AddUnityAxis("Horizontal");
                AddUnityAxis("Vertical");
            }
        }
    }
    public void AddButton(string n,KeyCode k)//添加按钮的方法，两个参数表示按钮的名称和键位
    {
        if (buttons.ContainsKey(n))//判断按钮按键集合中是否包含该按钮
            buttons[n] = k;//包含则加入键位
        else
            buttons.Add(n, k);//不包含则加入这个按钮和键位
    }
    private void AddAxis(string n,KeyCode pk,KeyCode nk)//添加自定义轴的方法，三个参数代表自定义轴的名称，正方向和负方向
    {
        if (axis.ContainsKey(n))
        {
            axis[n] = new fps_InputAxis() { positive = pk, negative = nk };
        }
        else
        {
            axis.Add(n, new fps_InputAxis() { positive = pk, negative = nk });
        }
    }
    private void AddUnityAxis(string n)//添加unity自定义轴的方法
    {
        if (!unityAxis.Contains(n))
        {
            unityAxis.Add(n);
        }
    }
    //外界获取调用的方法
    public bool GetButton(string button)
    {
        if (buttons.ContainsKey(button))
        {
            return Input.GetKey(buttons[button]);//外界获取按钮的方法，只要按键没有被抬起就一直调用
        }
        return false;
    }
    public bool GetButtonDown(string button)
    {
        if (buttons.ContainsKey(button))
        {
            return Input.GetKeyDown(buttons[button]);//外界获取按钮按下的方法
        }
        return false;
    }
    public float GetAxis(string axis)//轴返回的值在-1到1之间，外界获取自定义轴的方法
    {
        if (this.unityAxis.Contains(axis))
        {
            return Input.GetAxis(axis);
        }
        else
            return 0;
    }
    public float GetAxisRaw(string axis)//这里只返回三个值，-1，0，1
    {
        if (this.axis.ContainsKey(axis))//判断自定义轴是否包含这个轴
        {
            float val = 0;
            if (Input.GetKey(this.axis[axis].positive))
                return 1;//按下正向键返回1
            if (Input.GetKey(this.axis[axis].negative))
                return -1;
            return val;
        }
        else if (unityAxis.Contains(axis))//如果自定义的轴不包含这个轴，就去unity自定义的轴去找
        {
            return Input.GetAxisRaw(axis);
        }
        else//都没有找到该轴
        {
            return 0;
        }
    }
}
```
## 2.4用户输入的参数封装及赋值
>1.创建一个空物体，作为我们的主角，重命名为FP_Player，将其位置设置为X：16.44，Z：-45.65，加入一个组件character controller(角色控制器)，将参数Center中的Y改为1，Radius改为0.4，后期所有的主角行为都通过角色控制器来操作。
>2.书写一个脚本表示用户的输入状态，命名为fps_PlayerParameter，将脚本拖动到FP_Player中

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class fps_PlayerParameter : MonoBehaviour
{
    [HideInInspector]//以下操作是通过用户的输入来操作，不需要显示在用户面板中，因此就隐藏属性
    public Vector2 inputSmoothLook;//定义一个二维向量表示鼠标的输入
    [HideInInspector]
    public Vector2 inputMoveVector;//定义一个二维向量表示按键的输入
    [HideInInspector]
    public bool inputCrouch;//是否下蹲
    [HideInInspector]
    public bool inputJump;//是否跳跃
    [HideInInspector]
    public bool inputSprint;//是否冲刺
    [HideInInspector]
    public bool inputFire;//是否开火
    [HideInInspector]
    public bool inputReload;//是否装弹
}
```
>3.创建一个脚本，为上述定义的变量进行赋值，命名为fps_FPInput，拖动到FP_Player中
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps_FPInput : MonoBehaviour
{
    public bool LockCursor//由于是第一人称视角，不需要显示光标，锁定光标 
    {
        get { return Cursor.lockState == CursorLockMode.Locked ? true : false; }//锁定返回true,反则返回false
        set
        {
            Cursor.visible = value;//设定一个鼠标可见的变量
            Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
    private fps_PlayerParameter parameter;//给脚本的参数进行赋值
    private fps_Input input;
    void Start()
    {
        LockCursor = true;
        parameter = this.GetComponent<fps_PlayerParameter>();//赋值parameter，通过该脚本所在物体中找到组件fps_PlayerParameter
        input = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<fps_Input>();//赋值input，通过游戏物体的标签区寻找组件fps_Input
    }
    private void Update()
    {
        InitialInput();
    }
    private void InitialInput()//对变量进行赋值
    {
        parameter.inputMoveVector = new Vector2(input.GetAxis("Horizontal"), input.GetAxis("Vertical"));
        parameter.inputSmoothLook = new Vector2(input.GetAxisRaw("Mouse X"), input.GetAxisRaw("Mouse Y"));//通过鼠标控制相机进行旋转来达到视觉旋转的特效
        parameter.inputCrouch = input.GetButton("Crouch");
        parameter.inputJump = input.GetButton("Jump");
        parameter.inputFire = input.GetButton("Fire");
        parameter.inputSprint = input.GetButton("Sprint");
        parameter.inputReload = input.GetButton("Reload");
    }
}
```


