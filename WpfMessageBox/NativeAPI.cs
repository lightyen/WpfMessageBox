using System;
using System.Runtime.InteropServices;


namespace WpfMessageBox {

    //https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-messagebox

    public static class NativeAPI {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern MessageBoxReturn MessageBox(IntPtr hWnd, string text, string caption, MessageBoxFlag type);
    }

    [Flags]
    public enum MessageBoxFlag {
        /// <summary>
        /// The message box contains one push button: OK. This is the default. 
        /// </summary>
        Ok = 0x00000000,
        OkCancel = 0x00000001,
        AbortRetryIgnore = 0x00000002,
        YesNoCancel = 0x00000003,
        YesNo = 0x00000004,
        RetryCancel = 0x00000005,
        CancelTryContinue = 0x00000006,
        Help = 0x00004000,
        IconExclamation = 0x00000030,
        IconWarning = 0x00000030,
        IconImformation = 0x00000040,
        IconQuestion = 0x00000020,
        IconStop = 0x00000010,
        IconError = 0x00000010,
        IconHand = 0x00000010,
        // 還有更多選項, 但是我懶了...
    }

    [Flags]
    public enum MessageBoxReturn {
        Abort = 3,
        Cancel = 2,
        Continue = 11,
        Ignore = 5,
        No = 7,
        Ok = 1,
        Retry = 4,
        TryAgain = 10,
        Yes = 6,
    }
}
