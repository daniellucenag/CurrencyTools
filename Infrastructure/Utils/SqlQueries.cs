namespace Infrastructure.Utils
{
    internal static class SqlQueries
    {
        #region SQL
        #region INSERT
        public const string SQL_INSERT_CURRENCY = @"			
			INSERT INTO dbo.Currency
			(
				CurrencyId,
				Name,
				Description,
				CreatedAt
			)
			VALUES
			(
				@CurrencyId,
				@Name,
				@Description,
				@CreatedAt
			)
		";

        #endregion;
        #endregion;
    }
}
