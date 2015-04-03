using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MSHRCS.Presentation.Helpers
{
	public static class UIHelper
	{
		private static string GetPropertyName<TModel, TValue>(Expression<Func<TModel, TValue>> exp)
		{
			var body = exp.Body as MemberExpression;

			if (body != null)
			{
				return body.Member.Name;
			}

			var ubody = (UnaryExpression)exp.Body;
			body = (MemberExpression)ubody.Operand;

			return body.Member.Name;
		}

		public static MvcHtmlString CheckBoxForJson<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
		{
			var propName = GetPropertyName(expression);
			var html = "<input type=\"checkbox\" name=\""
				+ propName + "\" id=\""
				+ propName + "\" value=\"true\" />";
			return MvcHtmlString.Create(html);
		}
	}
}