using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
    private int score;

    private bool gameOver;
    private bool restart;

    public Text firstValue;
    public Text plusMinus;
    public Text secondValue;
    public Text answerValue;
    public int updateVal;

    public int correctAnswer;
    public Text[] checkVal;

    int checkAnswer = 0;
    int val1 = 0;
    int val2 = 0;
    int oper = 0;

    public int _score;

    void Start()
    {
        gameOver = false;
        restart = false;
        updateVal = 0;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore ();
        StartCoroutine(SpawnWaves());
        //chooseOperator();
    }

    void Update()
    {

        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    void chooseOperator()
    {
        int oper = 0;

        oper = UnityEngine.Random.Range(0, 2);

        createQuestion(oper);
    }

    void createQuestion(int operation)
    {
        int result = 0;
        int value1 = 0;
        int value2 = 0;

        int temp = 0;

        while (true)
        {
            temp++;

            bool isGood = true;

            if (operation <= 2)
            {
                value1 = UnityEngine.Random.Range(0, 50);
                value2 = UnityEngine.Random.Range(0, 50);
            }

            result = getResult(value1, value2, operation);

            if (operation == 0 || operation == 1)
            {
                int resultMin = 0;
                int resultAdd = 0;
                resultMin = value1 - value2;
                resultAdd = value1 + value2;

                if(resultMin == resultAdd)
                {
                    isGood = false;
                }
            }

            if(checkAnswer == result && val1 == value1 && val2 == value2 && oper == operation)
            {
                isGood = false;
            }

            if(result <= 0)
            {
                isGood = false;
            }

            if(result > 99)
            {
                isGood = false;
            }

            if(isGood)
            {
                if(operation == 0)
                {
                    int results = value1 + value2;
                    if(results != result)
                    {
                        isGood = false;
                    }
                }
                if(operation == 1)
                {
                    int results = value1 + value2;
                    if(results != result)
                    {
                        isGood = false;
                    }
                }
            }

            if(isGood)
            {
                checkAnswer = result;
                val1 = value1;
                val2 = value2;
                oper = operation;

                break;
            }

        }

        setAnswers(val1, val2, operation, result);

    }

    private void setAnswers(int val1, int val2, int oper, int result)
    {
        int question = UnityEngine.Random.Range(0, 4);

        if(question == 0)
        {
            firstValue.text = "?";
            secondValue.text = val2.ToString();
            plusMinus.text = GetOperator(oper);
            answerValue.text = result.ToString();
            correctAnswer = val1;
        }

        if (question == 1)
        {
            firstValue.text = val1.ToString();
            secondValue.text = val2.ToString();
            plusMinus.text = "?";
            answerValue.text = result.ToString();
            correctAnswer = oper;
        }

        if (question == 2)
        {
            firstValue.text = val1.ToString();
            secondValue.text = "?";
            plusMinus.text = GetOperator(oper);
            answerValue.text = result.ToString();
            correctAnswer = val2;
        }

        if (question == 3)
        {
            firstValue.text = val1.ToString();
            secondValue.text = val2.ToString();
            plusMinus.text = GetOperator(oper);
            answerValue.text = "?";
            correctAnswer = result;
        }

        firstValue.transform.parent.Find("Selected").gameObject.SetActive(question == 0);
        plusMinus.transform.parent.Find("Selected").gameObject.SetActive(question == 1);
        secondValue.transform.parent.Find("Selected").gameObject.SetActive(question == 2);
        answerValue.transform.parent.Find("Selected").gameObject.SetActive(question == 3);

        if(question != 1)
        {
            int[] responses = new int[4];

            List<int> checkAnswers = new List<int>();

            checkAnswers.Add(correctAnswer);

            while(true)
            {
                int answer = 0;

                int checkAnswer = 0;

                while(true)
                {
                    bool isGood = true;

                    answer = UnityEngine.Random.Range(1, correctAnswer * 2 + 3);

                    if(answer <= 0)
                    {
                        isGood = false;
                    }

                    if(isGood)
                    {
                        break;
                    }

                    checkAnswer++;
                }

                if(!checkAnswers.Contains(answer))
                {
                    checkAnswers.Add(answer);
                }

                if(checkAnswers.Count == 4)
                {
                    break;
                }

                checkAnswers.Sort();
                responses = checkAnswers.ToArray();
                Array.Sort(responses);

                for(int i = 0; i < 4; i++)
                {
                    checkVal[i].fontSize = 2;
                    checkVal[i].text = responses[i].ToString();
                }
            }
        }
        else
        {
            checkVal[0].text = "+";
            checkVal[0].fontSize = 3;
            checkVal[0].text = "-";
            checkVal[0].fontSize = 3;

        }

    }

    public string GetOperator(int oper)
    {
        if (oper == 0)
            return "+";
        else if (oper == 1)
            return "-";
        else
            return "";
    }

    public int getResult(int val1, int val2, int oper)
    {
        if(oper == 0)
        {
            return val1 + val2;
        }
        else if(oper == 1)
        {
            return val1 - val2;
        }
        else
        {
            return 0;
        }
    }

    [TestMethod()]
    public void getResultSubtractTest()
    {
        int num1 = 10;
        int num2 = 6;
        int operators = 1;

        int result = getResult(num1, num2, operators);
        Assert.AreEqual(result, 4);
    }

    [TestMethod()]
    public void getResultAdditionTest()
    {
        int num1 = 24;
        int num2 = 11;
        int operators = 0;

        int result = getResult(num1, num2, operators);
        Assert.AreEqual(result, 35);
    }


    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(spawnWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(-spawnValues.x, spawnValues.y, spawnValues.z);
                Vector3 spawnPosition1 = new Vector3(0, spawnValues.y, spawnValues.z);
                Vector3 spawnPosition2 = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                Instantiate(hazard, spawnPosition1, spawnRotation);
                Instantiate(hazard, spawnPosition2, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver ()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }
}