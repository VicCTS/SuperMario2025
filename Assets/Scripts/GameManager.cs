using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int killedEnemies = 0;

    public void AddKill()
    {
        killedEnemies++;
    }
}
