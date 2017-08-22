using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GV.save();
        GV.load();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
