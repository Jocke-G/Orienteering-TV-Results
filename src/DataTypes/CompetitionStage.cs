﻿using System.Collections.Generic;

namespace OrienteeringTvResults.DataTypes
{
    public class CompetitionStage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<CompetitionClass> Classes { get; set; }

        public CompetitionStage()
        {
            Classes = new List<CompetitionClass>();
        }
    }
}
