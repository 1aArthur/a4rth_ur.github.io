using Microsoft.UI.Dispatching;

namespace CareerOS.Services;

public class PomodoroService
{
    private DispatcherQueueTimer? _timer;
    private TimeSpan _remaining;

    public event Action<TimeSpan>? Tick;
    public event Action? Completed;

    public void Start(int minutes)
    {
        _remaining = TimeSpan.FromMinutes(minutes);
        EnsureTimer();
        _timer!.Start();
        Tick?.Invoke(_remaining);
    }

    public void Pause() => _timer?.Stop();

    public void Reset(int minutes)
    {
        Pause();
        _remaining = TimeSpan.FromMinutes(minutes);
        Tick?.Invoke(_remaining);
    }

    private void EnsureTimer()
    {
        _timer ??= DispatcherQueue.GetForCurrentThread().CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick -= OnTick;
        _timer.Tick += OnTick;
    }

    private void OnTick(DispatcherQueueTimer sender, object args)
    {
        if (_remaining.TotalSeconds <= 1)
        {
            sender.Stop();
            _remaining = TimeSpan.Zero;
            Tick?.Invoke(_remaining);
            Completed?.Invoke();
            return;
        }

        _remaining = _remaining.Subtract(TimeSpan.FromSeconds(1));
        Tick?.Invoke(_remaining);
    }
}
