using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Runtime.InteropServices;
using WindowsInput; 
namespace RemoteController
{
    class ServerSocket
    {
        //即將從client接收data的變數
        const int HeartBeat = 9;
        const int MouseMode = 10;
        const int KeyboardMode = 11;
        const int PowerPointMode = 12;
        const int SystemMode = 13;

        public Socket listener;

        public static string data = null;
        public RemoteController gui;
        private string ip;
        public bool isRunning = false;
        public int mouseX, mouseY, offsetX, offsetY;
        public static int mode = MouseMode;

        //呼叫 Windows API 動態函示庫
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private const int MOUSEEVENTF_MOVE = 0x0001;
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        public const int MOUSEEVENTF_WHEEL = 0x0800;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff);
        const uint WM_SYSCOMMAND = 0x0112;
        const int SC_MONITORPOWER = 0xf170;

        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;


        public ServerSocket(RemoteController form, string localIp)
        {
            gui = form;
            ip = localIp;
        }

        public void MoveCursor(int newX, int newY)
        {
            //Cursor.Current = new Cursor(Cursor.Current.Handle);
            //Cursor.Position = new Point(newX, newY);
            //gui.setPos("X: " + newX + "   Y: " + newY);
        }
        public void MouseRun(Socket handler)
        {
            //接收客戶端byte資料寫入databuffer
            //Ecoding bytes to String型態變數data
            byte[] bytes = new Byte[1024];
            bytes = new byte[1024];
            int bytesRec = handler.Receive(bytes);
            data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

            try
            {
                //將data轉為String陣列型態command並已#分隔
                string[] command = data.Split('#');
                int code, x, y, moveX, moveY;

                //將command儲存的滑鼠指令個別存取到
                //code,x,y
                for (int i = 0; i < command.Length - 1; i += 3)
                {
                    code = Convert.ToInt16(command[i]);//滑鼠事件
                    x = Convert.ToInt16(command[i + 1]);//水平移動值
                    y = Convert.ToInt16(command[i + 2]);//垂直移動值

                    //Handle所有滑鼠控制
                    switch (code)   
                    {
                        case 0:
                            //點下螢幕
                            mouseX = Cursor.Position.X;
                            mouseY = Cursor.Position.Y;
                            offsetX = x;
                            offsetY = y;
                            break;
                        case 1:
                            //鼠標移動
                            moveX = mouseX + (x - offsetX) * 2;
                            moveY = mouseY + (y - offsetY) * 2;
                            //MoveCursor(moveX, moveY);
                            Cursor.Current = new Cursor(Cursor.Current.Handle);
                            Cursor.Position = new Point(moveX, moveY);
                            //MoveCursor(moveX, moveY);
                            Thread.Sleep(10);
                            break;
                        case 2:
                            //點左鍵
                            mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                            mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                            break;
                        case 3:
                            //點右鍵
                            mouse_event(MOUSEEVENTF_RIGHTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                            mouse_event(MOUSEEVENTF_RIGHTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                            break;
                        case 4:
                            //滾輪移動
                            mouseY = Cursor.Position.Y;
                            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, y, 0);
                            break;
                        case 10:
                            mode = 10;
                            break;
                        case KeyboardMode:
                            mode = KeyboardMode;
                            break;
                        case SystemMode:
                            mode = SystemMode;
                            break;
                        case PowerPointMode:
                            mode = PowerPointMode;
                            break;
                        case HeartBeat:
                            break;
                        default:
                            MessageBox.Show("Error:接收到錯誤指令");
                            break;
                    }
                    // Thread等待12毫秒
                    // 太短滑鼠會有明顯頓感

                }
            }
            catch (Exception e)
            {
                //Gotta catch em' all!
                MessageBox.Show(e.ToString());
            }
        }
        public void KeyboardControll(Socket handler)
        {
            try
            {
                byte[] bytes = new Byte[1024*10];
                bytes = new byte[1024 * 10];
                int byteRec = handler.Receive(bytes);
                data = Encoding.ASCII.GetString(bytes, 0, byteRec);

                string[] command = data.Split('#');
                int code; string word;
                code = Convert.ToInt16(command[0]);
                word = Convert.ToString(command[1]);

                switch (code)
                {
                    case 0:
                        Console.WriteLine(word);
                        System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcessesByName("notepad");
                        if (p.Length > 0)
                        {
                            SetForegroundWindow(p[0].MainWindowHandle);
                        }
                        SendKeys.SendWait(word);
                        break;
         
                    case 2:
                        keybd_event(8, 0, 0, 0);
                        break;
                    case 65:
                        keybd_event(65,0,0,0);
                        break;
                    case 6:
                        keybd_event(65, 0, 0, 0);
                        break;
                    case MouseMode:
                        mode = MouseMode;
                        break;
                    case PowerPointMode:
                        mode = PowerPointMode;
                        break;
                    case SystemMode:
                        mode = SystemMode;
                        break;
                    case HeartBeat:
                        break;
                }
            }
            catch (Exception e)
            {
                //Gotta catch em' all!
                MessageBox.Show(e.ToString());
            }
        }
        public void PowerPointControll(Socket handler)
        {
            try
            {
                byte[] bytes = new Byte[1024];
                bytes = new byte[1024];
                int byteRec = handler.Receive(bytes);
                data = Encoding.ASCII.GetString(bytes, 0, byteRec);

                string[] command = data.Split('#');
                int code, x, y;
                code = Convert.ToInt16(command[0]);
                switch (code)
                {
                    case MouseMode:
                        mode = MouseMode;
                        break;
                    case KeyboardMode:
                        mode = KeyboardMode;
                        break;
                    case SystemMode:
                        mode = SystemMode;
                        break;
                }
            }
            catch (Exception e)
            {
                //Gotta catch em' all!
                MessageBox.Show(e.ToString());
            }
        }
        public void SystemControll(Socket handler)
        {
            byte[] bytes = new Byte[1024];
            bytes = new byte[1024];
            int byteRec = handler.Receive(bytes);
            data = Encoding.ASCII.GetString(bytes, 0, byteRec);

            try
            {
                string[] command = data.Split('#');
                int code, x, y;
                code = Convert.ToInt16(command[0]);
                switch (code)
                {
                    case 0://Mute
                        keybd_event(173, 0, 0, 0);
                        break;
                    case 1://VolumeDown
                        keybd_event(174, 0, 0, 0);
                        break;
                    case 2:
                        keybd_event(175, 0, 0, 0);
                        break;
                    case 3:
                        Type shellType = Type.GetTypeFromProgID("Shell.Application");
                        object shellObject = System.Activator.CreateInstance(shellType);
                        shellType.InvokeMember("ToggleDesktop", System.Reflection.BindingFlags.InvokeMethod, null, shellObject, null);
                        break;
                    case 4:
                        //System.Diagnostics.Process.Start("C:\\WINDOWS\\system32\\rundll32.exe", "powrprof.dll,SetSuspendState");
                        SendMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, 2);
                        break;
                    case MouseMode:
                        mode = MouseMode;
                        break;
                    case KeyboardMode:
                        mode = KeyboardMode;
                        break;
                    case PowerPointMode:
                        mode = PowerPointMode;
                        break;
                    case HeartBeat:
                        break;
                    default:
                        MessageBox.Show("Error:接收到錯誤指令");
                        break;
                }
            }
            catch (Exception e)
            {
                //Gotta catch em' all!
                MessageBox.Show(e.ToString());
            }


        }

     
        public void StartListening()
        {
            //找尋本機IP位置>>ipAddress
            //並建立本機端點>>localEndPoint
            IPAddress ipAddress = IPAddress.Parse(ip);
            Console.WriteLine(ipAddress);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 8221);

