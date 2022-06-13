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
        
        [HideInInspector] public int counter = 0;
    }

    [SerializeField] private List<EnemyKey> keys = new List<EnemyKey>();
    [Space]
    public int currentWave = 0;
    [SerializeField] private List<Scr_SpawnWave> waves = new List<Scr_SpawnWave>();

    private float enemyKeyListTimer = 0.0f;
    private GameObject player = null;

    public static Scr_EnemyManager instance = null;

    public void DecrementCounter(string name)
    {
        for(int i = 0; i < keys.Count; i++)
            if(name.Contains(keys[i].name))
                GetKey(keys[i].name).counter--;
    }

    void Start()
    {
        instance = this;

        player = GameObject.FindGameObjectWithTag("Player");

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

    Transform GetClosestTransform(Transform t)
    {
        int index = -1;
        float closestDistance = 99999.0f;
        for(int i = 0; i < transform.childCount; i++)
        {
            float distance = Vector3.Distance(transform.GetChild(i).position, t.position);
            if(distance < closestDistance)
            {
                closestDistance = distance;
                index = i;
            }
        }
        return transform.GetChild(index);
    }

    Transform GetTransform(char t)
    {
        if(t == 'R') return transform.GetChild(Random.Range(0,transform.childCount));
        if(t == 'P') return GetClosestTransform(player.transform);
        for(int i = 0; i < transform.childCount; i++)
            if(transform.GetChild(i).name[0] == t) return transform.GetChild(i);
        return null;
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
                        for(int i = 0; i < key.transforms.Length; i++)
                        {
                            if(limit == null || eKey.counter < limit.amount)
                            {
                                Transform t = GetTransform(key.transforms[i]);
                                Instantiate(eKey.prefab, t.position + eKey.offset, t.rotation);
                                eKey.counter++;
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
    }
}
