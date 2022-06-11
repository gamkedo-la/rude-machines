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

    [SerializeField] private List<EnemyKey> keys = new List<EnemyKey>();
    [Space]
    public int currentWave = 0;
    [SerializeField] private List<Scr_SpawnWave> waves = new List<Scr_SpawnWave>();

    private float enemyKeyListTimer = 0.0f;

    void Start()
    {
        foreach(var wave in waves)
            foreach(var command in wave.commands)
                command.timer = Random.value * command.checkAfterTime;
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
        List<Scr_SpawnWave.Command> commandsToRemove = new List<Scr_SpawnWave.Command>();
        foreach(var command in waves[currentWave].commands)
        {
            if(command.timer <= 0.0f)
            {
                foreach(var key in command.keys)
                {
                    EnemyKey eKey = GetKey(key.name);
                    if(eKey != null)
                    {
                        Scr_SpawnWave.Limit limit = waves[currentWave].GetLimit(eKey.name);
                        for(int i = 0; i < key.amount; i++)
                        {
                            if(limit == null || eKey.enemies.Count < limit.amount)
                            {
                                Transform t = transform.GetChild(Random.Range(0, transform.childCount));
                                eKey.enemies.Add(Instantiate(eKey.prefab, t.position + eKey.offset, t.rotation));
                            }
                        }
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

        foreach(var command in commandsToRemove) waves[currentWave].commands.Remove(command);
        commandsToRemove.Clear();

        RemoveDestroyedFromAllKeys();
    }
}
