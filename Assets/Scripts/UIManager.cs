using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드

// 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저
public class UIManager : MonoBehaviour {
    // 싱글톤 접근용 프로퍼티
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 변수
    public Text ammoText; // 탄약 표시용 텍스트
    public Text scoreText; // 점수 표시용 텍스트
    public Text waveText; // 적 웨이브 표시용 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화할 UI
    public GameObject upgradeUI; //업그레이드 UI
    public Text highScore; // 최고점수 텍스트
    public Text hpText; //현재 플레이어 스텟
    public Text damageText; // 현재 플레이어 총 데미지
    public Text goldText; //현재 골드량 텍스트
    public void UpdateHighScore(int Score)
    {
        highScore.text = "High Score : " + Score;
    }
    // 탄약 텍스트 갱신
    public void UpdateAmmoText(int magAmmo, int remainAmmo) {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }

    // 점수 텍스트 갱신
    public void UpdateScoreText(int newScore) {
        scoreText.text = "Score : " + newScore;
    }

    // 적 웨이브 텍스트 갱신
    public void UpdateWaveText(int waves, int count) {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count;
    }

    // 게임 오버 UI 활성화
    public void SetActiveGameoverUI(bool active) {
        gameoverUI.SetActive(active);
    }

    // 업그레이드 UI 활성화
    public void SetActiveUpgradeUI(bool active)
    {
        upgradeUI.SetActive(active);
    }
    //현재 캐릭터 체력상태
    public void UpdatePlayerHPText(float HP, float MaxHP)
    {
        hpText.text = "HP : " + HP + "/" + MaxHP;
    }
    //현재 캐릭터 총 데메지
    public void UpdateGunPowerText(float Damage)
    {
        damageText.text = "Deal : " + Damage;
    }
    //현재 골드
    public void UpdateGoldText()
    {
        goldText.text = "Gold : " + GameManager.instance.gold;
    }
    // 게임 재시작
    public void GameRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}