using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VWPrototype
{
    [CreateAssetMenu(menuName = "VacuumWar/Prototype/ShooterHeadParams")]
    public class ShooterHeadParams : ScriptableObject
    {
        public Rigidbody projectile;
        public float launchForce = 200f;
        public float damage = 10;
        public int ammoConsumePerRound = 1;

        // Todo: Shooting Feeling parameters
        // launchForce -> MuzzleVelocity
        // FireMode: Semi / Burst / Full
        // RoundMode: SingleBullet / Shotgun / Laser
        // RoundsPerMinute
        // RecoilHor
        // RecoilVer
        // RecoilRecorverSpeed
        // SpreadAngleIncreaseSpeed
        // SpreadAngleMax
        // SpreadRecoverSpeed
    }
}