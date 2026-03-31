using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

namespace connect4
{
    class Player
    {
        char _icon;
        ConsoleColor _color;
        bool _isAi;
        int _AiDifficulty;
        public char icon { get => _icon; set => _icon = value; }
        public ConsoleColor color { get => _color; set => color = value; }
        public bool isAi { get => _isAi; set => isAi = value; }
	    public int AiDifficulty { get => _AiDifficulty; set => _AiDifficulty = value; }



        public void playTurn(Board board)
        {
            if (_isAi)
            {
                aiTurn(board);
                return;
            }

            
        }

        private void aiTurn(Board board)
        {
            switch(AiDifficulty)
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
    }
    class Board
    {
        char[,] _board;
        public char[,] board { get => _board; }

        public Board(int width, int height)
        {
            _board = new char[width, height];
        }
    }
    
    class GUI
    {
        //int for GUI
        private static int _boardWidthOffset = 2;
        private static int _boardHeightOffset = 1;

        //ints for position of stuff on the menu
        private static int _menuPosition = 0;
        private static int _menuPositionMax;

        //colors uesd for gui
        //storing them as variables so i have the ability to quickly change the color scheme of the application when i want too
        static ConsoleColor primaryColor = ConsoleColor.White;
        static ConsoleColor primaryColorBackground = ConsoleColor.Black;
        static ConsoleColor textColor = ConsoleColor.White;
        static ConsoleColor textColorBackground = ConsoleColor.Black;

        public static void displayBoard(Board board)
        {

        }

        public static void displayControls()
        {
            string[] tutorialControls = { "WASD: Moves the position on the menu", "E: Select highlighted option", "P: quit the game at any time", "Press any key to exit" };
            _menuPosition = 6;
            _menuPositionMax = 1;
            displayMenuOptions(tutorialControls);
            _menuPosition = 0;

            string waitForInput = getInput();
            Console.Clear();
        }

        public static void mainMenu()
        {
            string[] mainMenuText = { "Play VS Player", "Play VS AI", "Controls", "Quit" };
            _menuPositionMax = 3;
            displayMenuOptions(mainMenuText);
            getUserInput();
        }

        private static void mainMenuOptions()
        {
            switch (_menuPosition)
            {
                case 0:

                    break;
                case 1:
                    break;
                case 2:
                    displayControls();
                    break;
                case 3:
                    Game.gameLocation = "exit";
                    break;
                default:

                    break;
            }
        }

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

        private static void getUserInput()
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
                    optionSelection();
                    break;
                //key far away from main game to exit said game -- mainly for debugging
                case "p":
                    Game.gameLocation = "exit";
                    break;
                default:
                    break;
            }
        }

        private static void optionSelection()
        {
            switch (Game.gameLocation)
            {
                case "MainMenu":
                    mainMenuOptions();
                    break;
            }
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
        static string _gameLocation = "MainMenu";
        static bool _playingGame = true;
        public static string gameLocation { get => _gameLocation; set => _gameLocation = value; }

        //player variables
        static Player player1;
        static Player player2;

        //game settings variables
        static Board _gameBoard;
        public static Board gameBoard { get => _gameBoard; set => _gameBoard = value; }
        public static void Main(string[] args)
        {
            Console.Clear();
            Console.CursorVisible = false;

            GUI.displayControls();

            while (_playingGame)
            {
                manageGameState();
            }
        }

        private static void manageGameState()
        {
            switch (_gameLocation)
            {
                case "MainMenu":
                    GUI.mainMenu();
                    break;
                default:
                    _playingGame = false;
                    break;
            }
        }

        public static void newGame(bool vsAi)
        {
            player1 = new Player();
            player2 = new Player();

            if (vsAi)
            {
                player2.isAi = true;
            }

        }
    }
}