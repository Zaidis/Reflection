using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<int> keys = new List<int>();
    public int batteryAmount { get; set; }
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Checks if you have the key to open the door.
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public bool CheckKey(int num) {
        for(int i = 0; i < keys.Count; i++) {
            if(num == keys[i]) {
                return true;
            }
        }
        return false;
    }

    public void AddKey(int key) {
        keys.Add(key);
    }
}
