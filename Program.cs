using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

namespace connect4
{
    abstract class Player
    {
        char _icon;
        ConsoleColor _color;
        public char icon { get => _icon; set => _icon = value; }
        public ConsoleColor color { get => _color; set => color = value; }


        public Player(char inputChar, ConsoleColor color)
        {
            _icon = inputChar;
            _color = color;
        }
        public virtual void playTurn(Board board)
        {

        }
    }

    class HumanPlayer : Player
    {
        public HumanPlayer(char inputChar, ConsoleColor color) : base(inputChar, color)
        {
            
        }

        public override void playTurn(Board board)
        {
            
        }

        public override string ToString()
        {
            return "player";
        }
    }
    class AiPlayer : Player
    {
        int _AiDifficulty;

        public AiPlayer(char inputChar, ConsoleColor color, int difficulty) : base(inputChar, color)
        {
            _AiDifficulty = difficulty;
        }

        public int AiDifficulty { get => _AiDifficulty; set => _AiDifficulty = value; }

        public override void playTurn(Board board)
        {
            switch (AiDifficulty)
            {
                //TODO create ai difficulty
                //if you have a 3 in a row starts at 100% to block from first turn you can get connect 4 and every other turn after that reduce this number by 25% to a minimum of 50%
                //level 1 ai priority
                //if it has a 3 in a row 50% chance to win
                //if placing a marker with either of the 2 methods below this would result in you getting a 4 in a row dont do it at 50% chance
                //if it can place a marker next to or diagonal to one of its own do it at a 25% chance
                //if you have a 2 in a row 10% chance to block it
                //if all previous checks fail. randomly place a marker
                case 1:

                    break;
                //level 2 ai priority
                //if it has a 3 in a row 75% chance to win
                //if you have a 3 in a row starts at 100% to block from first turn you can get connect 4 and every other turn after that reduce this number by 10% to a minimum of 50%
                //if placing a marker with either of the 2 methods below this would result in you getting a 4 in a row dont do it at 75% chance
                //if it can place a marker next to or diagonal to one of its own do it at a 75% chance
                //if you have a 2 in a row 25% chance to block it
                //if all previous checks fail. randomly place a marker
                case 2:

                    break;
                //level 2 ai priority
                //if it has a 3 in a row win
                //if you have a 3 in a row block it
                //might consider creating something more advance than this for playing when it isnt blocking if i have time
                //if placing a marker with either of the 2 methods below this would result in you getting a 4 in a row dont do it at 100% chance
                //if you have a 2 in a row 25% chance to block it
                //if it can place a marker next to or diagonal to one of its own do it at a 75% chance
                //if all previous checks fail. randomly place a marker
                case 3:

                    break;
            }
        }

        public override string ToString()
            {
                return "ai";
            }
    }
    class Board
    {
        char[,] _board;
        public char[,] board { get => _board; }

