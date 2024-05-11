using Timer = System.Threading.Timer;

namespace HW_ThreadTimer
{
    public partial class Form1 : Form
    {
        private Timer timer;
        private Thread timerThread;
        private int count;
        public Form1()
        {
            InitializeComponent();
            //初始化計時器，指定計時器計時時要執行的方法與計時器的間隔時間
            timer = new Timer(TimerCallback, null, Timeout.Infinite, 1000);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetBounds(0, 0, 400, 400);//設定視窗大小
        }
        private void button1_Click(object sender, EventArgs e)//當按鈕按下
        {
            count = 100;
            timerThread = new Thread(new ThreadStart(StartTimer));//初始化計時器執行緒
            timerThread.Start();//啟動計時器執行緒
        }
        private void StartTimer()
        {
            timer.Change(0, 1000); //變更開始的時間和計時器的時間間隔
        }
        private void TimerCallback(object state)//使用委派來指定Timer呼叫的方法
        {
            count--; // 每秒遞減秒數

            if (label1.InvokeRequired)//判斷是否需要進行跨執行緒操作
            {
                label1.Invoke(new Action(() => label1.Text = count.ToString()));//使用Invoke方法來委派到UI執行緒上操作
            }
            else
            {
                label1.Text = count.ToString();
            }

            if (count <= 0) //倒數完成
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);//Timer.Change使用間隔值Infinite來停止委派
                MessageBox.Show("時間到！");//顯示訊息
            }
        }
        //使用者手動關閉視窗時必須確保所有的執行緒都已經結束
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            timer?.Dispose();//確保在關閉應用程式時釋放Timer資源
            if (timerThread != null && timerThread.IsAlive)
            {
                timerThread.Join();//等待執行緒結束
            }
        }
    }
}
