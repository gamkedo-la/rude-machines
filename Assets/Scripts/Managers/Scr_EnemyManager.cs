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
        
        [HideInInspector] public List<GameObject> enemies = new List<GameObject>();
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
        //public string[] conditionTypes;
        //public int[] conditionValues;
        //[Space]
        public string[] keysWithTransformIndex;

        [HideInInspector] public float timer = 0.0f;
    }

    [SerializeField] private List<EnemyKey> keys = new List<EnemyKey>();
    [Space]
    public int currentWave = 0;
    [SerializeField] private List<Wave> waves = new List<Wave>();

    private float enemyKeyListTimer = 0.0f;

    void Start()
    {
        
    }

    EnemyKey GetKey(string name)
    {
        foreach(var key in keys)
        {
            if(key.name.Contains(name))
            {
                return key;
            }
        }
        return null;
    }

    void RemoveDestroyedFromAllKeys()
    {
        if(enemyKeyListTimer <= 0.0f)
        {
            List<GameObject> destroyedEnemies = new List<GameObject>();
            foreach(var key in keys)
            {
                foreach(var enemy in key.enemies)
                {
                    destroyedEnemies.Add(enemy);
                }
                foreach(var destroyedEnemy in destroyedEnemies)
                {
                    key.enemies.Remove(destroyedEnemy);
                }
                destroyedEnemies.Clear();
            }
            enemyKeyListTimer = 1.0f;
        }
    }

    void Update()
    {
        List<SpawnCommand> commandsToRemove = new List<SpawnCommand>();
        foreach(var command in waves[currentWave].spawnCommands)
        {
            if(command.timer <= 0.0f)
            {
                foreach(var keyTransform in command.keysWithTransformIndex)
                {
                    EnemyKey eKey = GetKey(keyTransform);
                    if(eKey != null)
                    {
                        Transform t = transform.GetChild(System.Int32.Parse(keyTransform.Replace(eKey.name, "")));
                        eKey.enemies.Add(Instantiate(eKey.prefab, t.position + eKey.offset, t.rotation));
                    }
                }

                if(--command.checkForAmount == 0) commandsToRemove.Add(command);
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
