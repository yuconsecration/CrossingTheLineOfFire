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

