using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Xml.Linq;

namespace SampleLibrary
{
    /// <summary>
    /// SQL Server データベースに対して tbCustomer テーブルの CRUD 操作を行うクラスです。
    /// </summary>
    public class CustomerDAO
    {
        private readonly string connectionString;

        /// <summary>
        /// CustomerDAO クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="connectionString">データベース接続文字列。</param>
        public CustomerDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// 新しい顧客を tbCustomer テーブルに追加します。
        /// </summary>
        /// <param name="code">顧客のコード。</param>
        /// <param name="name">顧客の名前。</param>
        /// <param name="address">顧客の住所。</param>
        /// <param name="phone">顧客の電話番号。</param>
        public void CreateCustomer(string code, string name, string address, string phone)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO tbCustomer (Code, Name, Address, Phone) VALUES (@Code, @Name, @Address, @Phone)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Code", code);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@Phone", phone);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 指定されたコードの顧客情報を取得します。
        /// </summary>
        /// <param name="code">顧客のコード。</param>
        /// <returns>顧客情報を含む DataTable。</returns>
        public DataTable ReadCustomer(string code)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM tbCustomer WHERE Code = '" + code + "'";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable customerTable = new DataTable();
                adapter.Fill(customerTable);

                return customerTable;
            }
        }

        /// <summary>
        /// 指定されたコードの顧客情報を取得します。
        /// </summary>
        /// <param name="code">顧客のコード。</param>
        /// <returns>顧客情報を含む DataTable。</returns>
        public DataTable ReadCustomer(HttpContext httpContext)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string code = httpContext.Request.QueryString["code"];
                string query = "SELECT * FROM tbCustomer WHERE Code = '" + code + "'";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable customerTable = new DataTable();
                adapter.Fill(customerTable);

                return customerTable;
            }
        }


        /// <summary>
        /// 指定されたコードの顧客情報を更新します。
        /// </summary>
        /// <param name="code">顧客のコード。</param>
        /// <param name="name">顧客の名前。</param>
        /// <param name="address">顧客の住所。</param>
        /// <param name="phone">顧客の電話番号。</param>
        public void UpdateCustomer(string code, string name, string address, string phone)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE tbCustomer SET Name = @Name, Address = @Address, Phone = @Phone WHERE Code = @Code";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Code", code);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@Phone", phone);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 指定されたコードの顧客情報を削除します。
        /// </summary>
        /// <param name="code">顧客のコード。</param>
        public void DeleteCustomer(string code)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM tbCustomer WHERE Code  = '" + code + "'";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
