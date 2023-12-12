public interface IHighScoreModule
{
    int LastHighScore { get; }
    void LoadLastHighScore();
    void SaveHighScore(int highScore);
}