using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time_Keeper
{
    public class NoSystemMenuException : System.Exception { }

    public class CustomSystemMenu
    {
        // Values taken from MSDN
        public enum ItemFlags
        {
            MF_UNCHECKED = 0x000, // Not checked
            MF_STRING = 0x000, // Contains a string as a label
            MF_DISABLED = 0x002, // Is disabled
            MF_GRAYED = 0x001, // Is grayed
            MF_CHECKED = 0x008, // Is checked
            MF_POPUP = 0x010, // Is a popup menu. Pass the menu handle of the popup menu into the ID parameter
            MF_BARBREAK = 0x020, // Is bar break
            MF_BREAK = 0x040, // Is a break
            MF_BYPOSITION = 0x400, // Is identified by the position
            MF_BYCOMMAND = 0x000, // Is identified by its ID
            MF_SEPARATOR = 0x800 // Is a separator (string and ID parameters are ignored)
        }

        public enum WindowMessages
        {
            WM_SYSCOMMAND = 0x0112
        }

        public class SystemMenu
        {
            [DllImport("user32.dll", EntryPoint = "GetSystemMenu", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
            private static extern IntPtr apiGetSystemMenu(IntPtr hWnd, int bReset);

            [DllImport("user32.dll", EntryPoint = "AppendMenuW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
            private static extern int apiAppendMenu(IntPtr hWnd, int flags, int newID, string item);

            [DllImport("user32.dll", EntryPoint = "InsertMenuW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
            private static extern int apiInsertMenu(IntPtr hWnd, int position, int flags, int newID, string item);

            // Handle to the system menu
            private IntPtr m_SysMenu = IntPtr.Zero;

            public SystemMenu() { }

            // Insert a separator at the given position index starting at 0
            public bool InsertSeparator(int pos)
            {
                return (InsertMenu(pos, ItemFlags.MF_SEPARATOR | ItemFlags.MF_BYPOSITION, 0, string.Empty));
            }

            // Simplified InsertMenu()
            public bool InsertMenu(int pos, int ID, string item)
            {
                return (InsertMenu(pos, ItemFlags.MF_BYPOSITION | ItemFlags.MF_STRING, ID, item));
            }

            // Insert a menu at the given position, the value of position depends on the value of flags
            public bool InsertMenu(int pos, ItemFlags flags, int ID, string item)
            {
                return (apiInsertMenu(m_SysMenu, pos, (int)flags, ID, item) == 0);
            }

            // Appends a separator
            public bool AppendSeparator()
            {
                return AppendMenu(0, string.Empty, ItemFlags.MF_SEPARATOR);
            }

            // This uses the ItemFlags.MF_String as a default value
            public bool AppendMenu(int ID, string item)
            {
                return AppendMenu(ID, item, ItemFlags.MF_STRING);
            }

            // Superseded function
            public bool AppendMenu(int ID, string item, ItemFlags flags)
            {
                return (apiAppendMenu(m_SysMenu, (int)flags, ID, item) == 0);
            }

            // Retrieves a new object from a Form object
            public static SystemMenu fromForm(Form form)
            {
                SystemMenu cSystemMenu = new SystemMenu();

                cSystemMenu.m_SysMenu = apiGetSystemMenu(form.Handle, 0);
                if (cSystemMenu.m_SysMenu == IntPtr.Zero)
                {
                    throw new NoSystemMenuException();
                }

                return cSystemMenu;
            }

            // Reset's the window menu to it's default
            public static void ResetSystemMenu(Form form)
            {
                apiGetSystemMenu(form.Handle, 1);
            }

            // Checks if an ID for a new system menu item is OK or not
            public static bool VerifyItemID(int ID)
            {
                return (ID < 0xF000 && ID > 0);
            }
        }
    }
}
