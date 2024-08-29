using UnityEngine;

// 왼쪽 끝으로 이동한 배경을 오른쪽 끝으로 재배치하는 스크립트
public class BackgroundLoop : MonoBehaviour {
    private BoxCollider2D boxColider = null;
    private float width; // 배경의 가로 길이

    private void Awake() {
        // 가로 길이를 측정하는 처리
        boxColider = GetComponent<BoxCollider2D>();
        width = boxColider.size.x;

    }

    private void Update() {
        // 현재 위치가 원점에서 왼쪽으로 width 이상 이동했을때 위치를 리셋

        //현재의 오브젝트의 위치가 -width 보다 작아지면
        if(transform.position.x <= -width)
        {
            //위치를 리셋함
            Reposition();
        }
    }

    // 위치를 리셋하는 메서드
    private void Reposition() {
        //offset을 width의 2배만큼 설정
        //-width가 되면 width 2번을 뛰어넘어야 함
        Vector2 offset = new Vector2(width * 2f, 0);
        //위에서 계산한 offset을 더해서 위치를 다시 설정
        //(앞에서부터 다시 뒤로 움직일 수 있도록)
        transform.position = (Vector2)transform.position + offset;
    }
}