        public Board(int width, int height)
        {
            _board = new char[width, height];

            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++) 
                { 
                    _board[i, j] = '.';
                } 
            }
        }

        public int getAvialableSpace(int column)
        {
            for (int i = _board.GetLength(1)-1; i >= 0; i--)
            {
                if (_board[column,i] == '.')
                {
                    return i;
                }
            }

            return 0;
        }
    }
    
    class GUI
    {
        //int for GUI
        private static int _boardWidthOffset = 2;
        private static int _boardHeightOffset = 1;

        //ints for position of stuff on the menu
        private static int _menuPosition = 0;
        public static int menuPosition { get => _menuPosition; set => _menuPosition = value; }
        private static int _menuPositionMax;

        //colors uesd for gui
        //storing them as variables so i have the ability to quickly change the color scheme of the application when i want too
        static ConsoleColor primaryColor = ConsoleColor.White;
        static ConsoleColor primaryColorBackground = ConsoleColor.Black;
        static ConsoleColor textColor = ConsoleColor.White;
        static ConsoleColor textColorBackground = ConsoleColor.Black;

        // stores last position to get back to from control menu
        static string _lastPosition;

        public static void displayBoard(Board inputBoard, Player[] players)
        {
            for (int i = 0; i < inputBoard.board.GetLength(0); i++)
            {
                for (int j = 0; j < inputBoard.board.GetLength(1); j++)
                {
                    Console.SetCursorPosition(i+_boardWidthOffset, j+_boardHeightOffset);

                    foreach (Player check in players)
                    {
                        if (check.icon == inputBoard.board[i,j])
                        {
                            Console.ForegroundColor = check.color;
                        }
                    }
                    Console.WriteLine(inputBoard.board[i,j]);

                    resetColor();
                }
            }
        }

        //display controls stuff
        public static void displayControls()
        {
            string[] tutorialControls = { "WASD: Moves the position on the menu", "E: Select highlighted option", "P: quit the game at any time", "Press any key to exit" };
            _menuPosition = 6;
            _menuPositionMax = 1;
            displayMenuOptions(tutorialControls);
            _menuPosition = 0;

            string waitForInput = getInput();
            Console.Clear();

            if (_lastPosition == null)
            {
                Game.gameLocation = "MainMenu";
                return;
            }

            Game.gameLocation = _lastPosition;
        }

        //main menu stuff
        public static void mainMenu()
        {
            string[] mainMenuText = { "Play VS Player", "Play VS AI", "Controls", "Quit" };
            string[] mainMenuOptions = { "PlayerGame", "AiDifficulty", "Controls", "exit"};

            _menuPositionMax = 3;
            _lastPosition = "MainMenu";
            displayMenuOptions(mainMenuText);
            getUserInput(mainMenuOptions);
        }

        // chose ai settings

        public static void selectAiDifficulty()
        {
            string[] AiDifficultyText = { "Easy", "Medium", "Hard", "Back" };
            string[] AiMenuOptions = { "Ai1", "Ai2", "Ai3", "MainMenu"};

            _menuPositionMax = 3;
            _lastPosition = "AiDifficulty";
            
            displayMenuOptions(AiDifficultyText);
            getUserInput(AiMenuOptions);
        }

        // create player game
        public static void createPlayerGame(Board board)
        {
            _menuPositionMax = board.board.GetLength(0)-1;
            _lastPosition = "MainMenu";

            Game.resetGame();

            Game.gameLocation = "InGame";
        }

        //create game with ai
        public static void createAiGame(string inputDifficulty)
        {
            string[] AiDifficultyText = { "Easy", "Medium", "Hard", "Back" };
            string[] AiMenuOptions = { "Ai1", "Ai2", "Ai3", "MainMenu"};

            _menuPositionMax = 3;
            _lastPosition = "MainMenu";
            
            displayMenuOptions(AiDifficultyText);
            getUserInput(AiMenuOptions);
        }

        static string[] boardOptions;
        public static void inGame(Board board, Player player)
        {   
            highlightBoardOption(board);
            getUserInput(boardOptions);
        }

        public static void showTurn(Board board, Player player)
        {
            Console.SetCursorPosition(_boardWidthOffset, board.board.GetLength(1)+_boardHeightOffset+1);

            Console.Write($"It is currently '{player.icon}' turn");
        }

        private static void highlightBoardOption(Board board)
        {
            if (board.getAvialableSpace(_menuPosition) < 0)
            {
                Console.SetCursorPosition(0+_boardWidthOffset,_menuPosition+_boardHeightOffset);
            }
            else
            {
                Console.SetCursorPosition(_menuPosition+_boardWidthOffset, board.getAvialableSpace(_menuPosition)+_boardHeightOffset);
            }

            Console.BackgroundColor = textColor;
            Console.ForegroundColor = textColorBackground;

            Console.Write(board.board[_menuPosition,board.getAvialableSpace(_menuPosition)]);

            resetColor();
        }

        public static void placePlayerIcon(Player player, Board board)
        {
            Console.SetCursorPosition(_menuPosition+_boardWidthOffset, board.getAvialableSpace(_menuPosition)+_boardHeightOffset);

            board.board[_menuPosition, board.getAvialableSpace(_menuPosition)] = player.icon;
            Console.ForegroundColor = player.color;
            Console.Write(player.icon);

            resetColor();
        }

        // win game
        public static void winGame(Board board, Player[] player, int winner)
        {
            Console.Clear();
            _menuPositionMax = 2;

            string[] winMenuText = { "Replay", "Main Menu", "Quit"};
            string[] winMenuOption = { "PlayerGame", "MainMenu", "exit"};

            Console.SetCursorPosition(_boardWidthOffset, 0);
            
            if (winner == -1)
            {
                Console.Write($"Game is a Tie!");
            }
            else
            {
                Console.Write($"player '{player[winner].icon}' Wins!");
            }
            

            
            displayMenuOptions(winMenuText);
            getUserInput(winMenuOption);
        }
        //generic menu stuffs
        private static void displayMenuOptions(string[] MenuText)
        {
            Console.BackgroundColor = textColorBackground;
            Console.ForegroundColor = textColor;

            //prints the entire main menu options
            for (int i = 0; i < MenuText.Count(); i++)
            {
                Console.SetCursorPosition(_boardWidthOffset, _boardHeightOffset + (2 * i));
                Console.WriteLine(MenuText[i]);
            }

            //overrides the menu option that is currently selected
            if (_menuPosition <= _menuPositionMax)
            {
                Console.SetCursorPosition(_boardWidthOffset, _boardHeightOffset + (2 * _menuPosition));
                Console.BackgroundColor = textColor;
                Console.ForegroundColor = textColorBackground;
                Console.WriteLine(MenuText[_menuPosition]);
            }


            resetColor();
        }

        private static void getUserInput(string[] options)
        {
            string switchInput = getInput();
            createBorder();
            switch (switchInput)
            {
                //moves the menus around
                case "w":
                case "a":
                    if (_menuPosition - 1 >= 0)
                    {
                        _menuPosition--;
                    }
                    break;
                case "s":
                case "d":
                    if (_menuPosition + 1 <= _menuPositionMax)
                    {
                        _menuPosition++;
                    }
                    break;
                //selecst what is used
                case "e":
                    if (Game.gameLocation == "InGame")
                    {
                        Game.doTurn(_menuPosition);
                        break;
                    }
                    optionSelection(options, _menuPosition);
                    break;
                //key far away from main game to exit said game -- mainly for debugging
                case "p":
                    Game.gameLocation = "exit";
                    break;
                default:
                    break;
            }
        }

        private static void optionSelection(string[] options, int selectedOption)
        {
            Console.Clear();
            _menuPosition = 0;
            Game.gameLocation = options[selectedOption];
        }

        public static void resetMenuPos()
        {
            menuPosition = 0;
        }


        //gets the input the user did and returns its value
        private static string getInput()
        {
            Console.SetCursorPosition(0, 0);
            return Console.ReadKey().KeyChar.ToString().ToLower();
        }

        private static void createBorder()
        {

        }

        private static void resetColor()
        {
            Console.BackgroundColor = textColorBackground;
            Console.ForegroundColor = textColor;
        }
    }
    class Game
    {
        static string _gameLocation = "Controls";
        static bool _playingGame = true;
        static int _turn = 0;
        public static string gameLocation { get => _gameLocation; set => _gameLocation = value; }

        //player variables
        static Player[] players;

        //game settings variables
        static Board _gameBoard = new Board(10,5);
        public static Board gameBoard { get => _gameBoard; set => _gameBoard = value; }
        public static void Main(string[] args)
        {
            Console.Clear();
            Console.CursorVisible = false;

            HumanPlayer player1 = new HumanPlayer('x', ConsoleColor.Red);
            HumanPlayer player2 = new HumanPlayer('o', ConsoleColor.Blue);

            players = new Player[2];
            players[0] = player1;
            players[1] = player2;

            _gameLocation = "Controls";

            // GUI.displayControls();
            while (_playingGame)
            {
                manageGameState();
            }
        }

        public static void doTurn(int row)
        {
            if (_gameBoard.board[row, _gameBoard.getAvialableSpace(row)] != '.')
            {
                return;
            }

            int storeForLater = _gameBoard.getAvialableSpace(row);
            GUI.placePlayerIcon(players[_turn], _gameBoard);

            // if (checkForWin(row, storeForLater))
            // {
            //     _gameLocation = "GameWon";
            //     GUI.resetMenuPos();
            //     return;
            // }

            if (checkForTie())
            {
                _gameLocation = "GameTie";
                GUI.resetMenuPos();
                return;
            }

            _turn++;

            if (_turn >= players.GetLength(0))
            {
                _turn = 0;
            }

            GUI.showTurn(_gameBoard, players[_turn]);
        }

        public static void resetGame()
        {
            _turn = 0;
            _gameBoard = new Board(10,5);
            _gameLocation = "InGame";
        }

        private static void manageGameState()
        {
            switch (_gameLocation)
            {
                case "MainMenu":
                    GUI.mainMenu();
                    break;
                case "Controls":
                    GUI.displayControls();
                    break;
                case "AiDifficulty":
                    GUI.selectAiDifficulty();
                    break;
                case "PlayerGame":
                    GUI.createPlayerGame(_gameBoard);
                    break;
                case "InGame":
                    GUI.displayBoard(_gameBoard, players);
                    GUI.showTurn(_gameBoard, players[_turn]);
                    GUI.inGame(gameBoard, players[1]);
                    break;
                case "GameWon":
                    GUI.winGame(_gameBoard, players, _turn);
                    break;
                case "GameTie":
                    GUI.winGame(_gameBoard, players, -1);
                    break;
                case "Ai1":
                case "Ai2":
                case "Ai3":
                    GUI.createAiGame(_gameLocation);
                    break;
                default:
                    _playingGame = false;
                    Console.Clear();
                    break;
            }
        }
    
        public static bool checkForWin(int placeX, int placeY)
        {
            // horizontal win checks
            if (placeX == 0)
            {
                if (horizontalCheck(placeX, placeY, 1, 2, 3))
                {
                    return true;
                }
            }

            if (placeX == 1)
            {
                if (horizontalCheck(placeX, placeY, 1, 2, 3))
                {
                    return true;
                }

                if (horizontalCheck(placeX, placeY, -1, 1, 2))
                {
                    return true;
                }
            }

            if (placeX == 2)
            {
                if (horizontalCheck(placeX, placeY, 1, 2, 3))
                {
                    return true;
                }

                if (horizontalCheck(placeX, placeY, -1, 1, 2))
                {
                    return true;
                }

                if (horizontalCheck(placeX, placeY, -2, -1, 1))
                {
                    return true;
                }
            }

            if (placeX >=3 && placeX <= _gameBoard.board.GetLength(0)-4)
            {
                if (horizontalCheck(placeX, placeY, 1, 2, 3))
                {
                    return true;
                }

                if (horizontalCheck(placeX, placeY, -1, 1, 2))
                {
                    return true;
                }

                if (horizontalCheck(placeX, placeY, -2, -1, 1))
                {
                    return true;
                }

                if (horizontalCheck(placeX, placeY, -3, -2, -1))
                {
                    return true;
                }

            }

            if (placeX == _gameBoard.board.GetLength(0)-1)
            {
                if (horizontalCheck(placeX, placeY, -3, -2, -1))
                {
                    return true;
                }
            }

            if (placeX == _gameBoard.board.GetLength(0)-2)
            {
                if (horizontalCheck(placeX, placeY, -3, -2, -1))
                {
                    return true;
                }

                if (horizontalCheck(placeX, placeY, -2, -1, 1))
                {
                    return true;
                }
            }

            if (placeX == _gameBoard.board.GetLength(0)-3)
            {
                if (horizontalCheck(placeX, placeY, -3, -2, -1))
                {
                    return true;
                }

                if (horizontalCheck(placeX, placeY, -2, -1, 1))
                {
                    return true;
                }
                
                if (horizontalCheck(placeX, placeY, 2, -1, 1))
                {
                    return true;
                }
            }

            // vertical
            if (placeY == 0)
            {
                if (verticalCheck(placeX, placeY, 1, 2, 3))
                {
                    return true;
                }
            }

            if (placeY == 1)
            {
                if (verticalCheck(placeX, placeY, 1, 2, 3))
                {
                    return true;
                }

                if (verticalCheck(placeX, placeY, -1, 1, 2))
                {
                    return true;
                }
            }

            // diagonal checking to the right

            if (placeX <= _gameBoard.board.GetLength(0)-4 && (placeY == 0 || placeY == 1))
            {
                if (diagonalCheckRight(placeX, placeY, 1, 2, 3))
                {
                    return true;
                }
            }

            if ((placeX >= 3) && (placeY == _gameBoard.board.GetLength(1)-1 || placeY == _gameBoard.board.GetLength(1)-2))
            {
                if (diagonalCheckRight(placeX, placeY, -1, -2, -3))
                {
                    return true;
                }
            }

            if (placeX <= _gameBoard.board.GetLength(0)-3 && placeX != 0 && (placeY == 1 || placeY == 2))
            {
                if (diagonalCheckRight(placeX, placeY, 1, -1, 2))
                {
                    return true;
                }
            }

            if ((placeX >= 2) && (placeY == _gameBoard.board.GetLength(1)-2 || placeY == _gameBoard.board.GetLength(1)-3))
            {
                if (diagonalCheckRight(placeX, placeY, -1, -2, 1))
                {
                    return true;
                }
            }

            // diagonal checking to the left

            if (placeX <= _gameBoard.board.GetLength(0)-4 && placeX >= 3 && (placeY == 0 || placeY == 1))
            {
                if (diagonalCheckLeft(placeX, placeY, 1, 2, 3))
                {
                    return true;
                }
            }

            if (placeX >= 3 && placeX != _gameBoard.board.GetLength(0)-2 && (placeY == _gameBoard.board.GetLength(1)-1 || placeY == _gameBoard.board.GetLength(1)-2))
            {
                if (diagonalCheckLeft(placeX, placeY, -1, -2, -3))
                {
                    return true;
                }
            }

            if (placeX <= _gameBoard.board.GetLength(0)-3 && placeX != 0 && (placeY == 1 || placeY == 2))
            {
                if (diagonalCheckLeft(placeX, placeY, 1, -1, 2))
                {
                    return true;
                }
            }

            if ((placeX >= 2) && placeX <= _gameBoard.board.GetLength(0)-3 && (placeY == _gameBoard.board.GetLength(1)-2 || placeY == _gameBoard.board.GetLength(1)-3))
            {
                if (diagonalCheckLeft(placeX, placeY, -1, -2, 1))
                {
                    return true;
                }
            }
            
            return false;
        }


        private static bool horizontalCheck(int placeX, int placeY, int n1, int n2, int n3)
        {
            if (_gameBoard.board[placeX+n1,placeY] == players[_turn].icon && _gameBoard.board[placeX+n2,placeY] == players[_turn].icon && _gameBoard.board[placeX+n3,placeY] == players[_turn].icon)
            {
                return true;
            }

            return false;
        }

        private static bool verticalCheck(int placeX, int placeY, int n1, int n2, int n3)
        {
            if (_gameBoard.board[placeX,placeY+n1] == players[_turn].icon && _gameBoard.board[placeX,placeY+n2] == players[_turn].icon && _gameBoard.board[placeX,placeY+n3] == players[_turn].icon)
            {
                return true;
            }

            return false;
        }

        private static bool diagonalCheckRight(int placeX, int placeY, int n1, int n2, int n3)
        {
            if (_gameBoard.board[placeX+n1,placeY+n1] == players[_turn].icon && _gameBoard.board[placeX+n2,placeY+n2] == players[_turn].icon && _gameBoard.board[placeX+n3,placeY+n3] == players[_turn].icon)
            {
                return true;
            }

            return false;
        }

        private static bool diagonalCheckLeft(int placeX, int placeY, int n1, int n2, int n3)
        {
            if (_gameBoard.board[placeX-n1,placeY+n1] == players[_turn].icon && _gameBoard.board[placeX-n2,placeY+n2] == players[_turn].icon && _gameBoard.board[placeX-n3,placeY+n3] == players[_turn].icon)
            {
                return true;
            }

            return false;
        }

        private static bool checkForTie()
        {
            for (int i = 0; i < _gameBoard.board.GetLength(0); i++)
            {
                if (_gameBoard.board[i,0] == '.')
                {
                    return false;
                }
            }

            return true;
        }
    }
}