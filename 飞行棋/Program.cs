using System;

namespace 飞行棋
{
    class Program
    {
        /// <summary>
        /// 静态字段模拟全局变量
        /// </summary>
        public static int[] Maps = new int[100];
        //声明一个静态数组用来存储玩家A和玩家B的坐标
        static int[] PlayerPos = new int[2];
        //存储两个玩家的姓名
        static string[] PlayerNames = new string[2];

        

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            GameShow();

            #region 输入姓名
            Console.WriteLine("请输入玩家A姓名");
            PlayerNames[0] = Console.ReadLine();
            while (PlayerNames[0] == "")
            {
                Console.WriteLine("玩家A的姓名不能为空");
                PlayerNames[0] = Console.ReadLine();
            }

            Console.WriteLine("请输入玩家B姓名");
            PlayerNames[1] = Console.ReadLine();
            while (PlayerNames[1] == "" || PlayerNames[0] == PlayerNames[1])
            {
                if (PlayerNames[0] == PlayerNames[1])
                    Console.WriteLine("玩家B与A的姓名不能相同");
                else
                    Console.WriteLine("玩家B的姓名不能为空");

                Console.WriteLine("请重新输入");
                PlayerNames[1] = Console.ReadLine();
            }
            #endregion
            //玩家姓名输入好了，进行清屏
            Console.Clear();
            GameShow();
            Console.WriteLine("{0}的棋子用A表示，{1}的棋子用B表示", PlayerNames[0], PlayerNames[1]);
            //初始化地图
            InitailMap();
            DrawMap();
            //当玩家A和B没有一个人在终点的时候，两个玩家不停地玩游戏
            while (PlayerPos[0] < 99 && PlayerPos[1] < 99)
            {
                PlayGame(0);
                PlayGame(1);
            }//while
            Console.Clear();
            DrawMap();
            Console.WriteLine();
            Console.WriteLine();
            if(PlayerPos[0]==99)
                Console.WriteLine("恭喜玩家{0}胜利", PlayerNames[0]);
            if (PlayerPos[1] == 99)
                Console.WriteLine("恭喜玩家{0}胜利", PlayerNames[1]);

