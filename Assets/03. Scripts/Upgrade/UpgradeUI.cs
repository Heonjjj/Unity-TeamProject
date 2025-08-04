using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeUI : MonoBehaviour
{
    int selectSkill = 3;
    public List<UpgradeBtn> upgradeBtn;
    Player player;
    public void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }
    public void Start()
    {
        GameManager.Instance.upgradeUI = this;
        gameObject.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }
    public void UpgradeImage()
    {
        gameObject.SetActive(true);
        List<Skill> tempList = new List<Skill>(UpgradeManager.Instance.skillList);
        if (player.GetComponent<PlayerAttack>().multiShot2)//�ð� ����� ���� ������ ������� ����
        {
            tempList.Remove(tempList[6]);
        }
        else
        {
            tempList.Remove(tempList[5]);
        }
        if (player.GetComponent<PlayerAttack>().multiShot3)
        {
            tempList.Remove(tempList[5]);
        }
        //�÷��̾�� üũ �ϰ� üũ�� �������� �ش��ϴ� ��ų�� ����
        for (int i = 0; i < selectSkill; i++)
        {
            Skill randomSkill = tempList[Random.Range(0, tempList.Count)];//���� ��ų �Լ��� ����� ������°�� ��ų�� randomSkill������ ����
            upgradeBtn[i].SetSkill(randomSkill);
            tempList.Remove(randomSkill);

            //Skill randomSkill = RandomSkill();
            //if (i == 0)
            //{
            //    upgradeBtn[i].SetSkill(randomSkill);//i
            //}
            //else 
            //{
            //    if (i==1)
            //    {
            //        while (upgradeBtn[i-1]= upgradeBtn[0].GetSkill())//i-1 ������ų�̶� ���׷��̵��ư 0���� ��ų�� �ٸ� �� ���� ��ų�� �ٽ� �̾ƾ� ��
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
    //Skill RandomSkill()
    //{
    //    Skill r = UpgradeManager.Instance.skillList[Random.Range(0, UpgradeManager.Instance.skillList.Count)];
    //    //���׷��̵� �Ŵ����� �̱���ȭ�� ��ų����Ʈ�� 0��° ���� ��ų����Ʈ�� ��������° ���������� ������° ������ r�� ����
    //    return r;//���� ����� ��ų r�� ������ ���� ������ų �Լ��� �ٽ� ��ų ���·� ��
    //}
}