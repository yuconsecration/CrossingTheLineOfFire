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

