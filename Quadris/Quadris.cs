using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quadris
{
    class Quadris
    {
        // Hardcoded Tetrominos of Shame
        // Every shape in all of their orientations. 4 for most, 3 for some and 1 for one.
        static protected Tetromino sc = new Tetromino(); // Cube Shape
        static protected TetrominoOrientation scP1 = new TetrominoOrientation(new char[2, 2]
                     {
                            { 's', 's' },
                            { 's', 's' }
                     });

        static protected Tetromino si = new Tetromino(); // I shape
        static protected TetrominoOrientation siP1 = new TetrominoOrientation(new char[4, 1]
                        {
                            { 'i' },
                            { 'i' },
                            { 'i' },
                            { 'i' }
                        });
        static protected TetrominoOrientation siP2 = new TetrominoOrientation(new char[1, 4]
                {
                            { 'i', 'i', 'i', 'i' }

                });

        static protected Tetromino sz = new Tetromino(); // Z Shape
        static protected TetrominoOrientation zP1 = new TetrominoOrientation(new char[2, 3]
                        {
                            { 'e', 'z', 'z' },
                            { 'z', 'z', 'e' }
                        });
        static protected TetrominoOrientation zP2 = new TetrominoOrientation(new char[3, 2]
                {
                            { 'z', 'e', },
                            { 'z', 'z', },
                            { 'e', 'z', }
                });

        static protected Tetromino sn = new Tetromino(); // Mirrored Z Shape
        static protected TetrominoOrientation nP1 = new TetrominoOrientation(new char[2, 3]
                        {
                            { 'n', 'n', 'e' },
                            { 'e', 'n', 'n' }
                        });
        static protected TetrominoOrientation nP2 = new TetrominoOrientation(new char[3, 2]
                {
                            { 'e', 'n' },
                            { 'n', 'n' },
                            { 'n', 'e' }
                });

        static protected Tetromino sl = new Tetromino(); // L Shape
        static protected TetrominoOrientation lP1 = new TetrominoOrientation(new char[3, 2]
                        {
                            { 'l', 'e' },
                            { 'l', 'e' },
                            { 'l', 'l' }
                    });
        static protected TetrominoOrientation lP2 = new TetrominoOrientation(new char[2, 3]
                {
                            { 'l', 'l', 'l' },
                            { 'l', 'e', 'e' }
                });
        static protected TetrominoOrientation lP3 = new TetrominoOrientation(new char[3, 2]
        {
                            { 'l', 'l', },
                            { 'e', 'l', },
                            { 'e', 'l', }
        });
        static protected TetrominoOrientation lP4 = new TetrominoOrientation(new char[2, 3]
        {
                            { 'e', 'e', 'l' },
                            { 'l', 'l', 'l' }
        });

        static protected Tetromino sj = new Tetromino(); // Mirrored L Shape (J Shape)
        static protected TetrominoOrientation jP1 = new TetrominoOrientation(new char[3, 2]
                        {
                            { 'e', 'j' },
                            { 'e', 'j' },
                            { 'j', 'j' },
                    });
        static protected TetrominoOrientation jP2 = new TetrominoOrientation(new char[2, 3]
                {
                            { 'j', 'e', 'e' },
                            { 'j', 'j', 'j' },
                });
        static protected TetrominoOrientation jP3 = new TetrominoOrientation(new char[3, 2]
        {
                            { 'j', 'j' },
                            { 'j', 'e' },
                            { 'j', 'e' }
        });
        static protected TetrominoOrientation jP4 = new TetrominoOrientation(new char[2, 3]
        {
                            { 'j', 'j', 'j' },
                            { 'e', 'e', 'j' }
        });

        static protected Tetromino st = new Tetromino(); // T Shape
        static protected TetrominoOrientation tP1 = new TetrominoOrientation(new char[2, 3]
                        {
                            { 't', 't', 't' },
                            { 'e', 't', 'e' }
                    });
        static protected TetrominoOrientation tP2 = new TetrominoOrientation(new char[3, 2]
                {
                            { 'e', 't' },
                            { 't', 't' },
                            { 'e', 't' }
                });
        static protected TetrominoOrientation tP3 = new TetrominoOrientation(new char[2, 3]
        {
                            { 'e', 't', 'e' },
                            { 't', 't', 't' }
        });
        static protected TetrominoOrientation tP4 = new TetrominoOrientation(new char[3, 2]
        {
                            { 't', 'e' },
                            { 't', 't' },
                            { 't', 'e' },
        });

        // Static Constructor, to make sure that all the Tetrominos have their propper orientations.
        // Static constructors are only called once (automatically), they are usually there to initialize static fields
        static Quadris()
        {
            sc.Orientations.Add(scP1);

            si.Orientations.Add(siP1);
            si.Orientations.Add(siP2);

            sz.Orientations.Add(zP1);
            sz.Orientations.Add(zP2);

            sn.Orientations.Add(nP1);
            sn.Orientations.Add(nP2);

            sl.Orientations.Add(lP1);
            sl.Orientations.Add(lP2);
            sl.Orientations.Add(lP3);
            sl.Orientations.Add(lP4);

            sj.Orientations.Add(jP1);
            sj.Orientations.Add(jP2);
            sj.Orientations.Add(jP3);
            sj.Orientations.Add(jP4);

            st.Orientations.Add(tP1);
            st.Orientations.Add(tP2);
            st.Orientations.Add(tP3);
            st.Orientations.Add(tP4);
        }


        // Class Variables (Game variables)
        ConsoleKeyInfo lastInput; // Last Pressed Key goes here.
        protected int w, h; // Width and Height of the game area, for now it only supports 10w and 20h.
        protected char[,] field; // Game matrix
        protected char[,] renderField; // Render matrix

        protected double startSpeed; // Game start speed.
        protected double speed; // Current speed.
        protected uint score = 0; // Current Score.

        protected bool lastWasQuadris = false; // Boolean to check if the last line clear was 4 lines deep, aka Quadris.
        protected bool slamming = false; // Boolean Flag to tell the logic loop if the player is "Slamming" the current Tetromino.

        protected Tetromino currentTetromino; // Current Tetromino that's dropping.
        protected Tetromino nextTetromino; // Upcoming tetromino that will drop after currentTetromino drops.
        protected Tetromino savedTetromino; // Saved Tetromino, to swap with the current one.

        // Threads
        protected Thread inputThread; // A constatly looping thread that checks for input.
        protected bool input = false; // Variable to tell when to shut end the thread's endless cycle;

        protected Thread logicThread; // Logic thread, checks for colision, game end, updates score, etc.
        protected bool logic = false; // Variable to tell when to shut end the thread's endless cycle;

        protected Thread renderThread; // Render Thread, does all the rendering of the game, the game field and info.
        protected bool render = false; // Variable to tell when to shut end the thread's endless cycle;

        protected Thread soundThread; // Plays the tetris tune.
        protected bool sound = false; // Variable to tell when to shut end the thread's endless cycle;

        // Constructor with default values.
        public Quadris(int newW = 10, int newH = 20, double newStartSpeed = 5.0)
        {
            //Initialize the game field size;
            w = newW;
            h = newH;

            startSpeed = newStartSpeed;
            // Start the game with the startSpeed, speed will increase over time, as score increases.
            speed = startSpeed;

            field = new char[w, h];
            renderField = new char[w, h];

            // Assign the the threads to their corresponding looping functions.
            inputThread = new Thread(InputLoop);
            logicThread = new Thread(LogicLoop);
            renderThread = new Thread(RenderLoop);
            soundThread = new Thread(SoundLoop);
        }


        // Starter Function
        public void StartGame()
        {
            // Set the loop flags for the threads, before the threads start.
            input = true;
            logic = true;
            render = true;
            sound = true;

            // Set the console curosr (blinking underscore) to be hidden, this way it looks nicer.
            Console.CursorVisible = false;

            // Set cursor position to this to the 28th column and 18th line
            // Then write some text starting from the 28th column of the 18th line.
            Console.SetCursorPosition(28, 18); 
            Console.Write("A / D - Move Left and Right."); 

            Console.SetCursorPosition(28, 19);
            Console.Write("Q / E - Rotate Counter Clockwise and Clockwise.");

            Console.SetCursorPosition(28, 20);
            Console.Write("Space - Slam Piece.");

            Console.SetCursorPosition(28, 21);
            Console.Write("R - Save Tetromino / Swap Saved Tetromino.");

            Console.SetCursorPosition(28, 22);
            Console.Write("M - Music...Forever");

            // Fill both the game matrix and rendering matrix with 'e'mpty blocks.
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    field[i, j] = 'e';
                    renderField[i, j] = 'e';
                }
            }

            // Since SpawnNewTetromino() spawns the next tetromino, but nextTetromino is empty, we make one.
            RandomNextTetromino();
            SpawnNewTetromino();

            // Start the threads so the game start running.
            // We don't start the sound thread because it uses Console.Beep, which is really loud :C
            // So we made it optional, by pressing m, for music!
            inputThread.Start();
            logicThread.Start();
            renderThread.Start();
        }


        // Render Thread Helpers
        protected void DrawChar(char c)
        {
            // This function takes a character "c" and depending on what it is, changes the background colour
            // It types 2 empty characters except 'e'
            // Empty spaces ('e') in the render field are lines to help guide the Tetrominos.
            // There is also 'h', which is a hidden block.
            switch (c)
            {
                case 's'://Square shape
                    { Console.BackgroundColor = ConsoleColor.Yellow; break; }
                case 'l'://L shape
                    { Console.BackgroundColor = ConsoleColor.DarkYellow; break; }
                case 't'://T shape
                    { Console.BackgroundColor = ConsoleColor.Magenta; break; }
                case 'j'://Backwards L shape
                    { Console.BackgroundColor = ConsoleColor.Cyan; break; }
                case 'z'://Z shape
                    { Console.BackgroundColor = ConsoleColor.Green; break; }
                case 'n'://Backwards Z shape
                    { Console.BackgroundColor = ConsoleColor.Red; break; }
                case 'i'://Backwards Z shape
                    { Console.BackgroundColor = ConsoleColor.DarkCyan; break; }
                case 'f'://Frozen blocks
                    { Console.BackgroundColor = ConsoleColor.White; break; }
                case 'h'://Hidden Space
                    { Console.ResetColor(); break; }
                case 'e'://Empty Space
                    { Console.BackgroundColor = ConsoleColor.Black; break; }
                default:
                    { Console.BackgroundColor = ConsoleColor.DarkRed; break; }
            }

            if (c == 'e')
            {
                // If we are about to type out an empty game space, put a white line in a black background.
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" |");
            }
            else
            {
                // For any other character, use two empty spaces, which in the terminal window should look like a square.
                Console.Write("  ");
            }

            // We type out two characters at a time, because in the terminal window (usually on CMD) the characters are twice as high, as they are wide.

            // Reset the color for the next element to be rendered.
            Console.ResetColor();
        }

        protected void RenderUpdateInfo()
        {
            // Type out the score and current speed of the game.
            Console.SetCursorPosition(28, 14);
            Console.Write("Score: " + score + "             ");
            Console.SetCursorPosition(28, 16);
            Console.Write("Speed: " + speed + "             ");
        }

        protected char[,] GenerateTetrominoInfoArray(Tetromino renderTetromino)
        {
            // This function generates 4x4 arrays of tetrominos.
            // It is used to display the next and saved tetrominos.
            // It is 4x4 to be universal and accomidate any tetromino.

            // Initialize the matrix with 'h'idden characters.
            char[,] temp = new char[4, 4] {
                { 'h','h','h','h' },
                { 'h','h','h','h' },
                { 'h','h','h','h' },
                { 'h','h','h','h' }
            };

            // These loops copy over the matrix of the saved or next tetromnio over to the 4x4 matrix.
            // The if statement ensures that we only copy the parts we need, basically, ignore the empty spaces.
            for (int i = 0; i < renderTetromino.Orientations[renderTetromino.Phase].Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < renderTetromino.Orientations[renderTetromino.Phase].Matrix.GetLength(1); j++)
                {
                    if (renderTetromino.Orientations[renderTetromino.Phase].Matrix[i, j] != 'e')
                    {
                        temp[i, j] = renderTetromino.Orientations[renderTetromino.Phase].Matrix[i, j];
                    }
                }
            }

            // We return the matrix to be displayed.
            return temp;
        }

        protected void RenderUpdateNextTetromino()
        {
            Console.SetCursorPosition(28, 1);
            Console.Write("Next Tetromino:");

            // Generate a 4x4 matrix to display the Tetromino graphically
            char[,] temp = GenerateTetrominoInfoArray(nextTetromino);

            // Print out the matrix
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(28, 2 + i);
                for (int j = 0; j < 4; j++)
                {
                    DrawChar(temp[i, j]);
                }
            }
        }

        protected void RenderUpdateSavedtTetromino()
        {
            // Same as the above function (RenderUpdateNextTetromino)
            // The difference is that this one does it in a different location.
            // And with different text.
            // For a different Tetromino.

            Console.SetCursorPosition(28, 8);
            Console.Write("Saved Tetromino:");

            if (savedTetromino != null)
            {
                char[,] temp = GenerateTetrominoInfoArray(savedTetromino);
                for (int i = 0; i < 4; i++)
                {
                    Console.SetCursorPosition(28, 9 + i);
                    for (int j = 0; j < 4; j++)
                    {
                        DrawChar(temp[i, j]);
                    }
                }
            }

        }


        // Logic Thread Helpers
        protected void ShiftDown(int end)
        {
            // This function takes in an argument "end"
            // And shifts down the field matrix from top (index 0) to bottom, the bottom being "end"
            for (int i = end; i > 0; i--)
            {
                for (int j = 0; j < w; j++)
                {
                    field[j, i] = field[j, i - 1];
                }
            }

            // After it shifts all of them down, it empties the top line
            for (int i = 0; i < w; i++)
            {
                field[i, 0] = 'e';
            }
        }

        protected uint CheckForLines()
        {
            // This function checks how many, if any, lines we have cleared.
            uint lines = 0;
            for (int i = 0; i < h; i++)
            {
                bool cleared = true;
                for (int j = 0; j < w; j++)
                {
                    if (field[j, i] != 'f')
                    {
                        cleared = false;
                        break;
                    }
                }
                if (cleared)
                {
                    lines++;
                    // if we have cleared the line, shift down the next line.
                    ShiftDown(i);
                }
            }
            return lines;
        }

        protected void UpdateScore(uint lines)
        {
            // Update score depending on how many "lines" have been cleared.
            if (lines > 0)
            {
                if (lastWasQuadris)
                {
                    score += 1200;
                }
                else
                {
                    score += lines * 200 + (200 * (uint)lines / 4);
                }

                lastWasQuadris = lines == 4;

                // Increase speed with increaments of 5 for every 3000 score.
                speed = startSpeed + (5 * (int)(score / 3000));
            }
        }

        protected void Freeze()
        {
            // This function freezes the tetromino and updates the game matrix.

            // We make a simulated field with the tetromino overlaped on top of the game field.
            char[,] simulatedField = UpdateSimulation(currentTetromino);

            // We copy freeze all non frozen and non empty blocks and copy them over to the game matrix.
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (simulatedField[j, i] != 'e' && simulatedField[j, i] != 'f')
                    {
                        simulatedField[j, i] = 'f';
                    }
                    field[j, i] = simulatedField[j, i];
                }
            }
        }

        protected void SpawnNewTetromino()
        {
            // Makes currentTetromino to be the Next one and generates a new nextTetromino.
            currentTetromino = Tetromino.Copy(nextTetromino);
            RandomNextTetromino();
        }

        protected void RandomNextTetromino()
        {
            // "Randomly" generates nextTetromino.
            Random rng = new Random();
            switch (rng.Next(6))
            {
                case 0: { nextTetromino = Tetromino.Copy(sc); break; }
                case 1: { nextTetromino = Tetromino.Copy(si); break; }
                case 2: { nextTetromino = Tetromino.Copy(sz); break; }
                case 3: { nextTetromino = Tetromino.Copy(sn); break; }
                case 4: { nextTetromino = Tetromino.Copy(st); break; }
                case 5: { nextTetromino = Tetromino.Copy(sl); break; }
                case 6: { nextTetromino = Tetromino.Copy(sj); break; }
                default: { break; }
            }
        }

        protected void SaveTetromino()
        {
            // Saves the currentTetromino and spawns a new one.
            // If there is one already saved, swaps the saved one and the current one.
            if (savedTetromino == null)
            {
                savedTetromino = Tetromino.Copy(currentTetromino);
                SpawnNewTetromino();
            }
            else
            {
                Tetromino temp = Tetromino.Copy(currentTetromino);
                currentTetromino = Tetromino.Copy(savedTetromino);
                savedTetromino = Tetromino.Copy(temp);
                currentTetromino.X = 4;
                currentTetromino.Y = 0;
                //currentTetromino.Phase = 0;
            }

        }


        // Collision and Bounds Checking
        protected char[,] UpdateSimulation(Tetromino sim)
        {
            // Takes a tetromino as input and generates a simulated matrix that overlaps the tetromino on the game matrix.
            char[,] simulatedField = new char[w, h];

            // Copy the game matrix as the simulated one.
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    simulatedField[j, i] = field[j, i];
                }
            }

            // Put the tetromino in the simulated matrix.
            for (int i = 0; i < sim.Orientations[sim.Phase].Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < sim.Orientations[sim.Phase].Matrix.GetLength(1); j++)
                {
                    if (sim.Orientations[sim.Phase].Matrix[i, j] != 'e')
                    {
                        simulatedField[sim.X + j, sim.Y + i] = sim.Orientations[sim.Phase].Matrix[i, j];
                    }
                }
            }

            return simulatedField;
        }

        protected bool CheckForCollisionDown()
        {
            // Generate a simulated game matrix with the current Tetromino.
            char[,] simulatedField = UpdateSimulation(currentTetromino);

            // Check if the next time it moves down, weather it will:
            // A - Go out of bounds.
            // B - Overlap with a frozen block.
            // If so, it has collided.
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (simulatedField[j, i] != 'e' && simulatedField[j, i] != 'f')
                    {
                        if (i + 1 > h - 1 || field[j, i + 1] == 'f')
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        protected bool CheckForCollisionLeft()
        {
            // Same as CheckForCollisionDown, but for moving left instead of down.
            char[,] simulatedField = UpdateSimulation(currentTetromino);

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (simulatedField[j, i] != 'e' && simulatedField[j, i] != 'f')
                    {
                        if (j - 1 < 0 || field[j - 1, i] == 'f')
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        protected bool CheckForCollisionRight()
        {
            // Same as CheckForCollisionDown, but for moving right instead of down.
            char[,] simulatedField = UpdateSimulation(currentTetromino);

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (simulatedField[j, i] != 'e' && simulatedField[j, i] != 'f')
                    {
                        if (j + 1 > w - 1 || field[j + 1, i] == 'f')
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        protected bool CheckForOverlap(Tetromino sim)
        {
            // This try catch block is here as a dirty hack!
            // This dirty hack returns true (overlap) if the simulated piece is out of bounds.
            // In my defense, this function was previously called "CheckForBounds"
            try
            {
                // Create a new simulated field with a simulated piece to check for overlaps
                char[,] simulatedField = UpdateSimulation(sim);

                // For each point in the simulated field...
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        // ...check only for the active blocks(non - frozen, non - empty)
                        if (simulatedField[j, i] != 'e' && simulatedField[j, i] != 'f')
                        {
                            // If any active block in the simulated field overlaps with a frozen block in the real field, then we have found a overlap.
                            if (field[j, i] == 'f')
                            {
                                return true;
                            }
                        }
                    }
                }
                // If we find no overlaps between the simulated and the real field, return false, no overlaps.
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        protected void Rotate(int direction)
        {
            int oldX = currentTetromino.X; // Save old X position, in case the simulated Tetromino is overlaping a frozen piece.
            int newX = currentTetromino.X; // Shifted X position, in case the new orientation is out of bounds.
            int oldOrientation = currentTetromino.Phase; // Save old orientation, in case the new one is overlaping a frozen piece.
            int newOrientation = currentTetromino.Phase; // New orientation

            // Simulated Tetromino, to see if after rotation, the current tetromino is overlaping or out of bounds.
            Tetromino sim = Tetromino.Copy(currentTetromino);

            //If direction is 1, it's clockwise, if it's -1 it's counter clock wise, so we do bounds checking.
            newOrientation = newOrientation + direction;
            if (newOrientation > currentTetromino.PhaseCount - 1) // -1 because if there are 4 orientations, the last one will have position 3 in the List.
            {
                newOrientation = 0;
            }
            else if (newOrientation < 0)
            {
                newOrientation = currentTetromino.PhaseCount - 1;
            }

            //If the oriented piece is out of bounds, push it back.
            if (newX > w - 1 - currentTetromino.Orientations[newOrientation].Padding)
            {
                newX = w - 1 - currentTetromino.Orientations[newOrientation].Padding;
            }

            //Assign our simulated Tetromino it's new position and orientation.
            sim.X = newX;
            sim.Phase = newOrientation;

            //Check if the simulated piece overlaps a frozen piece, if it's not overlaping, make the current piece, same as the simulated one.
            if (CheckForOverlap(sim) == false)
            {
                currentTetromino.X = newX;
                currentTetromino.Phase = newOrientation;
            }
        }

        protected void MoveDown()
        {
            //Move down up until the very bottom, works for only the currently active piece.
            if (currentTetromino.Y < h - 1)
            {
                currentTetromino.Y++;
            }
        }

        protected void MoveLeft()
        {
            // Check for possible collision and make sure we wont go out of bounds.
            if (currentTetromino.X > 0 && CheckForCollisionLeft() == false)
            {
                currentTetromino.X--;
            }
        }

        protected void MoveRight()
        {
            //Since the origin point of each Tetromino is on it's top left, make sure the right-most piece of a tetromino is not out of bounds.
            if (currentTetromino.X < w - currentTetromino.CurrentOrientation.Padding && CheckForCollisionRight() == false)
            {
                currentTetromino.X++;
            }
        }


        // Core Loops
        protected void InputLoop()
        {
            while (input)
            {
                // Read the Keypress
                lastInput = Console.ReadKey(true);

                if (lastInput.KeyChar != 0)
                {
                    // Make it lower case, just in case they have Caps-Lock on.
                    switch (Char.ToLower(lastInput.KeyChar))
                    {
                        case 'a':
                            {
                                MoveLeft();
                                break;
                            }
                        case 'd':
                            {
                                MoveRight();
                                break;
                            }
                        case 'q':
                            {
                                Rotate(-1);
                                break;
                            }
                        case 'e':
                            {
                                Rotate(1);
                                break;
                            }
                        case 'm':
                            {
                                // Start the sound thread.
                                soundThread.Start();
                                break;
                            }
                        case 'r':
                            {
                                SaveTetromino();
                                break;
                            }
                        case ' ':
                            {
                                slamming = true;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                    // Clear the Key so that we don't repeat the action
                    lastInput = new ConsoleKeyInfo();
                }

            }
        }

        protected void LogicLoop()
        {
            while (logic)
            {
                // If we're not slamming the tetromino, then slow down the game.
                if (!slamming)
                {
                    Thread.Sleep((int)(1000 / (speed / 2.0)));
                }

                //Check for colission, if not move down.
                if (CheckForCollisionDown())
                {
                    Freeze();
                    // Stop slamming once we hit the bottom piece.
                    slamming = false;
                    SpawnNewTetromino();
                }
                else
                {
                    MoveDown();
                }

                // CheckForLines checks if we cleared a line and returns how many.
                // Update score then takes what CheckForLines returns and uses it to update our score.
                UpdateScore(CheckForLines());

                // If the spawn location has been frozen, it's game over.
                if (field[4, 0] == 'f')
                {
                    // Stop all core loops
                    logic = false;
                    render = false;
                    input = false;
                    sound = false;

                    // Wait a bit so all loops conclude.
                    Thread.Sleep(100);

                    // Write The score and etc.
                    Console.SetCursorPosition(0, 22);
                    Console.ResetColor();
                    Console.WriteLine("Game Over!");
                    Console.WriteLine("Score: " + score);
                    Console.WriteLine("Press Key to Continue.");
                    Console.ReadKey();

                    // Exit the program, might add hard core mode for linux users.
                    // It runs rm -rf without parameters if the lose...when they lose.
                    Environment.Exit(0);
                }
            }
        }

        protected void RenderLoop()
        {
            while (render)
            {
                // Copy field, which only has the frozen blocks, over to the renderField.
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        renderField[j, i] = field[j, i];
                    }
                }

                // Overlap the current Tetromino, over the render field
                for (int i = 0; i < currentTetromino.CurrentOrientation.Matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < currentTetromino.CurrentOrientation.Matrix.GetLength(1); j++)
                    {
                        if (currentTetromino.CurrentOrientation.Matrix[i, j] != 'e')
                        {
                            try
                            {
                                renderField[currentTetromino.X + j, currentTetromino.Y + i] = currentTetromino.CurrentOrientation.Matrix[i, j];
                            }
                            catch (Exception)
                            {
                                int x = currentTetromino.X + j;
                                int y = currentTetromino.Y + i;
                                //throw;
                            }

                        }
                    }
                }

                // Core rendering loop happens here!
                Console.SetCursorPosition(0, 0);

                Console.WriteLine(); // Top Padding
                Console.ResetColor();
                Console.Write("  "); // Top Left Padding
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("                        "); // Top Border
                Console.ResetColor();
                for (int i = 0; i < h; i++)
                {
                    Console.ResetColor();
                    Console.Write("  "); // Left Padding
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("  "); // Left Border
                    Console.ResetColor();
                    for (int j = 0; j < w; j++)
                    {
                        DrawChar(renderField[j, i]); // Game field
                    }
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine("  "); // Right Border
                    Console.ResetColor();
                }
                Console.ResetColor();
                Console.Write("  "); // Bottom Left Padding
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("                        "); // Bottom Border
                Console.ResetColor();

                RenderUpdateNextTetromino(); // Show next Tetromino
                RenderUpdateSavedtTetromino(); // Show saved Tetromino
                RenderUpdateInfo(); // Show speed and score
            }

        }

        protected void SoundLoop()
        {
            // This function is pretty self-explanitory.
            double[] noteDuration = {
            406.250, 203.125, 203.125, 406.250, 203.125, 203.125, 406.250, 203.125, 203.125, 406.250,
            203.125, 203.125, 609.375, 203.125, 406.250, 406.250, 406.250, 406.250, 203.125, 203.125,
            203.125, 203.125, 609.375, 203.125, 406.250, 203.125, 203.125, 609.375, 203.125, 406.250,
            203.125, 203.125, 406.250, 203.125, 203.125, 406.250, 406.250, 406.250, 406.250, 406.250, 406.250
        };
            double[] rawSequence = {
            659.25511, 493.8833, 523.25113, 587.32954, 523.25113, 493.8833, 440.0, 440.0, 523.25113,
            659.25511, 587.32954, 523.25113, 493.8833, 523.25113, 587.32954, 659.25511, 523.25113,
            440.0, 440.0, 440.0, 493.8833, 523.25113, 587.32954, 698.45646, 880.0, 783.99087,
            698.45646, 659.25511, 523.25113, 659.25511, 587.32954, 523.25113, 493.8833, 493.8833,
            523.25113, 587.32954, 659.25511, 523.25113, 440.0, 440.0, 100.0
        };

            while (sound)
            {
                for (int i = 0; i < noteDuration.Length; i++)
                {
                    Console.Beep((int)rawSequence[i], (int)noteDuration[i] + 100);
                }
            }
        }
    }
}
