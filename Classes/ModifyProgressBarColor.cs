using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TechnicianMap.Classes
{
	public static  class ModifyProgressBarColor
	{
		//https://stackoverflow.com/questions/778678/how-to-change-the-color-of-progressbar-in-c-sharp-net-3-5
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
		static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
		public static void SetState(this ProgressBar pBar, int state)
		{
			SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
		}
	}
}
