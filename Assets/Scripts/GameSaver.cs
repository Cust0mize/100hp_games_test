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

    public void SetCurrentCoins(float value) {
        float newCoinsValue = GetCurrentCoins() + value;
        PlayerPrefs.SetFloat(_currentCoins, newCoinsValue);
    }

    public int GetWaweNumber() {
        return PlayerPrefs.GetInt(_waweCount, 1);
    }

    public void SetWaweNumber(int value) {
        PlayerPrefs.SetInt(_waweCount, value);
    }

    public void SetSkillLevel(SkillType skillType, int level) {
        switch (skillType) {
            case SkillType.TimeShootSkill: PlayerPrefs.SetInt(_shootTimeLevel, level); break;
            case SkillType.RadiusSkill: PlayerPrefs.SetInt(_radiusLevel, level); break;
        }
    }

    public int GetSkillLevel(SkillType skillType) {
        int result = 0;
        switch (skillType) {
            case SkillType.AttackSkill: result = (int)PlayerPrefs.GetFloat(_bulletDamage, 1); break;
            case SkillType.TimeShootSkill: result = PlayerPrefs.GetInt(_shootTimeLevel, 1); break;
            case SkillType.RadiusSkill: result = PlayerPrefs.GetInt(_radiusLevel, 1); break;
        }
        return result;
    }

    public void SetSkillValue(SkillType skillType, float improvementMultiplier) {
        switch (skillType) {
            case SkillType.AttackSkill: PlayerPrefs.SetFloat(_bulletDamage, GetSkillValue(skillType) + improvementMultiplier); break;
            case SkillType.TimeShootSkill: PlayerPrefs.SetFloat(_shootTime, GetSkillValue(skillType) * improvementMultiplier); break;
            case SkillType.RadiusSkill: PlayerPrefs.SetFloat(_towerRadius, GetSkillValue(skillType) * improvementMultiplier); break;
        }
    }

    public float GetSkillValue(SkillType skillType) {
        float result = 0;
        switch (skillType) {
            case SkillType.AttackSkill: result = PlayerPrefs.GetFloat(_bulletDamage, Bullet.DefaultBulletDamage); break;
            case SkillType.TimeShootSkill: result = PlayerPrefs.GetFloat(_shootTime, Tower._defaultShootTime); break;
            case SkillType.RadiusSkill: result = PlayerPrefs.GetFloat(_towerRadius, Tower._defaultShootRadius); break;
        }
        return result;
    }
}