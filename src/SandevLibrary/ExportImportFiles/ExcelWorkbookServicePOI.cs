using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;

namespace SandevLibrary.ExportImportFiles
{
    public class ExcelWorkbookServicePOI
    {
        /// <summary>
        ///  Read Execl data to DataTable (DataSet)
        /// </summary>
        /// <param name="filePath">Specify the path of the Execl file</param>
        /// <param name="isFirstLineColumnName">Set whether the first row is a column name</param>
        /// <returns>Return a DataTable dataset</returns>
        public static DataSet ExcelToDataSet(string filePath, bool isFirstLineColumnName)
        {
            DataSet dataSet = new DataSet();
            int startRow = 0;
            try
            {
                using (FileStream fs = File.OpenRead(filePath))
                {
                    IWorkbook workbook = null;
                    //  If it is 2007+ Excel version
                    if (filePath.IndexOf(".xlsx") > 0)
                    {
                        workbook = new XSSFWorkbook(fs);
                    }
                    //  If it is the version of Excel 2003-
                    else if (filePath.IndexOf(".xls") > 0)
                    {
                        workbook = new HSSFWorkbook(fs);
                    }
                    if (workbook != null)
                    {
                        int numOfSheet = 1;

                        //Read each sheet of Excel in a loop, and each sheet page is converted into a DataTable and placed in the DataSet
                        for (int p = 0; p < workbook.NumberOfSheets; p++)
                        {
                            if (p > 1)
                            {
                                break;
                            }
                            else
                            {
                                ISheet sheet = workbook.GetSheetAt(p);
                                DataTable dataTable = new DataTable();
                                dataTable.TableName = sheet.SheetName;
                                if (sheet != null)
                                {
                                    int rowCount = sheet.LastRowNum;//Get the total number of rows
                                    if (rowCount > 0)
                                    {
                                        IRow firstRow = sheet.GetRow(0);//Get the first row
                                        int cellCount = firstRow.LastCellNum;//Get the total number of columns

                                        //Build the columns of the datatable
                                        if (isFirstLineColumnName)
                                        {
                                            startRow = 1;//If the first row is the column name, start reading from the second row
                                            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                            {
                                                ICell cell = firstRow.GetCell(i);
                                                if (cell != null)
                                                {
                                                    if (cell.StringCellValue != null)
                                                    {
                                                        DataColumn column = new DataColumn(cell.StringCellValue);
                                                        dataTable.Columns.Add(column);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                            {
                                                DataColumn column = new DataColumn("column" + (i + 1));
                                                dataTable.Columns.Add(column);
                                            }
                                        }

                                        //Fill row
                                        for (int i = startRow; i <= rowCount; i++)
                                        {
                                            IRow row = sheet.GetRow(i);
                                            if (row == null) continue;

                                            DataRow dataRow = dataTable.NewRow();
                                            for (int j = row.FirstCellNum; j < cellCount; j++)
                                            {
                                                ICell cell = row.GetCell(j);
                                                if (cell == null)
                                                {
                                                    dataRow[j] = "";
                                                }
                                                else
                                                {
                                                    //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)
                                                    switch (cell.CellType)
                                                    {
                                                        case CellType.Blank:
                                                            dataRow[j] = "";
                                                            break;
                                                        case CellType.Numeric:
                                                            short format = cell.CellStyle.DataFormat;
                                                            //Processing time format (2015.12.5, 2015/12/5, 2015-12-5, etc.)

                                                            if (format == 0)
                                                                dataRow[j] = cell.NumericCellValue;
                                                            else if (format == 14)
                                                                dataRow[j] = cell.DateCellValue;
                                                            else if (format == 15)
                                                                dataRow[j] = cell.DateCellValue;
                                                            else if (format == 31)
                                                                dataRow[j] = cell.DateCellValue;
                                                            else if (format == 43)
                                                                dataRow[j] = cell.NumericCellValue;
                                                            else if (format == 49)
                                                                dataRow[j] = cell.NumericCellValue;
                                                            else if (format == 57)
                                                                dataRow[j] = cell.DateCellValue;
                                                            else if (format == 58)
                                                                dataRow[j] = cell.DateCellValue;
                                                            else if (format == 166)
                                                                dataRow[j] = cell.NumericCellValue;
                                                            else if (format == 167)
                                                                dataRow[j] = cell.NumericCellValue;
                                                            else if (format == 168)
                                                                dataRow[j] = cell.DateCellValue;
                                                            else if (format == 169)
                                                                dataRow[j] = cell.DateCellValue;
                                                            else if (format == 170)
                                                                dataRow[j] = cell.DateCellValue;
                                                            else if (format == 171)
                                                                dataRow[j] = cell.DateCellValue;
                                                            else
                                                            {
                                                                if (format == 49)
                                                                {
                                                                    if (DateUtil.IsCellDateFormatted(cell))
                                                                    {
                                                                        DateTime dateTime = cell.DateCellValue;
                                                                        ICellStyle style = cell.CellStyle;
                                                                        // Excel uses lowercase m for month whereas .Net uses uppercase
                                                                        string formatDate = style.GetDataFormatString().Replace('m', 'M');
                                                                        dataRow[j] = dateTime.ToString(formatDate);
                                                                    }
                                                                    else
                                                                        dataRow[j] = cell.NumericCellValue;
                                                                }
                                                            }

                                                            break;
                                                        case CellType.String:
                                                            dataRow[j] = cell.StringCellValue;
                                                            break;
                                                        case CellType.Formula:
                                                            short formatFormula = cell.CellStyle.DataFormat;
                                                            if (formatFormula == 0)
                                                                dataRow[j] = cell.NumericCellValue;
                                                            else if (formatFormula == 43)
                                                                dataRow[j] = cell.NumericCellValue;
                                                            else if (formatFormula == 49)
                                                                dataRow[j] = cell.NumericCellValue;
                                                            else if (formatFormula == 165)
                                                                dataRow[j] = cell.NumericCellValue;
                                                            else
                                                                dataRow[j] = cell.StringCellValue;

                                                            break;
                                                        default:
                                                            dataRow[j] = sheet.GetRow(i).GetCell(j).NumericCellValue;
                                                            break;
                                                    }
                                                }
                                            }
                                            dataTable.Rows.Add(dataRow);
                                        }
                                    }
                                }
                                dataSet.Tables.Add(dataTable);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dataSet;
        }

        /// <summary>
        /// Read Data From DataTable To Excel
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="Outpath"></param>
        /// <returns></returns>
        public static bool DataTableToExcel(DataSet dataSet, string Outpath)
        {
            bool result = false;
            try
            {
                if (dataSet == null || dataSet.Tables == null || dataSet.Tables.Count == 0 || string.IsNullOrEmpty(Outpath))
                    throw new Exception("The input DataSet or path is abnormal");
                int sheetIndex = 0;
                //Determine the instance type of the workbook based on the extension of the output path
                IWorkbook workbook = null;
                string pathExtensionName = Outpath.Trim().Substring(Outpath.Length - 5);
                if (pathExtensionName.Contains(".xlsx"))
                {
                    workbook = new XSSFWorkbook();
                }
                else if (pathExtensionName.Contains(".xls"))
                {
                    workbook = new HSSFWorkbook();
                }
                else
                {
                    Outpath = Outpath.Trim() + ".xls";
                    workbook = new HSSFWorkbook();
                }
                //Export DataSet to Excel
                foreach (DataTable dt in dataSet.Tables)
                {
                    sheetIndex++;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ISheet sheet = workbook.CreateSheet(string.IsNullOrEmpty(dt.TableName) ? ("sheet" + sheetIndex) : dt.TableName);//Create a table named Sheet0
                        int rowCount = dt.Rows.Count;//Rows
                        int columnCount = dt.Columns.Count;//Number of columns

                        //Set column header
                        IRow row = sheet.CreateRow(0);//Set the first row of excel as column header
                        for (int c = 0; c < columnCount; c++)
                        {
                            ICell cell = row.CreateCell(c);
                            cell.SetCellValue(dt.Columns[c].ColumnName);
                        }

                        //Set the cells in each row and column,
                        for (int i = 0; i < rowCount; i++)
                        {
                            row = sheet.CreateRow(i + 1);
                            for (int j = 0; j < columnCount; j++)
                            {
                                ICell cell = row.CreateCell(j);//The second row of excel starts to write data
                                cell.SetCellValue(dt.Rows[i][j].ToString());
                            }
                        }
                    }
                }
                //Output data to outPath
                using (FileStream fs = File.OpenWrite(Outpath))
                {
                    workbook.Write(fs);//Write data to the opened xls file
                    result = true;
                }
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
