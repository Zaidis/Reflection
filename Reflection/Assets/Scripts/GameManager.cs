using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<int> keys = new List<int>();
    public int batteryAmount { get; set; }
    private AudioSource source;
    [SerializeField] private string keyCode; //for the end
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }

        source = this.GetComponent<AudioSource>();
    }

    private void Start() {
        NewKeyCode();
    }

    public void NewKeyCode() {
        for(int i = 0; i < 4; i++) {
            //keycode = 4 digits
            int rand = Random.Range(0, 10); //0 - 9
            keyCode += rand.ToString();
        }
    }

    public string Code() {
        return keyCode;
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

    public void PlaySound(AudioClip clip) {
        source.clip = clip;
        source.Play();
    }
}
