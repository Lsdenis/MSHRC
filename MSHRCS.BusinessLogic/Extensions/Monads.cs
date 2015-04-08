using System;

namespace MSHRCS.BusinessLogic.Extensions
{
	public static class Monads
	{
		public static TValue With<TInput, TValue>(this TInput o, Func<TInput, TValue> expression)
			where TInput : class
			where TValue : class
		{
			return o == null ? null : expression(o);
		}

		public static TValue WithDefault<TInput, TValue>(this TInput o, Func<TInput, TValue> expression, TValue defaultValue)
			where TInput : class
		{
			return o == null ? defaultValue : expression(o);
		}
	}
}
