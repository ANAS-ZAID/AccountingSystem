using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingSystem.core.shared;
using AccountingSystem.Model;
using System.Data.Common;
using AccountingSystem.core.Functions;
using System.Drawing.Printing;

namespace AccountingSystem.core.Classes
{
    public class CRUD
    {
        private static CRUD instance;
        private static DataTable instanceDataTable;
        public bool recordingOprations = false;
        List<string> columnsNamesTableOprations = new List<string> { "operationName", "operationType", "employeeId", "description", "operationNumber", "date" };
        List<dynamic> valuesRecordeTableOprations = new List<dynamic> { };
        List<string> tableColumnsInAR = new List<string>();
        List<string> tableColumnsInEN;
        string descriptionOpration;
      //  Employee employee;
        public string sectionName;
        public decimal ExchangeRate { get; set; }
        private  SqlConnection _dbConnection;
       
        


        DataRow dataRow;

        private CRUD()
        {
            _dbConnection = DatabaseConnection.GetConnection();
            //_dbConnection = dbConnection;
        }
        public static CRUD getInstance()
        {
            if(instance == null)
                instance = new CRUD();
            return instance;
        }
        private CRUD(string sectionName, List<string> tableColumnsInEN, List<string> tableColumnsInAR)
        {
           // _dbConnection = DatabaseConnection.GetConnection();
            this.sectionName = sectionName;
          //  employee = Program.loginData.employee;
            this.tableColumnsInAR = tableColumnsInAR;
            this.tableColumnsInEN = tableColumnsInEN;
            recordingOprations = true;
        }
        public static DataTable getInstanceDataTable()
        {
            if(instanceDataTable == null)
                instanceDataTable = new DataTable();
            return instanceDataTable;
        }
        public void initialCRUD(string sectionName, List<string> tableColumnsInEN, List<string> tableColumnsInAR)
        { 
           // _dbConnection = DatabaseConnection.GetConnection();
            this.sectionName = sectionName;
            //  employee = Program.loginData.employee;
            this.tableColumnsInAR = tableColumnsInAR;
            this.tableColumnsInEN = tableColumnsInEN;
            recordingOprations = true;
           
           
        }
        public bool Create(string procedureName,DataRow values)
        {
            bool status = true;
            descriptionOpration = String.Empty;
            SqlCommand cmd = new SqlCommand(procedureName, _dbConnection);
            try
            {
                _dbConnection.Open();
                cmd.Transaction = _dbConnection.BeginTransaction();
              //  AppDialogAleart.showAleartError(tableColumnsInEN.Count +"en"+ tableColumnsInAR.Count + "ar" + values.ItemArray.Count().ToString() + "v" + "hhhhhhhhhhhhhhhhhhh");
                // تبدأ من الواحد لان االمصفوفات التي تحتوي على أسماء الحقول والمصفوفه التي تحتوي علي البيانات الجديده يتم أرسالها مع حقول الأيدي بنما السجل الجديد لايحتوي على  أيدي
                for (int i = 1; i < tableColumnsInEN.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@" + tableColumnsInEN[i], values[i]);
                   // AppDialogAleart.showAleartError(tableColumnsInEN[i]);
                    if (recordingOprations)
                    {
                        descriptionOpration += tableColumnsInAR[i] + "/" + values[i] + ";";
                        //AppDialogAleart.showAleartError(tableColumnsInAR[i] + values[i]);
                    }

                }
               
                cmd.CommandType = CommandType.StoredProcedure;
                object result = cmd.ExecuteScalar();

                // التحقق من النتيجة والتعيين لمتغير ID
                if (result != DBNull.Value)
                {
                    int operationNumber = Convert.ToInt32(result);
                 
                    descriptionOpration = "الرقم / " + operationNumber + " ; " + descriptionOpration;
                    if (recordingOprations)
                    {
                        operationNumber = recordingOpration("إضافه", descriptionOpration, operationNumber, cmd);
                        if (!(operationNumber > 0))
                        {
                            throw new Exception("حدث خطأ ما");
                        }
                    }
                    else
                    {

                        throw new Exception("حدث خطأ ما");
                    }

                }
                cmd.Transaction.Commit();


            }
            catch (Exception ex)
            {
                status = false;
                cmd.Transaction.Rollback();
                AppDialogAleart.showAleartError(ex.Message);

            }
            finally
            {
                _dbConnection.Close();
            }
            return status;
        }
        public int recordingOpration(string operationType, string descriptionOpration, int operationNumber, SqlCommand cmd)
        {
            int number = 0;
            valuesRecordeTableOprations = new List<dynamic> { sectionName, operationType, 1, descriptionOpration, operationNumber, DateTime.Now };
            cmd.Parameters.Clear();
            cmd.CommandText = "addOperation";
            try
            {

                for (int i = 0; i < valuesRecordeTableOprations.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@" + columnsNamesTableOprations[i], valuesRecordeTableOprations[i]);
                }

                cmd.CommandType = CommandType.StoredProcedure;

                number = cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                AppDialogAleart.showAleartError(ex.Message);
            }
            finally
            {
                //  _dbConnection.Close();
            }
            return number;
        }
        public bool Update(string procedureName,DataRow values, string privesData = "")
        {
            descriptionOpration = " البيانات قبل التعديل / \n" + privesData + "\n  البيانات بعد التعديل / \n ";
            bool status = true;
            SqlCommand cmd = new SqlCommand(procedureName, _dbConnection);
            try
            {


                _dbConnection.Open();
                cmd.Transaction = _dbConnection.BeginTransaction();
                for (int i = 0; i < tableColumnsInEN.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@" + tableColumnsInEN[i], values[i]);
                    if (recordingOprations)
                    {
                        descriptionOpration += tableColumnsInAR[i] + " / " + values[i] + " ; ";
                    }
                }

                cmd.CommandType = CommandType.StoredProcedure;
                Object result = cmd.ExecuteScalar();



              
                // التحقق من النتيجة والتعيين لمتغير ID
                if (result != DBNull.Value)
                {
                    int operationNumber = Convert.ToInt32(result);

                    if (recordingOprations)
                    {
                        operationNumber = recordingOpration("تعديل", descriptionOpration, operationNumber, cmd);
                        if (!(operationNumber > 0))
                        {

                            throw new Exception("حدث خطأ ما");
                        }
                    }

                }
                else
                {

                    throw new Exception("حدث خطأ ما");
                }
                cmd.Transaction.Commit();
            }
            catch (Exception ex)
            {
                status = false;
                //     cmd.Transaction.Rollback();
                AppDialogAleart.showAleartError(ex.Message);
            }
            finally
            {
                _dbConnection.Close();
            }
            return status;
        }
        public bool DeleteRowFromTable(string tableName, int id, string privesData = "")
        {
            descriptionOpration = " البيانات قبل عملية الحذف  : \n" + privesData;
            SqlCommand cmd = new SqlCommand("DeleteRowFromTable", _dbConnection);
            bool status = true;
            try
            {
                _dbConnection.Open();
                cmd.Transaction = _dbConnection.BeginTransaction();
                cmd.Parameters.AddWithValue("@TableName", tableName);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                object result = cmd.ExecuteScalar();

                // التحقق من النتيجة والتعيين لمتغير ID
                if (result != DBNull.Value)
                {
                    int operationNumber = Convert.ToInt32(result);
                    if (recordingOprations)
                    {
                        operationNumber = recordingOpration("حذف", descriptionOpration, operationNumber, cmd);
                        if (!(operationNumber > 0))
                        {

                            throw new Exception("حدث خطأ ما");
                        }
                    }

                }
                else
                {

                    throw new Exception("حدث خطأ ما");
                }
                cmd.Transaction.Commit();

            }
            catch (Exception ex)
            {
                status = false;
                cmd.Transaction.Rollback();
                AppDialogAleart.showAleartError(ex.Message);
            }
            finally
            {
                _dbConnection.Close();
            }
            return status;
        }

