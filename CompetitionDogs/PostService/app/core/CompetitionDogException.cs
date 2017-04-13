using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionDog
{
    class CompetitionDogException : Exception
    {
        private string errorMessage;

        public CompetitionDogException(string message) : base(message) {
            this.errorMessage = message;
        }

        public CompetitionDogException(CompetitionDogException ex)
        {
            this.errorMessage = ex.errorMessage;
        }

        public string getMessage()
        {
            return errorMessage;
        }
    }
}
