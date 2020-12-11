using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 20;
    public int enrageAttackDamage = 40;

    public Vector3 attackOffset;
    public float attackRange = 3f;
    public LayerMask attackMask;

    public void Attack()
    {
        // collision based damage taken, will need to refactor to use bullet hit to trigger damage taken.
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<RestartOnPlayerDeath>().TakeDamage(attackDamage);
        }
    }
}
