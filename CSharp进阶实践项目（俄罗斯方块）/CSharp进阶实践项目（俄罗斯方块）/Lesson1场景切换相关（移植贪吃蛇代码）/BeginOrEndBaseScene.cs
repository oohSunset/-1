using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp进阶实践项目_俄罗斯方块_
{
    abstract class BeginOrEndBaseScene : ISceneUpdate
    {
        protected int nowSelIndex = 0;
        protected string strTitle;
        protected string strOne;

        public abstract void EnterJDoSomthing();

        public void Update()
        {
            //开始和结束场景的 游戏逻辑
            //选择当前的选项 然后 监听 键盘输入 wsj
            Console.ForegroundColor = ConsoleColor.White;
            //显示标题
            Console.SetCursorPosition(Game.w / 2 - strTitle.Length, 5);
            Console.WriteLine(strTitle);
            //显示下方选项
            Console.SetCursorPosition(Game.w / 2 - strOne.Length, 8);
            Console.ForegroundColor = nowSelIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
            Console.WriteLine(strOne);
            Console.SetCursorPosition(Game.w / 2 - 4, 10);
            Console.ForegroundColor = nowSelIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
            Console.WriteLine("结束游戏");
            //检测输入
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    nowSelIndex = 0;
                    break;
                case ConsoleKey.S:
                    nowSelIndex = 1;
                    break;
                case ConsoleKey.J:
                    EnterJDoSomthing();
                    break;
            }
        }
    }
}
