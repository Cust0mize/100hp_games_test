using UnityEngine;

public class GameSaver
{
    private const string _currentCoins = "coins";
    private const string _bulletDamage = "bullet_damage";
    private const string _towerRadius = "tower_radius";
    private const string _shootTime = "shoot_time";

    public float GetCurrentCoins() {
        return PlayerPrefs.GetFloat(_currentCoins, 0);
    }

    public void SetCurrentCoint(float value) {
        float newCoinsValue = GetCurrentCoins() + value;
        PlayerPrefs.SetFloat(_currentCoins, newCoinsValue);
    }

    public float GetBulletDamage() {
        return PlayerPrefs.GetFloat(_bulletDamage, 1);
    }

    public void SetBulletDamage(float value) {
        PlayerPrefs.SetFloat(_bulletDamage, value);
    }

    public float GetTowerRadius() {
        return PlayerPrefs.GetFloat(_towerRadius, Constans.DefaultRadius);
    }

    public void SetTowerRadius(float newRadius) {
        PlayerPrefs.SetFloat(_towerRadius, newRadius);
    }    
    
    public float GetShootTime() {
        return PlayerPrefs.GetFloat(_shootTime, Constans.DefaultShootTime);
    }

    public void SetShootTime(float newRadius) {
        PlayerPrefs.SetFloat(_shootTime, newRadius);
    }
}