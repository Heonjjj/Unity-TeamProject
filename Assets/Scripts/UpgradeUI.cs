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
        //와일문 한 번 써보기
        for (int i = 0; i < selectSkill; i++)
        {
            int index = Random.Range(0, UpgradeManager.Instance.skillList.Count);
            Skill randomSkill = UpgradeManager.Instance.skillList[index];
            if (upgradeBtn[0] == null)
            {
                upgradeBtn[i].SetText(UpgradeManager.Instance.skillList[index].name, UpgradeManager.Instance.skillList[index].effect);
                upgradeBtn[i].SetSkill(randomSkill);
            }
            else if (upgradeBtn[i] != upgradeBtn[0])
            {
                upgradeBtn[i].SetText(UpgradeManager.Instance.skillList[index].name, UpgradeManager.Instance.skillList[index].effect);
                upgradeBtn[i].SetSkill(randomSkill);
                //지금 선택된 스킬이 업그레이드 버튼[i-1].skill==randomskill이랑 같다면 다시 랜덤한 값을 넣어줘야 함>>다시 검사
                //while(업그레이드 버튼[i-1].skill==randomskill)
                //{

                //}
            }
            else if (!(upgradeBtn[0] || upgradeBtn[1] || upgradeBtn[2]))
            {
                upgradeBtn[i].SetText(UpgradeManager.Instance.skillList[index].name, UpgradeManager.Instance.skillList[index].effect);
                upgradeBtn[i].SetSkill(randomSkill);
            }
            else
            {
                i--;
            }
        }
    }
}
//방 클리어시 시간 멈추고 UI를 화면에 띄워주고 버튼 클릭시 적용 시키게한 후 UI사라지게 하기