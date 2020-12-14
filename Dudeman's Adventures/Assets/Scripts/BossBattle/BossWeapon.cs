using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{

    private float timeBtwShots;
    public float startTimeBtwShots;

    public Transform player;
    public GameObject AIBulletController;
    public Transform ShootPoint;
    public Transform Muzzle;

    public int attackDamage = 20;
    public int enragedAttackDamage = 200;

    public Vector3 attackOffset;
    public float attackRange = 3f;
    private float enrageAttackRange = 2f;
    public LayerMask attackMask;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }

    public void Attack()
    {
        Shoot();
    }

    public void EnragedAttack()
    {
        // collision based damage taken
        Vector3 pos = transform.position;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, enrageAttackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<RestartOnPlayerDeath>().TakeDamage(enragedAttackDamage);
        }
    }

    void Shoot()
    {
        Instantiate(AIBulletController, Muzzle.position, Quaternion.identity);
        timeBtwShots = startTimeBtwShots * Time.fixedDeltaTime;
        // PlayRandomShoot();
    }
}