        public DataRow GetRowDataFromTable(string tableName, int id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("GetRowDataFromTable", _dbConnection))
                {
                    _dbConnection.Open();

                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    cmd.Parameters.AddWithValue("@id", id);

                    DataTable dataTable = new DataTable();
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    dataTable.Load(reader);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        dataRow = dataTable.Rows[0];

                    }
                }

            }
            catch (Exception e)
            {
                AppDialogAleart.showAleartError(e.ToString());
            }
            finally
            {
                _dbConnection.Close();
            }
            return dataRow;
        }
        public DataTable read(string tableName)
        {
            DataTable dataTable = new DataTable();
            try
            {
                _dbConnection.Open();
                using (SqlCommand cmd = new SqlCommand("GetTableData", _dbConnection))
                {
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    cmd.CommandType = CommandType.StoredProcedure;
                    dataTable.Load(cmd.ExecuteReader());
                    dataTable.TableName = dataTable.Rows.Count.ToString();
                    for (int i = 0; i < tableColumnsInAR.Count; i++)
                    {
                        dataTable.Columns[i].Caption = tableColumnsInAR[i];
                    }


                    //dataTable.Load(reader);
                }
            }
            catch (SqlException ex)
            {
                AppDialogAleart.showAleartError(ex.ToString());

            }
            finally
            {
                _dbConnection.Close();
            }
            return dataTable;
        }
        public DataTable readByProcedure(string procedureName)
        {
            DataTable dataTable = new DataTable();
            try
            {
                _dbConnection.Open();
                using (SqlCommand cmd = new SqlCommand(procedureName, _dbConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dataTable.Load(cmd.ExecuteReader());
                    dataTable.TableName = dataTable.Rows.Count.ToString();
                    for (int i = 0; i < tableColumnsInAR.Count; i++)
                    {
                        dataTable.Columns[i].Caption = tableColumnsInAR[i];
                    }


                    //dataTable.Load(reader);
                }
            }
            catch (SqlException ex)
            {
                AppDialogAleart.showAleartError(ex.ToString());

            }
            finally
            {
                _dbConnection.Close();
            }
            return dataTable;
        }
        public DataTable select(string table, string column = "*", string where = "1=1")
        {
            DataTable dataTable = new DataTable();

            string query = "SELECT " + column + " FROM " + table + " WHERE " + where;
            try
            {
                _dbConnection.Open();
                using (SqlCommand cmd = new SqlCommand(query, _dbConnection))
                {

                    dataTable.Load(cmd.ExecuteReader());
                }
            }
            catch (SqlException ex)
            {
                AppDialogAleart.showAleartError(ex.ToString());

            }
            finally
            {
                _dbConnection.Close();
            }
            return dataTable;
        }
        public int selectRecordeCount(string table, List<string> varName, List<dynamic> values, string spritor = "")
        {

            int rowCount = 1;
            try
            {
                string where = " where ";
                for (int i = 0; i < varName.Count; i++)
                {
                    if (spritor != "")
                    {
                        if (int.Parse(spritor[0].ToString()) == i)
                        {
                            where += varName[i] +" "+ spritor[1] + "=@" + varName[i];

                        }
                        else where += varName[i] + "=@" + varName[i];

                    }
                    else where += varName[i] + "=@" + varName[i];

                    if (i + 1 < varName.Count)
                    {
                        where += " AND  ";

                    }
                }
               
                string query = "SELECT COUNT(*) " + " FROM " + table + where;
                using (SqlCommand cmd = new SqlCommand(query, _dbConnection))
                {
                    _dbConnection.Open();
                    for (int i = 0; i < varName.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + varName[i], values[i]);
                    
                    }

                    rowCount = (int)cmd.ExecuteScalar();
                
                }
            }
            catch (SqlException ex)
            {
                AppDialogAleart.showAleartError(ex.ToString());

            }
            finally
            {
                _dbConnection.Close();
            }
            return rowCount;
        }
        public DataTable searchRecordeByLike(string tableName, List<string> columnsName, List<string> values)
        {
            DataTable dataTable = new DataTable();
            try
            {   string where = " where ";
                for (int i = 0; i < columnsName.Count; i++)
                {
                    where += columnsName[i] + " LIKE '%" + values[i] + "%' ";
                    if (i + 1 < columnsName.Count)
                         where += " OR  ";     
                }

                string selectStatment =" SELECT * FROM " + tableName + where;
                _dbConnection.Open();
                using (SqlCommand cmd = new SqlCommand(selectStatment, _dbConnection))
                {
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    dataTable.Load(cmd.ExecuteReader());
                    dataTable.TableName = dataTable.Rows.Count.ToString();
                    for (int i = 0; i < tableColumnsInAR.Count; i++)
                    {
                        dataTable.Columns[i].Caption = tableColumnsInAR[i];
                    }
                }
            }
            catch (SqlException ex)
            {
                AppDialogAleart.showAleartError(ex.ToString());

            }
            finally
            {
                _dbConnection.Close();
            }
            return dataTable;
        }
        public DataTable SearchRecordsByLike(string tableName, List<string> columnsName, List<string> values, int pageNumber, int pageSize = 10)
        {
            string whereClause = "";
            SqlParameter[] parameters = new SqlParameter[values.Count];
            DataTable dataTable = new DataTable();
            try
            {
                // Build dynamic WHERE clause with safe parameterization
                for (int i = 0; i < columnsName.Count; i++)
                {
                    whereClause += columnsName[i] + " LIKE @param" + i + "% ";
                    parameters[i]=(new SqlParameter("@param" + i, values[i]));

                    if (i + 1 < columnsName.Count)
                    {
                        whereClause += " OR ";
                    }
                }

                // Construct paginated query with RowNum for efficient retrieval
                string query = @"
            WITH Row_Num AS (
                SELECT *, ROW_NUMBER() OVER (ORDER BY @ColumnName) AS RowNum
                FROM QUOTENAME(@TableName)
            )
            SELECT *
            FROM Row_Num
            WHERE "+ whereClause + @" And RowNum BETWEEN @StartRow AND @EndRow
            ORDER BY @ColumnName";

                // Open connection and execute query with parameters
                _dbConnection.Open();
                using (SqlCommand cmd = new SqlCommand(query, _dbConnection))
                {
                    cmd.Parameters.AddRange(parameters);
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    cmd.Parameters.AddWithValue("@ColumnName", "id"); // Replace with desired column for ordering
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    cmd.Parameters.AddWithValue("@PageNumber", pageNumber);

                    int startRow = (pageNumber - 1) * pageSize + 1;
                    int endRow = pageNumber * pageSize;
                    cmd.Parameters.AddWithValue("@StartRow", startRow);
                    cmd.Parameters.AddWithValue("@EndRow", endRow);

                   
                    dataTable.Load(cmd.ExecuteReader());
                
                    // Execute separate query to get total record count
                    cmd.CommandText = "SELECT Count(*) FROM " + tableName;
                    cmd.CommandType = CommandType.Text;
                    dataTable.TableName = (1).ToString();

                    // Set column captions (optional, can be done elsewhere)
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        // Consider using a dictionary for column name mapping (if applicable)
                        dataTable.Columns[i].Caption = dataTable.Columns[i].ColumnName; // Default to column name
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions appropriately (logging, error messages, etc.)
                AppDialogAleart.showAleartError(ex.Message);
                // Execute separate query to get total record count
                // Consider returning a null DataTable or error indicator in case of exceptions
            }
            finally
            {
                _dbConnection.Close();
            }

            return dataTable;
        }
        //public  DataTable SearchRecordsByLikeAsync(string tableName, List<string> columnsName, List<string> values, int pageNumber, int pageSize = 10)
        //{
        //    string whereClause = "";
        //    string columnName = "id";
        //    DataTable dataTable = new DataTable();
        //    List<SqlParameter> parameters = new List<SqlParameter>();

        //    try
        //    {
        //        // Build dynamic WHERE clause with LIKE operators
        //        for (int i = 0; i < columnsName.Count; i++)
        //        {
        //            string parameterName = $"@{columnsName[i]}";
        //            whereClause += $"{columnsName[i]} LIKE {parameterName} ";

        //            if (i + 1 < columnsName.Count)
        //            {
        //                whereClause += " OR ";
        //            }

        //            parameters.Add(new SqlParameter(parameterName, "%" + values[i] + "%"));
        //        }

        //        // Construct the complete SQL statement with pagination
        //        string sql = @"
        //                    WITH Row_Num AS (
        //                        SELECT *, ROW_NUMBER() OVER (ORDER BY @ColumnName) AS RowNum
        //                        FROM @TableName
        //                        WHERE " + whereClause + @"
        //                    )
        //                    SELECT *
        //                    FROM Row_Num
        //                    WHERE RowNum BETWEEN @StartRow AND @EndRow";

        //        parameters.Add(new SqlParameter("@TableName", tableName));
        //        parameters.Add(new SqlParameter("@ColumnName", columnName)); // Assuming a default sorting column
        //        parameters.Add(new SqlParameter("@StartRow", (pageNumber - 1) * pageSize + 1));
        //        parameters.Add(new SqlParameter("@EndRow", pageNumber * pageSize));

        //        // Use asynchronous connection and command execution for performance

        //        {
        //             _dbConnection.Open();

        //            using (SqlCommand cmd = new SqlCommand(sql, _dbConnection))
        //            {
        //                cmd.Parameters.AddRange(parameters.ToArray());

        //                dataTable.Load( cmd.ExecuteReader());

        //                // Optionally set table name from separate query (consider efficiency)
        //                cmd.CommandText = "SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName";
        //                cmd.Parameters.Clear();
        //                cmd.Parameters.AddWithValue("@TableName", tableName);
        //                dataTable.TableName = (string) cmd.ExecuteScalar();

        //                // Set column captions (ensure tableColumnsInAR is populated correctly)
        //                for (int i = 0; i < dataTable.Columns.Count && i < tableColumnsInAR.Count; i++)
        //                {
        //                    dataTable.Columns[i].Caption = tableColumnsInAR[i];
        //                }
        //            }
        //        }

        //        return dataTable;
        //    }
        //    catch (SqlException ex)
        //    {
        //        // Handle SQL exceptions appropriately (logging, user feedback)
        //        AppDialogAleart.showAleartError(ex.ToString());// Example logging
        //        throw; // Re-throw for potential caller handling
        //    }
        //    finally
        //    {
        //        _dbConnection.Close();
        //    }
        //}
        public DataTable getPagedData(string tableName, int pageNumber , int pageSize = 10, string columnName = "id")
        { DataTable dataTable = new DataTable();
          
            try
            {
                _dbConnection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetPagedData", _dbConnection))
                {
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    cmd.Parameters.AddWithValue("@ColumnName", columnName);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    cmd.Parameters.AddWithValue("@PageNumber", pageNumber);

                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    dataTable.Load(cmd.ExecuteReader());
                    cmd.CommandText = "SELECT Count(*) FROM " + tableName;
                    cmd.CommandType = CommandType.Text;
                    dataTable.TableName = cmd.ExecuteScalar().ToString();
                    for (int i = 0; i < tableColumnsInAR.Count; i++)
                    {
                        dataTable.Columns[i].Caption = tableColumnsInAR[i];
                    }

                    //   AppDialogAleart.showAleartConfirmation(dataTable.TableName);
                    // ... (معالجة البيانات)
                }
            }
            catch(Exception ex) 
            {
                AppDialogAleart.showAleartError(ex.Message);
            }
            finally
            {
                _dbConnection.Close();
            }
            return dataTable;
        }

    }
}
