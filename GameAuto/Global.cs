using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace GameAuto
{
    public class Global
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        static int g_sleep = 0;

        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        public enum SystemMetric
        {
            SM_CXSCREEN = 0,  // 0x00
            SM_CYSCREEN = 1,  // 0x01
            SM_CXVSCROLL = 2,  // 0x02
            SM_CYHSCROLL = 3,  // 0x03
            SM_REMOTECONTROL = 0x2001, // 0x2001
        }
        
        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(SystemMetric smIndex);
        public static int BALL_POS;
        public static void MouseEventTo(Point pt1, Point pt2)
        {
            int x = pt1.X; int y = pt1.Y;
            int cx = GetSystemMetrics(SystemMetric.SM_CXSCREEN);
            int cy = GetSystemMetrics(SystemMetric.SM_CYSCREEN);

            int posX = 2 * 32768 * x / cx;
            int posY = 2 * 32768 * y / cy;
            mouse_event((int)MouseEventFlags.ABSOLUTE | (int)MouseEventFlags.MOVE, posX, posY, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            //if (Global.g_sleep == 1)
            //  Thread.Sleep(200);
            //Thread.Sleep(500);
            x = pt2.X;
            y = pt2.Y;
            cx = GetSystemMetrics(SystemMetric.SM_CXSCREEN);
            cy = GetSystemMetrics(SystemMetric.SM_CYSCREEN);

            posX = 2 * 32768 * x / cx;
            posY = 2 * 32768 * y / cy;
            mouse_event((int)MouseEventFlags.ABSOLUTE | (int)MouseEventFlags.MOVE, posX, posY, 0, 0);
            //Thread.Sleep(1);
            //Thread.Sleep(500);
            x = pt2.X;
            y = pt2.Y;
            cx = GetSystemMetrics(SystemMetric.SM_CXSCREEN);
            cy = GetSystemMetrics(SystemMetric.SM_CYSCREEN);
            
            posX = 2 * 32768 * x / cx;
            posY = 2 * 32768 * y / cy;
            mouse_event((int)MouseEventFlags.ABSOLUTE | (int)MouseEventFlags.MOVE, posX, posY, 0, 0);
            
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            //Thread.Sleep(500);
            /*if(Global.g_sleep == 1)
            {
                Global.g_sleep = 0;
                Thread.Sleep(200);
            }
            else
                Global.g_sleep = 1;*/

            Global.BALL_POS = -1;
        }
        public static void MouseDownTo(Point pt)
        {
            int x = pt.X; int y = pt.Y;
            int cx = GetSystemMetrics(SystemMetric.SM_CXSCREEN);
            int cy = GetSystemMetrics(SystemMetric.SM_CYSCREEN);

            int posX = 2 * 32768 * x / cx;
            int posY = 2 * 32768 * y / cy;
            mouse_event((int)MouseEventFlags.ABSOLUTE | (int)MouseEventFlags.MOVE, posX, posY, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            //Thread.Sleep(500);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            //Thread.Sleep(500);
        }

        public static void MouseMoveToAndUp(Point pt)
        {
            int x = pt.X; int y = pt.Y;
            int cx = GetSystemMetrics(SystemMetric.SM_CXSCREEN);
            int cy = GetSystemMetrics(SystemMetric.SM_CYSCREEN);

            int posX = 2 * 32768 * x / cx;
            int posY = 2 * 32768 * y / cy;
            mouse_event((int)MouseEventFlags.ABSOLUTE | (int)MouseEventFlags.MOVE, posX, posY, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            Thread.Sleep(500);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            Thread.Sleep(500);
        }

        public static void MouseMoveTo(Point pt)
        {
            int x = pt.X; int y = pt.Y;
            int cx = GetSystemMetrics(SystemMetric.SM_CXSCREEN);
            int cy = GetSystemMetrics(SystemMetric.SM_CYSCREEN);

            int posX = 2 * 32768 * x / cx;
            int posY = 2 * 32768 * y / cy;
            mouse_event((int)MouseEventFlags.ABSOLUTE | (int)MouseEventFlags.MOVE, posX, posY, 0, 0);
        }
        public static void ALT_F4KeyEnter()
        {
            /*System.Threading.Thread.Sleep(5000);
            keybd_event(0x12, 0xb8, 0, 0);//ALT
            keybd_event(0x73, 0, 0, 0);//F4
            keybd_event(0x73, 0, 2, 0);//F4
            keybd_event(0x12, 0xb8, 2, 0);//ALT
            System.Threading.Thread.Sleep(1000);*/
        }
        public static void F11_ZoomKeyEnter()
        {
            System.Threading.Thread.Sleep(5000);
            keybd_event(0x7A, 0, 0, 0);
            keybd_event(0x7A, 0, 2, 0);
            System.Threading.Thread.Sleep(1000);
            keybd_event(0x11, 0, 0, 0);
            keybd_event(0x30, 0, 0, 0);
            keybd_event(0x11, 0, 2, 0);
            System.Threading.Thread.Sleep(1000);
            keybd_event(0x11, 0, 0, 0);
            keybd_event(0x60, 0, 0, 0);
            keybd_event(0x11, 0, 2, 0);
            System.Threading.Thread.Sleep(1000);
            keybd_event(0x11, 0, 0, 0);
            keybd_event(0x6D, 0, 0, 0);
            keybd_event(0x6D, 0, 2, 0);
            keybd_event(0x6D, 0, 0, 0);
            keybd_event(0x6D, 0, 2, 0);
            keybd_event(0x11, 0, 2, 0);
            System.Threading.Thread.Sleep(1000);
        }
        public static void F11KeyEnter()
        {
            System.Threading.Thread.Sleep(5000);
            keybd_event(0x7A, 0, 0, 0);
            keybd_event(0x7A, 0, 2, 0);
            System.Threading.Thread.Sleep(1000);
        }
        /*public static void F11KeyEnter()
        {
            System.Threading.Thread.Sleep(5000);
            keybd_event(0x7A, 0, 0, 0);
            keybd_event(0x7A, 0, 2, 0);
            System.Threading.Thread.Sleep(1000);
            keybd_event(0x11, 0, 0, 0);
            keybd_event(0x30, 0, 0, 0);
            keybd_event(0x11, 0, 2, 0);
            System.Threading.Thread.Sleep(1000);
            keybd_event(0x11, 0, 0, 0);
            keybd_event(0x6D, 0, 0, 0);
            keybd_event(0x6D, 0, 2, 0);
            keybd_event(0x6D, 0, 0, 0);
            keybd_event(0x6D, 0, 2, 0);
            keybd_event(0x11, 0, 2, 0);
            System.Threading.Thread.Sleep(1000);
        }*/
        public static void StringKeyEnter(string line)
        {
            /*foreach (char c in line) // Repeat one character of the line.
            {
                ConsoleKey ck;
                ck = (ConsoleKey)c;
            }
                return;*/
            foreach (char c in line) // Repeat one character of the line.
            {
                System.Threading.Thread.Sleep(500);
                ConsoleKey ck;
                Enum.TryParse<ConsoleKey>(c.ToString(), true, out ck);
                if (c >= 0x41 && c <= 0x5a)
                {
                    keybd_event(0xA0, 0, 0, 0);
                    Enum.TryParse<ConsoleKey>(c.ToString(), true, out ck);
                }
                else if(c >= 0x61 && c <= 0x7a)
                {
                    Enum.TryParse<ConsoleKey>(c.ToString(), true, out ck);
                }
                else
                {
                    ck = (ConsoleKey)c;
                    switch (ck)
                    {
                        case (ConsoleKey)0x40:
                            keybd_event(0xA0, 0, 0, 0);
                            ck = (ConsoleKey)0x32;
                            break;
                        case (ConsoleKey)0x5e:
                            keybd_event(0xA0, 0, 0, 0);
                            ck = (ConsoleKey)0x36;
                            break;
                        case (ConsoleKey)0x2a:
                            keybd_event(0xA0, 0, 0, 0);
                            ck = (ConsoleKey)0x38;
                            break;
                        case (ConsoleKey)0x21://1
                        case (ConsoleKey)0x23://3
                        case (ConsoleKey)0x24://4
                        case (ConsoleKey)0x25://5
                            keybd_event(0xA0, 0, 0, 0);
                            ck = ck + 0x10;
                            break;
                        case (ConsoleKey)0x26://6
                        case (ConsoleKey)0x27://7
                        case (ConsoleKey)0x28://8
                            keybd_event(0xA0, 0, 0, 0);
                            ck = ck + 0x11;
                            break;                        
                        case (ConsoleKey)0x29://9
                            keybd_event(0xA0, 0, 0, 0);
                            ck = (ConsoleKey)0x30;
                            break;
                    }
                }
                if (ck != 0) // If ck is not abnormal English character.
                {
                    keybd_event((byte)ck, 0, 0, 0); // Press the key
                    keybd_event(0xA0, 0, 2, 0);
                }
            }
        }

        /*public static void SendString(string inputStr)
        {
            var hWnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            WinAPI.SetForegroundWindow(hWnd);
            List<WinAPI.INPUT> keyList = new List<WinAPI.INPUT>();
            foreach (short c in inputStr)
            {
                switch (c)
                {
                    case 8: // Translate \t to VK_TAB
                        {
                            WinAPI.INPUT keyDown = new WinAPI.INPUT();
                            keyDown.type = 1; //Keyboard
                            keyDown.union.keyboardInput.wVk = (short)WinAPI.WindowsVirtualKey.VK_TAB;
                            keyDown.union.keyboardInput.dwFlags = 0;
                            keyDown.union.keyboardInput.wScan = 0; //use VirtualKey
                            keyList.Add(keyDown);
                            WinAPI.INPUT keyUp = new WinAPI.INPUT();
                            keyUp.type = 1; //Keyboard
                            keyUp.union.keyboardInput.wVk = (short)WinAPI.WindowsVirtualKey.VK_TAB;
                            keyUp.union.keyboardInput.dwFlags = 0x0002;
                            keyUp.union.keyboardInput.wScan = 0; //use VirtualKey
                            keyList.Add(keyUp);
                        }
                        break;
                    case 10: // Translate \n to VK_RETURN
                        {
                            WinAPI.INPUT keyDown = new WinAPI.INPUT();
                            keyDown.type = 1; //Keyboard
                            keyDown.union.keyboardInput.wVk = (short)WinAPI.WindowsVirtualKey.VK_RETURN;
                            keyDown.union.keyboardInput.dwFlags = 0;
                            keyDown.union.keyboardInput.wScan = 0; //use VirtualKey
                            keyList.Add(keyDown);
                            WinAPI.INPUT keyUp = new WinAPI.INPUT();
                            keyUp.type = 1; //Keyboard
                            keyUp.union.keyboardInput.wVk = (short)WinAPI.WindowsVirtualKey.VK_RETURN;
                            keyUp.union.keyboardInput.dwFlags = 0x0002;
                            keyUp.union.keyboardInput.wScan = 0; //use VirtualKey
                            keyList.Add(keyUp);
                        }
                        break;
                    default:
                        {
                            WinAPI.INPUT keyDown = new WinAPI.INPUT();
                            keyDown.type = 1; //Keyboard
                            keyDown.union.keyboardInput.wVk = 0; //Use unicode
                            keyDown.union.keyboardInput.dwFlags = 0x0004; //Unicode Key Down
                            keyDown.union.keyboardInput.wScan = c;
                            keyList.Add(keyDown);
                            WinAPI.INPUT keyUp = new WinAPI.INPUT();
                            keyUp.type = 1; //Keyboard
                            keyUp.union.keyboardInput.wVk = 0; //Use unicode
                            keyUp.union.keyboardInput.dwFlags = 0x0004 | 0x0002; //Unicode Key Up
                            keyUp.union.keyboardInput.wScan = c;
                            keyList.Add(keyUp);
                        }
                        break;
                }
            }
            WinAPI.SendInput((uint)keyList.Count, keyList.ToArray(), Marshal.SizeOf(typeof(WinAPI.INPUT)));
        }*/
        /*public static void SendKeyPress(KeyCode keyCode)
        {
            INPUT input = new INPUT
            {
                Type = 1
            };
            input.Data.Keyboard = new KEYBDINPUT()
            {
                Vk = (ushort)keyCode,
                Scan = 0,
                Flags = 0,
                Time = 0,
                ExtraInfo = IntPtr.Zero,
            };

            INPUT input2 = new INPUT
            {
                Type = 1
            };
            input2.Data.Keyboard = new KEYBDINPUT()
            {
                Vk = (ushort)keyCode,
                Scan = 0,
                Flags = 2,
                Time = 0,
                ExtraInfo = IntPtr.Zero
            };
            INPUT[] inputs = new INPUT[] { input, input2 };
            if (SendInput(2, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
                throw new Exception();
        }*/

        public static Rectangle g_rcROI = Rectangle.Empty;
        public static bool license_Verify = false;
        public struct MOVEMENT
        {
            public static readonly MOVEMENT Empty;

            public int nX;
            public int nY;
            public int nD;
            public int nScore;

            public static bool operator ==(MOVEMENT left, MOVEMENT right)
            {
                if (left.nX == right.nX && left.nY == right.nY && left.nD == right.nD && left.nScore == right.nScore)
                    return true;

                return false;
            }

            public static bool operator !=(MOVEMENT left, MOVEMENT right)
            {
                return !(left == right);
            }
        }

        public static MOVEMENT g_moveStep1 = MOVEMENT.Empty;
        public static MOVEMENT g_moveStep2 = MOVEMENT.Empty;

        public static bool g_bAssistantMode = false;
        public static MainWindow g_MainWindow = null;

        public enum CHARACTER_TYPE
        {
            CHAR_NONE = 8,
            CHAR_RED = 1,
            CHAR_BLUE = 2,
            CHAR_YELLOW = 3,
            CHAR_PURPLE = 4,
            CHAR_GREEN = 5,
            CHAR_DARK = 6,
            CHAR_TEA = 7,
            CHAR_KING = 9,

            CHAR_OCTOPUS = 9,
            CHAR_OCTOPUS_WATER = 10,
            CHAR_WOOD = 11,
        };
        public static bool g_selectGame = false;
        public static bool g_LoginGame = false;
        public static bool g_SignInGame = false;
        public static bool isStandardRegion = false;

        public static int g_deviationX = -420;
        public static int g_deviationY = -160;

        public static int g_standardX = 721;
        public static int g_standardY = 460;

        public static bool g_Box = false;
        /*public static int DEF_WND_W = 831;
        public static int DEF_WND_H = 658;
*/
        public static int DEF_WND_W = 540;
        public static int DEF_WND_H = 540;
        public static uint PelsWidth = 1920;
        public static uint PelsHeight = 1080;
        //public static int DEF_REMAIN_X = 10 + 16;
        //public static int DEF_REMAIN_Y = 39 + 13;
        //public static int DEF_REMAIN_W = 164;
        //public static int DEF_REMAIN_H = 170;

        public static int DEF_MARKS_X = 15;
        public static int DEF_MARKS_Y = 204;
        public static int DEF_MARKS_W = 189;
        public static int DEF_MARKS_H = 69;

        //public static int DEF_DUCK_X = 50 + 10;
        //public static int DEF_DUCK_Y = 256 + 39;
        //public static int DEF_DUCK_W = 131;
        //public static int DEF_DUCK_H = 131;

        public static int DEF_MAIN_BOARD_X = 165;
        public static int DEF_MAIN_BOARD_Y = 110;
        public static int DEF_MAIN_BOARD_W = 365;
        public static int DEF_MAIN_BOARD_H = 365;

        public static int DEF_ITEM_W = 45;
        public static int DEF_ITEM_H = 45;
        
        public static bool GetRatioCalcedValues(int nWid, int nHei, ref int X, ref int Y)
        {
            if (nWid * nHei * X * Y == 0)
                return false;

            float fRatioX = (float)nWid / (float)DEF_WND_W;
            float fRatioY = (float)nHei / (float)DEF_WND_H;

            X = (int)(X * fRatioX);
            Y = (int)(Y * fRatioY);

            return true;
        }

        public static bool IsProcessing = false;
        public static int g_LandCount = 0;
    }
}
