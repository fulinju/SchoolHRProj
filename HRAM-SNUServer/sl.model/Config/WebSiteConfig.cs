using System;
using Core.Config;

namespace sl.model
{
    [Serializable]
    public class WebSiteConfig : ConfigFileBase
    {
        /// <summary>
        /// 网站域名
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 网站名称
        /// </summary>
        public string WebName { get; set; }
        /// <summary>
        /// 联系邮箱
        /// </summary>
        public string WebEmail { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string Post { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string CellPhone { get; set; }
        /// <summary>
        /// Logo
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 备案
        /// </summary>
        public string BeiAn { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 版权
        /// </summary>
        public string WebCopyRight { get; set; }
        /// <summary>
        /// 公司描述
        /// </summary>
        public string WebDescription { get; set; }

        public string WebSiteKey { get; set; }

        /// <summary>
        /// 图片最大高度
        /// </summary>
        public int ImgMaxHeight { get; set; }
        /// <summary>
        /// 图片最大宽度
        /// </summary>
        public int ImgMaxWidth { get; set; }

        /// <summary>
        /// 缩图最大宽度
        /// </summary>
        public int ThumbnailWidth { get; set; }
        /// <summary>
        /// 缩图最大高度
        /// </summary>
        public int ThumbnailHeight { get; set; }

        /// <summary>
        /// 水印类型，0，关闭水印，1,文字水印，2、图处水印
        /// </summary>
        public int WaterMarkType { get; set; }

        /// <summary>
        /// 允许上传的后缀
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 附件目录
        /// </summary>
        public string AttachPath { get; set; }

        /// <summary>
        /// 上传方式，按月，按日。按自定义
        /// </summary>
        public int AttachSaveType { get; set; }

        /// <summary>
        /// 附件大小
        /// </summary>
        public int AttachImgSize { get; set; }

        /// <summary>
        /// 水印文字
        /// </summary>
        public string WatermarkText { get; set; }

        /// <summary>
        /// 水印方向
        /// </summary>
        public int WatermarkPosition { get; set; }

        /// <summary>
        /// 水印图片
        /// </summary>
        public string WatermarkPic { get; set; }

        public string AttchFileSize { get; set; }
        /// <summary>
        /// 网站关键字
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 网站统计代码
        /// </summary>
        public string WebSiteTjCode { get; set; }
    }
}