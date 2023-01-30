using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DofusChasseForms
{
    public class WindowsGesture
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);
        [DllImport("user32.dll")]
        public static extern int ShowWindow(IntPtr hWnd, uint Msg);
        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        public IntPtr MakeLParam(int x, int y) => (IntPtr)((y << 16) | (x & 0xFFFF));


        public const Int32 WM_LBUTTONDOWN = 0x0201;
        public const Int32 WM_LBUTTONUP = 0x0202;
        public const Int32 WM_RBUTTONDOWN = 0x0204;
        public const Int32 WM_RBUTTONUP = 0x0205;

        public const Int32 WM_MOUSEHOVER = 0x02A1;


        public const UInt32 WM_KEYDOWN = 0x0100;
        public const UInt32 WM_KEYUP = 0x0101;

        public void PerformLeftClick(IntPtr windowHandle, Point point)
        {
            var pointPtr = MakeLParam(point.X, point.Y);
            SendMessage(windowHandle, WM_LBUTTONDOWN, IntPtr.Zero, pointPtr);
            SendMessage(windowHandle, WM_LBUTTONUP, IntPtr.Zero, pointPtr);
        }

        public void PerformRightClick(IntPtr windowHandle, Point point)
        {
            var pointPtr = MakeLParam(point.X, point.Y);
            SendMessage(windowHandle, WM_RBUTTONDOWN, IntPtr.Zero, pointPtr);
            SendMessage(windowHandle, WM_RBUTTONUP, IntPtr.Zero, pointPtr);
        }

        public async Task WriteString(IntPtr windowHandle, string indice)
        {
            //CAS PARTICULIER REPORTED
            if (indice == "Casque à comes")
                indice = "Casque à cornes";
            if (indice == "Tissu à carreaux noi")
                indice = "Tissu à carreaux noué";
            if (indice == "Caim")
                indice = "Cairn";
            if (indice.Contains("Piache"))
            {
                indice = indice.Replace("Piache", "Pioche");
            }
            //END CAS PATICULIER

            indice = indice.ToLower().Replace("œ", "oe");
            foreach (var carac in indice)
            {
                await Task.Delay(10);
                switch (carac)
                {
                    case '/':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.Divide, (IntPtr)0x0);
                        break;
                    case '-':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.Subtract, (IntPtr)0x0);
                        break;
                    case '\'':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.D4, (IntPtr)0x0);
                        break;
                    case ' ':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.Space, (IntPtr)0x0);
                        break;
                    case 'a':
                    case 'ä':
                    case 'â':
                    case 'à':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.A, (IntPtr)0x0);
                        break;
                    case 'b':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.B, (IntPtr)0x0);
                        break;
                    case 'c':
                    case 'ç':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.C, (IntPtr)0x0);
                        break;
                    case 'd':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.D, (IntPtr)0x0);
                        break;
                    case 'e':
                    case 'é':
                    case 'è':
                    case 'ê':
                    case 'ë':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.E, (IntPtr)0x0);
                        break;
                    case 'f':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.F, (IntPtr)0x0);
                        break;
                    case 'g':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.G, (IntPtr)0x0);
                        break;
                    case 'h':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.H, (IntPtr)0x0);
                        break;
                    case 'i':
                    case 'ï':
                    case 'î':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.I, (IntPtr)0x0);
                        break;
                    case 'j':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.J, (IntPtr)0x0);
                        break;
                    case 'k':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.K, (IntPtr)0x0);
                        break;
                    case 'l':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.L, (IntPtr)0x0);
                        break;
                    case 'm':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.M, (IntPtr)0x0);
                        break;
                    case 'n':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.N, (IntPtr)0x0);
                        break;
                    case 'o':
                    case 'ö':
                    case 'ô':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.O, (IntPtr)0x0);
                        break;
                    case 'p':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.P, (IntPtr)0x0);
                        break;
                    case 'q':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.Q, (IntPtr)0x0);
                        break;
                    case 'r':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.R, (IntPtr)0x0);
                        break;
                    case 's':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.S, (IntPtr)0x0);
                        break;
                    case 't':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.T, (IntPtr)0x0);
                        break;
                    case 'u':
                    case 'ù':
                    case 'û':
                    case 'ü':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.U, (IntPtr)0x0);
                        break;
                    case 'v':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.V, (IntPtr)0x0);
                        break;
                    case 'w':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.W, (IntPtr)0x0);
                        break;
                    case 'x':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.X, (IntPtr)0x0);
                        break;
                    case 'y':
                    case 'ÿ':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.Y, (IntPtr)0x0);
                        break;
                    case 'z':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.Z, (IntPtr)0x0);
                        break;
                    case '0':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.NumPad0, (IntPtr)0x0);
                        break;
                    case '1':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.NumPad1, (IntPtr)0x0);
                        break;
                    case '2':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.NumPad2, (IntPtr)0x0);
                        break;
                    case '3':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.NumPad3, (IntPtr)0x0);
                        break;
                    case '4':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.NumPad4, (IntPtr)0x0);
                        break;
                    case '5':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.NumPad5, (IntPtr)0x0);
                        break;
                    case '6':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.NumPad6, (IntPtr)0x0);
                        break;
                    case '7':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.NumPad7, (IntPtr)0x0);
                        break;
                    case '8':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.NumPad8, (IntPtr)0x0);
                        break;
                    case '9':
                        PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.NumPad9, (IntPtr)0x0);
                        break;
                }
            }
        }
    }
}
