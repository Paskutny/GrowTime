# GrowTime

Hover over a young animal and see how long it still needs to grow up.

```
Piglet ( Tame, Happy )
[E] Pet
[Shift + E] Rename
Grows in: 12m 34s (58%)
```

- Works for every creature with a growth stage: Lox calves, piglets, wolf cubs, chicks, Asksvin hatchlings and modded creatures that use Valheim's growth component.
- Exact on dedicated servers and in multiplayer. The time comes from the animal's synced spawn time and the server's world clock, not from a local timer.
- Shown in your game language. Ships with English, German, Spanish, French, Russian and Portuguese (European and Brazilian). Other languages fall back to English.
- **Client-side only.** Install it just for yourself. The server and other players don't need it, and it changes nothing about the game. It only adds a line of text.

## Configuration

`BepInEx/config/paskutny.growtime.cfg` (created on first start):

| Setting | Default | Description |
|---|---|---|
| `Enabled` | `true` | Show the grow time line. |
| `ShowPercentage` | `false` | Also show the growth progress in percent. |

## Translations

Translations are plain text files in the mod folder: `Translations/<Language>.txt`. `<Language>` is the name Valheim uses, for example `French`, `Spanish`, `Russian` or `Portuguese_Brazilian`. To add a language, copy `English.txt`, rename it and translate the values. Pull requests are welcome.

## Links

Source and issues: https://github.com/Paskutny/GrowTime

---

## Deutsch

Schau mit dem Fadenkreuz auf ein Jungtier und sieh, wie lange es noch braucht, bis es ausgewachsen ist.

```
Ferkel ( Zahm, Glücklich )
[E] Streicheln
[Shift + E] Umbenennen
Wächst in: 12m 34s (58%)
```

- Funktioniert bei allen Tieren mit Wachstumsphase: Lox-Kälber, Ferkel, Wolfswelpen, Küken, Asksvin-Junge und Tiere aus anderen Mods, die Valheims Wachstumssystem nutzen.
- Auch auf Dedicated Servern und im Multiplayer genau. Die Zeit kommt aus dem synchronisierten Spawn-Zeitpunkt des Tiers und der Weltzeit des Servers, nicht aus einem lokalen Timer.
- Wird in deiner Spielsprache angezeigt. Enthalten sind Deutsch, Englisch, Spanisch, Französisch, Russisch und Portugiesisch (Portugal und Brasilien), andere Sprachen zeigen Englisch.
- **Nur clientseitig.** Installiere die Mod einfach für dich selbst. Server und Mitspieler brauchen sie nicht, und sie verändert nichts am Spiel. Sie fügt nur eine Textzeile hinzu.

### Einstellungen

`BepInEx/config/paskutny.growtime.cfg` (wird beim ersten Start angelegt):

| Einstellung | Standard | Beschreibung |
|---|---|---|
| `Enabled` | `true` | Zeile mit der Wachstumszeit anzeigen. |
| `ShowPercentage` | `false` | Zusätzlich den Fortschritt in Prozent anzeigen. |
