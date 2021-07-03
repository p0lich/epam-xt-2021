using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_Task8.Entities
{
    public class UserAwardComposite
    {
        public Guid UserId { get; }
        public Guid AwardId { get; }

        public UserAwardComposite(Guid userId, Guid awardId)
        {
            UserId = userId;
            AwardId = awardId;
        }
    }
}
