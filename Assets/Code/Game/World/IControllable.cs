using System;
using System.Collections.Generic;
using UnityEngine;

namespace AshenCode.FloopyBirb.Agents
{
    public interface IControllable
    {
         void Control(Transform transform, Action action);

    }
}