using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState//设立枚举表示玩家的状态
{
    None,//无状态
    Idle,//站立状态
    Walk,//行走状态
    Crouch,//蹲伏状态
    Run,//行走状态
}
public class fps_PlayerControl : MonoBehaviour {
    private PlayerState state = PlayerState.None;//设置默认状态为无状态
    public float sprintSpeed = 10.0f;//设置冲刺状态的相关速度
    public float sprintJumpSpeed = 8.0f;
    public float normalSpeed = 6.0f;//设置正常状态下的相关速度
    public float normalJumpSpeed = 7.0f;
    public float crouchSpeed = 2.0f;
    public float crouchJumpSpeed = 5.0f;
    public float crouchDeltaHeight = 0.5f;//定义玩家蹲伏的时候下降的高度
    public float gravity = 20.0f;//定义一个重力加速度
    public float cameraMoveSpheed = 8.0f;
    public AudioClip jumpAudio;
    private float speed;//代表玩家当前的速度
    private float jumpSpeed;
    private Transform mainCamera;//代表相机
    private float standardCamHeight;//存储相机的标准高度
    private float crouchingCamHeight;//存储蹲伏时相机的高度
    private bool grounded = false;//表示玩家是否在地面
    private bool walking = false;
    private bool crouching = false;
    private bool stopCrouching = false;
    private bool runing = false;
    private Vector3 normalControllerCenter = Vector3.zero;
    private float normalControllerHeight = 0.0f;
    private float timer = 0;//计时器默认为零
    private CharacterController controller;
    private AudioSource audioSource;//播放角色的脚步声
    private fps_PlayerParameter parameter;
}

