using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps_PlayerInventory : MonoBehaviour {
    //�趨���Ͻ���Կ�׿�ID�Ĵ洢
    private List<int> keysArr;
    private void Start()
    {
        keysArr = new List<int>();
    }
    //���Կ�׿�
    public void AddKey(int keyId)
    {
        if (!keysArr.Contains(keyId))
            keysArr.Add(keyId);
    }
    //�ж��û��Ƿ�ӵ��Կ�׿�
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