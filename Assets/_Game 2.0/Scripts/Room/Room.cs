using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject[] reward = default;
    IRoomActivables[] roomActivables;

    private void Awake()
    {
        RefreshIRoomActivablesReferences();
    }

    public void ActivateRoom()
    {
        for (int i = 0; i < roomActivables.Length; i++)
        {
            roomActivables[i].Activate();
        }     
    }

    private void RefreshIRoomActivablesReferences()
    {
        roomActivables = GetComponentsInChildren<IRoomActivables>();
    }

    public void EndWave()
    {
        for (int i = 0; i < roomActivables.Length; i++)
        {
            roomActivables[i].Deactivate();
        }
        FindObjectOfType<CharacterController>().key = true;
        StartCoroutine(SpawnItem());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Nave"))
        {
            ActivateRoom();
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    IEnumerator SpawnItem()
    {
        FindObjectOfType<SpawnerPool>().GetParticle(7 ,transform.position);
        yield return new WaitForSeconds(0.75f);
        if(reward.Length > 0)
            Instantiate(reward[Random.Range(0, reward.Length - 1)], transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.6f);
        Gizmos.DrawSphere(transform.position, 0.3f);
    }

}
