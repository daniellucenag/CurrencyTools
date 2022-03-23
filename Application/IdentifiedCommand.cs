using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
	public class IdentifiedCommand<T, R> : IRequest<R> where T : IRequest<R>
	{	
		public T Command { get; }
		public Guid Id { get; }
		public IdentifiedCommand(T command, Guid id)
		{
			Command = command;
			Id = id;
		}
	}
}