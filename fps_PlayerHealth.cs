using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps_PlayerHealth : MonoBehaviour
{
    public bool isDead;//��������Ƿ������ı�ʶλ
    public float resetAfterDeathTime = 5;//�趨���������ȴ��೤ʱ���������
    public AudioClip deathClip;//����ʱ��Ҫ���ŵ���Ƶ
    public AudioClip damageClip;//����ʱ��Ҫ���ŵ���Ƶ
    public float maxHp = 100;//������������ֵ
    public float hp = 100;//���õ�ǰ������ֵ
    public float recoverSpeed = 1;//�������ֵ�Ļظ��ٶ�
    private float timer = 0;//��ʱ��
    private FadeInOut fader;//�������ʱ��Ҫӵ��һ�������Ľ���Ч��
    private void Start()
    {
        hp = maxHp;
        fader = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<FadeInOut>();
    }
}
