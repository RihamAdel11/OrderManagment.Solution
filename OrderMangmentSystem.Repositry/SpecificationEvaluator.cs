using Microsoft.EntityFrameworkCore;
using OrderManagmentSystem.Core.Entites;
using OrderManagmentSystem.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMangmentSystem.Repositry
{
	public static class SpecificationEvaluator<T> where T : BaseEntity 
	{
		public static IQueryable<T> GetQuery(IQueryable<T> Inputquery, ISpecification <T> spec)
		{
			var query = Inputquery;

			if (spec.Criteria != null)
			{
				query = query.Where(spec.Criteria);
			}
			
			query = spec.Includes.Aggregate(query, (CurrentQuery, IncludeExperssion) =>
				CurrentQuery.Include (IncludeExperssion));


			query = spec.Includes.Aggregate(query, (CurrentQuery, IncludeExperssion) =>
				CurrentQuery.Include(IncludeExperssion));

			return query;
		}

	}
}
