namespace BossFightLives.UiConfiguration
{
    public abstract class UiConfiguration
    {
        public bool Enabled;
        public abstract void Enable();
        public abstract void Disable();
    }
}