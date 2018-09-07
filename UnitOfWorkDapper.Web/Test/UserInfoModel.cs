using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWorkDapper.Web.Test
{
    public class UserInfoModel
    {
        public UserInfoModel()
        { }

        #region Model

        private int _ID;
        private int _UserId;
        private string _Name = "";
        private string _Gender = "";
        private int _Age = 0;
        private DateTime _Birthday = Convert.ToDateTime("1900-01-01");
        private string _Postcode = "";
        private string _PhoneNumber = "";
        private string _Email = "";
        private string _QQ = "";
        private bool _IsDelete = false;
        private string _Remark = "";
        private DateTime _UpdateTime = DateTime.Now;
        private DateTime _LastOrderTime = Convert.ToDateTime("1900-01-01");
        private string _LastOrderBrand = "";
        private decimal _LastOrderMoney = 0;
        private int _AllOrderCount = 0;
        private decimal _AllOrderMoney = 0;
        private int _PersonasRate = 0;
        private string _Tags = "";
        private int _UsingApp = 0;
        private string _CardType = "";
        private int _HasYanbao = 0;
        private string _Area1 = "";
        private string _Area2 = "";
        private int _GroupId = 0;
        private int _SysCompanyId = 0;
        private string _Ext1 = "未下单";
        private string _Ext2 = "";
        private string _Ext3 = "";
        private string _Ext4 = "";
        private string _Ext5 = "";

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
        public string Name
        {
            set { _Name = value; }
            get { return _Name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Gender
        {
            set { _Gender = value; }
            get { return _Gender; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Age
        {
            set { _Age = value; }
            get { return _Age; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Birthday
        {
            set { _Birthday = value; }
            get { return _Birthday; }
        }
        /// <summary>
        /// 用作用户的个性签名
        /// </summary>
        public string Postcode
        {
            set { _Postcode = value; }
            get { return _Postcode; }
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
        public string Email
        {
            set { _Email = value; }
            get { return _Email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QQ
        {
            set { _QQ = value; }
            get { return _QQ; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDelete
        {
            set { _IsDelete = value; }
            get { return _IsDelete; }
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
        /// 添加/更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _UpdateTime = value; }
            get { return _UpdateTime; }
        }
        /// <summary>
        /// 上次购机时间
        /// </summary>
        public DateTime LastOrderTime
        {
            set { _LastOrderTime = value; }
            get { return _LastOrderTime; }
        }
        /// <summary>
        /// 上次购机品牌
        /// </summary>
        public string LastOrderBrand
        {
            set { _LastOrderBrand = value; }
            get { return _LastOrderBrand; }
        }
        /// <summary>
        /// 上次购机金额
        /// </summary>
        public decimal LastOrderMoney
        {
            set { _LastOrderMoney = value; }
            get { return _LastOrderMoney; }
        }
        /// <summary>
        /// 用户在迪信通总的交易笔数
        /// </summary>
        public int AllOrderCount
        {
            set { _AllOrderCount = value; }
            get { return _AllOrderCount; }
        }
        /// <summary>
        /// 用户在迪信通总的交易金额
        /// </summary>
        public decimal AllOrderMoney
        {
            set { _AllOrderMoney = value; }
            get { return _AllOrderMoney; }
        }
        /// <summary>
        /// 画像完善度 0未填写画像  1部分完善 2已完善
        /// </summary>
        public int PersonasRate
        {
            set { _PersonasRate = value; }
            get { return _PersonasRate; }
        }
        /// <summary>
        /// 用户标签 , 多个用英文都好隔开
        /// </summary>
        public string Tags
        {
            set { _Tags = value; }
            get { return _Tags; }
        }
        /// <summary>
        /// 用户使用app情况:     0用户未使用app  1使用了迪信通app 2使用了txfapp 3两个app都使用了
        /// </summary>
        public int UsingApp
        {
            set { _UsingApp = value; }
            get { return _UsingApp; }
        }
        /// <summary>
        /// 用户在线下的购卡类型, 例如金卡, 银卡, 乐享卡
        /// </summary>
        public string CardType
        {
            set { _CardType = value; }
            get { return _CardType; }
        }
        /// <summary>
        /// 用户使用已经领取线下免费延保服务
        /// </summary>
        public int HasYanbao
        {
            set { _HasYanbao = value; }
            get { return _HasYanbao; }
        }
        /// <summary>
        /// 用户所属地区1 , 一般是 省级
        /// </summary>
        public string Area1
        {
            set { _Area1 = value; }
            get { return _Area1; }
        }
        /// <summary>
        /// 用户所属地区1 , 一般是 市级
        /// </summary>
        public string Area2
        {
            set { _Area2 = value; }
            get { return _Area2; }
        }
        /// <summary>
        /// 用户是不是店主 0不是店主 1是店主
        /// </summary>
        public int GroupId
        {
            set { _GroupId = value; }
            get { return _GroupId; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SysCompanyId
        {
            set { _SysCompanyId = value; }
            get { return _SysCompanyId; }
        }
        /// <summary>
        /// 成交类型: 已成交; 未成交; 已退款; 未下单
        /// </summary>
        public string Ext1
        {
            set { _Ext1 = value; }
            get { return _Ext1; }
        }
        /// <summary>
        /// 预留扩展 2
        /// </summary>
        public string Ext2
        {
            set { _Ext2 = value; }
            get { return _Ext2; }
        }
        /// <summary>
        /// 预留扩展 3
        /// </summary>
        public string Ext3
        {
            set { _Ext3 = value; }
            get { return _Ext3; }
        }
        /// <summary>
        /// 预留扩展 4
        /// </summary>
        public string Ext4
        {
            set { _Ext4 = value; }
            get { return _Ext4; }
        }
        /// <summary>
        /// 预留扩展 5
        /// </summary>
        public string Ext5
        {
            set { _Ext5 = value; }
            get { return _Ext5; }
        }

        #endregion Model
    }
}
