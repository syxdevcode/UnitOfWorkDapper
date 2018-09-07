using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWorkDapper.Web.Test
{
    public class UserModel
    {
        public UserModel()
        { }

        #region Model

        private int _ID;
        private string _UserName;
        private string _Password;
        private DateTime _RegisterTime = DateTime.Now;
        private string _ReqisterIp = "";
        private int _LoginSum = 0;
        private string _HeadPhoto = "";
        private string _NickName = "";
        private int _Grade = 0;
        private int _Exp = 0;
        private string _HXUser = "";
        private string _HXPassword = "";
        private string _Province = "";
        private string _City = "";
        private string _Region = "";
        private string _Steet = "";
        private int _DefaultAddressId = 0;
        private string _QRCode = "";
        private int _Point = 0;
        private int _RegisterSource = 0;
        private int _IsDelete;
        private string _OffLineMemberCode = "";
        private int _StoreId = 0;
        private int _CompanyId = 0;

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
        public string UserName
        {
            set { _UserName = value; }
            get { return _UserName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            set { _Password = value; }
            get { return _Password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime RegisterTime
        {
            set { _RegisterTime = value; }
            get { return _RegisterTime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReqisterIp
        {
            set { _ReqisterIp = value; }
            get { return _ReqisterIp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LoginSum
        {
            set { _LoginSum = value; }
            get { return _LoginSum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HeadPhoto
        {
            set { _HeadPhoto = value; }
            get { return _HeadPhoto; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NickName
        {
            set { _NickName = value; }
            get { return _NickName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Grade
        {
            set { _Grade = value; }
            get { return _Grade; }
        }
        /// <summary>
        /// 经验值
        /// </summary>
        public int Exp
        {
            set { _Exp = value; }
            get { return _Exp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HXUser
        {
            set { _HXUser = value; }
            get { return _HXUser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HXPassword
        {
            set { _HXPassword = value; }
            get { return _HXPassword; }
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
        public string Steet
        {
            set { _Steet = value; }
            get { return _Steet; }
        }
        /// <summary>
        /// 默认收货地址Id, 关联UserAddress表id
        /// </summary>
        public int DefaultAddressId
        {
            set { _DefaultAddressId = value; }
            get { return _DefaultAddressId; }
        }
        /// <summary>
        /// 二维码
        /// </summary>
        public string QRCode
        {
            set { _QRCode = value; }
            get { return _QRCode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Point
        {
            set { _Point = value; }
            get { return _Point; }
        }
        /// <summary>
        /// 1 APP  2官网   3 M端  4 微信 5 QQ 6 新浪微博 7 O2O小店
        /// </summary>
        public int RegisterSource
        {
            set { _RegisterSource = value; }
            get { return _RegisterSource; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsDelete
        {
            set { _IsDelete = value; }
            get { return _IsDelete; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OffLineMemberCode
        {
            set { _OffLineMemberCode = value; }
            get { return _OffLineMemberCode; }
        }
        /// <summary>
        /// XdApp storeId
        /// </summary>
        public int StoreId
        {
            set { _StoreId = value; }
            get { return _StoreId; }
        }
        /// <summary>
        /// XdApp companyId
        /// </summary>
        public int CompanyId
        {
            set { _CompanyId = value; }
            get { return _CompanyId; }
        }


        #endregion Model
    }
}
