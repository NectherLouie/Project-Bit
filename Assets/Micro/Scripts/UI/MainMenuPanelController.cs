using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Micro
{
    public class MainMenuPanelController : MonoBehaviour, 
        IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Action OnPanelClicked;

        private bool inputEnabled = false;

        public void EnableInput(bool pEnabled)
        {
            inputEnabled = pEnabled;
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (inputEnabled)
            {
                Debug.Log(name + " Game Object Clicked!");

                OnPanelClicked?.Invoke();
            }
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            //Output to console the GameObject's name and the following message
            Debug.Log("Cursor Entering " + name + " GameObject");
        }

        //Detect when Cursor leaves the GameObject
        public void OnPointerExit(PointerEventData pointerEventData)
        {
            //Output the following message with the GameObject's name
            Debug.Log("Cursor Exiting " + name + " GameObject");
        }
    }
}
