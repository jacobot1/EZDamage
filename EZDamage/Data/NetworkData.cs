using System;
using UnityEngine;

namespace EZDamage.Data
{
    /// <summary>
    /// A serializable representation of Unity's Vector3 for network transmission.
    /// </summary>
    [Serializable]
    public struct SVector3
    {
        public float x, y, z;

        public SVector3(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }

    /// <summary>
    /// Data payload for player damage.
    /// </summary>
    [Serializable]
    public struct PlayerDamageData
    {
        public SVector3 center;
        public float radius;
        public int damage;
        public String causeOfDeath;
    }
}
