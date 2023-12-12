public interface IGameSpeedService
{
    bool IsBoosting { get; }
    float CurrentGameSpeed { get; }
    void LoadGameSpeedData();
    void SetBoostMode(bool isOn);
    float BoostSpeedMultiplier { get; }
    void Reset();
}