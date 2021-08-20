using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Event_3 : TriggerEvent
{
    //recording lasts for 59ish seconds

    public List<GameObject> boxes = new List<GameObject>();
    [SerializeField] private int i = 0; //this will equal the amount of boxes that have turned on. starts at 0
    [SerializeField] private float amount;
    public AudioClip firstClip;
    public AudioClip secondClip;
    private AudioSource source;

    private void Awake() {
        source = GetComponent<AudioSource>();
    }
    private void Start() {
        amount = boxes.Count;
    }
    public override void StartEvent() {
        SoundManager.instance.StopMusic();
        PlayerMovement.instance.canMove = false; //can no longer move
        CamMovement.instance.hasFlashlight = false;
        CamMovement.instance.TurnOffFlashlight();
        RenderSettings.fogDensity = 0.5f;

        

        Invoke("TurnOn", 5);
        source.clip = firstClip;
        source.Play();
    }
    private void SpawnBox() {
        int rand = Random.Range(0, boxes.Count);
        GameObject box = boxes[rand].gameObject;
        box.SetActive(true);
        boxes.Remove(box);
        i++;
    }
    public void TurnOn() {
        if(i != amount)
            SpawnBox();
        
        RenderSettings.fogDensity = 0.19f;
        CamMovement.instance.TurnOnFlashlight();
        Invoke("TurnOff", 5f);

    }

    public void TurnOff() {
        RenderSettings.fogDensity = 0.5f;
        CamMovement.instance.TurnOffFlashlight();
        if(i != amount)
            Invoke("TurnOn", 5f);
        else {
            Invoke("PlayCreepySound", 3f);
        }
    }

    public void PlayCreepySound() {
        source.clip = secondClip;
        source.Play();
        Invoke("ChangeScene", 10f);
    }

    public void ChangeScene() {
        SceneManager.LoadScene("Scene_2");
    }
}
