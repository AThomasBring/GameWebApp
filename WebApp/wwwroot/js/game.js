let gameState = [];
const backendUrl = "https://localhost:7122/api/game";

function initializeGameBoard() {
    const gameBoard = document.getElementById('game-container');
    gameBoard.innerHTML = '';

    gameState.forEach((row, rowIndex) => {
        const rowDiv = document.createElement('div');
        rowDiv.classList.add('row');

        row.forEach((cellValue, colIndex) => {
            const cellDiv = document.createElement('div');
            cellDiv.classList.add('cell');
            if (cellValue > 0) {
                const tileDiv = document.createElement('div');
                tileDiv.classList.add('tile', `tile-${cellValue}`);
                tileDiv.textContent = cellValue;
                cellDiv.appendChild(tileDiv);
            }
            rowDiv.appendChild(cellDiv);
        });

        gameBoard.appendChild(rowDiv);
    });
}

async function updateGameState() {
    try {
        const response = await fetch('/api/game', { method: 'GET' });
        if (!response.ok) {
            throw new Error(`Error retrieving game state: ${response.status} - ${response.statusText}`);
        }
        gameState = await response.json();
        initializeGameBoard();
    } catch (error) {
        console.error(error);
    }
}

async function startNewGame() {
    try {
        const response = await fetch(`${backendUrl}/start`, { method: 'POST' });
        if (!response.ok) {
            throw new Error(`Error starting new game: ${response.status} - ${response.statusText}`);
        }
        updateGameState();
    } catch (error) {
        console.error(error);
    }
}

//Could not actually get this to successfully call the backend, not sure why.
window.move = async function (direction) {
    try {
        const response = await fetch(`${backendUrl}/move`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ direction })
        });

        if (!response.ok) {
            throw new Error(`Error moving tiles: ${response.status} - ${response.statusText}`);
        }

        const result = await response.json();

        if (typeof result === 'string') {
            alert(result);
            startNewGame();
        } else {
            gameState = result;
            initializeGameBoard();
        }
    } catch (error) {
        console.error(error);
    }
}

function handleKeyPress(event) {
    switch (event.key) {
        case 'ArrowLeft':
            move('left');
            break;
        case 'ArrowRight':
            move('right');
            break;
        case 'ArrowUp':
            move('up');
            break;
        case 'ArrowDown':
            move('down');
            break;
    }
}

function initializeGame() {
    document.addEventListener('keydown', handleKeyPress);
    startNewGame();
}

document.addEventListener('DOMContentLoaded', initializeGame);
