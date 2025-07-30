using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI effectText;
    public void SetText(string title,string effect)
    { 
        titleText.text = title;
        effectText.text = effect;   
    }

}
