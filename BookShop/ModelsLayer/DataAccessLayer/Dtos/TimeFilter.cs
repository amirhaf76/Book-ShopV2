namespace BookShop.ModelsLayer.DataAccessLayer.Dtos
{
    public class TimeFilter
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; } 

        public IQueryable<T> AddStartTimeCondition<T>(IQueryable<T> query, Func<T, DateTime> getDateTime)
        {
            if (StartTime != null)
            {
                query = query.Where(q => getDateTime(q) >= StartTime);
            }

            return query;
        }
        public IQueryable<T> AddEndTimeCondition<T>(IQueryable<T> query, Func<T, DateTime> getDateTime)
        {
            if (EndTime != null)
            {
                query = query.Where(q => getDateTime(q) <= EndTime);
            }

            return query;
        }

        public IQueryable<T> AddStartTimeCondition<T>(IQueryable<T> query, Func<T, DateTime?> getDateTime)
        {
            if (StartTime != null)
            {
                query = query.Where(q => getDateTime(q) >= StartTime);
            }

            return query;
        }
        public IQueryable<T> AddEndTimeCondition<T>(IQueryable<T> query, Func<T, DateTime?> getDateTime)
        {
            if (EndTime != null)
            {
                query = query.Where(q => getDateTime(q) <= EndTime);
            }

            return query;
        }
    }

}
