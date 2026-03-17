let pomodoroInterval;
function startPomodoro() {
  const el = document.getElementById('pomodoro-min');
  const display = document.getElementById('pomodoro-display');
  if (!el || !display) return;
  let remaining = parseInt(el.value || '25', 10) * 60;
  clearInterval(pomodoroInterval);
  pomodoroInterval = setInterval(async () => {
    remaining--;
    const m = Math.floor(remaining / 60).toString().padStart(2,'0');
    const s = (remaining % 60).toString().padStart(2,'0');
    display.textContent = `${m}:${s}`;
    if (remaining <= 0) {
      clearInterval(pomodoroInterval);
      alert('Pomodoro concluído!');
      await fetch('/Sessions/RegisterPomodoro', { method:'POST', headers:{'Content-Type':'application/x-www-form-urlencoded'}, body:`minutos=${el.value}` });
    }
  }, 1000);
}
function pausePomodoro(){ clearInterval(pomodoroInterval); }
function resetPomodoro(){ const el=document.getElementById('pomodoro-min'); const d=document.getElementById('pomodoro-display'); if(el&&d){d.textContent=`${String(el.value).padStart(2,'0')}:00`;}}
