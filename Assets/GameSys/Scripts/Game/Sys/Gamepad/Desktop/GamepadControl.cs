namespace GameSys.Gamepad
{
    public abstract class GamepadControl : IGamepadControl
    {
        public event GamepadControlChangeEvent GamepadControlChange;

        public void ControlChanged(AxisType axisType, float before, float after)
        {
            GamepadControlChange?.Invoke(axisType, before, after);
        }

        public virtual void Poll() { }
    }
}
