using UnityEngine;
using System.Collections;

public class Character {

    public void setValues(GameObject _Portraits, bool _Heaven )
    {
        Portraits = _Portraits;
        Heaven = _Heaven;
    }

    public GameObject Portrait()
    {
        return Portraits;
    }

    public bool Side()
    {
        return Heaven;
    }

    private GameObject Portraits;
    private bool Heaven;
}
