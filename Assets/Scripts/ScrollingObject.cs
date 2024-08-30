using UnityEngine;

// 게임 오브젝트를 계속 왼쪽으로 움직이는 스크립트
public class ScrollingObject : MonoBehaviour {
    public float speed = 10f; // 이동 속도

    private void Update() {
        // 게임 오브젝트를 왼쪽으로 일정 속도로 평행 이동하는 처리
        //Transform.Translate(벡터값)
        //벡터값의 힘과 방향으로 물체를 이동시킴
        //Time.deltaTime -> 프레임을 균등하게 배분시켜주는 역할

        if(!GameManager.instance.isGameover)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}