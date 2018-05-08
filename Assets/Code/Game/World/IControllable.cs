using System;
using System.Collections.Generic;
using UnityEngine;

namespace AshenCode.FloopyBirb.Bird
{
    public interface IControllable
    {
         void Control(Transform transform, Action action);

    }
}