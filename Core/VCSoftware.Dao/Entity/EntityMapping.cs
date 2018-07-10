using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace VCSoftware.Dao
{
    public class EntityMapping<T> where T : BaseEntity
    {
        private TableAttribute _tableAttr;
        private IEnumerable<PropertyInfo> _properties;

        public EntityMapping()
        {
            this._tableAttr = typeof(T).GetCustomAttributes(typeof(TableAttribute), true).GetValue(0) as TableAttribute;
            this._properties = typeof(T).GetProperties();
        }

        /// <summary>
        /// 获取实体映射对应的数据表名
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            return this._tableAttr == null ? string.Empty : this._tableAttr.Name;
        }

        /// <summary>
        /// 获取实体字段名称列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetFieldNames()
        {
            var lstProperty = new List<string>();
            foreach (var prop in this._properties)
            {
                //获取column特性绑定的名称，空则默认为字段名称
                var columnAttr = prop.GetCustomAttributes(typeof(ColumnAttribute), true).GetValue(0) as ColumnAttribute;
                //排除标记为NotMapped属性的字段
                var notmappedAttr = prop.GetCustomAttributes(typeof(NotMappedAttribute), true).GetValue(0) as NotMappedAttribute;
                if (notmappedAttr != null) continue;
                var columnName = columnAttr == null ? columnAttr.Name : string.Empty;
                lstProperty.Add(string.IsNullOrEmpty(columnName) ? prop.Name : columnName);
            }
            return lstProperty;
        }

        /// <summary>
        /// 获取实体某个实例字段名称及值列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EntityField> GetFields(T t)
        {
            var lstProperty = new List<EntityField>();
            foreach (var f in this._properties)
            {
                //获取column特性绑定的名称，空则默认为字段名称
                var columnAttr = f.GetCustomAttribute(typeof(ColumnAttribute), true) as ColumnAttribute;
                //排除标记为NotMapped属性的字段
                var notmappedAttr = f.GetCustomAttribute(typeof(NotMappedAttribute), true) as NotMappedAttribute;
                if (notmappedAttr != null) continue;
                var columnName = columnAttr != null ? columnAttr.Name : f.Name;
                var columnVal = typeof(T).GetProperty(f.Name).GetValue(t, null);
                //是否为主键
                var isKey = false;
                var databaseGeneratedAttr = f.GetCustomAttribute(typeof(DatabaseGeneratedAttribute), true) as DatabaseGeneratedAttribute;
                if (databaseGeneratedAttr != null && databaseGeneratedAttr.DatabaseGeneratedOption == DatabaseGeneratedOption.Identity)
                    isKey = true;
                lstProperty.Add(new EntityField
                {
                    Name = columnName,
                    Value = columnVal,
                    IsKey = isKey
                });
            }
            return lstProperty;
        }

        /// <summary>
        /// 获取主键名称
        /// </summary>
        /// <returns></returns>
        public string GetKeyFieldName()
        {
            var keyFieldName = string.Empty;
            foreach (var f in this._properties)
            {
                //是否为主键
                var databaseGeneratedAttr = f.GetCustomAttribute(typeof(DatabaseGeneratedAttribute), true) as DatabaseGeneratedAttribute;
                if (databaseGeneratedAttr != null && databaseGeneratedAttr.DatabaseGeneratedOption == DatabaseGeneratedOption.Identity)
                {
                    keyFieldName = f.Name;
                    break;
                }
            }
            return keyFieldName;
        }
    }
}
