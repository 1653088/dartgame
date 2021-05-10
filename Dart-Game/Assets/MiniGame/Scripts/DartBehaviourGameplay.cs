using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CLL;
//using Imba.Utils;
//using Imba.UI;
using System;

public class DartBehaviourGameplay : DartGameManager
{
    //public Camera cam;
    [SerializeField]
    private GameObject _dart = null;
    [SerializeField]
    private GameObject _centralPoint = null;
    [SerializeField]
    private GameObject _cloneDart = null;
    [SerializeField]
    private float _shootForce = 4f;
    [SerializeField]
    private float _xMinLength = 1.0f;


    private static float _xCollided = 0f;
    private static float _yCollided =0f;
    private static float _xDownMouse =0f;
    private static float _yDownMouse =0f;
    private float _coolDownTime = 0.0f;
    private float _maxTime = 2.0f;
    private int _scorePoints = 0;
    private int _score = 0;
    private int _pointInDB = 0;
    private float _lengthFlick= 0f;
    private int _numStepCountScore = 0;
    private bool _isDartThrown = false;
    private bool _isFlicked = false;
    private float[,] _arrLength= {{0.178f, 0.372f },{ 2.126f, 2.326f},{3.493f,3.729f } };
    private void Start( )
    {
        AmountDart = 5;
    }
    private void Update( )
    {
        /*RaycastHit hit;
        var mouseX = Input.mousePosition.x;
        var mouseY = Input.mousePosition.y;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(mouseX,mouseY,0));
        if(Physics.Raycast(ray, out hit, 200) && AmountDart>0)
        {
            if(Input.GetMouseButtonDown(0))
            {
                _coolDownTime = 0.0f;
                _coolDownTime += Time.deltaTime;
                _xDownMouse = Input.mousePosition.x;
                _yDownMouse = Input.mousePosition.y;
            }
            _coolDownTime += Time.deltaTime;
            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                if(_coolDownTime <= _maxTime)
                {
                    mouseX = Input.mousePosition.x;
                    mouseY = Input.mousePosition.y;
                    
                    var distanceX = (mouseX - _xDownMouse) / 100;
                    if(distanceX > _xMinLength)
                    {
                        return;
                    }
                    var distanceY =(mouseY - _yDownMouse)/100;
                    if(distanceY <= 0)
                    {
                        return;
                    }
                    ray = Camera.main.ScreenPointToRay(new Vector3(mouseX, mouseY, 0));
                    GameObject newDart = Instantiate(_dart, hit.point, Quaternion.identity);
                    newDart.GetComponent<Rigidbody>( ).isKinematic = false;
                    newDart.GetComponent<Rigidbody>( ).velocity = (transform.forward + transform.up) * (_shootForce + (float)distanceY) + transform.right * distanceX;
                    AmountDart--;
                }
                else
                {
                    Debug.Log("Flick Faster");
                } 
            }
        }*/
        var mouseX = Input.mousePosition.x;
        var mouseY = Input.mousePosition.y;
        Vector3 dartPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseX,mouseY, 0));
        Vector3 dartClonePos = Camera.main.ScreenToWorldPoint(new Vector3(mouseX,mouseY, 15));
        if(AmountDart > 0)
        {
            // Dart at position flick before the dart is thrown
            if(_isFlicked)
            {
                _cloneDart.transform.position = dartClonePos;
            }

            //handle dart
            if(Input.GetMouseButtonDown(0))
            {
                _isFlicked = true;
                _coolDownTime = 0.0f;
                _coolDownTime += Time.deltaTime;
                _xDownMouse = Input.mousePosition.x;
                _yDownMouse = Input.mousePosition.y;
            }
            _coolDownTime += Time.deltaTime;
            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                _isFlicked = false;
                if(_coolDownTime <= _maxTime)
                {
                    mouseX = Input.mousePosition.x;
                    mouseY = Input.mousePosition.y;

                    var distanceX = (mouseX - _xDownMouse) / 100;
                    if(distanceX > _xMinLength)
                    {
                        return;
                    }
                    var distanceY =(mouseY - _yDownMouse)/100;
                    if(distanceY <= 0)
                    {
                        return;
                    }
                    dartPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 8));
                    GameObject newDart = Instantiate(_dart, dartPos, Quaternion.identity);
                    newDart.GetComponent<Rigidbody>( ).isKinematic = false;
                    newDart.GetComponent<Rigidbody>( ).velocity = (transform.forward + transform.up) * (_shootForce + (float)distanceY) + transform.right * distanceX;
                    AmountDart--;
                }
                else
                {
                    Debug.Log("Flick Faster");
                }
            }
        }
        if(Dart._isCollideDartBoard)
        {
            Dart._isCollideDartBoard = false;
            _xCollided = Dart.xCollide;
            _yCollided = Dart.yCollide;
            _lengthFlick = Mathf.Sqrt((_centralPoint.transform.position.x - _xCollided) * (_centralPoint.transform.position.x - _xCollided) + (_centralPoint.transform.position.y - _yCollided) * (_centralPoint.transform.position.y - _yCollided));
            double radianCorner = Math.Atan2((_yCollided - _centralPoint.transform.position.y), (_xCollided - _centralPoint.transform.position.x));
            double degCorner = radianCorner * 180 / Math.PI;
            int intCorner = (int) degCorner;
            
            if(intCorner < 0)
            {
                intCorner = 180 + (intCorner + 180);
            }
            GetNumberThrown(intCorner);
            switch(GetRing(_xCollided, _yCollided))
            {
                case 0:
                {
                    _score = _pointInDB;
                    UIGameplay.Bonus = "";
                    break;
                }
                case 1:
                {
                    _score = _pointInDB * 2;
                    UIGameplay.Bonus = "Double";
                    break;
                }
                case 2:
                {
                    _score = _pointInDB * 3;
                    UIGameplay.Bonus = "Triple";
                    break;
                }
                case 3:
                {
                    _score = 25;
                    UIGameplay.Bonus = "Bull";
                    break;
                }
                case 4:
                {
                    _score = 50;
                    UIGameplay.Bonus = "Bulls Eye";
                    break;
                }
                case -1:
                {
                    _score = 0;
                    UIGameplay.Bonus = "";
                    break;
                }
            }
            if(!Dart._isCollideDartBoard)
            {
                _isDartThrown = true;
            }
        }
        if(_isDartThrown)
        {
            _isDartThrown = false;
            _scorePoints += _score;
            UIGameplay.Score = _score.ToString( );
            UIGameplay.SumScore = _scorePoints.ToString( );
            UIGameplay.IsThrown = true;
        }
    }
    #region calculate Point
    private void GetNumberThrown(int radian)
    {
        if(radian >= 63 && radian < 81)
            _pointInDB = 1;
        else if(radian >= 297 && radian < 315)
            _pointInDB = 2;
        else if(radian >= 261 && radian < 279)
            _pointInDB = 3;
        else if(radian >= 27 && radian < 45)
            _pointInDB = 4;
        else if(radian >= 99 && radian < 117)
            _pointInDB = 5;
        else if(radian >= 351 && radian < 9)
            _pointInDB = 6;
        else if(radian >= 225 && radian < 243)
            _pointInDB = 7;
        else if(radian >= 189 && radian < 207)
            _pointInDB = 8;
        else if(radian >= 135 && radian < 153)
            _pointInDB = 9;
        else if(radian >= 333 && radian < 351)
            _pointInDB = 10;
        else if(radian >= 171 && radian < 189)
            _pointInDB = 11;
        else if(radian >= 117 && radian < 135)
            _pointInDB = 12;
        else if(radian >= 9 && radian < 27)
            _pointInDB = 13;
        else if(radian >= 153 && radian < 171)
            _pointInDB = 14;
        else if(radian >= 315 && radian < 333)
            _pointInDB = 15;
        else if(radian >= 207 && radian < 225)
            _pointInDB = 16;
        else if(radian >= 279 && radian < 297)
            _pointInDB = 17;
        else if(radian >= 45 && radian < 63)
            _pointInDB = 18;
        else if(radian >= 243 && radian < 261)
            _pointInDB = 19;
        else if(radian >= 81 && radian < 99)
            _pointInDB = 20;
        return;
    }

    private int GetRing(float x, float y)
    {
        double len = Mathf.Sqrt((_centralPoint.transform.position.x - x) * (_centralPoint.transform.position.x - x) + (_centralPoint.transform.position.y - y) * (_centralPoint.transform.position.y - y));
        if(len > _arrLength[2, 1])
            return -1;
        if(len >= _arrLength[2, 0] && len <= _arrLength[2, 1])
            return (int)Ring.Double;
        else if(len >= _arrLength[1, 0] && len <= _arrLength[1, 1])
            return (int)Ring.Triple;
        else if(len >= _arrLength[0, 0] && len <= _arrLength[0, 1])
            return (int)Ring.Bull;
        else if(len < _arrLength[0, 0])
            return (int)Ring.BullsEye;
        else
            return (int)Ring.None;
    }
    #endregion
}
