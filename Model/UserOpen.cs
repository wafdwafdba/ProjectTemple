//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserOpen
    {
        public System.Guid ID { get; set; }
        public Nullable<System.Guid> UserID { get; set; }
        public Nullable<int> OpenType { get; set; }
        public string OpenID { get; set; }
        public Nullable<bool> IsEnable { get; set; }
        public Nullable<System.DateTime> LastTime { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}