using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Stats
{
    public interface IModifierProvider
    {
        IEnumerator<float> GetAdditiveModifier(Stat stat);
    }
}

