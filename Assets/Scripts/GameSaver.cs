using System;
using UnityEngine;

public class GameSaver
{
    private const string _currentCoins = "coins";
    private const string _bulletDamage = "bullet_damage";
    private const string _towerRadius = "tower_radius";
    private const string _shootTime = "shoot_time";
    private const string _shootTimeLevel = "shoot_time_level";
    private const string _radiusLevel = "radius_level";
    private const string _waweCount = "_wawe_count";

    public float GetCurrentCoins() {
        return PlayerPrefs.GetFloat(_currentCoins, 0);
    }

    public void SetCurrentCoint(float value) {
        float newCoinsValue = GetCurrentCoins() + value;
        PlayerPrefs.SetFloat(_currentCoins, newCoinsValue);
    }

    public int GetTowerRadiusLevel() {
        return PlayerPrefs.GetInt(_radiusLevel, 1);
    }

    public float GetBulletDamage() {
        return PlayerPrefs.GetFloat(_bulletDamage, 1);
    }

    internal void SetRadiusTowerLevel(int level) {
        PlayerPrefs.SetInt(_radiusLevel, level);
    }

    public int GetShootTimeSkillLevel() {
        return PlayerPrefs.GetInt(_shootTimeLevel, 1);
    }

    public void SetBulletDamage(float value) {
        PlayerPrefs.SetFloat(_bulletDamage, value);
    }

    public void SetShootTimeLevel(int level) {
        level++;
        PlayerPrefs.SetInt(_shootTimeLevel, level);
    }

    public float GetTowerRadius() {
        return PlayerPrefs.GetFloat(_towerRadius, Tower._defaultShootRadius);
    }

    public void SetTowerRadius(float newRadius) {
        PlayerPrefs.SetFloat(_towerRadius, newRadius);
    }

    public float GetShootTime() {
        return PlayerPrefs.GetFloat(_shootTime, Tower._defaultShootTime);
    }

    public void SetShootTime(float newRadius) {
        PlayerPrefs.SetFloat(_shootTime, newRadius);
    }

    public int GetWaweNumber() {
        return PlayerPrefs.GetInt(_waweCount, 1);
    }

    public void SetWaweNumber(int value) {
        PlayerPrefs.SetInt(_waweCount, value);
    }
}