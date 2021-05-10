using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartGameManager : MonoBehaviour
{
    [SerializeField]
    private int _point = 0;
    public static int AmountDart = 5;
    public enum ModeDartGame
    {
        Classic,
        Points101,
        AroundTheWorld,
        AroundTheMovingWorld
    }
    public enum Ring
    {
        None,
        Double,
        Triple,
        Bull,
        BullsEye
    }
}
