using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Basic critter that only patrols around.
    /// </summary>
    public class Patroller : Critter
    {
        private void Update()
        {
            PatrolUpdateLogic();
        }

        private void FixedUpdate()
        {
            PatrolFixedUpdateLogic();
        }
    }
}