            Console.ReadKey();
        }
        /// <summary>
        /// 画游戏头
        /// </summary>
        public static void GameShow()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*********************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("********飞行棋*******");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*********************");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*********************");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("*********************");

        }
        /// <summary>
        /// 初始化地图
        /// </summary>
        public static void InitailMap()
        {
            int[] luckturn = { 6, 23, 40, 55, 69, 83 };
            for (int i = 0; i < luckturn.Length; i++)
            {
                Maps[luckturn[i]] = 1;
            }
            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };
            for (int i = 0; i < landMine.Length; i++)
            {
                Maps[landMine[i]] = 2;
            }
            int[] pause = { 9, 27, 60, 93 };
            for (int i = 0; i < pause.Length; i++)
            {
                Maps[pause[i]] = 3;
            }
            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                Maps[timeTunnel[i]] = 4;
            }
        }
        /// <summary>
        /// 画地图
        /// </summary>
        /// 
        public static void DrawMap()
        {
            //□◎☆▲卐　　　拿去复制
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("图例：幸运轮盘:◎   地雷:☆     暂停:▲      时空隧道:卐");

            //第一横行
            for (int i = 0; i < 30; i++)
            {
                //两个玩家坐标相同，并且都在地图上
                DrawPlayer(i);
            }

            //第一竖行
            for (int i = 30; i < 35; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < 29; j++)
                    Console.Write("  ");
                DrawPlayer(i);

            }
            //第二横行
            Console.WriteLine();
            for (int i = 64; i >= 35; i--)
            {
                DrawPlayer(i);
            }
            //第二竖行
            Console.WriteLine();
            for (int i = 65; i <= 69; i++)
            {
                DrawPlayer(i);
                Console.WriteLine();
            }
            //第三横行
            for (int i = 70; i <= 99; i++)
            {
                DrawPlayer(i);
            }
            Console.WriteLine();
        }


        /// <summary>
        /// 画玩家和地图相应的形状
        /// </summary>
        /// <param name="i">第i个地图块</param>
        public static void DrawPlayer(int i)
        {

            if (PlayerPos[0] == PlayerPos[1] && PlayerPos[1] == i)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("<>");
            }
            else if (PlayerPos[0] == i)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Ａ");
            }
            else if (PlayerPos[1] == i)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Ｂ");
            }
            else
            {
                switch (Maps[i])//□◎☆▲卐　　　拿去复制
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("□"); break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("◎"); break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("☆"); break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write("▲"); break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("卐"); break;
                }
            }
        }
        /// <summary>
        /// 玩游戏
        /// </summary>
        /// <param name="playerNumber">玩游戏的人</param>
        public static void PlayGame(int playerNumber)
        {
            Random r = new Random();
            Console.WriteLine("{0}开始按任意键开始掷骰子", PlayerNames[playerNumber]);
            Console.ReadKey(true);
            int tmep = r.Next(1, 7);
            Console.WriteLine("{0}掷骰子掷出了{1}", PlayerNames[playerNumber], tmep);
            PlayerPos[playerNumber] += tmep;
            ChangePos();
            Console.WriteLine("{0}按任意键行动", PlayerNames[playerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("{0}行动完了", PlayerNames[playerNumber]);
            Console.ReadKey(true);
            //玩家A可能踩到玩家B  方块  幸运转盘  地雷  暂停  时空隧道
            if (PlayerPos[playerNumber] == PlayerPos[1 - playerNumber])
            {
                Console.WriteLine("玩家{0}踩到玩家{1}，玩家{2}退6格", PlayerNames[playerNumber], PlayerNames[1 - playerNumber], PlayerNames[1 - playerNumber]);
                PlayerPos[1 - playerNumber] -= 6;
                Console.ReadKey(true);
            }
            else//猜到了关卡
            {
                switch (Maps[PlayerPos[playerNumber]])
                {
                    case 0:
                        Console.WriteLine("玩家{0}踩到了方块，安全", PlayerNames[playerNumber]);
                        Console.ReadKey(true);
                        break;
                    case 1:
                        Console.WriteLine("玩家{0}踩到了幸运轮盘，请选择1--交换位置  2--轰炸对方", PlayerNames[playerNumber]);
                        string input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine("玩家{0}选择和玩家{1}交换位置", PlayerNames[playerNumber], PlayerNames[1 - playerNumber]);
                                Console.ReadKey(true);
                                int temp = PlayerPos[playerNumber];
                                PlayerPos[playerNumber] = PlayerPos[1 - playerNumber];
                                PlayerPos[1 - playerNumber] = temp;
                                Console.WriteLine("交换完成！！按任意键继续");
                                Console.ReadKey(true);
                                break;
                            }
                            else if (input == "2")
                            {
                                Console.WriteLine("玩家{0}选择轰炸玩家{1},玩家{2}退6格", PlayerNames[playerNumber], PlayerNames[1 - playerNumber], PlayerNames[1 - playerNumber]);
                                Console.ReadKey(true);
                                PlayerPos[1 - playerNumber] -= 6;
                                ChangePos();
                                Console.WriteLine("玩家{0}退了6格", PlayerNames[1 - playerNumber]);
                                Console.ReadKey(true);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("重新输入1或者2");
                                input = Console.ReadLine();
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("玩家{0}踩到了地雷，退6格", PlayerNames[playerNumber]);
                        Console.ReadKey(true);
                        PlayerPos[playerNumber] -= 6;
                        ChangePos();
                        break;
                    case 3:
                        Console.WriteLine("玩家{0}踩到了暂停，暂停一回合", PlayerNames[playerNumber]);
                        Console.ReadKey(true);
                        Console.Clear();
                        DrawMap();
                        PlayGame(1- playerNumber);
                        break;
                    case 4:
                        Console.WriteLine("玩家{0}踩到了时空隧道，前进10格", PlayerNames[playerNumber]);
                        PlayerPos[playerNumber] += 10;
                        ChangePos();
                        Console.ReadKey(true);
                        break;
                }//switch

            }//else
            Console.Clear();
            DrawMap();

        }//playgame


        public static void ChangePos()
        {
            if(PlayerPos[0]<0)
            {
                PlayerPos[0] = 0;
            }
            if(PlayerPos[0]>99)
            {
                PlayerPos[0] = 99;
            }

            if(PlayerPos[1]<0)
            {
                PlayerPos[1] = 0;
            }
            if(PlayerPos[1]>99)
            {
                PlayerPos[1] = 99;
            }
        }
    }
}
