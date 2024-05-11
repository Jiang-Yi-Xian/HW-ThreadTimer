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
            //��l�ƭp�ɾ��A���w�p�ɾ��p�ɮɭn���檺��k�P�p�ɾ������j�ɶ�
            timer = new Timer(TimerCallback, null, Timeout.Infinite, 1000);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetBounds(0, 0, 400, 400);//�]�w�����j�p
        }
        private void button1_Click(object sender, EventArgs e)//����s���U
        {
            count = 100;
            timerThread = new Thread(new ThreadStart(StartTimer));//��l�ƭp�ɾ������
            timerThread.Start();//�Ұʭp�ɾ������
        }
        private void StartTimer()
        {
            timer.Change(0, 1000); //�ܧ�}�l���ɶ��M�p�ɾ����ɶ����j
        }
        private void TimerCallback(object state)//�ϥΩe���ӫ��wTimer�I�s����k
        {
            count--; // �C������

            if (label1.InvokeRequired)//�P�_�O�_�ݭn�i��������ާ@
            {
                label1.Invoke(new Action(() => label1.Text = count.ToString()));//�ϥ�Invoke��k�өe����UI������W�ާ@
            }
            else
            {
                label1.Text = count.ToString();
            }

            if (count <= 0) //�˼Ƨ���
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);//Timer.Change�ϥζ��j��Infinite�Ӱ���e��
                MessageBox.Show("�ɶ���I");//��ܰT��
            }
        }
        //�ϥΪ̤�����������ɥ����T�O�Ҧ�����������w�g����
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            timer?.Dispose();//�T�O�b�������ε{��������Timer�귽
            if (timerThread != null && timerThread.IsAlive)
            {
                timerThread.Join();//���ݰ��������
            }
        }
    }
}
