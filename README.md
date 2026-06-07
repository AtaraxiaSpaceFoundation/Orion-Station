<h1 align="center"> <img alt="Orion Station" width="480" height="120" src="https://raw.githubusercontent.com/AtaraxiaSpaceFoundation/asset-dump/refs/heads/master/OrionStation/Orion-Banner-Big.png" /> </h1>

<p align="center">
  Ваш проводник в космический симулятор безумия!<br>
  Основан на идеях <a href="https://github.com/tgstation/tgstation">/tg/station</a> и <a href="https://github.com/shiptest-ss13/Shiptest">Shiptest</a> из Space Station 13.
</p>

<div align="center">

  [![Steam](https://img.shields.io/badge/Steam-Скачать-blue?style=for-the-badge)](https://store.steampowered.com/app/1255460/Space_Station_14/)
  [![Client](https://img.shields.io/badge/Клиент-Скачать-purple?style=for-the-badge)](https://spacestation14.io/about/nightlies/)

</div>

---

**Orion** — это русскоязычный форк [Goob Station](https://github.com/Goob-Station/Goob-Reforged), который стремится возродить дух классического геймплея Space Station 13, черпая вдохновение из таких проектов, как [/tg/station](https://github.com/tgstation/tgstation) и [Shiptest](https://github.com/shiptest-ss13/Shiptest). Мы фокусируемся на сочетании проверенных временем механик с инновационными идеями, создавая уникальный и приятный опыт игры в космическое безумие.

---

<div align="center">
    
## Ссылки

</div>

[<img src="https://github.com/AtaraxiaSpaceFoundation/asset-dump/blob/master/Misc/Discord/discord-banner.png" alt="Discord" width="150" align="left">](https://discord.gg/K48JujjjsC)
**[Discord Server](https://discord.gg/K48JujjjsC)**<br>В космосе вас никто не услышит.

[<img src="https://i.imgur.com/XiS9QP5.png" alt="ASF" width="150" align="left">](https://github.com/AtaraxiaSpaceFoundation)
**[Ataraxia Space Foundation](https://github.com/AtaraxiaSpaceFoundation)**<br>Специализируемся на разработке этого билда.

---
<div align="center">

## Активность репозитория

![Активность PR](https://repobeats.axiom.co/api/embed/50c8c950821b573c8bcca8158d8f2b99ee06417d.svg "ZZZ")

</div>

---
<div align="center">

## Политика Orion

Любой сервер, заявляющий, что он является официальным представителем этого билда — не одобрен этой организацией.
Однако мы хотели бы пригласить всех желающих создать сервер на базе билда Orion.

</div>

> [!WARNING]  
> **Orion не имеет официальных игровых серверов**.

---

<div align="center">

## Документация

</div>

Проект имеет обширную [документацию](https://docs.goobstation.com/), которая охватывает все аспекты: от контента и сборки до движка, дизайна игры и многого другого. Это также отличный ресурс для новичков, желающих внести свой вклад в разработку.

---
<div align="center">

## Контрибуция

</div>

Мы всегда рады помощи в разработке, если вы хотите внести свой вклад, присоединяйтесь к [серверу разработки в Discord](https://discord.gg/zXk2cyhzPN). Вы можете помочь нам, решая проблемы из [списка открытых проблем](https://github.com/Goob-Station/Goob-Reforged/issues) или предлагая свои идеи. Не стесняйтесь задавать вопросы — мы всегда готовы помочь!

Перед отправкой изменений ознакомьтесь с [Contributor License Agreement](CLA.md). Для принятия Pull Request все его участники должны принять CLA через комментарий, предложенный автоматическим CLA-ботом.

Отправляя изменения и принимая CLA, участник сохраняет авторские права на собственный вклад, но предоставляет Ataraxia Space Foundation права, перечисленные в соглашении.

---
<div align="center">

## Сборка

</div>

</div>

> [!TIP]
> Используйте [IDE Rider](https://github.com/designinlife/jetbrains), он неимоверно облегчит вам жизнь, если вы собираетесь влиться в разработку (код), или билдить сборку, больше пары раз.

Следуйте гайду от [Джубами](https://docs.goobstation.com/en/general-development/setup.html) по настройке рабочей среды, но учитывайте, что репозитории отличаются друг от друга и некоторые вещи могут отличаться.
Ниже перечислены скрипты и методы облегчающие работу с билдом.

### Windows

> 1. Клонируйте данный репозиторий.
```shell
git clone https://github.com/AtaraxiaSpaceFoundation/Orion-Station-14.git
```
> 2. Откройте коммандную строку в папке репозитория и введите команду для того, чтобы подготовить движок игры.
```shell
git submodule sync --recursive
```
```shell
git submodule update --init --recursive
```
> 3. Следующим этапом идёт билд-билда, для этого нужно ввести команду с указанием того, для чего вы билдите, для этого нужно написать Release, Tools или Debug.
```shell
dotnet build --configuration Release/Tools/Debug
```
> [!TIP]
> К примеру **Release** - полная версия, **Tools** - урезаная версия, **Debug** - урезаная версия, но которая будет вылетать при любой ошибке. В большинстве случаев вам хватит **Tools**, что-бы не перенапрягать машину.

> 4. Далее вам требуется запустить сервер с клиентом, для этого есть несколько способов.
> - 4.1. Командами, в конце так же можно указать вместо Tools любой интересующий вас тип.
```shell
dotnet run --project Content.Server --configuration Tools
```
```shell
dotnet run --project Content.Client --configuration Tools
```
> - 4.2. Запуск .bat файла, который автоматически выполнит те же команды.
```shell
Scripts/bat/runQuickAll.bat
```
> 5. Подключитесь к **localhost** в появившемся окне и играйте!

---
<div align="center">

## Лицензия

</div>

Основной лицензией для оригинального кода Orion Station является [GNU Affero General Public License 3.0 или более поздней версии](LICENSES/AGPL-3.0-or-later.txt).

Конкретная лицензия каждого файла определяется его заголовком `SPDX-License-Identifier`, отдельным файлом `.license` или другой сопровождающей лицензионной информацией. Некоторые файлы, унаследованные или перенесённые из других проектов, могут распространяться под MPL, MIT или другими совместимыми лицензиями.

Большинство медиафайлов распространяется под лицензией [CC BY-SA 3.0](https://creativecommons.org/licenses/by-sa/3.0/), если в метаданных ресурса не указано иное.

Условия внесения изменений в проект определены в [Contributor License Agreement](CLA.md).

</div>

> [!NOTE]
> Некоторые ресурсы могут распространяться под некоммерческими лицензиями, включая CC BY-NC-SA 3.0. Такие материалы необходимо удалить или заменить при коммерческом использовании проекта.

<h1 align="right"> <img alt="Orion Station" src="https://raw.githubusercontent.com/AtaraxiaSpaceFoundation/asset-dump/refs/heads/master/OrionStation/Orion-Banner-Small.png" />  </h1>
