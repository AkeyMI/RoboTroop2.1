using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectPooling : MonoBehaviour
{
    private static ObjectPooling instance;

    static Dictionary<int, Queue<GameObject>> pool = new Dictionary<int, Queue<GameObject>>();
    static Dictionary<int, GameObject> parents = new Dictionary<int, GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static void PreLoad(GameObject objToPool, int amount)
    {
        int id = objToPool.GetInstanceID();

        GameObject parent = new GameObject();
        parent.name = objToPool.name + "Pool";
        parent.transform.SetParent(FindObjectOfType<SpawnerPool>().transform);
        parents.Add(id, parent);

        pool.Add(id, new Queue<GameObject>());

        for(int i = 0; i <amount; i++)
        {
            CreateObj(objToPool);
        }
    }

    static void CreateObj(GameObject objToPool)
    {
        int id = objToPool.GetInstanceID();

        GameObject go = Instantiate(objToPool) as GameObject;
        go.transform.SetParent(GetParent(id).transform);
        go.SetActive(false);

        pool[id].Enqueue(go);
    }

    static GameObject GetParent(int parentID)
    {
        GameObject parent;
        parents.TryGetValue(parentID, out parent);
        return parent;
    }

    public static GameObject GetObj(GameObject objToPool)
    {
        int id = objToPool.GetInstanceID();

        if (!pool.ContainsKey (id))
        {
            PreLoad(objToPool, 1);
        }

        if (pool[id].Count == 0)
        {
            CreateObj(objToPool);
        }

        GameObject go = pool[id].Dequeue();
        go.SetActive(true);

        return go;
    }

    public static void ReObj(GameObject objToPool, GameObject objToRecicle)
    {
        int id = objToPool.GetInstanceID();

        pool[id].Enqueue(objToRecicle);
        objToRecicle.SetActive(false);
    } 

    public static void ClearDictionary(Scene s, LoadSceneMode load)
    {
        pool.Clear();
        parents.Clear();
    }

    public void OnEnable()
    {
        SceneManager.sceneLoaded += ClearDictionary;
    }
}
