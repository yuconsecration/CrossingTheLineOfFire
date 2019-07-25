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