            // 建立TCP/IP Socket
                listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);


            // 將Listener socket與本機端點連線
            // 並等待Client端連線
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);
                gui.setServerStatus("Started");
                Console.WriteLine("Server Started!");
                // Program is suspended while waiting for an incoming connection.
                Socket handler = listener.Accept();
            
                data = null;
                Console.WriteLine("Client Connected!");

                isRunning = true;
                while (isRunning)
                {
             
                    gui.setClientStatus("True");
                    //set timer
                    System.Timers.Timer aTimer = new System.Timers.Timer();
                    aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    aTimer.Interval = 1000;
                    aTimer.Enabled = true;

                    // 當isRunning=true時重複執行
                    while (isRunning)
                    {
                        //bool blockingState = listener.Blocking;
                        //try
                        //{
                        //    byte[] tmp = new byte[1];
                        //    handler.Send(tmp, 0, 0);
                        //    Console.WriteLine("Connected~~~~~~");
                        //}
                        //catch (SocketException f)
                        //{
                        //    // 10035 == WSAEWOULDBLOCK
                        //    if (f.NativeErrorCode.Equals(10035))
                        //        Console.WriteLine("Still Connected, but the Send would block");
                        //    else
                        //    {
                        //        Console.WriteLine("Disconnected: error code {0}!", f.NativeErrorCode);
                        //    }
                        //}
                        //ChangeMode(mode, handler);
                        switch (mode)
                        {
                            case MouseMode:
                                MouseRun(handler);
                                break;
                            case SystemMode:
                                SystemControll(handler);
                                break;
                            case KeyboardMode:
                                KeyboardControll(handler);
                                break;
                            case PowerPointMode:
                                PowerPointControll(handler);
                                break;
                        }

                    }
                    // 傳資料到客戶端
                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            gui.setServerStatus("Stopped");
            Console.WriteLine("\nSERVER STOPPED");
            Console.Read();

        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (listener.Poll(1, SelectMode.SelectRead) && listener.Available == 0)
            {
                Console.WriteLine("\nSOCKET IS DIED!!!!");
            }
            else
            {
                
                Console.WriteLine("\nSOCKET IS ALIVE~~~");
            }
        }
    }
}
