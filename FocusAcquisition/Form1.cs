using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace FocusAcquisition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            timer1.Start();
        }

        /// <summary>
        /// 获取当前激活窗口
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();//获取当前激活窗口  


        /// <summary>
        /// 根据句柄获得标题
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpString"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
        [DllImport("user32", SetLastError = true)]
        public static extern int GetWindowText(
        IntPtr hWnd, //窗口句柄  
        StringBuilder lpString, //标题  
        int nMaxCount  //最大值  
        );


        /// <summary>
        /// 根据句柄获得类名
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpString"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int GetClassName(
            IntPtr hWnd, //句柄  
            StringBuilder lpString, //类名  
            int nMaxCount //最大值  
        );

        //根据句柄获得进程PID
        [DllImport("user32", EntryPoint = "GetWindowThreadProcessId")]
        private static extern int GetWindowThreadProcessId(
            IntPtr hwnd,
            out int pid
            );


        StringBuilder title;

        StringBuilder className;

        int pID;

        IntPtr myPtr;

        private void timer1_Tick(object sender, EventArgs e)
        {

            myPtr = GetForegroundWindow();

            // 窗口标题  
            title = new StringBuilder(255);
            GetWindowText(myPtr, title, title.Capacity);

            // 窗口类名  
            className = new StringBuilder(256);
            GetClassName(myPtr, className, className.Capacity);



            GetWindowThreadProcessId(myPtr, out pID);

            label1.Text = myPtr.ToString() + "\n" + title.ToString() + "\n" + className.ToString() + "\n" + pID;

        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_TextChanged(object sender, EventArgs e)
        {
            listView1.Items.Insert(0, new ListViewItem(new string[]{
            myPtr.ToString(),
             title.ToString(),
             className.ToString(),
             pID.ToString(),
             DateTime.Now.ToString("yy-MM-dd HH:mm:ss"),
            }));
        }
    }
}
