using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quadris
{

    class Program
    {
        // Hardcoded Shapes of Shame
        static Shape sc = new Shape();
        static Tetromino scP1 = new Tetromino(new char[2, 2]
                     {
                            { 's', 's' },
                            { 's', 's' }
                     }, 1);

        static Shape si = new Shape();
        static Tetromino siP1 = new Tetromino(new char[4, 1]
                        {
                            { 'i' },
                            { 'i' },
                            { 'i' },
                            { 'i' }
                        }, 0);
        static Tetromino siP2 = new Tetromino(new char[1, 4]
                {
                            { 'i', 'i', 'i', 'i' }
                            
                }, 3);

        static Shape sz = new Shape();
        static Tetromino zP1 = new Tetromino(new char[2, 3]
                        {
                            { 'e', 'z', 'z' },
                            { 'z', 'z', 'e' }
                        }, 2);
        static Tetromino zP2 = new Tetromino(new char[3, 2]
                {
                            { 'z', 'e', },
                            { 'z', 'z', },
                            { 'e', 'z', }
                }, 1);

        static Shape sn = new Shape();
        static Tetromino nP1 = new Tetromino(new char[2, 3]
                        {
                            { 'n', 'n', 'e' },
                            { 'e', 'n', 'n' }
                        }, 2);
        static Tetromino nP2 = new Tetromino(new char[3, 2]
                {
                            { 'e', 'n' },
                            { 'n', 'n' },
                            { 'n', 'e' }
                }, 1);

        static Shape sl = new Shape();
        static Tetromino lP1 = new Tetromino(new char[3,2]
                        {
                            { 'l', 'e' },
                            { 'l', 'e' },
                            { 'l', 'l' }
                    }, 1);
        static Tetromino lP2 = new Tetromino(new char[2, 3]
                {
                            { 'l', 'l', 'l' },
                            { 'l', 'e', 'e' }
                }, 2);
        static Tetromino lP3 = new Tetromino(new char[3, 2]
        {
                            { 'l', 'l', },
                            { 'e', 'l', },
                            { 'e', 'l', }
        }, 1);
        static Tetromino lP4 = new Tetromino(new char[2, 3]
        {
                            { 'e', 'e', 'l' },
                            { 'l', 'l', 'l' }
        }, 2);

        static Shape sj = new Shape();
        static Tetromino jP1 = new Tetromino(new char[3, 2]
                        {
                            { 'e', 'j' },
                            { 'e', 'j' },
                            { 'j', 'j' },
                    }, 1);
        static Tetromino jP2 = new Tetromino(new char[2, 3]
                {
                            { 'j', 'e', 'e' },
                            { 'j', 'j', 'j' },
                }, 2);
        static Tetromino jP3 = new Tetromino(new char[3, 2]
        {
                            { 'j', 'j' },
                            { 'j', 'e' },
                            { 'j', 'e' }
        }, 1);
        static Tetromino jP4 = new Tetromino(new char[2, 3]
        {
                            { 'j', 'j', 'j' },
                            { 'e', 'e', 'j' }
        }, 2);

        static Shape st = new Shape();
        static Tetromino tP1 = new Tetromino(new char[2, 3]
                        {
                            { 't', 't', 't' },
                            { 'e', 't', 'e' }
                    }, 2);
        static Tetromino tP2 = new Tetromino(new char[3, 2]
                {
                            { 'e', 't' },
                            { 't', 't' },
                            { 'e', 't' }
                }, 1);
        static Tetromino tP3 = new Tetromino(new char[2, 3]
        {
                            { 'e', 't', 'e' },
                            { 't', 't', 't' }
        }, 2);
        static Tetromino tP4 = new Tetromino(new char[3, 2]
        {
                            { 't', 'e' },
                            { 't', 't' },
                            { 't', 'e' },
        }, 1);


        // Class Variables (Game variables)
        static ConsoleKeyInfo lastInput;
        static int w = 10, h = 20;
        static char[,] field = new char[w, h];
        static char[,] renderField = new char[w, h];

        static double startSpeed = 5.0;
        static double speed = startSpeed;
        static uint score = 0;

        static bool lastWasQuadris = false;
        static bool slamming = false;

        static Shape currentShape;
        static Shape nextShape;
        static Shape savedShape;

        // Threads
        static Thread inputThread = new Thread(Input);
        static bool input = true;
        static Thread logicThread = new Thread(Logic);
        static bool logic = true;
        static Thread renderThread = new Thread(Render);
        static bool render = true;
        static Thread soundThread = new Thread(Sound);
        static bool sound = true;


        // Starter Function
        static void StartUp()
        {
            sc.phases.Add(scP1);
            sc.phaseCount = 1;

            si.phases.Add(siP1);
            si.phases.Add(siP2);
            si.phaseCount = 2;

            sz.phases.Add(zP1);
            sz.phases.Add(zP2);
            sz.phaseCount = 2;

            sn.phases.Add(nP1);
            sn.phases.Add(nP2);
            sn.phaseCount = 2;

            sl.phases.Add(lP1);
            sl.phases.Add(lP2);
            sl.phases.Add(lP3);
            sl.phases.Add(lP4);
            sl.phaseCount = 4;

            sj.phases.Add(jP1);
            sj.phases.Add(jP2);
            sj.phases.Add(jP3);
            sj.phases.Add(jP4);
            sj.phaseCount = 4;

            st.phases.Add(tP1);
            st.phases.Add(tP2);
            st.phases.Add(tP3);
            st.phases.Add(tP4);
            st.phaseCount = 4;

            Console.CursorVisible = false;
            
            Console.SetCursorPosition(28, 18);
            Console.Write("A / D - Move Left and Right.");
            Console.SetCursorPosition(28, 19);
            Console.Write("Q / E - Rotate Counter Clockwise and Clockwise.");
            Console.SetCursorPosition(28, 20);
            Console.Write("Space - Slam Piece.");
            Console.SetCursorPosition(28, 21);
            Console.Write("R - Save Shape / Swap Saved Shape.");
            Console.SetCursorPosition(28, 22);
            Console.Write("M - Mute / Unmute Music.");


            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    field[i, j] ='e';
                    renderField[i, j] = 'e';
                }
            }



            RandomNextShape();
            SpawnNewShape();
        }


        //Render Thread Helpers
        static void DrawChar(char c)
        {
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
                    { Console.BackgroundColor = ConsoleColor.Black; break; }
                case 'e'://Empty Space
                    { Console.BackgroundColor = ConsoleColor.Black; break; }
                default:
                    { Console.BackgroundColor = ConsoleColor.DarkRed; break; }
            }
            if (c == 'e')
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" |");
            }
            else
            {
                Console.Write("  ");
            }
            Console.ResetColor();
        }

        static void RenderUpdateInfo()
        {
            Console.SetCursorPosition(28, 14);
            Console.Write("Score: " + score + "             ");
            Console.SetCursorPosition(28, 16);
            Console.Write("Speed: " + speed + "             ");
        }

        static char[,] GenerateShapeInfoArray(Shape renderShape)
        {

            char[,] temp = new char[4, 4] {
                { 'h','h','h','h' },
                { 'h','h','h','h' },
                { 'h','h','h','h' },
                { 'h','h','h','h' }
            };

            for (int i = 0; i < renderShape.phases[renderShape.phase].matrix.GetLength(0); i++)
            {
                for (int j = 0; j < renderShape.phases[renderShape.phase].matrix.GetLength(1); j++)
                {
                    if (renderShape.phases[renderShape.phase].matrix[i, j] != 'e')
                    {
                        temp[i, j] = renderShape.phases[renderShape.phase].matrix[i, j];
                    }
                }
            }

            return temp;
        }

        static void RenderUpdateNextShape()
        {
            Console.SetCursorPosition(28, 1);
            Console.Write("Next Shape:");
            char[,] temp = GenerateShapeInfoArray(nextShape);
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(28, 2 + i);
                for (int j = 0; j < 4; j++)
                {
                    DrawChar(temp[i, j]);
                }
            }
        }

        static void RenderUpdateSavedtShape()
        {
            Console.SetCursorPosition(28, 8);
            Console.Write("Saved Shape:");

            if (savedShape != null)
            {
                char[,] temp = GenerateShapeInfoArray(savedShape);
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
        static void ShiftDown(int start)
        {
            for (int i = start; i > 0; i--)
            {
                for (int j = 0; j < w; j++)
                {
                    field[j, i] = field[j, i-1];
                }
            }
            for (int i = 0; i < w; i++)
            {
                field[i, 0] = 'e';
            }
        }

        static uint CheckForLines()
        {
            uint lines = 0;
            for (int i = 0; i < h; i++)
            {
                bool cleared = true;
                for (int j = 0; j < w; j++)
                {
                    if (field[j,i] != 'f')
                    {
                        cleared = false;
                        break;
                    }
                }
                if (cleared)
                {
                    lines++;
                    ShiftDown(i);
                }
            }
            return lines;
        }

        static void UpdateScore(uint lines)
        {
            if (lines > 0)
            {
                if (lastWasQuadris)
                {
                    score += 1200;
                }
                else
                {
                    score += lines * 200 + ( 200 * (uint)lines/4);
                }

                lastWasQuadris = lines == 4;

                speed = startSpeed + (5 * (int)(score / 3000));
            }
        }

        static void Freeze()
        {
            char[,] simulatedField = UpdateSimulation(currentShape);

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

        static void SpawnNewShape()
        {
            currentShape = Shape.Copy(nextShape);
            RandomNextShape();
        }

        static void RandomNextShape()
        {
            Random rng = new Random();
            switch (rng.Next(6))
            {
                case 0: { nextShape = Shape.Copy(sc); break; }
                case 1: { nextShape = Shape.Copy(si); break; }
                case 2: { nextShape = Shape.Copy(sz); break; }
                case 3: { nextShape = Shape.Copy(sn); break; }
                case 4: { nextShape = Shape.Copy(st); break; }
                case 5: { nextShape = Shape.Copy(sl); break; }
                case 6: { nextShape = Shape.Copy(sj); break; }
                default: { break; }
            }
        }

        static void SaveShape()
        {

            if (savedShape == null)
            {
                savedShape = Shape.Copy(currentShape);
                SpawnNewShape();
            }
            else
            {
                Shape temp = Shape.Copy(currentShape);
                currentShape = Shape.Copy(savedShape);
                savedShape = Shape.Copy(temp);
                currentShape.x = 4;
                currentShape.y = 0;
                //currentShape.phase = 0;
            }

        }


        // Collision and Bounds Checking
        static char[,] UpdateSimulation(Shape sim)
        {
            char[,] simulatedField = new char[w, h];
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    simulatedField[j, i] = field[j, i];
                }
            }

            for (int i = 0; i < sim.phases[sim.phase].matrix.GetLength(0); i++)
            {
                for (int j = 0; j < sim.phases[sim.phase].matrix.GetLength(1); j++)
                {
                    if (sim.phases[sim.phase].matrix[i, j] != 'e')
                    {
                        simulatedField[sim.x + j, sim.y + i] = sim.phases[sim.phase].matrix[i, j];
                    }
                }
            }
            return simulatedField;
        }
         
        static bool CheckForCollisionDown()
        {
            char[,] simulatedField = UpdateSimulation(currentShape);

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (simulatedField[j, i] != 'e' && simulatedField[j, i] != 'f')
                    {
                        if (i + 1 > h-1 || field[j, i + 1] == 'f')
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        static bool CheckForCollisionLeft()
        {
            char[,] simulatedField = UpdateSimulation(currentShape);

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

        static bool CheckForCollisionRight()
        {
            char[,] simulatedField = UpdateSimulation(currentShape);

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (simulatedField[j, i] != 'e' && simulatedField[j, i] != 'f')
                    {
                        if (j + 1 > w-1 || field[j + 1, i] == 'f')
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        static bool CheckForBounds(Shape sim)
        {
            try
            {
                char[,] simulatedField = UpdateSimulation(sim);

                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        if (simulatedField[j, i] != 'e' && simulatedField[j, i] != 'f')
                        {
                            if (field[j, i] == 'f')
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }


        // Movement and Rotation
        static public void RotateClockwise()
        {
            int oldX = currentShape.x, newX = currentShape.x, oldPhase = currentShape.phase, nextPhase = currentShape.phase;
            Shape sim = Shape.Copy(currentShape);

            if (nextPhase + 1 > currentShape.phaseCount - 1)
            {
                nextPhase = 0;
            }
            else
            {
                nextPhase++;
            }

            if (newX > w - 1 - currentShape.phases[nextPhase].padding)
            {
                newX = w - 1 - currentShape.phases[nextPhase].padding;
            }
            sim.x = newX;
            sim.phase = nextPhase;

            if (!CheckForBounds(sim))
            {
                currentShape.phase = oldPhase;
                currentShape.x = oldX;
                currentShape.phase = nextPhase;
                currentShape.x = newX;
            }
        }

        static public void RotateCounterClockwise()
        {
            int oldX = currentShape.x,newX = currentShape.x, oldPhase = currentShape.phase, nextPhase = currentShape.phase;
            Shape sim = Shape.Copy(currentShape);
            if (nextPhase - 1 < 0)
            {
                nextPhase = currentShape.phaseCount - 1;
            }
            else
            {
                nextPhase--;
            }

            if (newX > w - 1 - currentShape.phases[nextPhase].padding)
            {
                newX = w - 1 - currentShape.phases[nextPhase].padding;
            }

            sim.x = newX;
            sim.phase = nextPhase;

            if (!CheckForBounds(sim))
            {
                currentShape.phase = oldPhase;
                currentShape.x = oldX;

                currentShape.x = newX;
                currentShape.phase = nextPhase;
            }
        }

        static void MoveDown()
        {
            if (currentShape.y < h-1)
            {
                currentShape.y++;
            }
        }

        static void MoveLeft()
        {
            if (currentShape.x > 0)
            {
                currentShape.x--;
            }
        }

        static void MoveRight()
        {
            if (currentShape.x < w - currentShape.phases[currentShape.phase].padding)
            {
                currentShape.x++;
            }
        }


        // Core Loops
        static void Input()
        {
            while (input)
            {
                //Thread.CurrentThread.IsBackground = true;
                lastInput = Console.ReadKey(true);

                if (lastInput.KeyChar != 0)
                {
                    switch (lastInput.KeyChar)
                    {
                        case 'a':
                            {
                                if (!CheckForCollisionLeft())
                                {
                                    MoveLeft();
                                }
                                break;
                            }
                        case 'd':
                            {
                                if (!CheckForCollisionRight())
                                {
                                    MoveRight();
                                }
                                break;
                            }
                        case 'q':
                            {
                                RotateCounterClockwise();
                                break;
                            }
                        case 'e':
                            {
                                RotateClockwise();
                                break;
                            }
                        case 'm':
                            {
                                if (!soundThread.IsAlive)
                                {
                                    soundThread.Start();
                                }
                                else
                                {
                                    soundThread.Interrupt();
                                }
                                break;
                            }
                        case 'r':
                            {
                                SaveShape();
                                break;
                            }
                        case ' ':
                            {
                                slamming = true;
                                break;
                            }
                        default: { break; }
                    }
                    lastInput = new ConsoleKeyInfo();
                }

            }
        }

        static void Logic()
        {
            while (logic)
            {
                if (!slamming)
                {
                    Thread.Sleep((int)(1000 / (speed / 2.0)));
                }

                if (CheckForCollisionDown())
                {
                    Freeze();
                    slamming = false;
                    SpawnNewShape();
                }
                else
                {
                    MoveDown();
                }

                UpdateScore(CheckForLines());

                if (field[4,0] == 'f')
                {
                    logic = false;
                    render = false;
                    input = false;
                    sound = false;
                    Thread.Sleep(1000);

                    Console.SetCursorPosition(0,22);
                    Console.ResetColor();
                    Console.WriteLine("Game Over!");
                    Console.WriteLine("Score: " + score);
                    Console.WriteLine("Press Key to Continue.");
                    Console.ReadKey();

                    Environment.Exit(0);
                }
            }
        }

        static void Render()
        {
            while (render)
            {
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        renderField[j, i] = field[j, i];
                    }
                }

                for (int i = 0; i < currentShape.phases[currentShape.phase].matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < currentShape.phases[currentShape.phase].matrix.GetLength(1); j++)
                    {
                        if (currentShape.phases[currentShape.phase].matrix[i, j] != 'e')
                        {
                            try
                            {
                                renderField[currentShape.x + j, currentShape.y + i] = currentShape.phases[currentShape.phase].matrix[i, j];
                            }
                            catch (Exception)
                            {
                                int x = currentShape.x + j;
                                int y = currentShape.y + i;
                                //throw;
                            }
                            
                        }
                    }
                }

                Console.SetCursorPosition(0,0);

                Console.WriteLine();
                Console.ResetColor();
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("                        ");
                Console.ResetColor();
                for (int i = 0; i < h; i++)
                {
                    Console.ResetColor();
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("  ");
                    Console.ResetColor();
                    for (int j = 0; j < w; j++)
                    {
                        DrawChar(renderField[j, i]);
                    }
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine("  ");
                    Console.ResetColor();
                }
                Console.ResetColor();
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("                        ");
                Console.ResetColor();

                RenderUpdateNextShape();
                RenderUpdateSavedtShape();
                RenderUpdateInfo();
            }

        }

        static void Sound()
        {

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
                    Console.Beep((int)rawSequence[i], (int)noteDuration[i]+100);
                }
            }
        }

        static void Main(string[] args)
        {
            StartUp();

            inputThread.Start();
            logicThread.Start();
            renderThread.Start();
        }
    }
}
