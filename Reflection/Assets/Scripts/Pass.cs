using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Pass : MonoBehaviour
{

    [SerializeField] private string code; //the code being put inside the pass
    public TextMeshPro screenText;
    public TextMeshPro previousAnswer;
    public Door theDoor;
    private AudioSource source;

    private void Awake() {
        source = GetComponent<AudioSource>();
    }
    public void AddNumber(int num) {
        code += num.ToString();
        screenText.text = code;
        source.Play();
        if(code.Length == 4) {
            CheckPassCode();
        }
        //print(code);
    }

    public void CheckPassCode() {
        string goodCode = GameManager.instance.Code();
        if(goodCode != code) {
            //it is not the right code
            previousAnswer.text = code;
            code = "";
            screenText.text = "WRONG";
            Debug.Log("resetting code");
        } else {
            Debug.Log("Well done!");
            screenText.color = Color.green;
            screenText.text = "Correct";
            theDoor.GetComponent<Door>().OpenDoor();
        }
    }
}
