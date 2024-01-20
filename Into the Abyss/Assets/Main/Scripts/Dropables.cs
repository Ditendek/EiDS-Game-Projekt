using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu]
public class Dropables : ScriptableObject
{
    public Dropable[] dropablesList;
    private float ratioSum = 0;

    [Serializable]
    public struct Dropable{
        public GameObject dropableGameobject;
        public int numberOfDrops;
        public float ratio;
    }

    public Dropable getDropable() {
        if(ratioSum == 0) {
            ratioSum = getRatioSum();
        }

        float rng = UnityEngine.Random.Range(0f, ratioSum);

        float n = dropablesList[0].ratio;
        int i = 0;
        while(n <= rng) {
            i++;
            n += dropablesList[i].ratio;
        }

        return dropablesList[i];
    }

    private float getRatioSum() {
        float res = 0;

        foreach (Dropable d in dropablesList) {
            res += d.ratio;
        }
        
        return res;
    }
}
