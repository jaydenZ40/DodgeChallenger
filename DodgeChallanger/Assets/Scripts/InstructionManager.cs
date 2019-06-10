using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionManager : MonoBehaviour
{
    public TextMeshProUGUI instruction;

    void Start()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        string str = "Welcom to the battleground!" +
            "\n\nAttacker and dodger will be switched for each round." +
            "\n\nSuccessfully making damage will gain gold." +
            "\n\nGold can be spent for upgrading new weapons." +
            "\n\nYour goal is to beat your opponent." +
            "\n\nGood luck!";
        
        int i = 0;
        instruction.text = "";
        while (i < str.Length)
        {
            instruction.text += str[i++];
            yield return new WaitForSeconds(0.08f);
        }
    }
}
