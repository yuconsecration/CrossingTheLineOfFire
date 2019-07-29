using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps_PlayerHealth : MonoBehaviour
{
    public bool isDead;//定义玩家是否死亡的标识位
    public float resetAfterDeathTime = 5;//设定玩家死亡后等待多长时间进行重置
    public AudioClip deathClip;//死亡时需要播放的音频
    public AudioClip damageClip;//受伤时需要播放的音频
    public float maxHp = 100;//设置最大的生命值
    public float hp = 100;//设置当前的生命值
    public float recoverSpeed = 1;//玩家生命值的回复速度
    private float timer = 0;//计时器
    private FadeInOut fader;//玩家死亡时需要拥有一个场景的渐变效果
    private void Start()
    {
        hp = maxHp;
        fader = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<FadeInOut>();
    }
}
