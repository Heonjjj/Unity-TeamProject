using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public string name;
    public string effect;
    int statType;
    public float value;
    public Skill(string name, string effect,int statType, float value)
    {
        this.name = name;
        this.effect = effect;
        this.statType = statType;
        this.value = value;
    }//��ų �����ֱ�

    public virtual void SelectSkill(Character chracter)
    {
        
    }//��ų�� �������� ��



}



//������ ��ų���� ���۷��̵� �۾�

//��ų UIǥ��
//���͸� �� ��� ���� Ŭ�������� ��
//3������ �������� ȭ�鿡 ����ְ�
//3������ �ϳ��� ���콺�� Ŭ���ϸ� ü��,ü��ȸ��,���ݷ�,���ݼӵ�,�̵��ӵ��� ������ �ϱ�

//���� ���͸� ����� ��
//ȭ�� ����,ȭ�� �߰�,ȭ�� ƨ���