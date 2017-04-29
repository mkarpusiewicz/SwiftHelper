using System.Linq.Expressions;

namespace UnitTests.Extensions
{
    public class ActionWrapper<T>
    {
        private readonly string _methodName;
        public readonly T Do;

        public ActionWrapper(Expression<T> action)
        {
            Do = action.Compile();
            _methodName = ((MethodCallExpression) action.Body).Method.Name;
        }

        public override string ToString()
        {
            return _methodName;
        }
    }
}