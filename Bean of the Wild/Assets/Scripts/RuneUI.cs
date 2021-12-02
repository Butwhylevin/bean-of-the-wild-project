using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RuneUI : MonoBehaviour
{
    RuneRaycaster runeScript;
    public TMP_Text activeRuneText;

    private void Start() 
    {
        runeScript = gameObject.GetComponent<RuneRaycaster>();
    }

    private void Update() 
    {
        if(runeScript.runeActive)
        {
            activeRuneText.gameObject.SetActive(true);
            UpdateLabel();
        }
        else
        {
            activeRuneText.gameObject.SetActive(false);
        }
    }

    private void UpdateLabel()
    {
        if(runeScript.activeRuneNumber == 1)
        {
            activeRuneText.text = "stasis";
        }
        if(runeScript.activeRuneNumber == 2)
        {
            activeRuneText.text = "magnesis";
        }
        if(runeScript.activeRuneNumber == 3)
        {
            activeRuneText.text = "cryonis";
        }
        if(runeScript.activeRuneNumber == 4)
        {
            activeRuneText.text = "circleBomb";
        }
        if(runeScript.activeRuneNumber == 5)
        {
            activeRuneText.text = "cubeBomb";
        }
        if(runeScript.activeRuneNumber == 6)
        {
            activeRuneText.text = "rewind";
        }
    }
}
