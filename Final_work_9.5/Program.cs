using System;
using SFML.Learning;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System.Threading;

class Final_work_9_5 : Game
{
    static Color[] colors = new Color[] { Color.Blue, Color.Green, Color.Red, Color.Cyan, Color.Magenta, Color.Yellow, Color.Black};

    static bool Endgame = true;

    static int maxpoint = 0;
    static int currentpoint = 0;
    static int points = 0;
    static int deltapoint = 3;
    static int delay = 40;

    static int bigradius = 200;
    static int R = 200;
    static int smallradius = 0;
    static int deltaradius = 5;
    static int level = 1;
    static int pointlevel = 1000;


    static string klickSound = LoadSound("korotkiy-pisk1.wav");
    static string crashSound = LoadSound("korotkiy-gudok.wav");
    static string bgMusic = LoadMusic("harakternyiy-plavayuschiy-fon-24867.wav");

    static void Main(string[] args)
   {
        SetFont("comic.ttf");

        InitWindow(800, 600, "Final_work_9_5");

        Random rnd = new Random();
        int i = rnd.Next(0, 7);

        PlayMusic(bgMusic, 20);
        
        while (true) 
        {
            DispatchEvents();

            if (Endgame == true) 
            {
                            
               smallradius = smallradius + deltaradius;

               currentpoint = currentpoint + deltapoint;
                            
                if (GetKeyDown(Keyboard.Key.Space) == true)
                {
                    bigradius = smallradius;
                    smallradius = 0;
                    points = points + currentpoint;
                    currentpoint = 0;
                    PlaySound(klickSound, 40);
                }

                if (smallradius >= bigradius) 
                {
                    i = rnd.Next(0, 7);
                    smallradius = 0;
                    bigradius = R;
                    if (maxpoint < points) maxpoint = points;
                    currentpoint = 0;
                    points = 0;
                    PlaySound(crashSound, 40);
                }                    
                if (maxpoint >= 1000 && maxpoint < 1500) 
                {
                    R = 230;
                    delay = 30;
                    level = 2;
                    pointlevel = 1500;
                    deltapoint = 4;
                }
                if (maxpoint >= 1500 && maxpoint < 2000)
                {
                    R = 260;
                    delay = 20;
                    level = 3;
                    pointlevel = 2000;
                    deltapoint = 5;
                }
                if (maxpoint >= 2000)
                {
                    R = 290;                    
                    delay = 10;
                    level = 4;
                    pointlevel = 2500;
                    deltapoint = 6;
                }
                if (maxpoint >= 2500)
                {
                    ClearWindow(255, 255, 255);
                    DrawText(230, 180, "Победа! Ваш рекорд: " + maxpoint, 24);
                    DisplayWindow();
                    Delay(4000);
                    Endgame = false;
                }
            }
            if (Endgame == false)
            {
                ClearWindow(255, 255, 255);
                DrawText(120, 220, "ESC - выход из игры, BackSpace - начать сначала", 24);
                DisplayWindow();
            }

            var isExit = GetKey(Keyboard.Key.Escape);
            var isRestartGame = GetKey(Keyboard.Key.BackSpace);
            var isChitCode = GetKeyDown(Keyboard.Key.L);

            if (isExit) break;
            if (isRestartGame)
            {
                R = 200;
                bigradius = 200;
                delay = 40;
                level = 1;
                pointlevel = 1000;
                deltapoint = 3;
                Endgame = true;
                maxpoint = 0;
            }
            if (isChitCode)
            {
                R = R + 30;
                delay = delay - 10;
                level++;
                pointlevel = pointlevel + 500;
                deltapoint = deltapoint + 1;
                maxpoint = maxpoint + 1000;
            }
            if (Endgame == true)
            {
                ClearWindow(0, 0, 0);
                SetFillColor(255, 255, 255);

                if (maxpoint < 2000) DrawText(1, 100, "Наберите " + pointlevel + " для перехода", 18);
                if (maxpoint < 2000) DrawText(1, 120, "на следующий уровень", 18);
                if (maxpoint >= 2000) DrawText(1, 100, "Наберите " + pointlevel + " для победы", 18);

                DrawText(700, 1, "Уровень: " + level, 18);
                DrawText(1, 1, "Инструкция", 18);
                DrawText(4, 20, "Не выходите за пределы круга!", 18);
                DrawText(1, 50, "Клавиши:", 18);
                DrawText(1, 70, "Остановить: ПРОБЕЛ", 18);
                DrawText(1, 550, "Максимальный результат : " + maxpoint, 18);
                DrawText(1, 570, "Очки : " + (points + currentpoint), 18);

                FillCircle(500, 300, bigradius);
                SetFillColor(colors[i]);

                FillCircle(500, 300, smallradius);
                DisplayWindow();
                Delay(delay);
            }
        }
        // End Game

        ClearWindow(200, 200, 200);

        DrawText(300, 250, "GAME OVER", 30);
        DisplayWindow();

        Delay(4000);
    }
}
