using UnityEngine;

public class Enemy : MonoBehaviour
{
    private readonly float _speed = 5f; //скорость движения врага
    [SerializeField] private int coin = 0; //очки за убийство
    [SerializeField] private float lifeTime = 0; //время жизни
    public int Coin => coin;

    private void Update()
    {
        Live();
        transform.Translate(Vector3.back * _speed * Time.deltaTime);
    }

    private void Live()
    {
        lifeTime -= Time.deltaTime;

        if (GameController.IsOver) //если игра окончена уничтожаем всех врагов
        {
            Destroy(gameObject);
        }
        else if (lifeTime <= 0) //если время жизни истекло, то унименьшаем количество очков и уничтожаем врага
        {
            GameController.Miss();
            Destroy(gameObject);
        }
    }
}