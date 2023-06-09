using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    [SerializeField] Transform archer;
    [SerializeField] Transform arrowPoint;

    protected override void Awake()
    {
        base.Awake();

        data = GameManager.Resource.Load<TowerData>("Data/ArcherTowerData");
    }

    private void OnEnable()
    {
        StartCoroutine(LookRoutine());
        StartCoroutine(AttackRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (enemyList.Count > 0)
            {
                Attack(enemyList[0]);
                yield return new WaitForSeconds(data.Towers[0].delay);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void Attack(EnemyController enemy)
    {
        Arrow arrow = GameManager.Resource.Instantiate<Arrow>("Prefab/Arrow", arrowPoint.position, arrowPoint.rotation);
        arrow.SetTarget(enemy);
        arrow.SetDamage(data.Towers[0].damage);
    }

    IEnumerator LookRoutine()
    {
        while (true)
        {
            if (enemyList.Count > 0)
            {
                archer.LookAt(enemyList[0].transform.position);
            }

            yield return null;
        }
    }
}
