using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeBtn : MonoBehaviour
{
    Skill skill;
    // Start is called before the first frame update
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI effectText;
    public void SetText(string title,string effect)
    {
        titleText.text = title;
        effectText.text = effect;
    }
    public void SetSkill(Skill skill)
    {
        this.skill = skill;
    }
    public void Click()
    {

    }//������ �� ĳ���Ϳ� ���� �� �� �ֵ���
}
