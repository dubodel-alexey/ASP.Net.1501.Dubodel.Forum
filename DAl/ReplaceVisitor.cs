using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAl
{

    public class ParameterTypeVisitor<TFrom, TTo> : ExpressionVisitor
    {

        private Dictionary<string, ParameterExpression> convertedParameters;
        private Expression<Func<TFrom, bool>> expression;

        public ParameterTypeVisitor(Expression<Func<TFrom, bool>> expresionToConvert)
        {
            convertedParameters = expresionToConvert.Parameters
                .ToDictionary(
                    x => x.Name,
                    x => Expression.Parameter(typeof(TTo), x.Name)
                );

            expression = expresionToConvert;
        }

        public Expression<Func<TTo, bool>> Convert()
        {
            return (Expression<Func<TTo, bool>>)Visit(expression);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType == typeof(TFrom))
            {
                var memeberInfo = typeof(TTo).GetMember(node.Member.Name).First();
                var newExp = Visit(node.Expression);
                return Expression.MakeMemberAccess(newExp, memeberInfo);
            }
            return base.VisitMember(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            var newParameter = convertedParameters[node.Name];
            return newParameter;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var newExp = Visit(node.Body);
            return Expression.Lambda(newExp, convertedParameters.Select(x => x.Value));
        }
    }
}
