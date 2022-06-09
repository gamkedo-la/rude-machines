using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_EnemyManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyKey
    {
        public string name;
        public GameObject prefab;
        public Vector3 offset;
        public List<GameObject> enemies = new List<GameObject>();
    }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public List<SpawnCommand> spawnCommands = new List<SpawnCommand>();
    }

    [System.Serializable]
    public class SpawnCommand
    {
        public float checkAfterTime;
        public int checkForAmount; //-1 means infinite
        [Space]
        public string[] conditionType;
        public int[] conditionValue;
        [Space]
        public string[] key;

        [HideInInspector] public float timer = 0.0f;
    }

    public int currentWave = 0;
    [SerializeField] private List<Wave> waves = new List<Wave>();

    void Start()
    {
        
    }

    void Update()
    {
        List<SpawnCommand> commandsToRemove = new List<SpawnCommand>();
        foreach(var command in waves[currentWave].spawnCommands)
        {
            if(command.timer <= 0.0f)
            {
                if(command.checkForAmount == 0) commandsToRemove.Add(command);
                else command.checkForAmount--;

                command.timer = command.checkAfterTime;
            }
            else
            {
                command.timer -= Time.deltaTime;
            }
        }

        foreach(var command in commandsToRemove) waves[currentWave].spawnCommands.Remove(command);
        commandsToRemove.Clear();
    }
}
