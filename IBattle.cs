using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
    public interface IBattle
    {
        void Battle();
        IEnumerator Fight();
        void GetClosestEnemy(Collider[] enemys);
        IEnumerator StuckToTarget();
    }
}