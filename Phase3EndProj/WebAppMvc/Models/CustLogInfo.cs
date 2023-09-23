using System;
using System.Collections.Generic;

namespace WebAppMvc.Models;

public partial class CustLogInfo
{
    public int LogId { get; set; }

    public string? CustEmail { get; set; }

    public string? CustName { get; set; }

    public string? LogStatus { get; set; }

    public int? UserInfo { get; set; }

    public virtual UserInfo? UserInfoNavigation { get; set; }
}
