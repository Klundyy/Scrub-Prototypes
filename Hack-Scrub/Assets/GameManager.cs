using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextAsset wordFile;
    [SerializeField] GameObject wordObject;
    [SerializeField] GameObject TypingInput;
    private List<GameObject> wordObjectList;
    private List<string> spawnedWordList;
    private Dictionary<int, string> wordSet;

    [SerializeField] private float timeToSpawn;
    [SerializeField] private TMP_Text scoreText;
    private float currentTimeToSpawn;
    private int setLength;
    private int score;
    void Start()
    {
        wordSet = new Dictionary<int, string>();
        spawnedWordList = new List<string>();
        wordObjectList = new List<GameObject>();
        setLength = 0;
        score = 0;
        LoadWords();
    }

    public void LoadWords(){
        if(wordFile != null){
            string[] words = wordFile.text.Split(new[] {'\n', '\r', ' '}, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words){
                string tempWord = word.ToLower().Trim();
                if(word.Length > 2 && word.Length < 10){
                    wordSet[setLength] = tempWord;
                    setLength += 1;
                }
            }
        } else{
            Debug.Log("Word file does not exist");
        }
    }

    public void SpawnWord(){
        int randomIndex = Random.Range(0,setLength);
        string randomWord = wordSet[randomIndex];
        if(!spawnedWordList.Contains(randomWord)){
            spawnedWordList.Add(randomWord);
            float randomSpawnX = Random.Range(transform.position.x - 10f, transform.position.x + 10f);
            float randomSpawnY = Random.Range(transform.position.y - 4.5f, transform.position.y + 4.5f);
            Vector3 randomSpawnLocation = new Vector3(randomSpawnX, randomSpawnY, 0f);
            GameObject spawnedObject = Instantiate(wordObject, randomSpawnLocation, quaternion.identity);
            spawnedObject.GetComponent<TMP_Text>().text = randomWord;
            float randomColorIndex = Random.Range(0f,1f);
            Debug.Log(randomColorIndex);
            if(randomColorIndex < 0.75){
                spawnedObject.GetComponent<TMP_Text>().color = Color.green;
            }else{
                spawnedObject.GetComponent<TMP_Text>().color = Color.red;
            }
            wordObjectList.Add(spawnedObject);
        } else{
            SpawnWord();
        }
    }
    public void CompareWord(){
        if(spawnedWordList.Contains(TypingInput.GetComponent<TypingInput>().getWord())){
            int wordIndex = spawnedWordList.IndexOf(TypingInput.GetComponent<TypingInput>().getWord());
            GameObject comparedObject = wordObjectList[wordIndex];
            if(comparedObject.GetComponent<TMP_Text>().color == Color.green){
                score += 1;
            } else if (comparedObject.GetComponent<TMP_Text>().color == Color.red){
                score -= 1;
            }
            spawnedWordList.RemoveAt(wordIndex);
            TypingInput.GetComponent<TypingInput>().resetWord();
            Destroy(comparedObject);
            wordObjectList.RemoveAt(wordIndex);
        }
    }
    void Update()
    {
        scoreText.text = score.ToString();
        if(currentTimeToSpawn > 0){
            currentTimeToSpawn -= Time.deltaTime;
        } else{
            SpawnWord();
            currentTimeToSpawn = timeToSpawn;
        }
        CompareWord();
    }
}
