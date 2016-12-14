window.onload = function () {
  // Fancy graphics when you win the game
  modal();

  // For now load map index 1
  let map1 = mapsArray[1];
  for (let x = 0; x < map1.height; x++) {
    // Create a row div
    let row = document.createElement('div');
    row.setAttribute('class', 'row');
    // populate current row with tiles
    for (let y = 0; y < map1.width; y++) {
      let tile = document.createElement('div');
      tile.setAttribute("id", "x" + x + "y" + y);
      let tileType = map1.mapGrid[x][y];
      if (tileType == "W") {
        tile.setAttribute("class", "tile" + Tiles.Wall);
      } else if (tileType == "G") {
        tile.setAttribute("class", "tile" + Tiles.Goal);
      } else if (tileType == "B") {
        tile.setAttribute("class", "tile" + Tiles.Block);
      } else if (tileType == "P") {
        tile.setAttribute("class", "tile" + Tiles.Player);
      } else {
        tile.setAttribute("class", "tile");
      }
      row.appendChild(tile);
    }
    anchor.appendChild(row);
  }
  anchor.style.width = (map1.width * 40) + "px";
}

document.onkeydown = function (event) {
  let player = getPlayer();
  move(player, event);

  return true;
}

document.onkeyup = function (event) {
  let goals = document.getElementsByClassName(Tiles.Goal);
  let blocks = document.getElementsByClassName(Tiles.Goal + Tiles.Block);
  console.log("Goals: " + goals.length + " | blocks in goals: " + blocks.length);

  if (goals.length == blocks.length) {
     document.getElementById('myModal').style.display = "block";
  }

  return true;
}


function getXY(playerTile) {
  return playerTile.getAttribute('id').replace("x", "").split("y");
}

function move(oldPos, event) {
  let key = event.keyCode;
  let newXY = getXY(oldPos);
  // Update newXY coords based on direction.
  switch (key) {
  case 37: //LEFT
    console.log('Left');
    newXY[1] = (newXY[1] === "0") ? 0 : --newXY[1];
    break;
  case 38: //UP
    console.log('Up');
    newXY[0] = newXY[0] === "0" ? 0 : --newXY[0];
    break;
  case 39: //RIGHT
    console.log('Right');
    newXY[1] = ++newXY[1];
    break;
  case 40: //DOWN
    console.log('Down');
    newXY[0] = ++newXY[0];
    break;
  default:
    console.log('Other character (not an arrow key)');
    break;
  }

  // Check the next tile if it is a box or wall and move the if possible.
  let nextPos = document.getElementById("x" + newXY[0] + "y" + newXY[1]);
  if (nextPos.className.includes(Tiles.Wall)) {
    return false;
  } else if (nextPos.className.includes(Tiles.Block)) {
    // if next tile has a block, check to see if that block can move.
    if (move(nextPos, event)) {
      nextPos.className += oldPos.className.includes(Tiles.Player) ? Tiles.Player : Tiles.Block;
      oldPos.className = oldPos.className.replace(Tiles.Player, '');
      oldPos.className = oldPos.className.replace(Tiles.Block, '');
      return true;
    }
  } else {
    // move the entity
    nextPos.className += oldPos.className.includes(Tiles.Player) ? Tiles.Player : Tiles.Block;
    oldPos.className = oldPos.className.replace(Tiles.Player, '');
    oldPos.className = oldPos.className.replace(Tiles.Block, '');
    return true;
  }
}

function getPlayer() {
  return document.getElementsByClassName('player')[0];
}

// Fancy Graphics
function modal() {
  // Get the modal
  var modal = document.getElementById('myModal');
  // Get the <span> element that closes the modal
  var span = document.getElementsByClassName("close")[0];

  // When the user clicks on <span> (x), close the modal
  span.onclick = function () {
    modal.style.display = "none";
  }

  // When the user clicks anywhere outside of the modal, close it
  window.onclick = function (event) {
    if (event.target == modal) {
      modal.style.display = "none";
    }
  }
}