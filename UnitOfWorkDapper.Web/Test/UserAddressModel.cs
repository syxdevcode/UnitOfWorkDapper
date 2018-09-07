using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWorkDapper.Web.Test
{
    public class UserAddressModel
    {
        public UserAddressModel()
        { }

        #region Model

        private int _ID;
        private int _UserId;
        private string _Province = "";
        private string _City = "";
        private string _Region = "";
        private string _Street = "";
        private string _Steet = "";
        private string _Name = "";
        private string _PhoneNumber = "";
        private string _Postcode = "";
        private int _Default = 0;
        private decimal _Longitude = 0;
        private decimal _Latitude = 0;
        private string _MapAddr1 = "";
        private string _MapAddr2 = "";
        private string _MapAddr3 = "";
        private string _MapAddr4 = "";
        private string _MapAddr5 = "";
        private string _Remark = "";
        private string _Extend1 = "";
        private DateTime _UpdateTime = DateTime.Now;
        private int _IsDelete = 0;

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserId
        {
            set { _UserId = value; }
            get { return _UserId; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Province
        {
            set { _Province = value; }
            get { return _Province; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string City
        {
            set { _City = value; }
            get { return _City; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Region
        {
            set { _Region = value; }
            get { return _Region; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Street
        {
            set { _Street = value; }
            get { return _Street; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Steet
        {
            set { _Steet = value; }
            get { return _Steet; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _Name = value; }
            get { return _Name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber
        {
            set { _PhoneNumber = value; }
            get { return _PhoneNumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Postcode
        {
            set { _Postcode = value; }
            get { return _Postcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Default
        {
            set { _Default = value; }
            get { return _Default; }
        }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal Longitude
        {
            set { _Longitude = value; }
            get { return _Longitude; }
        }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal Latitude
        {
            set { _Latitude = value; }
            get { return _Latitude; }
        }
        /// <summary>
        /// 从地图拖动位置定位到的位置的  省级名称
        /// </summary>
        public string MapAddr1
        {
            set { _MapAddr1 = value; }
            get { return _MapAddr1; }
        }
        /// <summary>
        /// 从地图拖动位置定位到的位置的  市级名称
        /// </summary>
        public string MapAddr2
        {
            set { _MapAddr2 = value; }
            get { return _MapAddr2; }
        }
        /// <summary>
        /// 从地图拖动位置定位到的位置的  县/区级名称
        /// </summary>
        public string MapAddr3
        {
            set { _MapAddr3 = value; }
            get { return _MapAddr3; }
        }
        /// <summary>
        /// 从地图拖动位置定位到的位置的  街道级名称
        /// </summary>
        public string MapAddr4
        {
            set { _MapAddr4 = value; }
            get { return _MapAddr4; }
        }
        /// <summary>
        /// 从地图拖动位置定位到的位置的  楼号-门牌号
        /// </summary>
        public string MapAddr5
        {
            set { _MapAddr5 = value; }
            get { return _MapAddr5; }
        }
        /// <summary>
        /// 预留备注
        /// </summary>
        public string Remark
        {
            set { _Remark = value; }
            get { return _Remark; }
        }
        /// <summary>
        /// 扩展字段
        /// </summary>
        public string Extend1
        {
            set { _Extend1 = value; }
            get { return _Extend1; }
        }
        /// <summary>
        /// 添加/修改时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _UpdateTime = value; }
            get { return _UpdateTime; }
        }
        /// <summary>
        /// 删除标记 0保留 1删除
        /// </summary>
        public int IsDelete
        {
            set { _IsDelete = value; }
            get { return _IsDelete; }
        }

        #endregion Model
    }
}
