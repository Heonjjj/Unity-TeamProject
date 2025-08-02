using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GlobalUIClickSound : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clicked = EventSystem.current.currentSelectedGameObject;

            if (clicked != null && clicked.GetComponent<Button>() != null)
            {
                AudioManager.Instance.PlayClickSFX();
            }
        }
    }
}
