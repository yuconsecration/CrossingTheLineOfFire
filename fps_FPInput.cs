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

