using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CLL;
//using Imba.Utils;
//using Imba.UI;

public class UIGamePlay : MonoBehaviour
{
    [SerializeField]
    private GameObject _101Point = null;
    [SerializeField]
    private GameObject _pointADart = null;
    [SerializeField]
    private TMP_Text _amountDart = null;
    [SerializeField]
    private TMP_Text _time = null;
    [SerializeField]
    private TMP_Text _score = null;
    [SerializeField]
    private TMP_Text _bestScore = null;
    [SerializeField]
    private TMP_Text _textTutorial = null;
    [SerializeField]
    private TMP_Text _winText = null;
    float tempTime = 60f;
    private void Update( )
    {
        tempTime -= Time.deltaTime;
        float seconds = Mathf.FloorToInt(tempTime % 60);
        _time.text = seconds.ToString( );
        if(tempTime <= 0)
        {
            TimerEnded( );
        }

        void TimerEnded( )
        {
            _winText.text = "You Lost!";
            //UIManager.ShowPopup(UIPopupName.Dart_Score.ToString());
        }
    }

}
