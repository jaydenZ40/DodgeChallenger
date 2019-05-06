using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public static int playerNum = 0;

    void Start()
    {
        instance = this;
    }

}
