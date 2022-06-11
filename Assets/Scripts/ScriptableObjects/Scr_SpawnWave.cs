using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Scr_SpawnWave
{
    public string name = "Wave";
    public List<Limit> limits = new List<Limit>();
    public List<Command> commands = new List<Command>();

    [System.Serializable]
    public class Key
    {
        public string name;
        public int amount;
    }

    [System.Serializable]
    public class Command
    {
        public float checkAfterTime;
        public int checkForAmount; //-1 means infinite
        [Space]
        public Key[] keys;

        [HideInInspector] public float timer = 0.0f;
    }

    [System.Serializable]
    public class Limit
    {
        public string name;
        public int amount;
    }

    public Limit GetLimit(string name)
    {
        foreach(var limit in limits)
        {
            if(limit.name == name)
            {
                return limit;
            }
        }
        return null;
    }
}