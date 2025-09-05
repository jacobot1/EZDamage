using EZDamage.Controllers;
using EZDamage.Data;
using System;
using UnityEngine;

namespace EZDamage
{
    /// <summary>
    /// Public API for other mods to call to inflict networked damage.
    /// </summary>
    public static class API
    {
        /// <summary>
        /// Inflicts damage to players within a specified radius.
        /// </summary>
        /// <param name="center">The centerpoint of a sphere within which to damage players</param>
        /// <param name="radius">The radius of a sphere within which to damage players</param>
        /// <param name="damage">The amount of damage to inflict (in Shovel hits)</param>
        /// <param name="causeOfDeath">The cause of death to display if a player dies from the damage</param>
        public static void DamagePlayers(Vector3 center, float radius = 1f, int damage = 1, CauseOfDeath causeOfDeath = CauseOfDeath.Unknown)
        {
            var data = new PlayerDamageData
            {
                center = new SVector3(center),
                radius = radius,
                damage = damage,
                causeOfDeath = causeOfDeath.ToString()
            };
            EZDamage.PlayerDamageMessage.SendServer(data);
        }

        /// <summary>
        /// Inflicts damage to enemies within a specified radius.
        /// </summary>
        /// <param name="center">The centerpoint of a sphere within which to damage enemies</param>
        /// <param name="radius">The radius of a sphere within which to damage enemies</param>
        /// <param name="damage">The amount of damage to inflict (in Shovel hits)</param>
        public static void DamageEnemies(Vector3 center, float radius = 1f, int damage = 1)
        {
            DamageController.DamageOrKillEnemiesInRadius(center, radius, damage);
        }

        /// <summary>
        /// Kills players within a specified radius.
        /// </summary>
        /// <param name="center">The centerpoint of a sphere within which to kill players</param>
        /// <param name="radius">The radius of a sphere within which to kill players</param>
        /// <param name="causeOfDeath">The cause of death to display</param>
        public static void KillPlayers(Vector3 center, float radius = 1f, CauseOfDeath causeOfDeath = CauseOfDeath.Unknown)
        {
            var data = new PlayerDamageData
            {
                center = new SVector3(center),
                radius = radius,
                damage = -1,
                causeOfDeath = causeOfDeath.ToString()
            };
            EZDamage.PlayerDamageMessage.SendServer(data);
        }

        /// <summary>
        /// Kills enemies within a specified radius.
        /// </summary>
        /// <param name="center">The centerpoint of a sphere within which to kill enemies</param>
        /// <param name="radius">The radius of a sphere within which to kill enemies</param>
        public static void KillEnemies(Vector3 center, float radius = 1f)
        {
            DamageController.DamageOrKillEnemiesInRadius(center, radius, -1);
        }

        /// <summary>
        /// Damages everything within a specified radius.
        /// </summary>
        /// <param name="center">The centerpoint of a sphere within which to inflict damage</param>
        /// <param name="radius">The radius of a sphere within which to inflict damage</param>
        /// <param name="damage">The amount of damage to inflict (in Shovel hits)</param>
        /// <param name="causeOfDeath">The cause of death to display if a player dies from the damage</param>
        public static void DamageEverything(Vector3 center, float radius = 1f, int damage = 1, CauseOfDeath causeOfDeath = CauseOfDeath.Unknown)
        {
            DamagePlayers(center, radius, damage, causeOfDeath);
            DamageEnemies(center, radius, damage);
        }

        /// <summary>
        /// Kills everything within a specified radius.
        /// </summary>
        /// <param name="center">The centerpoint of a sphere within which to kill things</param>
        /// <param name="radius">The radius of a sphere within which to kill things</param>
        /// <param name="causeOfDeath">The cause of death to display for players</param>
        public static void KillEverything(Vector3 center, float radius = 1f, CauseOfDeath causeOfDeath = CauseOfDeath.Unknown)
        {
            DamagePlayers(center, radius, -1, causeOfDeath);
            DamageEnemies(center, radius, -1);
        }
    }
}