# TowerDefence

Проект я делал на версии Unity2020.1.17f1. Также сделал версию Unity2019.4.18f1 и поместил [в соседнюю ветку](https://github.com/Fakhriev/TowerDefence/tree/Unity2019.4.18f1), 
но, думаю, она в некоторых моментах может отработать не так стабильно, как на основная версия, хоть я её и докрутил немного.

Также я изначально делал под Anroid Платформу. Если у вас слишком большие кнопки в игре, то следует сделать Switch Platform на Android и установить портретный 
формат.

## Описание проекта

Попытался сделать систему для удобного настраивания большинства игровых компонентов в проекте. Через ScriptableObject'ы можно настроить 
характеристики врагов или башен. Самим башням поменять внешний вид или максимальный уровень улучшения, установить какая скорость атаки, урон и дальность атаки будет
на каждом уровне башни.

Можно настроить игровые раунды: указать сколько в раунде будет волн и из каких врагов и в каком количестве будет состоять каждая отдельная волна. Указать, например,
что Раунд 1 состоит из 3 Волн, Первая волна - два врага типа Солдат, один враг типа Голем, вторая волна - 3 врага типа Орк, а третья волна 2 Солдата, 3 Голема, 
потом опять 2 Солдата и потом один Орк.

Можно забалансить тех же врагов, поменяв любому из них показатели Здоровья, Скорости, Урона в секунду или Золото за убииство. Ну или изменить им префаб.
ScriptableObject'ы для настройки находятся по адресу Assets/Prefabs/Enemy/Enemies Data SO - для Врагов, Assets/Prefabs/Tower/Towers Data SO - для Башен, у вторых
можно настраивать такие показатели: стоимость постройки, стоимость апгрейда на следующий уровень, статы на каждом из уровней(Урон, Атак в Секунде, Дальность), 
внешний вид башни на каждом из уровней.

Можно быстро создать новую локацию с новой дорожкой, по которой будут идти враги: для этого нужно продублировать уже существующую локацию, передвинуть в ней MovePoint'ы,
расставить основания башен и расставить декоративные префабы, код сделает остальное. Также нужно в ScriptableObjecte-Конфиге Локаций в Ресурсах закинуть новую локацию 
и установить с какого по какой Раунд игрок будет на ней играть. Так можно насоздавать кучу локаций и каждой из них установить свои диапазон Раундов, чтобы игрок
часто видел разные уровни.

UpgradeMenu который открывается при нажатии на построенную башню будет учитывать кол-во доступных апгрейдов башни и, если апгрейд на следующий уровень возможен,
то он покажет кнопку апгрейда(если не хватает денег на апгрейд, то покажет, но сделает недоступной), а если апгрейд на следующий уровень не предусмотрен, то
то кнопку не покажет.

ScriptableObject для настройки Раундов по адресу: Assets/Resources/MainConfigs/Main Rounds Config.asset. - он содержит в себе массив Раундов(Rounds).
Каждый Раунд состоит из Волн(Waves). Каждая Волна состоит из Микроволны(Microwave) и Интервала до следующей волны(Timer To Next Wave). 
Каждая Микроволна представляет из себя группу врагов в волне: в ней указывается какой тип врага в Микроволне(EnemyType - Soldier, например) и количество врагов
этого типа(Amount).

Создав три элемента Microwave в Волне и указав, например, что первая Микроволна - это два Солдата, вторая Микроволна - это 1 Голем, а третья Микроволна -
это три Орка, вы сформируете из этого Волну, которая состоит, соответственно, из двух Солдат, одного Голема и трёх Орков. Также сформировав еще несколько подобных 
или отличных Волн, вы сформируете Раунд. Игрок, одолев всех врагов в раунде, одерживает Победу.

Микроволну я добавлял, так как других способо разнообразить Волну я не смог придумать. Но благодаря ней можно спавнить сколько угодно врагов, каких угодно типов
и в любом порядке. Другое дело, что в куче вкладок в одном ScriptableObject'е это делать не удобно.

## На что можно еще посмотреть

На то как работает башня. Tower.prefab - это группа классов Tower, TowerUpgrade, TowerAttack, TowerTargetSystem. 

Система Таргетов: TargetSystem, если не имеет целей, то берет в качестве цели первого врага, который зайдет в её радиус тригера, получив ссылку на его компонент. 
Радиус тригера, при этом, является дальностью(Range) атаки башни. Если цель уже имеется, то он добавляет его в лист GameObject'ов и, когда текующая цель умрет 
или выйдет из радиуса башни, то TargetSystem обратиться к ближейшему врагу в листе GameObject'ов и получит ссылку на него - это сделано для того, чтобы 
снизить до минимума вызов метода GetComponent. Сама TargetSystem, при получении новой цели, сразу же даст команду AttackSystem с информацией о том, какую цель
атаковать. 

AttackSystem содержит в себе пулл стрел и через каждый свой интервал атаки, который основан на скорости атаки башни на текущем её уровне, 
будет вызывает метод Shoot, который обращается к пулу стрел, находит там свободную стрелу и дает ей ссылку на врага от TargetSystem и урон башни, 
который следует нанести врагу.

TowerUpgrade, при прокачке, обновит все характеристики башни и посчитает новую стоимость продажи башни, которая основана на новой итоговой сумме золота,
затраченной на башню.

_________

Я все *Event и некоторые *Controller классы делал синглтонами, так как другого не придумал, но это вылилось в очень удобный способ подписки на их события. 
Те же BuildMenu и UpgradeMenu проверяют достаточно ли у игрока денег на покупку не в Update, а при срабатывании соответствующего события в GoldController'е. 
В этот момент происходит обновление состояния меню и те башни или улучшения, на которые у игрока достаточно денег будут доступны для нажатия и покупки, а те
на которые денег не достаточно, соответственно, не доступны.

Также можно удобно мониторить смерти врагов и типы врагов, которые были убиты. Или следить за состоянием самой игры: враги и башни, например, отслеживают
момент поражения игрока и, когда он проигрывает, у врагов включается анимация победы, а башни перестают стрелять.

Подписки на события я прикручивал много где: тот же TargetSystem не в Update проверяет умер ли враг, а всегда подписана на OnDie Event своего Таргета.

## Дополнительно

Можно легко добавлять новые типы врагов: нужно создать им ScriptableObject и внешний вид, создать описывающий их Enum в EnemyData и засунуть нового врага
в массив в EnemySpawner'е в игровой сцене. Дальшо уже можно спавнить их через Rounds Config сколько угодно.

С добавлением новых башен будет неудобнее, так как нужно будет добавлять новую кнопку в меню строительства башен, а там я специально делал так, что красиво
ставятся только 4 кнопки, так как подсмотрел такое меню строительства в одной игре и оно мне очень понравилось. В остальном, процесс создания башни точно такой же.
