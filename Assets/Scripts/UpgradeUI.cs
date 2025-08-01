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
        List<Skill> tempList = new List<Skill>(UpgradeManager.Instance.skillList);
        //���Ϲ� �� �� �Ẹ��
        for (int i = 0; i < selectSkill; i++)
        {
            Skill randomSkill = tempList[Random.Range(0, tempList.Count)];//���� ��ų �Լ��� ����� ������°�� ��ų�� randomSkill������ ����
            upgradeBtn[i].SetSkill(randomSkill);
            tempList.Remove(randomSkill);

            //Skill randomSkill = RandomSkill();
            //if (i == 0)
            //{
            //    upgradeBtn[0].SetSkill(randomSkill);//i
            //}
            //else 
            //{
            //    if (upgradeBtn[0] != null)//i==1
            //    {
            //        while (randomSkill != upgradeBtn[0].GetSkill())//i-1 ������ų�̶� ���׷��̵��ư 0���� ��ų�� �ٸ� �� ���� ��ų�� �ٽ� �̾ƾ� ��
            //        {
            //            upgradeBtn[1].SetSkill(randomSkill);//randomSkill=RandomSkill();
            //        }
            //    }
            //    else//i==2�϶�
            //    {
            //        while (randomSkill != upgradeBtn[0].GetSkill() && randomSkill != upgradeBtn[1].GetSkill())//0���̶� 1���̶� �Ѵ� �ٸ� ��
            //        {
            //            upgradeBtn[2].SetSkill(randomSkill);//randomSkill=RandomSkill();
            //        }
            //    }
            //    //upgradeBtn[i].SetSkill(randomSkill);
            //}

            //���� ���õ� ��ų�� ���׷��̵� ��ư[i - 1].skill == randomskill�̶� ���ٸ� �ٽ� ������ ���� �־���� ��>> �ٽ� �˻�
            //    while (���׷��̵� ��ư[i - 1].skill == randomskill)                
        }        
    }
    Skill RandomSkill()
    {
        Skill r = UpgradeManager.Instance.skillList[Random.Range(0, UpgradeManager.Instance.skillList.Count)];
        //���׷��̵� �Ŵ����� �̱���ȭ�� ��ų����Ʈ�� 0��° ���� ��ų����Ʈ�� ��������° ���������� ������° ������ r�� ����
        return r;//���� ����� ��ų r�� ������ ���� ������ų �Լ��� �ٽ� ��ų ���·� ��
    }
}