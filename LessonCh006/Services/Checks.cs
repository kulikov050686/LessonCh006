namespace Services
{
    /// <summary>
    /// Проверки
    /// </summary>
    public static class Checks
    {
        /// <summary>
        /// Корректность ввода названия
        /// </summary>
        /// <param name="name"> Название </param>        
        public static bool CorrectnessOfName(string name)
        {
            for(int i = 0; i < name.Length; i++)
            {
                if(ForbiddenSymbol(name[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Запрещённый символ
        /// </summary>
        /// <param name="Symbol"> Символ </param>        
        private static bool ForbiddenSymbol(char Symbol)
        {
            return Symbol == '/' ||
                   Symbol == '|' ||
                   Symbol == '/' ||
                   Symbol == '.' ||
                   Symbol == ',' || 
                   Symbol == '&';
        }
    }
}
