using System.ComponentModel;

namespace DomainCentricDemo.WebApp.Policies {
    public enum Policy {

        [Description("IsAdminOnly")]
        IS_ADMIN_ONLY = 0,

        [Description("IsSoleAuthorOrAdmin")]
        IS_SOLE_AUTHOR_OR_ADMIN = 1,

    }
}
