using System;
using System.Collections.Generic;
using System.Windows.Markup;

namespace BeatServerBrowser.Core.Extentions
{
    public class EnumListExtension : MarkupExtension
    {
        private readonly Type enumType_;

        public EnumListExtension(Type type)
        {
            this.enumType_ = type;
        }

        /// <summary>
        /// 列挙体をDescriptionとEnumの辞書で返します。
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var enumArray = Enum.GetValues(this.enumType_);
            var enumDictionry = new Dictionary<string, Enum>();
            foreach (var enumValue in enumArray) {
                if (enumValue is Enum enumMember) {
                    enumDictionry.Add(enumMember.GetDescription(), enumMember);
                }
            }
            return enumDictionry;
        }
    }
}
