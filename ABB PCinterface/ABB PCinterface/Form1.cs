using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using ABB.Robotics;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;//如果没有在这个就没有NetworkScanner
using ABB.Robotics.Controllers.ConfigurationDomain;//ConfigurationDatabase 类
using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Controllers.IOSystemDomain;
using ABB.Robotics.Controllers.EventLogDomain;
using ABB.Robotics.Controllers.MotionDomain;

namespace ABB_PCinterface
{
    public partial class Form1 : Form
    {
        Controller RBTCtroller = null;
        private BackgroundWorker UIRenew= null;//BackgroundWorker
        private delegate void UIshowDel(string s);
        private delegate void UIRefDel();
        private UIshowDel mydel;
        private UIRefDel mydel1;
        Thread UIRefreshThread;
        RobTarget p;
        JointTarget J;



        public Form1()
        {
            InitializeComponent();
        }
        //连接函数
           private void btnConnect_Click(object sender, EventArgs e)
        {
            ListViewItem itemSelected = listView1.SelectedItems[0];
            ControllerInfo ctrlinfo = (ControllerInfo)itemSelected.Tag;
            if (ctrlinfo.Availability==Availability.Available)
            {
                if (ctrlinfo.IsVirtual==true)
                {   
                    RBTCtroller = ControllerFactory.CreateFrom(ctrlinfo);
                    this.RBTCtroller.Logon(UserInfo.DefaultUser);
                    listView1.Enabled = false;
                    this.button1.Enabled = false;//“连接”为禁止
                    this.btnConnect.Enabled = false;
                }
                else
                {
                    if (MessageBox.Show("此机器人控制器为真实控S制器，确定连接", "WARNING", MessageBoxButtons.YesNo) == DialogResult.Yes) 
                    {
                        RBTCtroller = ControllerFactory.CreateFrom(ctrlinfo);
                    }
                        
                }
                btnDisConnect.Visible = true;
                initFunc();
            } 
        }
        //启动监视线程
           private void initFunc()
           {
               UIRefreshThread = new Thread(new ThreadStart(UiRefreshiFunc));
               mydel1 = new UIRefDel(UIshow);
               UIRefreshThread.IsBackground = true;
               UIRefreshThread.Start();
           }
           private void UIshow()
           {
               ControllerOperatingMode ctrlMode;
               ControllerState ctrlState;
               ctrlMode = RBTCtroller.OperatingMode;
               switch (ctrlMode.ToString())
               {
                   case "0":
                       btnModel.Text = "Auto";break;
                   case "1":
                       btnModel.Text = "Init";break;
                   case "2":
                       btnModel.Text = "MannualReducedSpeed";break;
                   case "3":
                       btnModel.Text = "ManualFullSpeed";break;
                   case "4":
                       btnModel.Text = "AutoChange";break;
                   case "5":
                       btnModel.Text = "MannualFullSpeedChange";break;
                   case "6":
                       btnModel.Text = "NotApplicable";break;
                   default: 
                       btnModel.Text = ctrlMode.ToString();
                       break;
               }
               ctrlState = RBTCtroller.State;
               switch (ctrlState.ToString())
               {
                   case "0":
                       lblState.Text = "Init";break;
                   case "1":
                       lblState.Text = "MotorsOff";break;
                   case"2":
                       lblState.Text = "MotorsOn";break;
                   case "3":
                       lblState.Text = "GuardStop";break;
                   case "4":
                       lblState.Text = "EmgStopp";break;
                   case "5":
                       lblState.Text = "EmgStopRest";break;
                   case "6":
                       lblState.Text = "SystemFailure";break;
                   case "99":
                       lblState.Text = "Unknown";break;
                   default:
                       lblState.Text = ctrlState.ToString();
                       break;
               }


               //throw new NotImplementedException();
               switch (comboBox1.SelectedItem.ToString())
               {
                   case "Base"://注意获取直角坐标的类继承关系，及参数
                       p = RBTCtroller.MotionSystem.ActiveMechanicalUnit.GetPosition(CoordinateSystemType.Base);
                       break;
                   case "Tool":
                       p = RBTCtroller.MotionSystem.ActiveMechanicalUnit.GetPosition(CoordinateSystemType.Tool);
                       break;
                   case "WorkObject":
                       p = RBTCtroller.MotionSystem.ActiveMechanicalUnit.GetPosition(CoordinateSystemType.WorkObject);
                       break;
                   case "World":
                       p = RBTCtroller.MotionSystem.ActiveMechanicalUnit.GetPosition(CoordinateSystemType.World);
                       break;
                   case "Undefined":
                       J = RBTCtroller.MotionSystem.ActiveMechanicalUnit.GetPosition();
                       break;
                   default:break;
               }
               textBox1Num.Text = p.Trans.X.ToString();//此次的p要定义到函数外边，否则会提示：有可能未赋值的错误！！！
               textBox2Num.Text = p.Trans.Y.ToString();
               textBox3Num.Text = p.Trans.Z.ToString();
               textBox4Num.Text = p.Rot.Q1.ToString();
               textBox5Num.Text = p.Rot.Q2.ToString();
               textBox6Num.Text = p.Rot.Q3.ToString(); //此次还有个Q4不知道用来做什么                  
           }
        /*
        //Conbox选择项改变，进行数据显示切换
           void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
           {

              RobTarget p;
              JointTarget J;
               //throw new NotImplementedException();
               switch (comboBox1.SelectedItem.ToString())
               {
                   case "Base":
                        p = RBTCtroller.MotionSystem.ActiveMechanicalUnit.GetPosition(CoordinateSystemType.Base);
                   break;
                   case "Tool":
                        p = RBTCtroller.MotionSystem.ActiveMechanicalUnit.GetPosition(CoordinateSystemType.Tool);
                   break;
                   case "WorkObject":
                        p = RBTCtroller.MotionSystem.ActiveMechanicalUnit.GetPosition(CoordinateSystemType.WorkObject);
                   break;
                   case "World":
                        p = RBTCtroller.MotionSystem.ActiveMechanicalUnit.GetPosition(CoordinateSystemType.World);
                   break;
                   case "Undefined":
                        J = RBTCtroller.MotionSystem.ActiveMechanicalUnit.GetPosition();
                   break;
                   default:break;
               }
               textBox1Num.Text = p.Trans.X.ToString();
               textBox2Num.Text = p.Trans.Y.ToString();
               textBox3Num.Text = p.Trans.Z.ToString();
               textBox4Num.Text = p.Rot.Q1.ToString();
               textBox5Num.Text = p.Rot.Q2.ToString();
               textBox6Num.Text = p.Rot.Q3.ToString();
           }
        */
        //扫描函数
           private void button1_Click(object sender, EventArgs e)
           {
               NetworkScanner Netscan = new NetworkScanner();
               Netscan.Scan();
               ControllerInfoCollection ctrlerInfos = Netscan.Controllers;
               if (ctrlerInfos.Capacity!=0)
               {
                   foreach (ControllerInfo ctrlerinfo in ctrlerInfos)
                   {
                       ListViewItem items = new ListViewItem(ctrlerinfo.ControllerName);
                       items.SubItems.Add(ctrlerinfo.Version.ToString());
                       items.SubItems.Add(ctrlerinfo.IPAddress.ToString());
                       listView1.Items.Add(items);
                       items.Tag = ctrlerinfo;
                   }
                   btnConnect.Visible = true;
               }
               else
               {
                   MessageBox.Show("没有可用的控制器", "提示", MessageBoxButtons.OKCancel);
               }
               
           }
        //断开连接
           private void btnDisConnect_Click(object sender, EventArgs e)
           {
               RBTCtroller.Logoff();
               RBTCtroller.Dispose();
               RBTCtroller = null;
               btnConnect.Visible = true;
               btnDisConnect.Visible = false;
               listView1.Enabled = true;
           }

