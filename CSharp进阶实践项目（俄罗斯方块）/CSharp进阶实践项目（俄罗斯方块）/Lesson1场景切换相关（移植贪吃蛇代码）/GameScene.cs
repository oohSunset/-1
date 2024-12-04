using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp进阶实践项目_俄罗斯方块_
{
    class GameScene : ISceneUpdate
    {
        public Map map;
        public BlockWorker blockWorker;

        //Thread inputThread;
        //关闭线程标识符
        //bool isRunning;

        public GameScene()
        {
            map = new Map(this);
            blockWorker = new BlockWorker();

            InputThread.Instance.inputEvent += CheckInputThread;

            //isRunning = true;
            //inputThread = new Thread(CheckInputThread);
            ////设置为后台线程，生命周期随主线程决定
            //inputThread.IsBackground = true;
            ////开启线程
            //inputThread.Start();
        }

        //关闭线程的方法
        public void StopThread()
        {
            InputThread.Instance.inputEvent -= CheckInputThread;

            //isRunning = false;
            //inputThread = null;
        }

        private void CheckInputThread()
        {
            //while (isRunning)
            //{
            //这只是 另一个输入线程 每帧会执行的逻辑 不需要自己来循环了
                if (Console.KeyAvailable)
                {
                    //为避免影响主线程 在输入后加锁
                    //锁搬砖工人blockWorker--因为它要改变光标位置
                    lock (blockWorker)
                    {
                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.LeftArrow:
                                //判断能不能变形
                                if (blockWorker.CanChange(E_Change_Type.Left, map))
                                    blockWorker.Change(E_Change_Type.Left);
                                break;
                            case ConsoleKey.RightArrow:
                                //判断能不能变形
                                if (blockWorker.CanChange(E_Change_Type.Right, map))
                                    blockWorker.Change(E_Change_Type.Right);
                                break;
                            case ConsoleKey.A:
                                if (blockWorker.CanMoveRL(E_Change_Type.Left, map))
                                    blockWorker.MoveRL(E_Change_Type.Left);
                                break;
                            case ConsoleKey.D:
                                if (blockWorker.CanMoveRL(E_Change_Type.Right, map))
                                    blockWorker.MoveRL(E_Change_Type.Right);
                                break;
                            case ConsoleKey.S:
                                //向下移动
                                if (blockWorker.CanMove(map))
                                    blockWorker.AutoMove();
                                break;
                        }
                    }
                    
                }
            //}
        }

        public void Update()
        {
            //锁里面不能包含休眠 不然还是会影响到所有进程
            lock (blockWorker)
            {
                //地图绘制
                map.Draw();
                //搬运工
                blockWorker.Draw();
                //自动向下移动
                if (blockWorker.CanMove(map))
                    blockWorker.AutoMove();
            }

            //线程控制程序慢一些
            Thread.Sleep(200);

        }

    }
}
