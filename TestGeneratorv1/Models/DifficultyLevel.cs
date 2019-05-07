using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestGeneratorv1
{
    public class DifficultyLevel
    {

        public DifficultyLevel()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DifficultyLevelId { get; set; }

        public int Level { get; set; }
            
    }
}
