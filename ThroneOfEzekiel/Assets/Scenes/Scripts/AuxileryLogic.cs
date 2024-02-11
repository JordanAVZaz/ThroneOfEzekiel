using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public static class AuxileryLogic
{

    // returns true if it finds the component of an interactive object
    public static bool IsIt<T>(InteractiveObject sample) where T : UnityEngine.Component
    {
        if (sample == null) return false;
        return sample.GetComponent<T>() != null;
    }


    // 
    public static T GetIt<T>(InteractiveObject sample) where T : InteractiveObject
    {
        return sample.GetComponent<T>();
    }

}
