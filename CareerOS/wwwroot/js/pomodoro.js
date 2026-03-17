let pomodoroInterval;

function getCsrfToken() {
  const meta = document.querySelector('meta[name="csrf-token"]');
  return meta ? meta.getAttribute('content') : '';
}

function startPomodoro() {
  const el = document.getElementById('pomodoro-min');
  const display = document.getElementById('pomodoro-display');
  if (!el || !display) return;

  const minutes = Math.max(1, Math.min(parseInt(el.value || '25', 10), 180));
  let remaining = minutes * 60;

  clearInterval(pomodoroInterval);
  pomodoroInterval = setInterval(async () => {
    remaining--;
    const m = Math.floor(remaining / 60).toString().padStart(2, '0');
    const s = (remaining % 60).toString().padStart(2, '0');
    display.textContent = `${m}:${s}`;

    if (remaining <= 0) {
      clearInterval(pomodoroInterval);
      alert('Pomodoro concluído!');
      await fetch('/Sessions/RegisterPomodoro', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded',
          'X-CSRF-TOKEN': getCsrfToken()
        },
        body: `minutos=${minutes}`
      });
    }
  }, 1000);
}

function pausePomodoro() { clearInterval(pomodoroInterval); }

function resetPomodoro() {
  const el = document.getElementById('pomodoro-min');
  const d = document.getElementById('pomodoro-display');
  if (el && d) {
    const minutes = Math.max(1, Math.min(parseInt(el.value || '25', 10), 180));
    d.textContent = `${String(minutes).padStart(2, '0')}:00`;
  }
}
