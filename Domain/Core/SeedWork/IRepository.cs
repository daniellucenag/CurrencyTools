using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.SeedWork
{
	public interface IRepository<in T> where T : IAggregateRoot
	{
		Task Add(T item);
	}
}
