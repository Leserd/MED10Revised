using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour {

    public static Sprite A_Stars(int stars)
    {
            var load = Resources.LoadAll<Sprite>("Stars");
            return load[stars];        
    }
    


}
