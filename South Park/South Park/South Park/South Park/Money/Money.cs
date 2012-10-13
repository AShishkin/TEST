
namespace South_Park
{
    struct Money
    {
        /// <summary>
        /// Монеты
        /// </summary>
        /// <param name="count">Колличество</param>
        public Money(int count)
        {
            this.Count = count;
        }
        /// <summary>
        /// Колличество монет
        /// </summary>
        public int Count;
    }
}
