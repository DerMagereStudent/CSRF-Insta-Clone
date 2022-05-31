using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.V1.Extensions; 

public static class DbContextExtensions {
	/// <summary>
	/// Gets the tracked entity reference for an object with the same primary key values.
	/// </summary>
	public static async Task<TEntity?> FindTrackedAsync<TEntity>(this DbContext context, TEntity entity) where TEntity : class {
		// Get the EF Core model type definition for TEntity, which contains all the model information including keys, unique constraints, ...
		var entityType = context.Model.FindRuntimeEntityType(typeof(TEntity));
		
		// Get the collection of properties (columns) which define the primary or composite primary key
		var keyProperties = entityType?.FindPrimaryKey()?.Properties;
		
		// If the entity hat no primary or composite primary key, this method cannot continue
		if (keyProperties is null)
			return null;

		// Get all values from the primary key properties of the entity instance
		var keyValues = keyProperties.Select(prop => prop.GetGetter().GetClrValue(entity)).ToArray();

		return await context.FindAsync<TEntity>(keyValues);
	}
}