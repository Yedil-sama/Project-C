using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Cards
{
    public interface IRace
    {
        string RaceName { get; }

        void ApplyRaceModifier(Unit unit); 
    }

}
