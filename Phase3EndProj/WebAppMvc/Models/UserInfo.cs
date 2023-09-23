using System;
using System.Collections.Generic;

namespace WebAppMvc.Models;

public  class UserInfo
{
    public int UserId { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<CustLogInfo> CustLogInfos { get; set; } = new List<CustLogInfo>();
}
