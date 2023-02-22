using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Hangman : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _textForScript;
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private TextMeshProUGUI _hints;
    [SerializeField] private int hp = 7;



    private List<char> guessedLetters = new List<char>();
    private List<char> wrongTriedLetter = new List<char>();

    private string[] words =
    {
        "House",
        "Blueberry",
        "Depth",
        "Mirror",
        "Password"
    };

    private string[] hints =
    {
        "a building having walls, windows, a roof, and rooms inside in which people live or work",
        "purple small sweet berry",
        "furniture has height, width and....",
        "you can see your reflection",
        "login and ...."
    };


    private string hintWord = "Hint:";

    private string wordToGuess = "";

    private KeyCode lastKeyPressed;

    private void Start()
    {
        int randomIndex = Random.Range(0, words.Length);
        wordToGuess = words[randomIndex];

        hintWord += hints[randomIndex];
        string initialWord = "";
        for (int i = 0; i < wordToGuess.Length; i++)
        {
            initialWord += " _";
        }

        _textField.text = initialWord;
        _textForScript.text = wordToGuess.ToUpper();
        _hpText.text = hp.ToString();

        _hints.text = hintWord;
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {

            if (e.keyCode != KeyCode.None && lastKeyPressed != e.keyCode)
            {
                ProcessKey(e.keyCode);

                lastKeyPressed = e.keyCode;
            }
        }
    }



    private void ProcessKey(KeyCode key)
    {
        char pressedKeyString = key.ToString()[0];

        string wordUppercase = wordToGuess.ToUpper();

        bool wordContainsPressedKey = wordUppercase.Contains(pressedKeyString);

        bool letterWasGuessed = guessedLetters.Contains(pressedKeyString);

        if (!wordContainsPressedKey && !wrongTriedLetter.Contains(pressedKeyString))
        {
            wrongTriedLetter.Add(pressedKeyString);
            hp -= 1;

            if (hp <= 0)
            {
                SceneManager.LoadSceneAsync(sceneBuildIndex: 2);
            }
            else
            {
                _hpText.text = hp.ToString();
            }

        }


        if (wordContainsPressedKey && !letterWasGuessed)
        {
            guessedLetters.Add(pressedKeyString);
        }

        string stringToPrint = "";

        for (int i = 0; i < wordUppercase.Length; i++)
        {
            char letterInWord = wordUppercase[i];

            if (guessedLetters.Contains(letterInWord))
            {
                stringToPrint += letterInWord;
            }
            else
            {
                stringToPrint += " _";
            }
        }

        if (wordUppercase == stringToPrint)
        {
            SceneManager.LoadSceneAsync(sceneBuildIndex: 1);
        }

        _textField.text = stringToPrint;
    }
}