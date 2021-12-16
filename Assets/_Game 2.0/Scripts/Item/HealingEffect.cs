using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_Name", menuName = "RoboTroop/Effects/HealingEffect", order = 1)]
public class HealingEffect : Effect
{
    [SerializeField] int lifeToCure = 1;

    public override void Apply()
    {
        ShootController minion = FindObjectOfType<ShootController>();

        if(minion == null)
        {
            minion = FindObjectOfType<MinionController>().MinionAtk.GetComponent<ShootController>();
        }

        minion.HealMinion(lifeToCure);
        FindObjectOfType<CharacterController>().helUpEfect.Play();
        FindObjectOfType<SpawnerPool>().GetSound(7);
    }
}
