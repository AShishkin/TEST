
namespace South_Park
{
    /* 
     * Структура для хранения колличества
     * допустимых смертей. 
     */ 
    struct Continuum
    {
        /// <summary>
        /// Колличсество попыток
        /// </summary>
        /// <param name="count">число попыток</param>
        public Continuum(int count)
        {
            this.Count = count;
        }

        public int Count;
    }
}
