using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Scr_SpawnWave
{
    public string name = "Wave";
    public List<Command> commands = new List<Command>();

    [System.Serializable]
    public class Key
    {
        public string name;
        public int amount;
        public int[] transformIndexes;
    }

    [System.Serializable]
    public class Command
    {
        public float checkAfterTime;
        public int checkForAmount; //-1 means infinite
        [Space]
        //public string[] conditionTypes;
        //public int[] conditionValues;
        //[Space]
        public Key[] keys;

        [HideInInspector] public float timer = 0.0f;
    }
}