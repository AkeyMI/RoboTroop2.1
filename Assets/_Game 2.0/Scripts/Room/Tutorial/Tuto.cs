using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    [SerializeField] int walkPoints;
    [SerializeField] int shotPoints;
    [SerializeField] GameObject[] doors;
    [SerializeField] GameObject[] spawners;

    SpawnerPool sp;
    int currentDoor;

    void Start()
    {
        FindObjectOfType<CharacterController>().key = false;
        sp = FindObjectOfType<SpawnerPool>();
    }
    public void WalkPoint()
    {
        walkPoints--;

        if (walkPoints == 0)
        {
            FindObjectOfType<CharacterController>().key = true;
            doors[0].GetComponent<Door>().Deactivate();
        }
    }

    public void ShotPoint()
    {
        shotPoints--;

        if (shotPoints <= 0)
        {
            doors[1].GetComponent<Door>().Deactivate(); 
            doors[2].GetComponent<Door>().Deactivate();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NaveController>() != null)
            StartCoroutine(StartShot());
    }

    IEnumerator StartShot()
    {
        doors[1].GetComponent<Door>().Activate();
        GetComponent<BoxCollider>().enabled = false;
        foreach (GameObject go in spawners)
            sp.GetParticle(14, go.transform.position);

        yield return new WaitForSeconds(3);

        foreach (GameObject go in spawners)
            go.SetActive(true);

    }
}
