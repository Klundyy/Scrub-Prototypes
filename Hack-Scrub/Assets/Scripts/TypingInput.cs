using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TypingInput : MonoBehaviour
{
    [SerializeField] private TMP_Text textDisplay;
    [SerializeField] private int maxWordLength = 10;
    private string typedWord;
    void Start()
    {
        typedWord = "";
    }

    private void checkInput(){
        if(Input.anyKeyDown){
            string keysPressed = Input.inputString;
            if(Input.GetKeyDown(KeyCode.Backspace)){
                removeLetter();
            } else if (keysPressed.Length == 1 && typedWord.Length < 10){
                enterLetter(keysPressed);
            }
        }
    }

    private void enterLetter(string letter){
        if(letter != null){
            typedWord = String.Concat(typedWord,letter);
        }
    }   
    private void removeLetter(){
        if(typedWord.Length > 0){
            typedWord = typedWord.Remove(typedWord.Length - 1);
        }
    }
    public string getWord(){
        return typedWord;
    }
    void Update()
    {
        checkInput();
        textDisplay.text = typedWord;
    }
}
