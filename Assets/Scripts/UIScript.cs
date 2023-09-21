using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coins; //����� ����� �� ����� ����
    [SerializeField] private TextMeshProUGUI timer; //����� ������� �� ����� ����
    [SerializeField] private GameObject resultPanel; 
    [SerializeField] private TextMeshProUGUI resultTxt; // �������� ����
    [SerializeField] private GameObject startPanel;
    [SerializeField] private TextMeshProUGUI startTimerTxt; //������ �� ������ ����

    private readonly StringBuilder _stringBuilder = new();

    private void Start()
    {
        startPanel.SetActive(true);
        resultPanel.SetActive(false);
        StartCoroutine(CountDown());
    }

    private IEnumerator Check()
    {
        Statistic(); //��������� ������� ������

        if (GameController.IsOver && !resultPanel.activeSelf) //���� ���� ���������
        {
            resultPanel.SetActive(true);
            resultTxt.text = coins.text;
            Cursor.lockState = CursorLockMode.None;
        }

        yield return new WaitForSeconds(0f);
        StartCoroutine(Check());
    }

    IEnumerator CountDown() //����� �� ������ ����
    {
        var startTimer = 3f;
        while (startTimer > 0)
        {
            startTimerTxt.text = startTimer.ToString();
            yield return new WaitForSeconds(1f);
            startTimer--;
        }

        yield return new WaitForSeconds(1f);
        startPanel.gameObject.SetActive(false);
        StartCoroutine(Check());
    }

    private void Statistic() //������ �������� ����� � ����������� ������� ����
    {
        _stringBuilder.Append("Coins: ");
        _stringBuilder.Append(GameController.Score);

        coins.text = _stringBuilder.ToString();
        timer.text = GameController.Timer.ToString("0:00");

        _stringBuilder.Clear();
    }

    public void Restart() //���������� ����
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}