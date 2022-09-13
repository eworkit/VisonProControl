using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Linq.Expressions;

namespace Utilities.ExMethod
{
    public static class LambdaEx
    {
        public static Action<TA1> Fix<TA1>(this Func<Action<TA1>, Action<TA1>> f)
        {
            return x => f(f.Fix())(x);
        }

        public static Func<TA1, TR> Fix<TA1, TR>(this Func<Func<TA1, TR>, Func<TA1, TR>> f)
        {
            return x => f(Fix(f))(x);
        }
        public static Func<TA1, TA2, TR> Fix<TA1, TA2, TR>(this Func<Func<TA1, TA2, TR>, Func<TA1, TA2, TR>> f)
        {
            return (x, y) => f(Fix(f))(x, y);
        }
        public static Func<TA1, TA2, TA3, TR> Fix<TA1, TA2, TA3, TR>(this Func<Func<TA1, TA2, TA3, TR>, Func<TA1, TA2, TA3, TR>> f)
        {
            return (x, y, z) => f(Fix(f))(x, y, z);
        }
        static void test()
        {
            var fib = Fix<int, int, int, int>(f => (n, x, y) => n < 2 ? y : f(n - 1, y, x + y));
            fib(1, 2, 3);
        }
        public static IQueryable<T> WhereIn<T, TValue>(this IQueryable<T> query, System.Linq.Expressions.Expression<Func<T, TValue>> obj, IEnumerable<TValue> values)
        {
            return query.Where(BuildContainsExpression(obj, values));
        }

        private static System.Linq.Expressions.Expression<Func<TElement, bool>> BuildContainsExpression<TElement, TValue>(System.Linq.Expressions.Expression<Func<TElement, TValue>> valueSelector, IEnumerable<TValue> values)
        {
            if (null == valueSelector)
            {
                throw new ArgumentNullException("valueSelector"); 
            }
            if (null == values)
            {
                throw new ArgumentNullException("values");
            }
            var p = valueSelector.Parameters.Single();
            if (!values.Any()) return e => false;

            var equals = values.Select(value => (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Equal(valueSelector.Body, System.Linq.Expressions.Expression.Constant(value, typeof(TValue))));
            var body = equals.Aggregate(System.Linq.Expressions.Expression.Or);
            return System.Linq.Expressions.Expression.Lambda<Func<TElement, bool>>(body, p);
        }
    }
    public static class GeneralEventHandling
    {
        static object GeneralHandler(params object[] args)
        {
            Console.WriteLine("您的事件发生了说");
            return null;
        }

        public static void AttachGeneralHandler(object target, System.Reflection.EventInfo targetEvent)
        {
            //获得事件响应程序的委托类型
            var delegateType = targetEvent.EventHandlerType;

            //这个委托的Invoke方法有我们所需的签名信息
            var invokeMethod = delegateType.GetMethod("Invoke");

            //按照这个委托制作所需要的参数
            var parameters = invokeMethod.GetParameters();
            var paramsExp = new ParameterExpression[parameters.Length];
           var argsArrayExp = new  Expression[parameters.Length];

            //参数一个个转成object类型。有些本身即是object，管他呢……
            for (int i = 0; i < parameters.Length; i++)
            {
                paramsExp[i] = Expression.Parameter(parameters[i].ParameterType, parameters[i].Name);
                argsArrayExp[i] =  Expression.Convert(paramsExp[i], typeof(Object));
            }
           
            //调用我们的GeneralHandler
            var executeMethod = typeof(GeneralEventHandling).GetMethod(
                "GeneralHandler", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            Expression lambdaBodyExp =
                Expression.Call(null, executeMethod, Expression.NewArrayInit(typeof(Object), argsArrayExp));

            //如果有返回值，那么将返回值转换成委托要求的类型
            //如果没有返回值就这样搁那里就成了
            if (!invokeMethod.ReturnType.Equals(typeof(void)))
            {
                //这是有返回值的情况
                lambdaBodyExp = Expression.Convert(lambdaBodyExp, invokeMethod.ReturnType);
            }

            //组装到一起
            LambdaExpression dynamicDelegateExp = Expression.Lambda(delegateType, lambdaBodyExp, paramsExp);

            //我们创建的Expression是这样的一个函数：
            //(委托的参数们) => GeneralHandler(new object[] { 委托的参数们 })

            //编译
            Delegate dynamiceDelegate = dynamicDelegateExp.Compile();

            //完成!
            targetEvent.AddEventHandler(target, dynamiceDelegate);
        }
        //    GeneralEventHandling.AttachGeneralHandler(tsbOpen, tsbOpen.GetType().GetEvent("Click"));
    }
}
