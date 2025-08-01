using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeUI : MonoBehaviour
{
    int selectSkill = 3;
    public List<UpgradeBtn> upgradeBtn;
    public void Start()
    {
        UpgradeImage();
    }
    public void UpgradeImage()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < selectSkill; i++)
        {
            int index = Random.Range(0, UpgradeManager.Instance.skillList.Count);
            Skill randomSkill = UpgradeManager.Instance.skillList[index];
            upgradeBtn[i].SetText(UpgradeManager.Instance.skillList[index].name, UpgradeManager.Instance.skillList[index].effect);
            upgradeBtn[i].SetSkill(randomSkill);
        }
    }
}
//방 클리어시 시간 멈추고 UI를 화면에 띄워주고 버튼 클릭시 적용 시키게한 후 UI사라지게 하기