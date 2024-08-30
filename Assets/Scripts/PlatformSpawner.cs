using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour {
    public GameObject platformPrefab; // 생성할 발판의 원본 프리팹
    public int count = 3; // 생성할 발판의 개수

    public float timeBetSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    public float yMin = -3.5f; // 배치할 위치의 최소 y값
    public float yMax = 1.5f; // 배치할 위치의 최대 y값
    private float xPos = 20f; // 배치할 위치의 x 값

    private GameObject[] platforms; // 미리 생성한 발판들
    private int currentIndex = 0; // 사용할 현재 순번의 발판

    private Vector2 poolPosition = new Vector2(0, -25); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float lastSpawnTime; // 마지막 배치 시점


    void Start() {
        // 변수들을 초기화하고 사용할 발판들을 미리 생성
        platforms = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            //배열의 요소에 게임 오브젝트를 생성해서 집어넣음
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
        }
        //마지막 생성 시점 초기화
        lastSpawnTime = 0f;
        //다음 배치 간격 초기화
        timeBetSpawn = 1f;

    }

    void Update() {
        // 순서를 돌아가며 주기적으로 발판을 배치
        if(GameManager.instance.isGameover)
        {
            return;
        }
        //쿨타임 2초
        if(Time.time >= lastSpawnTime + timeBetSpawn)
        {
            //발판 활성화 될 때 시간을 측정함
            lastSpawnTime = Time.time;//1초
            //다음 활성화 까지의 시간을 랜덤으로 설정함
            timeBetSpawn = Random.Range(timeBetSpawn, timeBetSpawnMax);
            //활성화 될 발판의 y위치를 랜덤으로 설정함
            float yPos = Random.Range(yMin, yMax);

            //현재 활성화 되어야 하는 인덱스의 게임 오브젝트를 
            //껐다가 다시 킴(그래야 OnEnable이 동작하기 때문)
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            //생성되는 발판의 위치를 정의
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);
            //순번을 다음으로 넘김
            currentIndex++;

            //3개를 생성했는데, 인덱스가 3이 넘어버리면 안되니까
            //인덱스가 3이상이 되면 다시 인덱스를 0으로 초기화 해줌
            if(currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}