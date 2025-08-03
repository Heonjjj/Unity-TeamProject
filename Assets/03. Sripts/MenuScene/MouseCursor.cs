using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mousecursor : MonoBehaviour
{
    private RectTransform rectTransform;
    private Animator animator;
    public Vector2 offset = new Vector2(10f, -10f);  // ���� ���� ��¦ �̵�

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
        Cursor.visible = false; // �ý��� Ŀ�� �����
    }

    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            Input.mousePosition,
            null,
            out pos
        );
        rectTransform.localPosition = pos+offset;

        // ���콺 ������ �ִ� ���¸� �ִϸ����Ϳ� ����
        bool isPressed = Input.GetMouseButton(0); // ������ �ִ� ���� true
        animator.SetBool("IsClick", isPressed);
    }
}
