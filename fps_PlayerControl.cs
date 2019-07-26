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
public class fps_PlayerControl : MonoBehaviour
{
    private PlayerState state = PlayerState.None;//设置默认状态为无状态
    public PlayerState State
    {
        get
        {
            if (runing)
                state = PlayerState.Run;
            else if (walking)
                state = PlayerState.Walk;
            else if (crouching)
                state = PlayerState.Crouch;
            else
                state = PlayerState.Idle;
            return state;
        }
    }
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
    private Vector3 moveDirection = Vector3.zero;
    private void Start()
    {
        crouching = false;//初始化操作
        walking = false;
        crouching = false;
        runing = false;
        jumpSpeed = normalSpeed;//初始化跳跃速度
        mainCamera = GameObject.FindGameObjectWithTag(Tags.mainCamera).transform;
        standardCamHeight = mainCamera.localPosition.y;//存储相机的标准高度
        crouchingCamHeight = standardCamHeight - crouchDeltaHeight;//蹲伏的相机高度
        audioSource = this.GetComponent<AudioSource>();
        controller = this.GetComponent<CharacterController>();
        parameter = this.GetComponent<fps_PlayerParameter>();
        normalControllerCenter = controller.center;
        normalControllerHeight = controller.height;//设定正常情况下的高度
    }
    public void FixedUpdate()
    {
        UpdateMove();
        AudioManagement();
    }
    private void UpdateMove()//操作人物移动
    {
        if (grounded)
        {
            moveDirection = new Vector3(parameter.inputMoveVector.x, 0, parameter.inputMoveVector.y);
            moveDirection = transform.TransformDirection(moveDirection);//将存在在世界坐标系的方向转变为自身坐标系中的方向
            moveDirection *= speed;
            if (parameter.inputJump)
            {
                moveDirection.y = jumpSpeed;
                AudioSource.PlayClipAtPoint(jumpAudio, transform.position);
                CurrentSpeed();
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;//表示跳跃后会下落
        CollisionFlags flags = controller.Move(moveDirection * Time.deltaTime);
        grounded = (flags & CollisionFlags.CollidedBelow) != 0;
        if (Mathf.Abs(moveDirection.x) > 0 && grounded || Mathf.Abs(moveDirection.x) > 0 && grounded)
        {
            if (parameter.inputSprint)
            {
                walking = false;
                runing = true;
                crouching = false;
            }
            else if (parameter.inputCrouch)
            {
                crouching = true;
                walking = false;
                runing = false;
            }
            else
            {
                walking = true;
                crouching = false;
                runing = false;
            }
        }
        else
        {
            if (walking)
                walking = true;
            if (runing)
                runing = true;
            if (parameter.inputCrouch)
                crouching = true;
            else
                crouching = false;
        }
        if (crouching)
        {
            controller.height = normalControllerHeight - crouchDeltaHeight;
            controller.center = normalControllerCenter - new Vector3(0, crouchDeltaHeight / 2, 0);
        }
        else
        {
            controller.height = normalControllerHeight;
            controller.center = normalControllerCenter;
        }
        UpdateCrouch();
        CurrentSpeed();
    }
    private void CurrentSpeed()//赋值速度
    {
        switch (State)
        {
            case PlayerState.Idle:
                speed = normalSpeed;
                jumpSpeed = normalJumpSpeed;
                break;
            case PlayerState.Crouch:
                speed = crouchSpeed;
                jumpSpeed = crouchJumpSpeed;
                break;
            case PlayerState.Run:
                speed = sprintSpeed;
                jumpSpeed = sprintJumpSpeed;
                break;
            case PlayerState.Walk:
                speed = normalSpeed;
                jumpSpeed = normalJumpSpeed;
                break;
        }
    }
    private void AudioManagement()//设置脚步声操作
    {
        if (State == PlayerState.Walk)
        {
            audioSource.pitch = 1.0f;
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else if (State == PlayerState.Run)
        {
            audioSource.pitch = 1.3f;
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
    private void UpdateCrouch()//蹲伏操作过程中相机的相关操作
    {
        if (crouching)
        {
            if (mainCamera.localPosition.y > crouchingCamHeight)
            {
                if (mainCamera.localPosition.y - (crouchDeltaHeight * Time.deltaTime * cameraMoveSpheed) < crouchingCamHeight)
                    mainCamera.localPosition = new Vector3(mainCamera.localPosition.x, crouchingCamHeight, mainCamera.localPosition.z);
                else
                    mainCamera.localPosition -= new Vector3(0, crouchDeltaHeight * Time.deltaTime * cameraMoveSpheed, 0);
            }
            else
                mainCamera.localPosition = new Vector3(mainCamera.localPosition.x, crouchingCamHeight, mainCamera.localPosition.z);
        }
        else
        {
            if (mainCamera.localPosition.y < standardCamHeight)
            {
                if (mainCamera.localPosition.y + (crouchDeltaHeight * Time.deltaTime * cameraMoveSpheed) > standardCamHeight)
                {
                    mainCamera.localPosition = new Vector3(mainCamera.localPosition.x, standardCamHeight, mainCamera.localPosition.x);
                }
                else
                {
                    mainCamera.localPosition = new Vector3(0, crouchDeltaHeight * Time.deltaTime * cameraMoveSpheed, 0);
                }
            }
            else
            {
                mainCamera.localPosition = new Vector3(mainCamera.localPosition.x, standardCamHeight, mainCamera.localPosition.x);
            }
        }

    }

}
