# Системы и механики

## Оглавление
Системы
- [Игровой цикл](#игровой-цикл)
- [Карта](#карта)
- [Ресурсы](#ресурсы)
- [Строительство](#строительство)
- [Здания](#здания)
- [Враги](#враги)

Механики
- [Заклинания](#заклинания)
- [Награды](#награды)
- [Улучшения](#улучшения)

## Игровой цикл
Игровой цикл делится на несколько этапов, которые геймплейно обыгрываются как смена времени суток:

- **День** — время, когда игрок может строить здания, улучшать или удалять их с карты. Также в дневное время доступно изучение новых заклинаний, управление списком активных заклинаний, а также просмотр информации о башнях и предстоящих волнах.

- **Ночь** — этап, когда базу игрока атакуют враги. Во время ночи игрок может использовать заклинания из списка активных и просматривать информацию о врагах. Игрок также может досрочно завершить волну: в этом случае все оставшиеся и незаспаунившиеся враги нанесут урон замку, после чего автоматически начнется следующий этап.

- **Этап награды** — после завершения ночи игрок выбирает одну из трех случайных наград. Награды могут включать деньги, уникальные заклинания или специальные возможности (например, восстановление здоровья замка или ослабление следующей волны).

Переход между этапами осуществляется по действию игрока (нажатие специальной кнопки), за исключением этапа награды. Этап награды автоматически завершится после выбора игроком одной из наград.

## Карта
Карта представляет собой 2D-сетку тайлов, на которой размещаются игровые объекты: замок игрока, который нужно защищать, места появления врагов, дороги, по которым враги движутся к замку, а также зоны строительства, где игрок может размещать башни и другие элементы.

Карта разбита на несколько слоев, каждый из которых выполняет свою задачу:

- **Road** — слой с дорогами, по которым двигаются враги.
- **Mask** — технический слой, используемый для подсветки тайлов во время строительства.
- **ConstructionArea** — слой, предназначенный для строительства. Башни можно строить только на тайлах, находящихся на этом слое.
- **DestructableProps** — слой с разрушаемыми элементами. Эти объекты уничтожаются при строительстве, но могут вновь появляться на свободных тайлах.

Остальные слои используются для создания декоративной атмосферы и окружения.

## Ресурсы
В игре используется один ресурс — **золотые монеты**.

Изначально игрок получает стартовую сумму для постройки первых зданий. Золото можно зарабатывать следующими способами:

- строительство и работа шахт;
- получение наград;
- уничтожение врагов (при наличии соответствующего навыка).

Все затраты, включая улучшение зданий и изучение заклинаний, также оплачиваются золотыми монетами.

## Строительство
В игре доступно три вида построек:

- **Башни** — атакуют врагов различными способами.
- **Шахты** — добывают ресурсы для строительства и улучшений.
- **Алтари** — усиливают соседние башни или оказывают влияние на врагов в радиусе действия.

Каждое здание обладает базовыми способностями (атака, усиление, сбор золота), которые можно улучшать.

Башни имеют несколько состояний:
1. **Временное состояние** — башня выбрана для строительства, но еще не размещена. На этом этапе способности башни отключены, и она неактивна.
2. **Строительство** — время, необходимое для размещения башни на карте и активации ее способностей.
3. **Активное состояние** — башня размещена и выполняет свои функции.
4. **Демонтаж** — башня удаляется с карты.
Каждая башня имеет свою цену и занимает определенную площадь на карте. При продаже башни игрок возвращает часть ее стоимости; размер возврата зависит от уровня башни.


## Здания
// TODO

## Враги
// TODO

## Заклинания
// TODO

## Награды
// TODO

## Улучшения
// TODO
