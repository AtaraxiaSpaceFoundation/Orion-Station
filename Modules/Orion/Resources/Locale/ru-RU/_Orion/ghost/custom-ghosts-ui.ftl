custom-ghost-fail-exclusive-ghost = Этот призрак доступен только определённому человеку.
custom-ghost-fail-playtime-unavailable = Не удалось получить время игры. Обратитесь к администратору, если ошибка повторится.
custom-ghost-fail-server-insufficient-playtime = Отыграть {$requiredHours} {$requiredHours ->
  *[one] час
  [few] часа
  [many] часов
} на сервере. { -playtime(pH: $playtimeHours, pM: $playtimeMinutes) }
custom-ghost-fail-job-insufficient-playtime = Отыграть {$requiredHours} {$requiredHours ->
  *[one] час
  [few] часа
  [many] часов
} на должности "{$job}". { -playtime(pH: $playtimeHours, pM: $playtimeMinutes) }
custom-ghost-fail-department-insufficient-playtime = Отыграть {$requiredHours} {$requiredHours ->
  *[one] час
  [few] часа
  [many] часов
} в отделе "{$department}". { -playtime(pH: $playtimeHours, pM: $playtimeMinutes) }

custom-ghosts-window-title = Выбор призрака
custom-ghosts-window-show-all-checkbox = Показать всех
custom-ghosts-window-show-all-checkbox-tooltip = Переключает отображение неразблокированных призраков. Условия разблокировки отображаются при наведении мыши.
custom-ghost-window-tooltip-to-unlock = Чтобы получить возможность пользоваться этим призраком, вам надо:

-playtime = У вас наиграно {$pH} {$pH ->
  *[one] ч.
  [few] ч.
  [many] ч.
} {$pM} {$pM ->
  *[one] мин.
  [few] мин.
  [many] мин.
}
