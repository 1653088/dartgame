using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CLL;
//using Imba.Utils;
//using Imba.UI;

public class UIModeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiListMode = null;
    public void ClickPlayClassic( )
    {
        _uiListMode.SetActive(false);
    }
}
