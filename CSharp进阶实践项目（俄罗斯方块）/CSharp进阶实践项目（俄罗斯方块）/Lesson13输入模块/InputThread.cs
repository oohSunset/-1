using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp进阶实践项目_俄罗斯方块_
{

    //将输入模块单独建立成单例模式的对象
    class InputThread
    {
        //线程成员变量
        Thread inputThread;

        //输入检测事件
        public event Action inputEvent;

        private static InputThread instance = new InputThread();

        public static InputThread Instance
        {
            get
            {
                return instance;
            }
        }

        private InputThread()
        {
            inputThread = new Thread(InputCheck);
            inputThread.IsBackground = true;
            inputThread.Start();
        }

        private void InputCheck()
        {
            while (true)
            {
                //语法糖：如果这句代码为空的话就不会执行，如果不为空就会执行
                inputEvent?.Invoke();
            }
        }

    }
}
