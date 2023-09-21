using UnityEngine;

public class Enemy : MonoBehaviour
{
    private readonly float _speed = 5f; //�������� �������� �����
    [SerializeField] private int coin = 0; //���� �� ��������
    [SerializeField] private float lifeTime = 0; //����� �����
    public int Coin => coin;

    private void Update()
    {
        Live();
        transform.Translate(Vector3.back * _speed * Time.deltaTime);
    }

    private void Live()
    {
        lifeTime -= Time.deltaTime;

        if (GameController.IsOver) //���� ���� �������� ���������� ���� ������
        {
            Destroy(gameObject);
        }
        else if (lifeTime <= 0) //���� ����� ����� �������, �� ����������� ���������� ����� � ���������� �����
        {
            GameController.Miss();
            Destroy(gameObject);
        }
    }
}