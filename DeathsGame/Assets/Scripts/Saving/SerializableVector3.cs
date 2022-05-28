using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Saving
{
    [Serializable]
    public class SeriliazableVector3
    {
        private float x, y, z;

        public SeriliazableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }
    }
}
