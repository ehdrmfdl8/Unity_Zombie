using UnityEngine;
// 점수와 게임 오버 여부를 관리하는 게임 매니저
public class GameManager : MonoBehaviour {
    // 싱글톤 접근용 프로퍼티

    public static int score = 0; // 현재 게임 점수

    private int savedScore;
    private string KeyString = "HighScore";
    private bool showActive = false;
    public static GameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }
    //현재 보유 골드
    public long gold
    {
        //m_gold라는 값을 사용할때 즉 ex)a=m_gold라는 값을 실행했을때
        //return PlayerPrefs.GetInt("Gold");가 동작된다.
        get
        {
            if (!PlayerPrefs.HasKey("Gold"))
            {
                return 0;
            }

            string tmpGold = PlayerPrefs.GetString("Gold");
            return long.Parse(tmpGold);
        }
        //m_gold라는 값을 설정할때 즉 ex) m_gold =30이라는 값을 실행했을때 
        //PlayerPrefs.SetInt("Gold",value);가 동작한다.
        set
        {
            PlayerPrefs.SetString("Gold", value.ToString());
        }
    }

    private static GameManager m_instance; // 싱글톤이 할당될 static 변수

    public bool isGameover { get; private set; } // 게임 오버 상태

    private void Awake() {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
        savedScore = PlayerPrefs.GetInt(KeyString);
        UIManager.instance.UpdateHighScore(savedScore);
    }

    private void Start() {
        // 플레이어 캐릭터의 사망 이벤트 발생시 게임 오버
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
    }
    private void Update()
    {
        HighScore();
        ShowUpgradeUI();
    }
    //Upgrade UI를 껐다 키게 하는 함수
    public void ShowUpgradeUI()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && showActive == false)
        {
            UIManager.instance.SetActiveUpgradeUI(true);
            showActive = true;
            Time.timeScale = 0;
        }
        if (Input.GetKeyUp(KeyCode.Tab) && showActive == true)
        {
            UIManager.instance.SetActiveUpgradeUI(false);
            showActive = false;
            Time.timeScale = 1;
        }
    }
    //최고점수를 보여주는 함수
    public void HighScore()
    {
        UIManager.instance.UpdateHighScore(savedScore);
        if (score > savedScore)
        {
            //최고 기록값을 현재 기록값으로 대체
            savedScore = score;
            PlayerPrefs.SetInt(KeyString, savedScore);
        }
        UIManager.instance.UpdateGoldText();
    }

    // 점수를 추가하고 UI 갱신
    public void AddScore(int newScore) {
        // 게임 오버가 아닌 상태에서만 점수 증가 가능
        if (!isGameover)
        {
            // 점수 추가
            score += newScore;
            // 점수 UI 텍스트 갱신
            UIManager.instance.UpdateScoreText(score);
        }
    }

    // 게임 오버 처리
    public void EndGame() {
        // 게임 오버 상태를 참으로 변경
        isGameover = true;
        // 게임 오버 UI를 활성화
        UIManager.instance.SetActiveGameoverUI(true);
    }
}