           private void Form1_Load(object sender, EventArgs e)
           {
               UIRenew = new BackgroundWorker();
               UIRenew.DoWork += UIRenew_DoWork;
               UIRenew.ProgressChanged += UIRenew_ProgressChanged;
               UIRenew.WorkerReportsProgress = true;
               UIRenew.RunWorkerAsync();

               mydel = new UIshowDel(UIinter);

               Thread aThread = new Thread(UiRefresh);
               aThread.IsBackground = true;
               aThread.Start();
               //combox初始化
               string[] coordinatingSys = { "Base", "Undefined", "Tool", "WorkObject", "World" };
               comboBox1.Items.AddRange(coordinatingSys);
               comboBox1.SelectedIndex = 0;//此处需要为下拉框赋默认值，否则在后面UI更新时，出现ToString（）函数错误
               //comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;   

               //按键部分初始化，所有函数均用同一个事件响应函数
               btnXp.Click += btnEvent_Click;
               btnXn.Click += btnEvent_Click;
               btnYp.Click += btnEvent_Click;
               btnYn.Click += btnEvent_Click;
               btnZp.Click += btnEvent_Click;
               btnZn.Click += btnEvent_Click;
               btnRp.Click += btnEvent_Click;
               btnRn.Click += btnEvent_Click;
               btnSp.Click += btnEvent_Click;
               btnSn.Click += btnEvent_Click;
               btnTp.Click += btnEvent_Click;
               btnTn.Click += btnEvent_Click;

               //Debug.WriteLine("LOAD 函数运行中");   
               //Debug.Assert(false,"提示错误","联系我");
          }

