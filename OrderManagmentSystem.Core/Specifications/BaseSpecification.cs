using OrderManagmentSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Core.Specifications
{
	public class BaseSpecification<T>:ISpecification <T> where T:BaseEntity
	{
		public Expression<Func<T, bool>> Criteria { get; set; } = null;
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; set; } = null;
		public Expression<Func<T, object>> OrderByDesc { get; set; } = null;
		public BaseSpecification ()
		{



		}
		public BaseSpecification(Expression<Func<T, bool>> CriteriaExperession)
		{
			Criteria = CriteriaExperession;
		}
		public void AddOrderBy(Expression<Func<T, object>> OrderByExperssion)
		{
			OrderBy = OrderByExperssion;


		}
		public void AddOrderByDesc(Expression<Func<T, object>> OrderByDescExperssion)
		{
			OrderByDesc = OrderByDescExperssion;


		}

	}
}
