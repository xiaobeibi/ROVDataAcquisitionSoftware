using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using 炫酷紫界面_一_.Model;
using System.IO;
using System.Windows.Threading;

namespace 炫酷紫界面_一_.ViewModel
{
    public class SaveExcelViewModel : ViewModelBase
    {
        private DispatcherTimer dispatcherTimer = null;

        public SaveExcelViewModel()
        {
            //一推初始化
            DataInit();

            //注册串口发过来的消息
            Messenger.Default.Register<string>(this, "SerialPortReceiveMessage", SerialPortMessageSave);
        }

        public void DataInit()
        {
            //初始化传感器数据
            SensorData = new JY9DVLModel();
            //深度计初始化
            DepthGauge = "0";
            //初始化工作目录
            CurrentPath = Environment.CurrentDirectory;

            //初始化目录
            if (!Directory.Exists(CurrentPath + @"\AUVData"))
            {
                Directory.CreateDirectory(CurrentPath + @"\AUVData");
            }

            //初始化定时器
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 60);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            string nowDate = DateTime.Now.ToString("yyyy-MM-dd");
            string nowFile = CurrentPath + @"\AUVData\" + nowDate + ".xls";

            if (!File.Exists(nowFile))
            {
                CreatExcel(nowFile);
            }
            else
            {
                AppendExcel(nowFile);
            }
        }

        //当前环境目录
        private string currentPath;
        public string CurrentPath
        {
            get { return currentPath; }
            set { currentPath = value; RaisePropertyChanged(() => CurrentPath); }
        }

        //13个数据
        private JY9DVLModel sensorData;
        public JY9DVLModel SensorData
        {
            get { return sensorData; }
            set { sensorData = value; RaisePropertyChanged(() => SensorData); }
        }

        //深度计
        private string depthGauge;
        public string DepthGauge
        {
            get { return depthGauge; }
            set { depthGauge = value; RaisePropertyChanged(() => DepthGauge); }
        }

        //从串口那接收来的消息
        private void SerialPortMessageSave(string msg)
        {
            string[] CurrentData = msg.Split(',');

            if (CurrentData.Length == 15 && CurrentData[0].Equals(":JY9") && CurrentData[CurrentData.Length - 1].Equals("$$\r"))
            {
                SensorData.AngleX = CurrentData[7];
                SensorData.AngleY = CurrentData[8];
                SensorData.AngleZ = CurrentData[9];
                SensorData.AngleVelX = CurrentData[4];
                SensorData.AngleVelY = CurrentData[5];
                SensorData.AngleVelZ = CurrentData[6];
                SensorData.AngleAccelX = CurrentData[1];
                SensorData.AngleAccelY = CurrentData[2];
                SensorData.AngleAccelZ = CurrentData[3];
            }
            if (CurrentData.Length == 9 && CurrentData[0].Equals(":DVL") && CurrentData[CurrentData.Length - 1].Equals("$$\r"))
            {
                SensorData.ForwardSpeed = CurrentData[2];
                SensorData.LateralSpeed = CurrentData[3];
                SensorData.VerticalSpeed = CurrentData[4];
                SensorData.HeightBottom = CurrentData[6];
            }
            if (CurrentData.Length == 3 && CurrentData[0].Equals(":DEP"))
            {
                DepthGauge = CurrentData[1];
            }
        }

        //创建文件函数
        private void CreatExcel(string filename)
        {
            try
            {
                HSSFWorkbook workbook2007 = new HSSFWorkbook();  //新建xlsx工作簿  
                workbook2007.CreateSheet("Sheet0");
                HSSFSheet SheetOne = (HSSFSheet)workbook2007.GetSheet("Sheet0");

                SheetOne.CreateRow(0);
                HSSFRow SheetRow = (HSSFRow)SheetOne.GetRow(0);

                SheetRow.CreateCell(0).SetCellValue("时间");
                SheetRow.CreateCell(1).SetCellValue("角度X");
                SheetRow.CreateCell(2).SetCellValue("角度Y");
                SheetRow.CreateCell(3).SetCellValue("角度Z");

                SheetRow.CreateCell(4).SetCellValue("角速度X");
                SheetRow.CreateCell(5).SetCellValue("角速度Y");
                SheetRow.CreateCell(6).SetCellValue("角速度Z");

                SheetRow.CreateCell(7).SetCellValue("加速度X");
                SheetRow.CreateCell(8).SetCellValue("加速度Y");
                SheetRow.CreateCell(9).SetCellValue("加速度Z");

                SheetRow.CreateCell(10).SetCellValue("前向速度");
                SheetRow.CreateCell(11).SetCellValue("侧向速度");
                SheetRow.CreateCell(12).SetCellValue("垂向速度");
                SheetRow.CreateCell(13).SetCellValue("离底高度m");
                SheetRow.CreateCell(14).SetCellValue("深度cm");

                FileStream file2007 = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                workbook2007.Write(file2007);
                file2007.Close();
                workbook2007.Close();
            }
            catch (Exception)
            {

            }
        }

        //添加数据函数
        private void AppendExcel(string filename)
        {
            try
            {
                HSSFWorkbook workbook = LoadWorkbook(filename);  //新建xlsx工作簿  

                HSSFSheet SheetOne = (HSSFSheet)workbook.GetSheet("Sheet0"); //获取工作表

                HSSFRow SheetRow = (HSSFRow)SheetOne.GetRow(0); //获取表头

                using (FileStream fout = new FileStream(filename, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                {
                    SheetRow = (HSSFRow)SheetOne.CreateRow(SheetOne.LastRowNum + 1); //在工作表中添加一行

                    SheetRow.CreateCell(0).SetCellValue(DateTime.Now.ToString("HH:mm:ss"));
                    SheetRow.CreateCell(1).SetCellValue(SensorData.AngleX);
                    SheetRow.CreateCell(2).SetCellValue(SensorData.AngleY);
                    SheetRow.CreateCell(3).SetCellValue(SensorData.AngleZ);

                    SheetRow.CreateCell(4).SetCellValue(SensorData.AngleVelX);
                    SheetRow.CreateCell(5).SetCellValue(SensorData.AngleVelY);
                    SheetRow.CreateCell(6).SetCellValue(SensorData.AngleVelZ);

                    SheetRow.CreateCell(7).SetCellValue(SensorData.AngleAccelX);
                    SheetRow.CreateCell(8).SetCellValue(SensorData.AngleAccelY);
                    SheetRow.CreateCell(9).SetCellValue(SensorData.AngleAccelZ);

                    SheetRow.CreateCell(10).SetCellValue(SensorData.ForwardSpeed);
                    SheetRow.CreateCell(11).SetCellValue(SensorData.LateralSpeed);
                    SheetRow.CreateCell(12).SetCellValue(SensorData.VerticalSpeed);
                    SheetRow.CreateCell(13).SetCellValue(SensorData.HeightBottom);
                    SheetRow.CreateCell(14).SetCellValue(DepthGauge);

                    fout.Flush();
                    workbook.Write(fout);
                }
                workbook = null;
            }
            catch (Exception)
            {

            }
        }

        //追加数据辅助函数
        public static HSSFWorkbook LoadWorkbook(string filename)
        {
            HSSFWorkbook workbook;
            using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                return workbook = new HSSFWorkbook(fileStream);
            }
        }




    }
}