           void btnEvent_Click(object sender, EventArgs e)
           {
               MessageBox.Show("按钮事件处理正确");
               //throw new NotImplementedException();
               switch (sender.ToString())
               {
                   case"btn_Xp": break;
                   case"btn_Xn": break;
                   case"btn_Yp": break;
                   case"btn_Yn": break;
                   case"btn_Zp": break;

               default:
                           break;
                }

           }
        //Progress bar
           void UIRenew_ProgressChanged(object sender, ProgressChangedEventArgs e)
           {
               //UiRefresh();
               lblXJ1.Text = e.ProgressPercentage.ToString();
               //throw new NotImplementedException();
           }
           void UIRenew_DoWork(object sender, DoWorkEventArgs e)
           {

               for (int i = 0; i < 100; i++)
               {
                   Thread.Sleep(1000);
                   UIRenew.ReportProgress(i);
               }

               //throw new NotImplementedException();
           }
              private void UiRefresh()//这种多线程界面更新为直接传递参数给代理函数
           {
               string returnRapid;
               int i;
              
               while(true)//Important
               { 
                if (RBTCtroller!=null)
               {
               MotionSystem modata=RBTCtroller.MotionSystem;
               i = modata.SpeedRatio;
               JointTarget jt = new JointTarget();
               jt = RBTCtroller.MotionSystem.ActiveMechanicalUnit.GetPosition();
               returnRapid = jt.ToString();
               this.BeginInvoke(mydel, new object[] { returnRapid });//这种多线程界面更新为直接传递参数给代理函数
               }
               Thread.Sleep(1000);
               }
           }
              private void UIinter(string s)
              {
                  lblPosi.Text = s;
              }
              void UiRefreshiFunc()//Thread的死循环函数
              { 
                while(true)
                    {
                        this.BeginInvoke(mydel1);
                        Thread.Sleep(1000);
                    }
              }

              private void btnEna_Click(object sender, EventArgs e)
              {
                  if (RBTCtroller.OperatingMode==ControllerOperatingMode.Auto)//只能在自动模式下才能切换电机使能，否则抛出异常。
                  {
                                      
                  if (RBTCtroller.State != ControllerState.MotorsOn )
                  {
                      //using (Mastership m=Mastership.Request(RBTCtroller.Rapid))//此次可以不用获得mastership
                      //{
                      RBTCtroller.State = ControllerState.MotorsOn;
                      btnEna.Text = "电机禁止";
                      //}

                  }
                  else
                  {
                      //using (Mastership m = Mastership.Request(RBTCtroller.Rapid))
                      //{
                          RBTCtroller.State = ControllerState.MotorsOff;
                          btnEna.Text = "电机使能";
                      //}
                  }
              }
              }

    }
}
