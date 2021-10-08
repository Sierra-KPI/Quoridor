# Quoridor

<p align="center">
  <img src="https://github.com/Sierra-KPI/Quoridor/blob/main/docs/Quoridor.jpg" data-canonical-src="https://github.com/Sierra-KPI/Quoridor/blob/main/docs/Quoridor.jpg" />
</p>

## Table of Contents

- [Description](#description)
- [Badges](#badges)
- [Contributing](#contributing)
- [License](#license)

### Description

The abstract strategy game Quoridor is surprisingly deep for its simple rules. The object of the game is to advance your pawn to the opposite edge of the board. On your turn you may either move your pawn or place a wall. You may hinder your opponent with wall placement, but not completely block them off. Meanwhile, they are trying to do the same to you. The first pawn to reach the opposite side wins.

## Badges

[![Theme](https://img.shields.io/badge/Theme-GameDev-blueviolet)](https://img.shields.io/badge/Theme-GameDev-blueviolet)
[![Game](https://img.shields.io/badge/Game-Quoridor-blueviolet)](https://img.shields.io/badge/Game-Quoridor-blueviolet)

---

## Example

```csharp
public bool MakeMove(Cell from, Cell to, Cell through)
{
    var moves = GetPossiblePlayersMoves(from, through);
    return Array.Exists(moves, element => element == to);
}

public bool PlaceWall(Wall wall)
{
    var cell1ID = GetIdOfCellByCoordinates(wall.Coordinates);
    var cell2ID = GetIdOfCellByCoordinates(wall.EndCoordinates);
    int diff = GetDiffId(cell1ID, cell2ID);
            
    _walls = _walls.Where(elem =>
    {
        //replace to GetIdOfCellByCoordinates
        var wallCell1 = GetCellByCoordinates(elem.Coordinates);
        var wallCell2 = GetCellByCoordinates(elem.EndCoordinates);
        return (wallCell1.Id != cell1ID || wallCell2.Id != cell2ID) &&
        (wallCell1.Id != cell1ID - diff || wallCell2.Id != cell2ID - diff) &&
        (wallCell1.Id != cell1ID + diff || wallCell2.Id != cell2ID + diff) &&
        (wallCell1.Id != cell1ID || wallCell2.Id != cell1ID + diff);
            }).ToList();

        _placedWalls.Add(wall);
        return true;
    }
}
```

---

## Pictures

[![Picture1](https://github.com/Sierra-KPI/Quoridor/blob/main/docs/ConsoleGame.png)](https://github.com/Sierra-KPI/Quoridor/blob/main/docs/ConsoleGame.png)

---

## Contributing

> To get started...

### Step 1

- 🍴 Fork this repo!

### Step 2

- **HACK AWAY!** 🔨🔨🔨

---

## License

[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](http://badges.mit-license.org)

- **[MIT license](http://opensource.org/licenses/mit-license.php)**
- Copyright 2021 © <a href="https://github.com/Sierra-KPI" target="_blank">Sierra</a>.
