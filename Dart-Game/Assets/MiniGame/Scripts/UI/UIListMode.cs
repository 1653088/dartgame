using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CLL;
//using Imba.Utils;
//using Imba.UI;

namespace CLL {
    public class UIListMode: MonoBehaviour
    {
        [SerializeField]
        private GameObject _mainUI = null;
        [SerializeField]
        private Button _home = null;

        /*protected override void OnShown( )
        {
            base.OnShown( );
            if(Parameter == null)
            {
                Hide( );
                return;
            }
        }*/

        public void ClickButtonHome( )
        {
            _mainUI.SetActive(false);
        }
    }
}
