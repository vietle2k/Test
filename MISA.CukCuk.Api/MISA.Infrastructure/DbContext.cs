using Dapper;
using MISA.Core;
using MISA.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure
{
     /// <summary>
     /// DbContext chiu trách nhiệm tương tác với Database
     /// CreatedBy: LXVIET (29/7/2021)
     /// </summary>
    public class DbContext<MISAEntity> : IBaseRepository<MISAEntity> where MISAEntity: class
    {
        #region Fields

        protected IDbConnection DbConnection;
        string _connectionString;
        #endregion
        public DbContext()
        {
            _connectionString = "" +
              "Host=47.241.69.179;" +
              "Port=3306;" +
              "Database=MISA.CukCuk_Demo_NVMANH;" +
              "User Id=dev;" +
              "Password= 12345678";
            DbConnection = new MySqlConnection(_connectionString);
        }

       
        #region Methods
        /// <summary>
        /// Lấy tất cả thông tin nhân viên
        /// </summary>
        /// <returns></returns>
        public List<MISAEntity> GetAll()
        {
            //var classname = typeof(MISAEntity).Name;
            ////Kết nối và lấy dữ liệu từ Db
            //var sqlCommand = $"SELECT * FROM {classname}";
            //var entities = DbConnection.Query<MISAEntity>(sqlCommand).ToList();
            ////trả về dữ liệu
            //return entities;

            var tableName = typeof(MISAEntity).Name;
            var sqlCommand = $"Proc_Get{tableName}s";
            var entities = DbConnection.Query<MISAEntity>(sqlCommand, commandType: CommandType.StoredProcedure).ToList();
            return entities;
        }
        /// <summary>
        /// Lấy thông tin nhân viên theo khóa chính
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public MISAEntity GetById(Guid entityId)
        {
            //var classname = typeof(MISAEntity).Name;
            ////Kết nối và lấy dữ liệu từ Db
            //var sqlCommand = $"SELECT * FROM {classname} WHERE {classname}Id = @{classname}Id";
            //DynamicParameters parameters = new DynamicParameters();
            //parameters.Add($"@{classname}Id", entityId);
            //var entity = DbConnection.QueryFirstOrDefault<MISAEntity>(sqlCommand,param:parameters);
            ////trả về dữ liệu
            //return entity;


            var tableName = typeof(MISAEntity).Name;
            var storeName = $"Proc_Get{tableName}ById";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{tableName}Id", entityId);
            var entity = DbConnection.QueryFirstOrDefault<MISAEntity>(storeName, param: dynamicParameters, commandType: CommandType.StoredProcedure);
            return entity;
        }

        public int Insert(MISAEntity entity)
        {
            var tableName = typeof(MISAEntity).Name;
            var storeName = $"Proc_Insert{tableName}";
            var rowsAffect = DbConnection.Execute(storeName, param: entity, commandType: CommandType.StoredProcedure);
            return rowsAffect;
        }

        public int Delete(Guid entityId)
        {
            var tableName = typeof(MISAEntity).Name;
            var storeName = $"Proc_Delete{tableName}";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@m_{tableName}Id", entityId);
            var rowsAffect = DbConnection.Execute(storeName, param: dynamicParameters, commandType: CommandType.StoredProcedure);
            return rowsAffect;
        }

        public int Update(MISAEntity entity)
        {
            var tableName = typeof(MISAEntity).Name;
            var storeName = $"Proc_Update{tableName}";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.AddDynamicParams(entity);
            var rowsAffect = DbConnection.Execute(storeName, param: dynamicParameters, commandType: CommandType.StoredProcedure);
            return rowsAffect;

        }

        public bool checkDuplicate(string value, string propName)
        {
            var tableName = typeof(MISAEntity).Name;

            var sqlCommand = $"SELECT * FROM {tableName} WHERE {propName} = @{propName}";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{propName}", value);
            var res = DbConnection.QueryFirstOrDefault<object>(sql: sqlCommand, param: parameters);
            if (res != null)
            {
                return true;
            }

             return false;
        }


        #endregion
    }
}
