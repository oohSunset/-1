using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.Lesson1;
using 贪吃蛇.Lesson4;
using 贪吃蛇.Lesson5;
using 贪吃蛇.Lesson6;

namespace 贪吃蛇.Lesson2
{
    class GameScene : ISceneUpdate
    {
        Map map;
        Snake snake;
        //用于降低速度
        int updateIndex = 0;
        Food food;

        public GameScene()
        {
            map = new Map();
            snake = new Snake(38, 9);
            food = new Food(snake);
        }

        public void Update()
        {
            //降低帧率
            if (updateIndex == 5555)
            {
                //地图绘制
                map.Draw();
                //食物绘制
                food.Draw();

                //蛇移动
                snake.Move();
                //蛇身体绘制
                snake.Draw();

                //检测是否撞墙
                if (snake.CheckEnd(map))
                {
                    //结束逻辑
                    Game.ChangeScene(E_SceneType.End);
                }

                //检测是否吃到食物（写这里的好处是不用去擦除食物原位置，因为蛇会擦除）
                snake.CheckEatFood(food);

                updateIndex = 0;
            }
            ++updateIndex;

            //在控制台中 检测玩家输入 让程序不被检测卡住
            //判断 有没有键盘输入 如果有 才为true
            if (Console.KeyAvailable)
            {
                //检测输入输出 不能再 间隔帧里面去处理 应该每次都检测 这样才准确
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        snake.ChangeDir(E_MoveDir.Up);
                        break;
                    case ConsoleKey.S:
                        snake.ChangeDir(E_MoveDir.Down);
                        break;
                    case ConsoleKey.A:
                        snake.ChangeDir(E_MoveDir.Left);
                        break;
                    case ConsoleKey.D:
                        snake.ChangeDir(E_MoveDir.Right);
                        break;
                }
            }
        }

    }
}
