using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using 炫酷紫界面_一_.Model;
using 炫酷紫界面_一_.UserControls;

namespace 炫酷紫界面_一_.View
{
    /// <summary>
    /// DataShowPage.xaml 的交互逻辑
    /// </summary>
    public partial class DataShowPage : Page
    {
        public DataShowPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FolderTreeList();
        }

        private void FileTree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ShowImageBiliBili.Visibility == Visibility.Visible)
            {
                ShowImageBiliBili.Visibility = Visibility.Collapsed;
            }

            try
            {
                FilesName filesname = FileTree.SelectedItem as FilesName;
                ExcelShowText.Text = filesname.FileName;
                ExcelReadDataList(filesname.FileName);
            }
            catch (Exception)
            {

            }
        }

        private void ExcelReadDataList(string fileName)
        {
            string currentPath = Environment.CurrentDirectory;
            string nowFile = currentPath + @"\AUVData\" + fileName;
            try
            {
                DataTable dt = ReadExcelToTable(nowFile);
                if (dt.Rows.Count > 0)
                {
                    List<DataName> lists = new List<DataName>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataName dataOne = new DataName();
                        dataOne.DataTime = dt.Rows[i][0].ToString();
                        dataOne.DataImagePath = "/炫酷紫界面（一）;component/images/com.tuyong.png";
                        dataOne.AngleX = dt.Rows[i][1].ToString();
                        dataOne.AngleY = dt.Rows[i][2].ToString();
                        dataOne.AngleZ = dt.Rows[i][3].ToString();
                        dataOne.AngleVelX = dt.Rows[i][4].ToString();
                        dataOne.AngleVelY = dt.Rows[i][5].ToString();
                        dataOne.AngleVelZ = dt.Rows[i][6].ToString();
                        dataOne.AngleAccelX = dt.Rows[i][7].ToString();
                        dataOne.AngleAccelY = dt.Rows[i][8].ToString();
                        dataOne.AngleAccelZ = dt.Rows[i][9].ToString();
                        dataOne.ForwardSpeed = dt.Rows[i][10].ToString();
                        dataOne.LateralSpeed = dt.Rows[i][11].ToString();
                        dataOne.VerticalSpeed = dt.Rows[i][12].ToString();
                        dataOne.HeightBottom = dt.Rows[i][13].ToString();
                        dataOne.DepthGauge = dt.Rows[i][14].ToString();
                        lists.Add(dataOne);
                    }
                    DataGridShow.ItemsSource = lists;
                }
            }
            catch (Exception)
            {
                _ = new MessageWindow()
                {
                    Owner = Window.GetWindow(this),
                    Header = "错误",
                    Message = "无法打开高版本Excel"
                }.ShowDialog();
            }
        }

        private void FolderTreeList()
        {
            string currentPath = Environment.CurrentDirectory;
            string nowFile = currentPath + @"\AUVData";

            DirectoryInfo Folder = new DirectoryInfo(nowFile);
            List<FilesName> filesName = new List<FilesName>();

            int count = 0;

            foreach (FileInfo file in Folder.GetFiles("*.xls"))
            {
                FilesName excel = new FilesName();
                excel.FileId = ++count;
                excel.FileName = file.Name;
                excel.FileImagePath = "/炫酷紫界面（一）;component/images/MicrosoftExcel.ico";
                filesName.Add(excel);
            }

            FileTree.ItemsSource = filesName;
        }

        /// <summary>
        /// 读取excel文件数据到DataTable
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="deleteFile"></param>
        /// <returns></returns>
        public static DataTable ReadExcelToTable(string filePath, bool deleteFile = false)
        {
            using (var tempFile = new FileStream(filePath, FileMode.Open))
            {
                var workbook = new HSSFWorkbook(tempFile);
                var sheet = workbook.GetSheetAt(0);
                var dataTable = new DataTable();
                var tableHeadRow = sheet.GetRow(0);
                for (int i = 0; i < tableHeadRow.PhysicalNumberOfCells; i++)
                {
                    var headCell = tableHeadRow.Cells[i];
                    dataTable.Columns.Add(new DataColumn(headCell.StringCellValue));
                }
                for (int i = 1; i < sheet.PhysicalNumberOfRows; i++)
                {
                    var row = sheet.GetRow(i);
                    var newRow = dataTable.NewRow();
                    for (int j = 0; j < row.PhysicalNumberOfCells; j++)
                    {
                        var cell = row.Cells[j];
                        newRow[j] = cell.StringCellValue;
                    }
                    dataTable.Rows.Add(newRow);
                }
                workbook.Clear();
                workbook.Close();
                if (deleteFile)
                {
                    File.Delete(filePath);
                }
                return dataTable;
            }
        }
    }
}
