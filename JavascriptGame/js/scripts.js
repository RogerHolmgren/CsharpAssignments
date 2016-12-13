window.onload = function () {
  for (var x = 0; x < 11; x++) {
    for (var y = 0; y < 11; y++) {
      let tile = document.createElement('div');
      tile.innerHTML = x + "," + y;
      var classArgs = "x" + x + "y" + y + " tile";

      if (x == 0 || y == 0 || x == 10 || y == 10) {
        classArgs += " wall";
      } else if (x == 1 && y == 2 || x == 2 && y == 2) {
        classArgs += " wall";
      } else if (x == 5 && y == 5) {
        tile.setAttribute('id', 'player');
        tile.innerHTML = "<h1>P</h1>";
      }

      tile.setAttribute("class", classArgs);
      anchor.appendChild(tile);
    }
  }
}

document.onkeydown = function (event) {
  var player = document.getElementById('player');
  var oldXY = getXY(player);
  var newXY = getNewXY(event.keyCode, oldXY);

  var newPos = document.getElementsByClassName("x" + newXY[0] + "y" + newXY[1])[0];

  console.log("old: " + oldXY + " | new: " + newXY);

  if (newPos.getAttribute('class').includes("wall")) {

  } else {
    player.innerHTML = oldXY[0] + "," + oldXY[1];
    player.removeAttribute("id");

    newPos.setAttribute('id', 'player');;
    newPos.innerHTML = "<h1>P</h1>"
  }

  return true;
}

function getXY(playerTile) {
  return playerTile.getAttribute('class').split(" ")[0].replace("x", "").split("y");
}

function getNewXY(key, XY) {
  var tempXY = [XY[0], XY[1]]
  switch (key) {
  case 37: //LEFT
    console.log('Left');
    tempXY[1] = (tempXY[1] === "0") ? 0 : --tempXY[1];
    break;
  case 38: //UP
    console.log('Up');
    tempXY[0] = tempXY[0] === "0" ? 0 : --tempXY[0];
    break;
  case 39: //RIGHT
    console.log('Right');
    tempXY[1] = tempXY[1] === "10" ? 10 : ++tempXY[1];
    break;
  case 40: //DOWN
    console.log('Down');
    tempXY[0] = tempXY[0] === "10" ? 10 : ++tempXY[0];
    break;
  default:
    console.log('Other character (not an arrow key)');
    break;
  }
  return tempXY
}