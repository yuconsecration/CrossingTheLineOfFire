using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps_PlayerInventory : MonoBehaviour {
    //设定集合进行钥匙卡ID的存储
    private List<int> keysArr;
    private void Start()
    {
        keysArr = new List<int>();
    }
    //添加钥匙卡
    public void AddKey(int keyId)
    {
        if (!keysArr.Contains(keyId))
            keysArr.Add(keyId);
    }
    //判断用户是否拥有钥匙卡
    public bool HasKey(int doorId)
    {
        if (keysArr.Contains(doorId))
        {
            return true;
        }
        else
            return false;
    }
}