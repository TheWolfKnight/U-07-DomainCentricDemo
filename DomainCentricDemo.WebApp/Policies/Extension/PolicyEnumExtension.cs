using System.ComponentModel;
using System.Reflection;

namespace DomainCentricDemo.WebApp.Policies.Extension {
    public static class PolicyEnumExtension {

        public static string GetName(this Policy me) {
            FieldInfo? info = typeof(Policy).GetField(me.ToString());

            if (info == null) return me.ToString();

            DescriptionAttribute[] attr = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attr == null || attr.Length == 0) return me.ToString();

            return attr.First().Description;

        }

    }
}
