public interface ITimePlayingModule
{
    int TimePlaying { get; }
    void StartTimer();
    void StopTimer();
    void ResetTimer();
}