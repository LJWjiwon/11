using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {
   public AudioClip deathClip; // 사망시 재생할 오디오 클립
   public float jumpForce = 700f; // 점프 힘

   private int jumpCount = 0; // 누적 점프 횟수
   private bool isGrounded = false; // 바닥에 닿았는지 나타냄
   private bool isDead = false; // 사망 상태

   private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
   private Animator animator; // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

    private void Awake()
    {
        //Awake에서 Component 객체를 할당 해줌
        //GetComponent 라는 함수를 통해서 가져옴 
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    private void Start() {
       // 초기화
   }

   private void Update() {
        // 사용자 입력을 감지하고 점프하는 처리
        if (isDead) //죽은 상태라면 
        {
            //아래 로직을 수행x
            return;
        }

        //Input.GetMouseButtonDowon(0)
        //마우스 왼쪽 버튼을 눌렀을 때 true를 반환
        //1은 마우스 오른쪽 버튼을 눌렀을 때 true를 반환
        //Input.GetMouseButton(0)
        //마우스 계속 입력됨
        if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            //점프 횟수 증가
            jumpCount++;
            //player의 속력을 순간적으로 0으로 초기화
            playerRigidbody.velocity = Vector2.zero;
            //RigidBody.AddForce(Vector2)
            //Vector의 방향으로 힘을 가함
            //지금은 y축으로 jumpforce 만큼 힘을 가함
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            //AudioSource에 있는 Clip을 재생함
            //지금은 jump 클립이 들어가 있기 때문에 점프하는 소리 재생
            playerAudio.Play();
        }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            //마우스 왼쪽 버튼에서 손을 떼는 순간
            //현재 속도를 절반으로 변경
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }


        //Jump하는 애니메이션 재생
        //animator.SetBool("파라메터 이름", 값);
        //animator의 파라메터에 값을 전달해줌(파라메터 이름 틀리면 안됨)
        animator.SetBool("Grounded", isGrounded);
    }

   private void Die() {
        // 사망 처리
        animator.SetTrigger("Die");

        //죽었을 때 사망소리 재생
        playerAudio.clip = deathClip;
        playerAudio.Play(); //재생

        //움직이면 안되기 때문에 Vector2.zero로 초기화
        //즉 속력이 0이 되는 것임
        playerRigidbody.velocity = Vector2.zero;
        isDead = true;

        //GameManager에 있는 OnPlayerDead 함수를 호출함
        GameManager.instance.OnPlayerDead();
   }

   private void OnTriggerEnter2D(Collider2D other) {
       // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
       //부딛힌 물체의 태그가 Dead이고 죽지않은 상태일 때
       if(other.CompareTag("Dead") && !isDead)
        {
            //Die 로직 실행
            Die();
        }
   }

   private void OnCollisionEnter2D(Collision2D collision) {
        // 바닥에 닿았음을 감지하는 처리
        //collision.contacts[0] - > 닿인 물체의 첫번째 요소
        //normal -> 방향만 가지고 있는 벡터 (ex) (0,1), (1,0)) )
        //닿인 지점의 각도가 70도 이상일 때 로직 수행
        if (collision.contacts[0].normal.y > 0.7f)
        {
            //isGrounded를 true로 설정
            isGrounded = true;
            //점프카운트 초기화
            jumpCount = 0;
        }
   }

   private void OnCollisionExit2D(Collision2D collision) {
       // 바닥에서 벗어났음을 감지하는 처리
       isGrounded = false;
   }
}