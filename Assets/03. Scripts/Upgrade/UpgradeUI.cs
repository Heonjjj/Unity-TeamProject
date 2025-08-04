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
        if (player.GetComponent<PlayerAttack>().multiShot2)//시간 관계상 내가 생각한 방법으로 진행
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
        //플레이어에서 체크 하고 체크가 돼있으면 해당하는 스킬을 제거
        for (int i = 0; i < selectSkill; i++)
        {
            Skill randomSkill = tempList[Random.Range(0, tempList.Count)];//랜덤 스킬 함수에 저장된 랜덤번째의 스킬을 randomSkill변수에 저장
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
            //        while (upgradeBtn[i-1]= upgradeBtn[0].GetSkill())//i-1 랜덤스킬이랑 업그레이드버튼 0번의 스킬이 다를 때 랜덤 스킬을 다시 뽑아야 함
            //        {
            //            upgradeBtn[1].SetSkill(randomSkill);//randomSkill=RandomSkill();
            //        }
            //    }
            //    else//i==2일때
            //    {
            //        while (randomSkill != upgradeBtn[0].GetSkill() && randomSkill != upgradeBtn[1].GetSkill())//0번이랑 1번이랑 둘다 다를 때
            //        {
            //            upgradeBtn[2].SetSkill(randomSkill);//randomSkill=RandomSkill();
            //        }
            //    }
            //    //upgradeBtn[i].SetSkill(randomSkill);
            //}

            //지금 선택된 스킬이 업그레이드 버튼[i - 1].skill == randomskill이랑 같다면 다시 랜덤한 값을 넣어줘야 함>> 다시 검사
            //    while (업그레이드 버튼[i - 1].skill == randomskill)                
        }
    }
    //Skill RandomSkill()
    //{
    //    Skill r = UpgradeManager.Instance.skillList[Random.Range(0, UpgradeManager.Instance.skillList.Count)];
    //    //업그레이드 매니저에 싱글톤화된 스킬리스트의 0번째 부터 스킬리스트의 마지막번째 순서까지의 랜덤번째 순서를 r에 저장
    //    return r;//랜덤 저장된 스킬 r을 리턴을 통해 랜덤스킬 함수에 다시 스킬 형태로 줌
    //}
}