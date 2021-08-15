using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class guides : MonoBehaviour
{
    public static guides instance;

    [TextArea()]
    public List<string> lines = new List<string>();
    public AudioClip clip;
    public int i = 0;
    private TextMeshProUGUI textObject;
    [SerializeField] private bool guideShown;
    private float timer;
    private AudioSource source;

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
        source = this.GetComponent<AudioSource>();
    }
    private void Start() {
        textObject = this.GetComponent<TextMeshProUGUI>();
        textObject.text = "";
        Invoke("GiveGuide", 46f); //46 == when the player can start to see the world
    }

    private void Update() {
        if (guideShown) {
            //there is a guide showing
            timer -= Time.deltaTime;
            if(timer <= 0) {
                timer = 0;
                textObject.text = "";
                guideShown = false;
            }
        }
    }
    /// <summary>
    /// Called when giving a new guide for the player
    /// </summary>
    public void GiveGuide() {
        string line = lines[i];
        textObject.text = line;
        source.Play();
        timer = 6f;
        guideShown = true;
        i++;
    }

}
