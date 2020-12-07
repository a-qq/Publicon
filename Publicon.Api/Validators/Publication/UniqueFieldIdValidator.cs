using FluentValidation.Validators;
using Publicon.Infrastructure.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Publicon.Api.Validators.Publication
{
    public class UniqueFieldIdValidator<T>: PropertyValidator
		where T : PublicationFieldToManipulateDTO
    {
		public UniqueFieldIdValidator()
			: base("{PropertyName} must have unique names.") { }

		protected override bool IsValid(PropertyValidatorContext context)
		{
			var list = context.PropertyValue as IList<T>;
			if (list != null)
			{
				foreach (var element in list)
				{
					if (!list.All(x => x.FieldId != element.FieldId || x.Equals(element)))
						return false;
				}
			}
			return true;
		}
	}
	public class UniqueFieldNameValidator<T> : PropertyValidator
	where T : FieldToAddDTO
	{

		public UniqueFieldNameValidator()
			: base("{PropertyName} must have unique names.") { }

		protected override bool IsValid(PropertyValidatorContext context)
		{
			var list = context.PropertyValue as IList<T>;
			if (list != null)
			{
				foreach (var element in list)
				{
					if (!list.All(x => x.Name != element.Name || x.Equals(element)))
						return false;
				}
			}
			return true;
		}
	}
}
