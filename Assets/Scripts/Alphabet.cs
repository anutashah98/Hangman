using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Alphabet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _guesedWord;
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI _keyText;

    

    private KeyCode lastKeyPressed;

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {

            if (e.keyCode != KeyCode.None && lastKeyPressed != e.keyCode)
            {
                lastKeyPressed = e.keyCode;
                ProcessKey(e.keyCode);
            }
        }
    }

    public void ProcessKey(KeyCode key)
    {
        char pressedKeyString = key.ToString()[0];

        string wordUppercase = _guesedWord.text.ToUpper();

        if (!wordUppercase.Contains(pressedKeyString) && pressedKeyString == _keyText.text[0]) 
        {
            panel.SetActive(true);
        }
        
    }
}
