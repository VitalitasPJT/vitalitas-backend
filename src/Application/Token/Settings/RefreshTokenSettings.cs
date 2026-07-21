namespace Application.Token.Settings
{
    public class RefreshTokenSettings
    {
        public int DurationInDays { get; }

        public RefreshTokenSettings(int durationInDays)
        {
            DurationInDays = durationInDays;
        }
    }
}
