using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mousecursor : MonoBehaviour
{
    private RectTransform rectTransform;
    private Animator animator;
    public Vector2 offset = new Vector2(10f, -10f);  // 좌측 위로 살짝 이동

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
        Cursor.visible = false; // 시스템 커서 숨기기
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

        // 마우스 누르고 있는 상태를 애니메이터에 전달
        bool isPressed = Input.GetMouseButton(0); // 누르고 있는 동안 true
        animator.SetBool("IsClick", isPressed);
    }
}
